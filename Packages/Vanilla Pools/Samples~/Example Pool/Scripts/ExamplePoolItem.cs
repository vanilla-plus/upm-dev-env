using System;

using UnityEngine;

namespace Vanilla.Pools.Samples
{

	[Serializable]
	public class ExamplePoolItem : PoolItem<ExamplePool, ExamplePoolItem>
	{

		public static ExamplePool _pool;
		public override ExamplePool Pool
		{
			get => _pool;
			set => _pool = value;
		}


		protected override void OnGet()
		{
			var t = transform;

			const float p = 10.0f;

			t.localPosition = new Vector3(x: UnityEngine.Random.Range(minInclusive: -p,
			                                                          maxInclusive:  p),
			                              y: UnityEngine.Random.Range(minInclusive: -p,
			                                                          maxInclusive:  p),
			                              z: UnityEngine.Random.Range(minInclusive: -p,
			                                                          maxInclusive:  p));

			const float e = 360.0f;

			t.localEulerAngles = new Vector3(x: UnityEngine.Random.Range(minInclusive: 0,
			                                                             maxInclusive: e),
			                                 y: UnityEngine.Random.Range(minInclusive: 0,
			                                                             maxInclusive: e),
			                                 z: UnityEngine.Random.Range(minInclusive: 0,
			                                                             maxInclusive: e));

			const float sMin = 0.1f;
			const float sMax = 3.0f;

			t.localScale = new Vector3(x: UnityEngine.Random.Range(minInclusive: sMin,
			                                                       maxInclusive: sMax),
			                           y: UnityEngine.Random.Range(minInclusive: sMin,
			                                                       maxInclusive: sMax),
			                           z: UnityEngine.Random.Range(minInclusive: sMin,
			                                                       maxInclusive: sMax));
		}


		protected override void OnRetire() { }

		[ContextMenu("Retire Self")]
		public void RetireSelf() => Retire();
		
	}

}