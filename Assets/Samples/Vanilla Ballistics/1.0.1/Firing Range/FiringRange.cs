using System;
using System.Collections.Generic;

using UnityEngine;

namespace Vanilla.Ballistics.Samples
{

	public class FiringRange : MonoBehaviour
	{

		[Header("References")]
		public Transform target;

		public Transform rangeTransform;

		public GameObject projectilePrefab;

		public Transform projectileParent;
		
		private List<GameObject> projectiles = new List<GameObject>();

		[Header("Settings")]
		[Range(min: 0.1f, 
		       max: 10.0f)]
		public float     rateOfFire = 0.5f;

		[Range(min: 0.1f, 
		       max: 10.0f)]
		public float secondsToReachTarget = 1.0f;

		public TestMode inaccuracyMode = TestMode.Perfect;

		[Range(min: 0.0f, 
		       max: 10.0f)]
		public float inaccuracyRange = 0.5f;

		public AnimationCurve inaccuracyDistribution;

		private float _t = 0;
		
		public enum TestMode
		{

			Perfect,
			OverGround,
			Approach,
			Spherical
			
		}

		void Start() => Fire();


		void Update()
		{
			_t += Time.deltaTime;

			if (_t > rateOfFire)
			{
				_t %= rateOfFire;

				Fire();
			}

			rangeTransform.localScale = inaccuracyMode switch
			                            {
				                            TestMode.Perfect => Vector3.zero,
				                            TestMode.Approach => new Vector3(x: inaccuracyRange,
				                                                             y: 0.1f,
				                                                             z: inaccuracyRange),
				                            TestMode.OverGround => new Vector3(x: inaccuracyRange,
				                                                               y: inaccuracyRange,
				                                                               z: 0.1f),
				                            TestMode.Spherical => Vector3.one * inaccuracyRange,
				                            _                  => throw new ArgumentOutOfRangeException()
			                            };
		}

		void Fire()
		{
			Invoke(methodName: nameof(Chill),
			       time: secondsToReachTarget);
			
			var p = transform.position;
			var d = target.position;

			var projectile = Instantiate(original: projectilePrefab,
			                          position: p,
			                          rotation: transform.rotation,
			                          parent: projectileParent);

			projectile.SetActive(true);

			var velocity = inaccuracyMode switch
			               {
				               TestMode.Perfect => BallisticsUtility.Get3DArcVelocity(origin: p,
				                                                                      target: d,
				                                                                      secondsToReachTarget: secondsToReachTarget),
				               TestMode.OverGround => BallisticsUtility.Get3DArcVelocityWithInaccuracyOverGround(origin: p,
				                                                                                                 target: d,
				                                                                                                 secondsToReachTarget: secondsToReachTarget,
				                                                                                                 inaccuracyRange: inaccuracyRange,
				                                                                                                 inaccuracyCurve: inaccuracyDistribution),
				               TestMode.Approach => BallisticsUtility.Get3DArcVelocityWithInaccurateApproach(origin: p,
				                                                                                             target: d,
				                                                                                             secondsToReachTarget: secondsToReachTarget,
				                                                                                             inaccuracyRange: inaccuracyRange,
				                                                                                             inaccuracyCurve: inaccuracyDistribution),
				               TestMode.Spherical => BallisticsUtility.Get3DArcVelocityWithSphericalInaccuracy(origin: p,
				                                                                                               target: d,
				                                                                                               secondsToReachTarget: secondsToReachTarget,
				                                                                                               inaccuracyRange: inaccuracyRange,
				                                                                                               inaccuracyCurve: inaccuracyDistribution),
				               _ => throw new ArgumentOutOfRangeException()
			               };

			transform.forward = velocity.normalized;

			if (inaccuracyMode == TestMode.Approach)
			{
				rangeTransform.forward = velocity;
				
//				rangeTransform.forward = Vector3.Reflect(inDirection: velocity,
//				                                         inNormal: Vector3.up);
			}
			
			p.y += velocity.y * secondsToReachTarget;

			projectile.GetComponent<Rigidbody>().AddForce(force: velocity,
			                                              mode: ForceMode.Impulse);

			projectiles.Add(item: projectile);


		}


		private void Chill()
		{
			var g = projectiles[0];

			projectiles.RemoveAt(0);

			Destroy(g.GetComponent<Rigidbody>());
		}
		
	}

}