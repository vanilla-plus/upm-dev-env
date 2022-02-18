//using System.Collections;
//using System.Collections.Generic;
//
//using UnityEngine;
//
//namespace Vanilla.Pools.Samples
//{
//
//	public class ExamplePoolBehaviour : MonoBehaviour
//	{
//
//		public KeyCode fillKey = KeyCode.F;
//
//		public KeyCode drainKey = KeyCode.D;
//
//		public KeyCode getKey = KeyCode.G;
//		
//		public KeyCode retireKey = KeyCode.R;
//
//		public KeyCode retireAllKey = KeyCode.X;
//		
//		public ExamplePool pool;
//		
//		public void Update()
//		{
//			if (Input.GetKeyDown(fillKey))
//			{
//				pool.Fill();
//			}
//			else if (Input.GetKeyDown(drainKey))
//			{
//				pool.Drain();
//			}
//			else if (Input.GetKeyDown(getKey))
//			{
//				pool.Get();
//			}
//			else if (Input.GetKeyDown(retireKey))
//			{
//				if (pool.Active.Count == 0) return;
//
//				pool.Retire(item: pool.Active[Mathf.RoundToInt(f: UnityEngine.Random.Range(minInclusive: 0,
//				                                                                           maxExclusive: pool.Active.Count - 1))]);
//			}
//			else if (Input.GetKeyDown(retireAllKey))
//			{
//				pool.RetireAll();
//			}
//		}
//
//	}
//
//}