using System;
using System.Collections;
using System.Collections.Generic;

using Cysharp.Threading.Tasks;

using UnityEngine;

#if vanilla_metascript
using Vanilla.MetaScript;
#endif

namespace Vanilla.DeltaValues
{

	[Serializable]
    public abstract class DeltaVec<T> : DeltaRangedValue<T>
        where T : struct

    {
	    [SerializeField]
	    public float ChangeEpsilon = Mathf.Epsilon;

	    [SerializeField]
	    public float MinMaxEpsilon = 0.01f;
	    
	    #region Construction

		public DeltaVec() { }


		public DeltaVec(string defaultName) : base(name: defaultName)
		{
			_Min = TypeMinValue;
			_Max = TypeMaxValue;

			Min = _Min;
			Max = _Max;

			ChangeEpsilon = float.Epsilon;
			MinMaxEpsilon = 0.01f;
		}


		public DeltaVec(string defaultName,
		                   T defaultValue) : base(defaultName: defaultName,
		                                          defaultValue: defaultValue)
		{
			_Min = TypeMinValue;
			_Max = TypeMaxValue;

			Min = _Min;
			Max = _Max;

			ChangeEpsilon = float.Epsilon;
			MinMaxEpsilon = 0.01f;
		}


		public DeltaVec(string defaultName,
		                   T defaultValue,
		                   float defaultChangeEpsilon) : base(defaultName: defaultName,
		                                                      defaultValue: defaultValue)
		{
			_Min = TypeMinValue;
			_Max = TypeMaxValue;

			Min = _Min;
			Max = _Max;

			ChangeEpsilon = defaultChangeEpsilon;
			MinMaxEpsilon = 0.01f;
		}


		public DeltaVec(string defaultName,
		                   T defaultValue,
		                   T defaultMin,
		                   T defaultMax) : base(defaultName: defaultName,
		                                        defaultValue: defaultValue,
		                                        defaultMin: defaultMin,
		                                        defaultMax: defaultMax)
		{
			ChangeEpsilon = float.Epsilon;
			MinMaxEpsilon = 0.01f;
		}


		public DeltaVec(string defaultName,
		                   T defaultValue,
		                   T defaultMin,
		                   T defaultMax,
		                   float changeEpsilon) : base(defaultName: defaultName,
		                                               defaultValue: defaultValue,
		                                               defaultMin: defaultMin,
		                                               defaultMax: defaultMax)
		{
			ChangeEpsilon = changeEpsilon;
			MinMaxEpsilon = 0.01f;
		}


		public DeltaVec(string defaultName,
		                   T defaultValue,
		                   T defaultMin,
		                   T defaultMax,
		                   float changeEpsilon,
		                   float minMaxEpsilon) : base(defaultName: defaultName,
		                                               defaultValue: defaultValue,
		                                               defaultMin: defaultMin,
		                                               defaultMax: defaultMax)
		{
			ChangeEpsilon = changeEpsilon;
			MinMaxEpsilon = minMaxEpsilon;
		}
		
		#endregion

		#region Math



		// Be smart here

		public abstract T New(float initial);
        
		public abstract T Add(T a);
		public abstract T Sub(T a);
		public abstract T Mul(T a);
		public abstract T Div(T a);

//		public abstract T New(float initial);


		#endregion
		
		#region Modulating



		public abstract void Lerp(float normal);


		public abstract void Lerp(float outgoing,
		                          float incoming);
		
//		protected abstract T GetScaledAmount(float amount);

		public async UniTask FillScaled(DeltaBool conditional,
		                                bool targetCondition = true,
		                                float amountPerSecond = 1.0f,
		                                float secondsToTake = 1.0f)
		{
			var rate = 1.0f / secondsToTake;

			while ((targetCondition ?
				        conditional.Value :
				        !conditional.Value) &&
			       !AtMax.Value)
			{
				Add(a: New(initial: amountPerSecond * Time.deltaTime * rate));
				
//				Value = Add(a: Value,
//				            b: New(scalar: amountPerSecond * Time.deltaTime * rate));
				
//				Value += GetScaledAmount(amountPerSecond * Time.deltaTime * rate);

				await UniTask.Yield();
			}

			Value = Max;
		}


		public async UniTask FillUnscaled(DeltaBool conditional,
		                                  bool targetCondition = true,
		                                  float amountPerSecond = 1.0f,
		                                  float secondsToTake = 1.0f)
		{
			var rate = amountPerSecond / secondsToTake;

			while ((targetCondition ?
				        conditional.Value :
				        !conditional.Value) &&
			       !AtMax.Value)
			{
//				Value += Time.unscaledDeltaTime * rate;

				Add(a: New(initial: amountPerSecond * Time.unscaledDeltaTime * rate));

				await UniTask.Yield();
			}

			Value = Max;
		}


		public async UniTask DrainScaled(DeltaBool conditional,
		                                 bool targetCondition = true,
		                                 float amountPerSecond = 1.0f,
		                                 float secondsToTake = 1.0f)
		{
			var rate = amountPerSecond / secondsToTake;

			while ((targetCondition ?
				        conditional.Value :
				        !conditional.Value) &&
			       !AtMin.Value)
			{
//				Value -= Time.deltaTime * rate;

				Sub(a: New(initial: amountPerSecond * Time.deltaTime * rate));

				await UniTask.Yield();
			}

			Value = Min;
		}


		public async UniTask DrainUnscaled(DeltaBool conditional,
		                                   bool targetCondition = true,
		                                   float amountPerSecond = 1.0f,
		                                   float secondsToTake = 1.0f)
		{
			var rate = amountPerSecond / secondsToTake;

			while ((targetCondition ?
				        conditional.Value :
				        !conditional.Value) &&
			       !AtMin.Value)
			{
//				Value -= Time.unscaledDeltaTime * rate;

				Sub(a: New(initial: amountPerSecond * Time.unscaledDeltaTime * rate));

				await UniTask.Yield();
			}

			Value = Min;
		}


		#if vanilla_metascript
		public async UniTask<Scope> FillScaled(Scope scope,
		                                       float amountPerSecond = 1.0f,
		                                       float secondsToTake = 1.0f)
		{
			var rate = amountPerSecond / secondsToTake;

			while (!AtMax.Value)
			{
				if (scope.Cancelled) return scope;

//				Value += Time.deltaTime * rate;
				
				Add(a: New(initial: amountPerSecond * Time.deltaTime * rate));
				
				await UniTask.Yield();
			}

			Value = Max;

			return scope;
		}


		public async UniTask<Scope> FillUnscaled(Scope scope,
		                                         float amountPerSecond = 1.0f,
		                                         float secondsToTake = 1.0f)
		{
			var rate = amountPerSecond / secondsToTake;

			while (!AtMax.Value)
			{
				if (scope.Cancelled) return scope;

//				Value += Time.unscaledDeltaTime * rate;
				
				Add(a: New(initial: amountPerSecond * Time.unscaledDeltaTime * rate));

				await UniTask.Yield();
			}

			Value = Max;

			return scope;
		}


		public async UniTask<Scope> DrainScaled(Scope scope,
		                                        float amountPerSecond = 1.0f,
		                                        float secondsToTake = 1.0f)
		{
			var rate = amountPerSecond / secondsToTake;

			while (!AtMin.Value)
			{
				if (scope.Cancelled) return scope;

//				Value -= Time.deltaTime * rate;

				Sub(a: New(initial: amountPerSecond * Time.deltaTime * rate));

				await UniTask.Yield();
			}

			Value = Min;

			return scope;
		}


		public async UniTask<Scope> DrainUnscaled(Scope scope,
		                                          float amountPerSecond = 1.0f,
		                                          float secondsToTake = 1.0f)
		{
			var rate = amountPerSecond / secondsToTake;

			while (!AtMin.Value)
			{
				if (scope.Cancelled) return scope;

//				Value -= Time.unscaledDeltaTime * rate;

				Sub(a: New(initial: amountPerSecond * Time.unscaledDeltaTime * rate));

				await UniTask.Yield();
			}

			Value = Min;

			return scope;
		}
		#endif
		
		#endregion

    }

}
