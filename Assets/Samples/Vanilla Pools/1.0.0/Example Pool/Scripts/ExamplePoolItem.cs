//using System;
//
//using Cysharp.Threading.Tasks;
//
//using UnityEngine;
//
//namespace Vanilla.Pools.Samples
//{
//
//	[Serializable]
//	public class ExamplePoolItem : IPoolItem
//	{
//
//		public async UniTask HandleGet()
//		{
//			var t = transform;
//
//			const float p = 10.0f;
//
//			await UniTask.Delay(500);
//
//			t.localPosition = new Vector3(x: UnityEngine.Random.Range(minInclusive: -p,
//			                                                          maxInclusive: p),
//			                              y: UnityEngine.Random.Range(minInclusive: -p,
//			                                                          maxInclusive: p),
//			                              z: UnityEngine.Random.Range(minInclusive: -p,
//			                                                          maxInclusive: p));
//
//			await UniTask.Delay(500);
//
//			const float e = 360.0f;
//
//			t.localEulerAngles = new Vector3(x: UnityEngine.Random.Range(minInclusive: 0,
//			                                                             maxInclusive: e),
//			                                 y: UnityEngine.Random.Range(minInclusive: 0,
//			                                                             maxInclusive: e),
//			                                 z: UnityEngine.Random.Range(minInclusive: 0,
//			                                                             maxInclusive: e));
//
//			await UniTask.Delay(500);
//
//			const float sMin = 0.1f;
//			const float sMax = 3.0f;
//
//			t.localScale = new Vector3(x: UnityEngine.Random.Range(minInclusive: sMin,
//			                                                       maxInclusive: sMax),
//			                           y: UnityEngine.Random.Range(minInclusive: sMin,
//			                                                       maxInclusive: sMax),
//			                           z: UnityEngine.Random.Range(minInclusive: sMin,
//			                                                       maxInclusive: sMax));
//		}
//
//
//		public override UniTask HandleRetire() => default;
//
//
//		[ContextMenu("Retire Self")]
//		public void RetireSelfImp() => Retire();
//
//	}
//
//}