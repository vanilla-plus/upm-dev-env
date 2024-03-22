using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

using Vanilla.MetaScript.DataSources;

namespace Vanilla
{

	[Serializable]
	public class State : IDisposable, ISerializationCallbackReceiver
	{

		[SerializeField]
		public string Name;

		[SerializeField]
		public ProtectedBoolSource Active;

		[SerializeField]
		public RangedFloatSource Progress;

		public bool UseScaledTime = false;

		public float FillSeconds = 0.5f;
		
		public State(string name,
		                  bool defaultActiveState = false,
		                  float fillSeconds = 1.0f)
		{
			Name = name;

			FillSeconds = fillSeconds;

			Active = new ProtectedBoolSource(defaultActiveState);

			Progress = new RangedFloatSource
			           {
				           Min = 0.0f,
				           Max = 1.0f,
				           Value = defaultActiveState ? 1.0f : 0.0f
			           };
		}

		public void Init() => Active.OnSet += HandleSet;
		public void Deinit() => Active.OnSet -= HandleSet;

		private void HandleSet(bool isTrue)
		{
			if (isTrue)
			{
				BeginFill();
			}
			else
			{
				BeginDrain();
			}
		}

		private void BeginFill()
		{
			if (UseScaledTime)
			{
				FillScaled(targetCondition: true,
				           amountPerSecond: 1.0f,
				           secondsToTake: FillSeconds)
					.Forget();
			}
			else
			{
				FillUnscaled(targetCondition: true,
				             amountPerSecond: 1.0f,
				             secondsToTake: FillSeconds)
					.Forget();
			}
		}


		private void BeginDrain()
		{
			if (UseScaledTime)
			{
				DrainScaled(targetCondition: false,
				            amountPerSecond: 1.0f,
				            secondsToTake: FillSeconds)
					.Forget();
			}
			else
			{
				DrainUnscaled(targetCondition: false,
				              amountPerSecond: 1.0f,
				              secondsToTake: FillSeconds)
					.Forget();
			}
		}
		
		


		public void Dispose() => Deinit();

		public void OnBeforeSerialize() { }


		public void OnAfterDeserialize()
		{
			Active.Name         = $"{Name}.Active";
			Progress.AtMin.Name = $"{Name}.Progress.AtMin";
			Progress.AtMax.Name = $"{Name}.Progress.AtMax";
			
			Progress.Value = Active.Value ?
				                 1.0f :
				                 0.0f;
		}


		public async UniTask FillScaled(bool targetCondition = true,
		                                float amountPerSecond = 1.0f,
		                                float secondsToTake = 1.0f)
		{
			var rate = 1.0f / secondsToTake;

			while ((targetCondition ?
				        Active.Value :
				        !Active.Value) &&
			       !Progress.AtMax.Value)
			{
				Progress.Value += amountPerSecond * Time.deltaTime * rate;

				await UniTask.Yield();
			}

			Progress.Value = Progress.Max;
		}


		public async UniTask FillUnscaled(bool targetCondition = true,
		                                  float amountPerSecond = 1.0f,
		                                  float secondsToTake = 1.0f)
		{
			var rate = amountPerSecond / secondsToTake;

			while ((targetCondition ?
				        Active.Value :
				        !Active.Value) &&
			       !Progress.AtMax.Value)
			{
				Progress.Value += amountPerSecond * Time.unscaledDeltaTime * rate;

				await UniTask.Yield();
			}

			Progress.Value = Progress.Max;
		}


		public async UniTask DrainScaled(bool targetCondition = true,
		                                 float amountPerSecond = 1.0f,
		                                 float secondsToTake = 1.0f)
		{
			var rate = amountPerSecond / secondsToTake;

			while ((targetCondition ?
				        Active.Value :
				        !Active.Value) &&
			       !Progress.AtMin.Value)
			{
				Progress.Value -= amountPerSecond * Time.deltaTime * rate;

				await UniTask.Yield();
			}

			Progress.Value = Progress.Min;
		}


		public async UniTask DrainUnscaled(bool targetCondition = true,
		                                   float amountPerSecond = 1.0f,
		                                   float secondsToTake = 1.0f)
		{
			var rate = amountPerSecond / secondsToTake;

			while ((targetCondition ?
				        Active.Value :
				        !Active.Value) &&
			       !Progress.AtMin.Value)
			{
				Progress.Value -= amountPerSecond * Time.unscaledDeltaTime * rate;

				await UniTask.Yield();
			}

			Progress.Value = Progress.Min;
		}

	}

}