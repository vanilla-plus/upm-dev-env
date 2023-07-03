using System;
using System.Collections.Generic;
using System.Threading;

using Cysharp.Threading.Tasks;

using UnityEngine;

//using Vanilla.Easing;

namespace Vanilla.MetaScript
{

	[Serializable]
	public class LerpMaterialValue : MetaTask
	{

		protected override bool CanDescribe() => MaterialList.Count > 0 ;

		protected override string DescribeTask() => $"Lerp [{ShaderProperty}] from [{StartValue}] to [{EndValue}]";


		public List<Material> MaterialList = new List<Material>();
		public float          StartValue   = 0.0f;
		public float          EndValue     = 20.0f;

		public float  secondsToTake  = 2.0f;
		public string ShaderProperty = "_Alpha";

		//public bool position = true;
		//public bool rotation = true;
		//public bool scale    = false;

		//public bool local = false;


		protected override async UniTask _Run(CancellationTokenSource cancellationTokenSource)
		{
			var t     = 0.0f;
			var scale = 1.0f / secondsToTake;

			var startFade = StartValue;
			var endFade   = EndValue;
		
			var trip = 0.0f;

			while (t < 1.0f)
			{

				trip = Mathf.Lerp(startFade, endFade, t);

				foreach (var m in MaterialList) 
				{
					if (m != null) m.SetFloat(ShaderProperty, trip);
				}


				t += Time.deltaTime * scale * Time.timeScale;

				await UniTask.Yield();
			}
		}

	}

}