#if UNITY_EDITOR || DEVELOPMENT_BUILD
#define debug
#endif

using System;

using UnityEngine;

namespace Vanilla.SmartValues
{

	[Serializable]
	public class SmartSinWave : SmartFloat
	{

		#region Properties



		[SerializeField]
		public float Frequency = 1.0f;

		[Range(min: 0,
		       max: 1.0f)]
		[SerializeField]
		private float _Time = 0.0f;
		public float Time
		{
			get => _Time;
			set
			{
				_Time = Mathf.Repeat(t: value,
				                     length: 1.0f);

				Value = Amplitude * (float) Math.Sin(2 * Math.PI * _Time) + VerticalShift;
			}
		}

		[SerializeField]
		private float Amplitude;

		[SerializeField]
		private float VerticalShift;



		#endregion

		#region Serialization



		public override void OnAfterDeserialize()
		{
			base.OnAfterDeserialize();

			Amplitude     = (Max - Min) / 2.0f;
			VerticalShift = (Max + Min) / 2.0f;
		}



		#endregion


		public void Update(float deltaTime) => Time += deltaTime * Frequency;


		#region Construction



		public SmartSinWave(string defaultName) : base(defaultName) { }


		public SmartSinWave(string defaultName,
		                    float defaultValue) : base(defaultName: defaultName,
		                                               defaultValue: defaultValue) { }


		public SmartSinWave(string defaultName,
		                    float defaultValue,
		                    float defaultChangeEpsilon) : base(defaultName: defaultName,
		                                                       defaultValue: defaultValue,
		                                                       defaultChangeEpsilon: defaultChangeEpsilon) { }


		public SmartSinWave(string defaultName,
		                    float defaultValue,
		                    float defaultMin,
		                    float defaultMax) : base(defaultName: defaultName,
		                                             defaultValue: defaultValue,
		                                             defaultMin: defaultMin,
		                                             defaultMax: defaultMax) { }


		public SmartSinWave(string defaultName,
		                    float defaultValue,
		                    float defaultMin,
		                    float defaultMax,
		                    float defaultChangeEpsilon) : base(defaultName: defaultName,
		                                                       defaultValue: defaultValue,
		                                                       defaultMin: defaultMin,
		                                                       defaultMax: defaultMax,
		                                                       changeEpsilon: defaultChangeEpsilon) { }


		public SmartSinWave(string defaultName,
		                    float defaultValue,
		                    float defaultMin,
		                    float defaultMax,
		                    float defaultChangeEpsilon,
		                    float defaultMinMaxEpsilon) : base(defaultName: defaultName,
		                                                       defaultValue: defaultValue,
		                                                       defaultMin: defaultMin,
		                                                       defaultMax: defaultMax,
		                                                       changeEpsilon: defaultChangeEpsilon,
		                                                       minMaxEpsilon: defaultMinMaxEpsilon) { }




		#endregion

	}

}