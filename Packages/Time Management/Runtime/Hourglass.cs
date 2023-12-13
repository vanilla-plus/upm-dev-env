using Cysharp.Threading.Tasks;

using UnityEngine;

using Vanilla.DeltaValues;



namespace Vanilla.TimeManagement
{

	public static class Hourglass
	{

		private static float _TimeScaleDampingVelocity;
		
		private static DeltaVec1 TargetTimeScale;

		private static DeltaBool Interpolating;

		private static float _PauseTimeScaleCache;

		private static float _SlowMotionSpeed = 0.1f;
		public static float SlowMotionSpeed
		{
			get => _SlowMotionSpeed;
			set => _SlowMotionSpeed = Mathf.Clamp(value: value,
			                                      min: 0.0f,
			                                      max: 1.0f);
		}
		
		private static float _FastForwardSpeed = 10.0f;
		public static float FastForwardSpeed
		{
			get => _FastForwardSpeed;
			set => _FastForwardSpeed = Mathf.Clamp(value: value,
			                                       min: 1.0f,
			                                       max: 100.0f);
		}

		public static float TimeScaleInterpolationSpeed = 0.125f;

		public static bool CanPause;
		
		public static DeltaState Paused;
		
		

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
		private static void Reset()
		{
			#if debug
			Debug.Log($"[{Time.frameCount}] [Hourglass] Reset");
			#endif

			_SlowMotionSpeed     = 0.1f;
			_FastForwardSpeed    = 10.0f;
			
			_PauseTimeScaleCache = 1.0f;

			_TimeScaleDampingVelocity = 0.0f;

			TargetTimeScale = new DeltaVec1(defaultName: "Target Time Scale",
			                                defaultValue: 1.0f,
			                                defaultMin: 0.0f,
			                                defaultMax: 100.0f,
			                                changeEpsilon: float.Epsilon);

			Interpolating = new DeltaBool(name: "Target Time Scale Interpolating",
			                              defaultValue: false);

			CanPause = true;

			Paused?.Deinit();

			Paused = new DeltaState(name: "Paused",
			                        defaultActiveState: false,
			                        fillSeconds: 0.5f);
		}


		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
		private static void Init()
		{
			#if debug
			Debug.Log($"[{Time.frameCount}] [Hourglass] Init");
			#endif
			
			Paused.Init();
			
			Paused.Progress.AtMin.OnFalse  += HandlePause;
			Paused.Progress.AtMin.OnTrue   += HandleResume;
			Interpolating.OnTrue           += StartInterpolating;
			TargetTimeScale.OnValueChanged += HandleTargetTimeScaleChange;
		}

		private static void HandleTargetTimeScaleChange(float incoming) => Interpolating.Value = true;


		private static void StartInterpolating() => Interpolate().Forget();

		private static async UniTask Interpolate()
		{
			#if debug
			Debug.Log($"[{Time.frameCount}] [Hourglass] Interpolate");
			#endif
			
			do
			{
				// This is our new weird 'IsPaused'.
				// Paused.Progress is our normal for driving animations, sound, etc
				// Paused.Active is responsible for running Fill/Drain automation
				// So if Paused.Progress.AtMin is now technically our 'exact' pause state.
				// If Paused.Progress.AtMin is true, the game is officially playing.
				// If Paused.Progress.AtMin is false, the game is officially paused.
				if (Paused.Progress.AtMin.Value) 
				{
					var target = TargetTimeScale.Value;

					if (Mathf.Abs(Time.timeScale - target) < 0.00001f)
					{
						Interpolating.Value = false;

						Time.timeScale = target;
					}
					else
					{
						Time.timeScale = Mathf.SmoothDamp(current: Time.timeScale,
						                                  target: target,
						                                  currentVelocity: ref _TimeScaleDampingVelocity,
						                                  smoothTime: TimeScaleInterpolationSpeed,
						                                  maxSpeed: 10.0f,
						                                  deltaTime: Time.unscaledDeltaTime);
						
						#if debug
						Debug.Log($"[{Time.frameCount}] [Hourglass] Interpolating... [{Time.timeScale} to {target}]");
						#endif
					}
				}

				await UniTask.Yield();
			}
			while (Application.isPlaying && Interpolating.Value);
		}


		private static void HandlePause()
		{
			_PauseTimeScaleCache = Time.timeScale;

			Time.timeScale = 0.0f;

			#if debug
			Debug.Log("[{Time.frameCount}] [Hourglass]  • • • P A U S E • • • ");
			#endif
		}


		private static void HandleResume()
		{
			Time.timeScale = _PauseTimeScaleCache;
		
			#if debug
			Debug.Log("[{Time.frameCount}] [Hourglass]  • • • R E S U M E • • • ");
			#endif
		}

		public static void SetTimeScale(float newTimeScale) => TargetTimeScale.Value = newTimeScale;

		public static void SetTimeScaleSlow()   => TargetTimeScale.Value = _SlowMotionSpeed;
		public static void SetTimeScaleNormal() => TargetTimeScale.Value = 1.0f;
		public static void SetTimeScaleFast()   => TargetTimeScale.Value = _FastForwardSpeed;
		
		public static void TryPause()
		{
			if (CanPause) Paused.Active.Flip();
		}

	}

}