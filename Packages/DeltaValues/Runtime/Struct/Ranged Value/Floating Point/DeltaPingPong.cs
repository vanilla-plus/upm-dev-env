#if UNITY_EDITOR || DEVELOPMENT_BUILD
#define debug
#endif

using System;

using UnityEngine;

namespace Vanilla.DeltaValues
{

	[Serializable]
	public class DeltaPingPong : DeltaFloat
	{

		#region Properties

		
		[Range(min: 0,
		       max: 2.0f)]
		[SerializeField]
		private float _Time = 0.0f;
		public float Time
		{
			get => _Time;
			set
			{
				_Time = Mathf.Repeat(t: value,
				                     length: 2.0f);

				Value = _Time <= 1.0f ?
					        _Time :
					        1.0f - (_Time - 1.0f);
			}
		}


		#endregion

		#region Serialization
		
		
		#endregion


		public void Update(float deltaTime) => Time += deltaTime;


		#region Construction



		public DeltaPingPong(string defaultName) : base(defaultName) { }


		public DeltaPingPong(string defaultName,
		                          float defaultValue) : base(defaultName: defaultName,
		                                                     defaultValue: defaultValue) { }


		public DeltaPingPong(string defaultName,
		                          float defaultValue,
		                          float defaultChangeEpsilon) : base(defaultName: defaultName,
		                                                             defaultValue: defaultValue,
		                                                             defaultChangeEpsilon: defaultChangeEpsilon) { }


		public DeltaPingPong(string defaultName,
		                          float defaultValue,
		                          float defaultMin,
		                          float defaultMax) : base(defaultName: defaultName,
		                                                   defaultValue: defaultValue,
		                                                   defaultMin: defaultMin,
		                                                   defaultMax: defaultMax) { }


		public DeltaPingPong(string defaultName,
		                          float defaultValue,
		                          float defaultMin,
		                          float defaultMax,
		                          float defaultChangeEpsilon) : base(defaultName: defaultName,
		                                                             defaultValue: defaultValue,
		                                                             defaultMin: defaultMin,
		                                                             defaultMax: defaultMax,
		                                                             changeEpsilon: defaultChangeEpsilon) { }


		public DeltaPingPong(string defaultName,
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