#if DEBUG_VANILLA && DATA_ASSETS
#define debug
#endif

#if UNITY_EDITOR
using UnityEditor;
#endif

using System;

using UnityEngine;
using UnityEngine.Events;

using Vanilla.QuitUtility;
using Vanilla.TypeMenu;



namespace Vanilla.DataAssets // --------------------------------------------------------------------------------------------------------- Generic //
{

	// ----------------------------------------------------------------------------------------------------------------------------------- Socket //
	
	[Serializable]
	public abstract class GenericSocket<TType,TSocket,TAsset, TAccessor>
		where TSocket : GenericSocket<TType,TSocket,TAsset, TAccessor>
		where TAsset : GenericAsset<TType,TSocket,TAsset,  TAccessor>
		where TAccessor : GenericAccessor<TType,TSocket,TAsset, TAccessor>
	{

		// Unity gets uppity if you ask EditorApplication.isPlaying from here
		
//		protected GenericSocket()
//		{
//			#if UNITY_EDITOR
//			if (!EditorApplication.isPlaying) return;
//			#endif
//			
//			Debug.Log("I'M BEING BORN");
//		}
//
//
//		protected GenericSocket(TType fallback)
//		{
//			#if UNITY_EDITOR
//			if (!EditorApplication.isPlaying) return;
//			#endif
//			
//			Debug.Log("I'M BEING BORN");
//
//			this.fallback = fallback;
//		}
//
//
//		protected GenericSocket(TType fallback, TAsset asset)
//		{
//			#if UNITY_EDITOR
//			if (!EditorApplication.isPlaying) return;
//			#endif
//			
//			Debug.Log("I'M BEING BORN");
//
//			this.fallback = fallback;
//			this.asset    = asset;
//		}

		[SerializeField]
		protected TAsset _asset;
		public TAsset asset
		{
			get => _asset;
			set
			{
				if (ReferenceEquals(objA: _asset,
				                    objB: value)) return;

				if (useFallback)
				{
					_asset = value;

					return;
				}

				Unplug();

				_asset = value;

				PlugIn();
			}
		}

		[SerializeField]
		protected TType _fallback;
		public TType fallback
		{
			get => _fallback;
			set
			{
				if (!useFallback)
				{
					_fallback = value;

					return;
				}

				if (Equals(objA: _fallback,
				           objB: value)) return;

				var outgoing = _fallback;

				_fallback = value;

				OnChanged?.Invoke(arg0: outgoing,
				                arg1: _fallback);

				OnBroadcast?.Invoke(arg0: _fallback);
			}
		}

		[SerializeField]
		private bool _useFallback = false;
		public bool useFallback
		{
			get => _useFallback;
			set
			{
				if (_useFallback == value) return;

				_useFallback = value;
				
				if (_useFallback)
				{
					Unplug();
				}
				else
				{
					PlugIn();
				}
			}
		}

		public UnityAction<TType> OnBroadcast;

		public UnityAction<TType, TType> OnChanged;

		public void PlugIn()
		{
			if (!AssetAssigned())
			{
				#if debug
				Debug.LogWarning(message: "Can't plug in - no asset!");
				#endif
				
				return;
			}

			_asset.onBroadcast += Broadcast;
			_asset.onChange    += Changed;
		}
		
		public void Unplug()
		{
			if (!AssetAssigned()) return;

			_asset.onBroadcast -= Broadcast;
			_asset.onChange    -= Changed;
		}

		internal void Broadcast(TType value) => OnBroadcast?.Invoke(arg0: value);


		internal void Changed(TType outgoing,
		                      TType incoming) => OnChanged?.Invoke(arg0: outgoing,
		                                                           arg1: incoming);

		public virtual TType Get() => useFallback ?
			                              fallback :
			                              AssetAssigned() ?
				                              _asset.value :
				                              fallback;

		public virtual void Set(TType value)
		{
			
			if (useFallback)
			{
				fallback = value;
			}
			else
			{
				if (AssetAssigned())
				{
					asset.value = value;
				}
			}
			
		}


		public string GetAssetName() => AssetAssigned() ? _asset.name : DataAssetsUtility.Unknown;


		public bool AssetAssigned() => !ReferenceEquals(objA: _asset,
		                                                objB: null);

	}

	// ------------------------------------------------------------------------------------------------------------------------------------ Asset //
	
	[Serializable]
	public abstract class GenericAsset<TType, TSocket, TAsset, TAccessor> : BaseAsset 
		where TSocket : GenericSocket<TType, TSocket, TAsset, TAccessor>
		where TAsset : GenericAsset<TType, TSocket, TAsset, TAccessor>
		where TAccessor : GenericAccessor<TType, TSocket, TAsset, TAccessor>
	{
		
		[SerializeField]
		internal TType _value;
		public TType value
		{
			get
			{
				foreach (var m in GetTasks)
				{
					m.Get(asset: this as TAsset);
				}
				
				return _value;
			}
			set
			{
//				#if DEBUG_DATA_ASSETS
//				if (!Quit.InProgress)
//				{
//					DataAssetsUtility.LogValueChange(name: name,
//					                                 typeName: typeof(TType).Name,
//					                                 from: _value,
//					                                 to: value);
//				}
//				#endif

				foreach (var m in SetTasks)
				{
					if (!m.Set(asset: this as TAsset,
					           outgoing: ref _value,
					           incoming: ref value)) return;
				}
				
				var outgoing = _value;
				
				_value = value;

				Broadcast();

				onChange?.Invoke(arg0: outgoing,
				                 arg1: _value);
			}
		}

		protected UnityAction<TType> _onBroadcast;
		public UnityAction<TType> onBroadcast
		{
			get => _onBroadcast;
			set
			{
				#if debug
				if (!Quit.InProgress)
				{
					DataAssetsUtility.LogSubscriptions(name: name,
					                                   currentInvocations: _onBroadcast?.GetInvocationList(),
					                                   newInvocations: value?.GetInvocationList());
				}
				#endif

				_onBroadcast = value;
			}

		}

		protected UnityAction<TType, TType> _onChange;
		public UnityAction<TType, TType> onChange
		{
			get => _onChange;
			set
			{
				#if debug
				if (!Quit.InProgress)
				{
					DataAssetsUtility.LogSubscriptions(name: name,
					                                   currentInvocations: _onChange?.GetInvocationList(),
					                                   newInvocations: value?.GetInvocationList());
				}
				#endif

				_onChange = value;
			}

		}


		public void PlugIntoGetBroadcast(Action<TType> action)
		{
//			foreach (var a in )
		}

		public void PlugIntoGetChanged(Action<TType,TType> action)
		{
			
		}
		
		public void PlugIntoSetBroadcast(Action<TType> action)
		{
			
		}

		public void PlugIntoSetChanged(Action<TType,TType> action)
		{
			
		}
		
		public void Unplug(Action<TType> action)
		{
			
		}

		protected override void OnValidate()
		{
			#if UNITY_EDITOR
			base.OnValidate();
			
			if (Application.isPlaying) value = _value;
			#endif
		}


		[ContextMenu(itemName: "Broadcast")]
		public override void Broadcast() => _onBroadcast?.Invoke(arg0: _value);

		[SerializeReference] 
		[TypeMenu]
		[Tooltip(tooltip: "These value modifiers will be applied to the current value before returning it.")]
		public TAccessor[] GetTasks = new TAccessor[0];

		[SerializeReference] 
		[TypeMenu]
		[Tooltip(tooltip: "These value modifiers will be applied to the incoming value whenever the setter is called.")]
		public TAccessor[] SetTasks = new TAccessor[0];
		
	}
	
	// -------------------------------------------------------------------------------------------------------------------------------- Accessors //
	
	[Serializable]
	public abstract class GenericAccessor<TType, TSocket, TAsset, TAccessor> : BaseAccessor
		where TAsset : GenericAsset<TType, TSocket, TAsset,TAccessor>
		where TSocket : GenericSocket<TType, TSocket, TAsset,TAccessor>
		where TAccessor : GenericAccessor<TType,TSocket,TAsset,TAccessor>
	{

		/// <summary>
		///		
		/// </summary>
		/// <param name="asset"></param>
		/// <returns></returns>
		public abstract void Get(TAsset asset);


		/// <summary>
		/// 
		/// </summary>
		/// <param name="asset"></param>
		/// <param name="outgoing"></param>
		/// <param name="incoming"></param>
		/// <returns></returns>
		public abstract bool Set(TAsset asset,
		                         ref TType outgoing,
		                         ref TType incoming);

	}

}