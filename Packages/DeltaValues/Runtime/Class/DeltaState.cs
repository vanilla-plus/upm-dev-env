using System;
using System.Collections;
using System.Collections.Generic;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Vanilla.DeltaValues
{

	[Serializable]
	public class DeltaState : IDisposable

	{

		[SerializeField]
		public string Name;

		[SerializeField]
		public DeltaBool Active;

		[SerializeField]
		public DeltaFloat Progress;

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

			Progress = new DeltaFloat(defaultName: Name + ".Progress",
			                          defaultValue: defaultActiveState ?
				                                        1.0f :
				                                        0.0f,
			                          defaultMin: 0.0f,
			                          defaultMax: 1.0f,
			                          changeEpsilon: float.Epsilon);
			
			Active.OnTrue += BeginFill;

			Active.OnFalse += BeginDrain;
		}


		private void BeginFill()
		{
			if (UseScaledTime)
			{
				Progress.FillScaled(Active,
				                    true,
				                    1.0f,
				                    FillSeconds).Forget();
			}
			else
			{
				Progress.FillUnscaled(Active,
				                      true,
				                      1.0f,
				                      FillSeconds).Forget();
			}
		}


		private void BeginDrain()
		{
			if (UseScaledTime)
			{
				Progress.DrainScaled(Active,
				                     false,
				                     1.0f,
				                     FillSeconds).Forget();
			}
			else
			{
				Progress.DrainUnscaled(Active,
				                       false,
				                       1.0f,
				                       FillSeconds).Forget();
			}
		}


		public void Dispose()
		{
			Active?.Dispose();
			Progress?.Dispose();
		}

	}

}