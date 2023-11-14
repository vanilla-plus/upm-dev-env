using System;
using System.Collections;
using System.Collections.Generic;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Vanilla.DeltaValues
{

	[Serializable]
	public class DeltaState : IDisposable, ISerializationCallbackReceiver
	{

		[SerializeField]
		public string Name;

		[SerializeField]
		public DeltaBool Active;

		[SerializeField]
		public DeltaVec1 Progress;

		public bool UseScaledTime = false;

		public float FillSeconds = 0.5f;
		
		public DeltaState(string name,
		                  bool defaultActiveState = false,
		                  float fillSeconds = 1.0f)
		{
			Name = name;

			FillSeconds = fillSeconds;

			Active = new DeltaBool(name: Name + ".Active",
			                       defaultValue: defaultActiveState);

			Progress = new DeltaVec1(defaultName: Name + ".Progress",
			                         defaultValue: defaultActiveState ?
				                                       1.0f :
				                                       0.0f,
			                         defaultMin: 0.0f,
			                         defaultMax: 1.0f,
			                         changeEpsilon: float.Epsilon);
		}

		public void Init()
		{
			Active.OnTrue += BeginFill;

			Active.OnFalse += BeginDrain;
		}

		public void Deinit()
		{
			Active.OnTrue -= BeginFill;

			Active.OnFalse -= BeginDrain;
		}

		private void BeginFill()
		{
			if (UseScaledTime)
			{
				Progress.FillScaled(conditional: Active,
				                    targetCondition: true,
				                    amountPerSecond: 1.0f,
				                    secondsToTake: FillSeconds).Forget();
			}
			else
			{
				Progress.FillUnscaled(conditional: Active,
				                      targetCondition: true,
				                      amountPerSecond: 1.0f,
				                      secondsToTake: FillSeconds).Forget();
			}
		}


		private void BeginDrain()
		{
			if (UseScaledTime)
			{
				Progress.DrainScaled(conditional: Active,
				                     targetCondition: false,
				                     amountPerSecond: 1.0f,
				                     secondsToTake: FillSeconds).Forget();
			}
			else
			{
				Progress.DrainUnscaled(conditional: Active,
				                       targetCondition: false,
				                       amountPerSecond: 1.0f,
				                       secondsToTake: FillSeconds).Forget();
			}
		}
		
		


		public void Dispose()
		{
			Deinit();
			
			Active?.Dispose();
			Progress?.Dispose();
		}


		public void OnBeforeSerialize() { }


		public void OnAfterDeserialize() => Progress.Value = Active.Value ?
			                                                     1.0f :
			                                                     0.0f;

	}

}