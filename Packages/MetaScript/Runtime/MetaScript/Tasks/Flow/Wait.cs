using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

using Vanilla.MetaScript.DataSources;
using Vanilla.TypeMenu;

namespace Vanilla.MetaScript.Flow
{

	[Serializable]
	public class Wait : MetaTask
	{

		protected override bool Validate => true;

		protected override string CreateAutoName() => $"Wait for {secondsToTake} seconds";

		public float secondsToTake = 1.0f;


		public override void OnValidate()
		{
			#if UNITY_EDITOR
			base.OnValidate();

			SecondsToTake.Value = secondsToTake;
			#endif
		}


		[TypeMenu("Red")]
		[SerializeReference]
		public FloatSource SecondsToTake = new DirectFloatSource
		                                   {
			                                   _value = 1.0f
		                                   };
		
		protected override async UniTask<Scope> _Run(Scope scope)
		{
			var timeRemaining = SecondsToTake.Value;

			while (timeRemaining > 0.0f)
			{
				if (scope.Cancelled) return scope;

				timeRemaining -= Time.deltaTime;
				
				await UniTask.Yield();
			}

			return scope;
		}

	}

}