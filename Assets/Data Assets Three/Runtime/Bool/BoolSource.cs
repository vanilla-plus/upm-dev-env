using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

using Vanilla.TypeMenu;

namespace Vanilla.DataAssets.Three
{

	// --------------------------------------------------------------------------------------------------------------------------- Generic Source //

	[Serializable]
	public abstract class BoolSource : ValueSource<bool> { }

	// -------------------------------------------------------------------------------------------------------------------------------- Raw Value //

	[Serializable]
	public class BoolSource_Raw : BoolSource
	{

		[SerializeField]
		private bool _value;
		public bool Value
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

		public override bool Get() => Value;

		public override void Set(bool newValue) => Value = newValue;


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
	public class BoolSource_DirectAssetReference : BoolSource
	{

		public BoolAsset asset;

		public override bool Get() => asset.source.Get();

		public override void Set(bool newValue) => asset.source.Set(newValue: newValue);


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
	
	// -------------------------------------------------------------------------------------------------------------------------------- Evaluator //

	[Serializable]
	public class BoolSource_Evaluator : BoolSource
	{

		[SerializeField]
		public bool not = false;
		
		[SerializeReference]
		[TypeMenu]
		public IBoolEvaluation evaluator;

		public override bool Get() => not ? !evaluator.Evaluate() : evaluator.Evaluate();

		public override void Set(bool newValue) { }

	}

}