using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Vanilla.DataAssets.Three
{

	// --------------------------------------------------------------------------------------------------------------------------- Generic Source //

	[Serializable]
	public abstract class FloatSource : ValueSource<float> { }

	// -------------------------------------------------------------------------------------------------------------------------------- Raw Value //

	[Serializable]
	public class FloatSource_Raw : FloatSource
	{
		
		[SerializeField]
		private float _value;
		public float Value
		{
			get => _value;
			set
			{
				if (Mathf.Epsilon > Mathf.Abs(f: _value - value)) return;

				var outgoing = _value;

				_value = value;

				SetReceived(outgoing: outgoing,
				            incoming: _value);
				
				BroadcastReceived(incoming: _value);
			}
		}

		public override float Get() => Value;

		public override void Set(float newValue) => Value = newValue;

		public override void Validate()
		{
			#if UNITY_EDITOR
			SetReceived(outgoing: _value,
			            incoming: _value);
			
			BroadcastReceived(incoming: _value);
			#endif
		}

	}

	// ------------------------------------------------------------------------------------------------------------------- Direct Asset Reference //

	[Serializable]
	public class FloatSource_DirectAssetReference : FloatSource
	{

		public FloatAsset asset;

		public override float Get() => asset.source.Get();

		public override void Set(float newValue) => asset.source.Set(newValue: newValue);

		public override async UniTask Initialize()
		{
			if (Initialized) return;

			asset.source.OnSet.AddListener(call: SetReceived);
			asset.source.OnBroadcast.AddListener(call: BroadcastReceived);
			
			await base.Initialize();
		}


		public override async UniTask DeInitialize()
		{
			if (!Initialized) return;
			
			asset.source.OnSet.RemoveListener(call: SetReceived);
			asset.source.OnBroadcast.RemoveListener(call: BroadcastReceived);
			
			await base.DeInitialize();

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
	
	// -------------------------------------------------------------------------------------------------------------- Addressable Asset Reference //

	// Check here for last plans on this - https://phoria.slite.com/app/channels/eL~IswgrZW/notes/_hh75jwPNB
	// tl;dr if you make each DataAsset Addressable and put it into its own separate AssetGroup, you should just be
	// able to reference them directly from an Addressable scene right? Isn't that exactly what happens in Rewild currently?
	
//	[Serializable]
//	public class FloatSource_AddressableAssetReference : FloatSource
//	{
//
//		[SerializeField]
//		public AssetReferenceT<FloatAsset> assetReference;
//
//		private FloatAsset asset;
//		
//		public override float Get() => asset.source.Get();
//
//		public override void Set(float newValue) => asset.source.Set(newValue: newValue);
//
//		public override async UniTask Initialize()
//		{
//			if (Initialized) return;
//
//			if (!assetReference.IsValid())
//			{
//				asset = await assetReference.LoadAssetAsync<FloatAsset>().Task;
//			}
//
//			asset.source.OnSet.AddListener(call: SetReceived);
//			asset.source.OnBroadcast.AddListener(call: BroadcastReceived);
//			
//			await base.Initialize();
//			
//		}
//
//
//		public override async UniTask DeInitialize()
//		{
//			if (!Initialized) return;
//
//			asset.source.OnSet.RemoveListener(call: SetReceived);
//			asset.source.OnBroadcast.RemoveListener(call: BroadcastReceived);
//
//			asset = null;
//			
//			assetReference.ReleaseAsset();
//			
//			await base.DeInitialize();
//
//		}
//
//	}
	
	

}