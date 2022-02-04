#if DEBUG_VANILLA && DATA_ASSETS
#define debug
#endif

#if UNITY_EDITOR
using UnityEditor;
#endif

using System;

using Cysharp.Threading.Tasks;

using UnityEngine;
using UnityEngine.Events;

using Vanilla.TypeMenu;
using Vanilla.UnityExtensions;

using static UnityEngine.Debug;

namespace Vanilla.DataAssets.Three
{

	// --------------------------------------------------------------------------------------------------------------------------- Generic Source //

	[Serializable]
	public abstract class GenericSource<TType> : IDataSource<TType>,
	                                             IPortable
	{

		[SerializeField]
		internal bool _initialized;
		public bool Initialized => _initialized;

		// Ah boy.
		// The whole idea of moving to async-everything is that your applications have all the time/space they need to setup and do whatever.
		// By splitting things into Get/Set and Get/SetAsync, you're creating a weird paradigm there.
		// Is the data here or not? It's not knowable anymore.
		// If it's assumed to be async and it happens instantly, it's just a bonus. But it can't go the other way...

		public abstract TType Get();

		public abstract void Set(TType newValue);

		[SerializeField]
		private UnityEvent<TType> _onBroadcast = new UnityEvent<TType>();
		public UnityEvent<TType> OnBroadcast => _onBroadcast;

		[HideInInspector]
		[SerializeField]
		private UnityEvent<TType, TType> _onSet = new UnityEvent<TType, TType>();
		public UnityEvent<TType, TType> OnSet => _onSet;

		public void Broadcast() => OnBroadcast.Invoke(arg0: Get());


		public virtual UniTask Initialize()
		{
			#if debug
			Log($"[{GetType().Name}] Initialize start");
			#endif

			if (_initialized) return default;

			_initialized = true;

			// Controversial? All sources broadcast upon initialization in order to warm up any listeners
			// Listeners that subscribe afterwards should Get() for themselves upon subscription

			Broadcast();

			// What about invoking Set...?

			#if debug
			Log($"[{GetType().Name}] Initialize end");
			#endif

			return default;
		}


		public virtual UniTask DeInitialize()
		{
			#if debug
			Log($"[{GetType().Name}] De-Initialize start");
			#endif

			if (!_initialized) return default;

			_initialized = false;

			#if debug
			Log($"[{GetType().Name}] De-Initialize end");
			#endif

			return default;
		}


		/// <summary>
		///		This method is used to invoke a Broadcast event.
		/// 
		///		This may be in response to a subscribed event chain or it can be called directly to start one.
		///
		///		If this source is an observer, then this method should be used as the listener.
		/// </summary>
		public void BroadcastReceived(TType incoming) => OnBroadcast.Invoke(arg0: incoming);


		/// <summary>
		///		This method is used to invoke a Set event.
		///
		/// 	This may be in response to a subscribed event chain or it can be called directly to start one.
		/// 
		///		If this source is an observer, then this method should be used as the listener.
		/// </summary>
		public void SetReceived(TType outgoing,
		                        TType incoming) => OnSet.Invoke(arg0: outgoing,
		                                                        arg1: incoming);


		public virtual void Validate() { }

		#if UNITY_EDITOR
		public virtual void Port(SerializedProperty previousInstance)
		{
			#if debug
			var runLogs = true;
			#else
			var runLogs = false;
			#endif

			TypeMenuUtility.TransferPersistentCalls(serializedEvent: previousInstance.FindPropertyRelative("_onBroadcast"),
			                                        targetEvent: OnBroadcast,
			                                        runLogs: runLogs);

			TypeMenuUtility.TransferPersistentCalls(serializedEvent: previousInstance.FindPropertyRelative("_onSet"),
			                                        targetEvent: OnSet,
			                                        runLogs: runLogs);
		}


		public void LogPersistentCalls()
		{
			#if debug
			_onBroadcast.LogPersistentCalls();
			_onSet.LogPersistentCalls();
			#endif
		}


		#endif

	}

}