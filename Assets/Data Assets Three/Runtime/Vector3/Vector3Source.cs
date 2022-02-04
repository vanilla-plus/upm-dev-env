using System;

using Cysharp.Threading.Tasks;

using UnityEditor;
using UnityEditor.Events;

using UnityEngine;
using UnityEngine.Events;

using static UnityEngine.Debug;

using Object = UnityEngine.Object;

namespace Vanilla.DataAssets.Three
{

	// --------------------------------------------------------------------------------------------------------------------------- Generic Source //

	[Serializable]
	public abstract class Vector3Source : StructSource<Vector3> { }

	// ---------------------------------------------------------------------------------------------------------------------------- Direct Source //

	[Serializable]
	public class Vector3Source_Raw : Vector3Source
	{
		
		[SerializeField]
		private Vector3 _value;
		public Vector3 Value
		{
			get => _value;
			set
			{
				if (_value == value) return;

				var outgoing = _value;

				_value = value;

				SetReceived(outgoing: outgoing,
				            incoming: _value);
				
				BroadcastReceived(incoming: _value);
			}
		}

		public override Vector3 Get() => Value;

		public override void Set(Vector3 newValue) => Value = newValue;

		public override void Validate()
		{
			#if UNITY_EDITOR
			SetReceived(outgoing: _value,
			            incoming: _value);
			
			BroadcastReceived(incoming: _value);
			#endif
		}

	}

	// ----------------------------------------------------------------------------------------------------------------------------- Asset Source //

	[Serializable]
	public class Vector3Source_DirectAssetReference : Vector3Source
	{

		public Vector3Asset asset;

		public override Vector3 Get() => asset.source.Get();

		public override void Set(Vector3 newValue) => asset.source.Set(newValue: newValue);

		public override async UniTask Initialize()
		{
			if (Initialized) return;

			await base.Initialize();
			
			asset.source.OnSet.AddListener(call: SetReceived);
			asset.source.OnBroadcast.AddListener(call: BroadcastReceived);
		}


		public override async UniTask DeInitialize()
		{
			if (!Initialized) return;
			
			await base.DeInitialize();

			asset.source.OnSet.RemoveListener(call: SetReceived);
			asset.source.OnBroadcast.RemoveListener(call: BroadcastReceived);
		}

		// I don't advise constantly checking if the asset or source is null; it just causes silent failure.
		// We check in Validate because asset & source are null when created or changed.
		// Otherwise, we WANT to know about nulls.

//		public bool AssetIsNull() => ReferenceEquals(objA: Asset,
//		                                             objB: null);
//
//		public bool SourceIsNull() => AssetIsNull() ||
//		                              ReferenceEquals(objA: Asset.source,
//		                                              objB: null);

	}

}