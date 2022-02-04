using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Vanilla.UnityExtensions;

namespace Vanilla.Ballistics
{

	public static class BallisticsUtility
	{

		public static Vector3 Get3DArcVelocity(Vector3 origin,
		                                       Vector3 target,
		                                       float secondsToReachTarget)
		{
			var dir     = target - origin;
			var dirFlat = dir;

			dirFlat.y = 0;

			// calculate xz and y
			var y  = dir.y;
			var xz = dirFlat.magnitude;

			// calculate starting speeds for xz and y. Physics forumulase deltaX = v0 * t + 1/2 * a * t * t
			// where a is "-gravity" but only on the y plane, and a is 0 in xz plane.
			// so xz = v0xz * t => v0xz = xz / t
			// and y = v0y * t - 1/2 * gravity * t * t => v0y * t = y + 1/2 * gravity * t * t => v0y = y / t + 1/2 * gravity * t
			var v0y  = y / secondsToReachTarget + 0.5f * Physics.gravity.magnitude * secondsToReachTarget;
			var v0xz = xz / secondsToReachTarget;

			// create result vector for calculated starting speeds
			var result = dirFlat.normalized; // get direction of xz but with magnitude 1

			result   *= v0xz; // set magnitude of xz to v0xz (starting speed in xz plane)
			result.y =  v0y;  // set y to v0y (starting speed of y plane)

			// Vanilla edit
			// The original result was consistently short by a specific margin related to travel time
			// Modifying the result by this much makes it a perfect hit regardless of travel time
			//result *= 1 + 0.005f / secondsToReachTarget;

			return result;
		}


//		/// <summary>
//		///		Gets a velocity value that, when applied to a rigidbody using .AddForce, will make the rigidbody inaccurately hit the target in however many seconds.
//		/// </summary>
//		/// <param name="origin">The starting position of the projectile</param>
//		/// <param name="target">The target position for the projectile to hit</param>
//		/// <param name="secondsToReachTarget">The amount of seconds the projectile will take to hit the target</param>
//		/// <param name="inaccuracyRange">The maximum amount of potential inaccuracy in world units.</param>
//		/// <param name="inaccuracyCurve">A curve used to nominate the chance of inaccuracy. A random point on the curve will be sampled using Random.value.</param>
//		/// <returns>A velocity that will make a rigidbody travel in an arc such that it hits the target in exactly timeToReachTarget seconds. The shot has a chance of being inaccurate by inaccuracyRange based on a random sample from inaccuracyCurve.</returns>
//		public static Vector3 Get3DArcVelocityWithInaccurateXY(Vector3 origin,
//		                                                       Vector3 target,
//		                                                       float secondsToReachTarget,
//		                                                       float inaccuracyRange,
//		                                                       AnimationCurve inaccuracyCurve)
//		{
//			var inaccuracy = Get2DInaccuracy(range: inaccuracyRange,
//			                                 distribution: inaccuracyCurve);
//
//			// This function always assumes that we are shooting horizontally at our target.
//			// So up is global rather than relative to the target.
//			target.y += inaccuracy.y;
//
//			// We need a direction vector perpendicular to the velocity's direction.
//			// This vector will act as a localized x-axis that we can add to the target position with our inaccuracy from above.
//			var product = Vector3.Cross(lhs: (origin - target).normalized,
//			                            rhs: Vector3.up);
//
//			target += product * inaccuracy.x;
//
//			return Get3DArcVelocity(origin: origin,
//			                        target: target,
//			                        secondsToReachTarget: secondsToReachTarget);
//		}


		/// <summary>
		///		Gets a velocity value that, when applied to a rigidbody using .AddForce, will make the rigidbody inaccurately hit the target in however many seconds.
		///
		///		The hit will be inaccurate perpendicular with the ground. This is best suited for artillery or bombers.
		/// 
		///		For example, slow lobs into the air ('like artillery') will miss on the relative X/Z.
		/// </summary>
		/// <param name="origin">The starting position of the projectile</param>
		/// <param name="target">The target position for the projectile to hit</param>
		/// <param name="secondsToReachTarget">The amount of seconds the projectile will take to hit the target</param>
		/// <param name="inaccuracyRange">The maximum amount of potential inaccuracy in world units.</param>
		/// <param name="inaccuracyCurve">A curve used to nominate the chance of inaccuracy. A random point on the curve will be sampled using Random.value.</param>
		/// <returns>A velocity that will make a rigidbody travel in an arc such that it hits the target in exactly secondsToReachTarget seconds. The shot has a chance of being inaccurate by inaccuracyRange based on a random sample from inaccuracyCurve.</returns>
		public static Vector3 Get3DArcVelocityWithInaccuracyOverGround(Vector3 origin,
		                                                               Vector3 target,
		                                                               float secondsToReachTarget,
		                                                               float inaccuracyRange,
		                                                               AnimationCurve inaccuracyCurve) => Get3DArcVelocity(origin: origin,
		                                                                                                                   target: target +
		                                                                                                                           Get2DInaccuracy(range: inaccuracyRange,
		                                                                                                                                           distribution: inaccuracyCurve).X_Y(y: 0),
		                                                                                                                   secondsToReachTarget: secondsToReachTarget);


		/// <summary>
		///		Gets a velocity value that, when applied to a rigidbody using .AddForce, will make the rigidbody inaccurately hit the target in however many seconds.
		///
		///		The hit will be inaccurate perpendicular to the approach of the shot.
		/// 
		///		For example, fast horizontal shots ('from a gun') will miss on the relative X/Y while slow lobs into the air ('like artillery') will miss on the relative X/Z.
		/// </summary>
		/// <param name="origin">The starting position of the projectile</param>
		/// <param name="target">The target position for the projectile to hit</param>
		/// <param name="secondsToReachTarget">The amount of seconds the projectile will take to hit the target</param>
		/// <param name="inaccuracyRange">The maximum amount of potential inaccuracy in world units.</param>
		/// <param name="inaccuracyCurve">A curve used to nominate the chance of inaccuracy. A random point on the curve will be sampled using Random.value.</param>
		/// <returns>A velocity that will make a rigidbody travel in an arc such that it hits the target in exactly secondsToReachTarget seconds. The shot has a chance of being inaccurate by inaccuracyRange based on a random sample from inaccuracyCurve.</returns>
		public static Vector3 Get3DArcVelocityWithInaccurateApproach(Vector3 origin,
		                                                             Vector3 target,
		                                                             float secondsToReachTarget,
		                                                             float inaccuracyRange,
		                                                             AnimationCurve inaccuracyCurve)
		{
			var vel = Get3DArcVelocity(origin: origin,
			                           target: target,
			                           secondsToReachTarget: secondsToReachTarget);

			var inaccuracy = Get3DInaccuracy(range: inaccuracyRange / secondsToReachTarget,
			                                 distribution: inaccuracyCurve);


			origin.y += vel.y * secondsToReachTarget;

			return vel +
			       Vector3.ProjectOnPlane(vector: inaccuracy,
			                              planeNormal: (origin - target).normalized);
		}


		/// <summary>
		///		Gets a velocity value that, when applied to a rigidbody using .AddForce, will make the rigidbody inaccurately hit the target in however many seconds.
		///
		///		The hit will be inaccurate anywhere in 3D space around the target, according to the provided range value.
		/// </summary>
		/// <param name="origin">The starting position of the projectile</param>
		/// <param name="target">The target position for the projectile to hit</param>
		/// <param name="secondsToReachTarget">The amount of seconds the projectile will take to hit the target</param>
		/// <param name="inaccuracyRange">The maximum distance/magnitude possible for the inaccuracy in world units.</param>
		/// <param name="inaccuracyCurve">A curve used to nominate the distribution of inaccuracy. A random point on the curve will be sampled using Random.value.</param>
		/// <returns>A velocity that will make a rigidbody travel in an arc such that it hits the target in exactly secondsToReachTarget seconds. The shot has a chance of being inaccurate by inaccuracyRange based on a random sample from inaccuracyCurve.</returns>
		public static Vector3 Get3DArcVelocityWithSphericalInaccuracy(Vector3 origin,
		                                                              Vector3 target,
		                                                              float secondsToReachTarget,
		                                                              float inaccuracyRange,
		                                                              AnimationCurve inaccuracyCurve) => Get3DArcVelocity(origin: origin,
		                                                                                                                  target: target +
		                                                                                                                          Get3DInaccuracy(range: inaccuracyRange,
		                                                                                                                                          distribution: inaccuracyCurve),
		                                                                                                                  secondsToReachTarget: secondsToReachTarget);


		/// <summary>
		///		Returns a radial vector2 with a randomized distribution back towards Vector3.zero.
		/// </summary>
		/// <param name="range">The maximum distance/magnitude possible for the point in world units.</param>
		/// <param name="distribution">A curve representing the distribution/spread of the inaccuracy.</param>
		/// <returns></returns>
		public static Vector2 Get2DInaccuracy(float range,
		                                      AnimationCurve distribution) => Vector2.Lerp(a: Vector2.zero,
		                                                                                   b: Random.insideUnitCircle.normalized * (range * 0.5f),
		                                                                                   t: distribution.Evaluate(time: Random.value));


		/// <summary>
		///		Returns a spherical vector3 with a randomized distribution back towards Vector3.zero. 
		/// </summary>
		/// <param name="range">The maximum distance/magnitude possible for the point in world units.</param>
		/// <param name="distribution">A curve representing the distribution/spread of the inaccuracy.</param>
		/// <returns></returns>
		public static Vector3 Get3DInaccuracy(float range,
		                                      AnimationCurve distribution) => Vector3.Lerp(a: Vector3.zero,
		                                                                                   b: Random.onUnitSphere * (range * 0.5f),
		                                                                                   t: distribution.Evaluate(time: Random.value));

	}

}