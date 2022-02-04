using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using UnityEngine.SceneManagement;

using Vanilla.DotNetExtensions;

using Object = UnityEngine.Object;

namespace Vanilla.UnityExtensions
{

	public static class TransformExtensions
	{

		// --------------------------------------------------------------------------------------------------------------------- Self-Destruction //


		public static void Destroy(this Transform input)
		{
			#if UNITY_EDITOR
			if (Application.isPlaying) Object.Destroy(obj: input.gameObject);
			else Object.DestroyImmediate(obj: input.gameObject);
			#else
                Object.Destroy(input.gameObject);
			#endif
		}


		// ------------------------------------------------------------------------------------------------------------- GetComponents From Query //


		public static IEnumerable<T> GetComponentsFromCollection<T>(this IEnumerable<Transform> input)
			where T : Component => input.Select(selector: t => t.GetComponent<T>()).Where(predicate: c => c != null);


		// ----------------------------------------------------------------------------------------------------------------- GetComponent Dynamic //


		public static T GetComponentDynamic<T>(this Transform input,
		                                       GetComponentStyle style,
		                                       bool includeInactive = false) => style switch
		                                                                        {
			                                                                        GetComponentStyle.InChildren => input.GetComponentInChildren<T>(includeInactive: includeInactive),
			                                                                        GetComponentStyle.InParent   => input.GetComponentInParent<T>(),
			                                                                        _                            => input.GetComponent<T>()
		                                                                        };


		public static T[] GetComponentsDynamic<T>(this Transform input,
		                                          GetComponentStyle style,
		                                          bool includeInactive = false) => style switch
		                                                                           {
			                                                                           GetComponentStyle.InChildren => input.GetComponentsInChildren<T>(includeInactive: includeInactive),
			                                                                           GetComponentStyle.InParent   => input.GetComponentsInParent<T>(includeInactive: includeInactive),
			                                                                           _                            => input.GetComponents<T>()
		                                                                           };


		// ------------------------------------------------------------------------------------------------------------------- Collection Queries //

		// Parents


		// This approach outperforms:
		// input.GetComponentsInParent<Transform>().Skip(1);
		// roughly 90% faster (1-2 ticks vs 9-11)
		public static IEnumerable<Transform> GetAllParents(this Transform input)
		{
			var t     = input.parent;
			var depth = 0;

			while (!ReferenceEquals(objA: t,
			                        objB: null))
			{
				depth++;

				t = t.parent;
			}

			var parents = new Transform[depth];

			t = input.parent;

			for (var i = 0;
			     i < depth;
			     i++)
			{
				parents[i] = t;

				t = t.parent;
			}

			return parents;
		}


		// Children

		// All


		// 4~6~ ticks | 64b | recursive
		public static IEnumerable<Transform> GetAllChildren(this Transform input)
		{
			var children = new Stack<Transform>(); // Queue and List work just as well.

			for (var i = input.childCount - 1;
			     i >= 0;
			     i--) children.Push(item: input.GetChild(index: i));

			while (children.Count > 0)
			{
				var n = children.Pop();

				yield return n;

				for (var i = n.childCount - 1;
				     i >= 0;
				     i--) children.Push(item: n.GetChild(index: i));
			}
		}


		// Immediate 

		// This looks super cool and concise, but it generates garbage :( 56b last I checked...
		// public static IEnumerable<Transform> GetImmediateChildren(this Transform input) => input.Cast<Transform>().Skip(1);


		/// <summary>
		///     Returns a Transform array containing only direct children of this transform.
		/// </summary>
		public static IEnumerable<Transform> GetImmediateChildren(this Transform input)
		{
			var children = new Transform[input.childCount];

			for (var i = 0;
			     i < input.childCount;
			     i++) children[i] = input.GetChild(index: i);

			return children;
		}


		// ------------------------------------------------------------------------------------------------------------------- Collection Filters //

		// By active state


		// This Linq query beat a loop-based approach by 14 ticks to 58 (about 75%)
		public static IEnumerable<Transform> ActiveInHierarchyDirty(this IEnumerable<Transform> input) => input.Where(predicate: c => c.gameObject.activeInHierarchy);


		public static IEnumerable<Transform> InactiveInHierarchy(this IEnumerable<Transform> input) => input.Where(predicate: c => !c.gameObject.activeInHierarchy);


		public static IEnumerable<Transform> ActiveSelves(this IEnumerable<Transform> input) => input.Where(predicate: c => c.gameObject.activeSelf);


		public static IEnumerable<Transform> InactiveSelves(this IEnumerable<Transform> input) => input.Where(predicate: c => !c.gameObject.activeSelf);


		// With common parent


		public static IEnumerable<Transform> WithCommonParent(this IEnumerable<Transform> input,
		                                                      Transform parent) => input.Where(predicate: t => ReferenceEquals(objA: t.parent,
		                                                                                                                       objB: parent));


		// In same scene instance


		public static IEnumerable<Transform> InSameSceneInstance(this IEnumerable<Transform> input,
		                                                         Scene scene) => input.Where(predicate: t => t.gameObject.scene.handle == scene.handle);


		// --------------------------------------------------------------------------------------------------------------------------- Validation //


		public static bool IsLookingAt(this Transform input,
		                               Vector3 target,
		                               float marginOfErrorInDegrees) => Vector3.Dot(lhs: input.forward,
		                                                                            rhs: (target - input.position).normalized) >
		                                                                marginOfErrorInDegrees.DegreesToDotProduct();


		// ------------------------------------------------------------------------------------------------------------------------- Translations //

		#region Translations



		/// <summary>
		/// 	Makes the input face directly away from the target.
		/// </summary>
		public static void LookAway(this Transform input,
		                            Transform target) => input.forward = target.DirectionTo(input);


		/// <summary>
		/// 	Makes the input face directly away from the target.
		/// </summary>
		public static void LookAway(this Transform input,
		                            Vector3 target) => input.forward = target.DirectionTo(input);



		#endregion

		// ---------------------------------------------------------------------------------------------------------------------------- Direction //


		/// <summary>
		///		Returns an un-normalized direction vector from input.position to target.
		/// </summary>
		public static Vector3 DirectionTo(this Transform input,
		                                  Vector3 target) => target - input.position;


		/// <summary>
		///		Returns an un-normalized direction vector from input to target.position.
		/// </summary>
		public static Vector3 DirectionTo(this Transform input,
		                                  Transform target) => target.position - input.position;


		/// <summary>
		///		Returns a normalized direction vector from input.position to target.
		/// </summary>
		public static Vector3 NormalDirectionTo(this Transform input,
		                                        Vector3 target) => (target - input.position).normalized;


		/// <summary>
		///		Returns a normalized direction vector from input.position to target.position.
		/// </summary>
		public static Vector3 NormalDirectionTo(this Transform input,
		                                        Transform target) => (target.position - input.position).normalized;

	}

}