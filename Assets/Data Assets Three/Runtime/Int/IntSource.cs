using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Vanilla.DataAssets.Three
{

	// --------------------------------------------------------------------------------------------------------------------------- Generic Source //

	[Serializable]
	public abstract class IntSource : ValueSource<int> { }

	// -------------------------------------------------------------------------------------------------------------------------------- Raw Value //

	[Serializable]
	public class IntSource_Raw : IntSource
	{
		
		[SerializeField]
		private int _value;
		public int Value
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

		public override int Get() => Value;

		public override void Set(int newValue) => Value = newValue;

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
	public class IntSource_DirectAssetReference : IntSource
	{

		public IntAsset asset;

		public override int Get() => asset.source.Get();

		public override void Set(int newValue) => asset.source.Set(newValue: newValue);

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

	}

}