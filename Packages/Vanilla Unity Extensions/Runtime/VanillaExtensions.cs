﻿//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//
//#if UNITY_EDITOR
//    using UnityEditor;
//#endif
//
//using UnityEngine;
//
//using Vanilla.UnityExtensions;
//
////using UnityEngine.EventSystems;
//
//using Vanilla.Core.Math;
//
////using Vanilla.Math;
//
//using Object = UnityEngine.Object;
//
//namespace Vanilla.Core
//{
//
//    public static class VanillaCoreExtensions
//    {
//
//        // -------------------------------------------------------------------------------------------------- Object //
//
////
////        public static bool IsNull(this Object input) => ReferenceEquals(objA: input,
////                                                                        objB: null);
////
////
////        public static bool IsNullLog(this Object input)
////        {
////            if (!ReferenceEquals(objA: input,
////                                 objB: null))
////                return false;
////
////            Debug.Log(message: "I'm null!");
////
////            return true;
////        }
////
////
////        public static bool IsNullWarning(this Object input)
////        {
////            if (!ReferenceEquals(objA: input,
////                                 objB: null))
////                return false;
////
////            Debug.LogWarning(message: "I'm null!");
////
////            return true;
////        }
////
////
////        public static bool IsNullError(this Object input)
////        {
////            if (!ReferenceEquals(objA: input,
////                                 objB: null))
////                return false;
////
////            Debug.LogError(message: "I'm null!");
////
////            return true;
////        }
//
//
//        // ---------------------------------------------------------------------------------------------- GameObject //
//
//        // Accessing Components //
//     
////        public static T GetComponentDynamic<T>(this GameObject   input,
////                                               GetComponentStyle style)
////        {
////            
////        }
//
//
//
////        public static T GetComponentDynamic<T>(this GameObject   input,
////                                               GetComponentStyle style)
////        {
////            if (input == null)
////            {
////                Vanilla.Error(culprit: "Null",
////                              message: "I can't run GetComponentDynamic because I'm null!");
////
////                return default;
////            }
////
////            switch (style)
////            {
////                default: return input.GetComponent<T>();
////
////                case GetComponentStyle.InChildren: return input.GetComponentInChildren<T>();
////
////                case GetComponentStyle.InParent: return input.GetComponentInParent<T>();
////            }
////        }
////
////
////        public static T[] GetComponentsDynamic<T>(this GameObject   input,
////                                                  GetComponentStyle style)
////        {
////            switch (style)
////            {
////                default: return input.GetComponents<T>();
////
////                case GetComponentStyle.InChildren: return input.GetComponentsInChildren<T>();
////
////                case GetComponentStyle.InParent: return input.GetComponentsInParent<T>();
////            }
////        }
//
//
//        // Destruction //
//
//
//        /// <summary>
//        ///     An agnostic call for self-destruction.
//        ///
//        ///     Supposedly, DestroyImmediate should be used instead if calling Destroy outside of the
//        ///     standard expected gameloop (i.e. in the Editor, outside of Playmode).
//        ///
//        ///     This is handled automatically and pre-processed out for builds altogether.
//        /// </summary>
////        public static void Destroy(this GameObject input)
////        {
////            if (input == null) return;
////
////            #if UNITY_EDITOR
////            if (Application.isPlaying) { Object.Destroy(input); }
////            else { Object.DestroyImmediate(input); }
////            #else
////                Object.Destroy(input);
////            #endif
////        }
////
////
////        public static void DestroyOnImmediateChildren<T>(this GameObject input) where T : MonoBehaviour => 
////            input.transform.GetImmediateChildren().DestroyTInTransforms<T>();
////
////
////        public static void DestroyOnAllChildren<T>(this GameObject input) where T : MonoBehaviour => 
////            input.transform.GetAllChildren().DestroyTInTransforms<T>();
////
////
////        public static void DestroyInHierarchy<T>(this GameObject input) where T : MonoBehaviour => 
////            input.transform.root.GetAllChildren().DestroyTInTransforms<T>();
////
////
////        public static void DestroyOnImmediateActiveChildren<T>(this GameObject input) where T : MonoBehaviour => 
////            input.transform.GetImmediateActiveChildren().DestroyTInTransforms<T>();
////
////
////        public static void DestroyOnAllActiveChildren<T>(this GameObject input) where T : MonoBehaviour => 
////            input.transform.GetAllChildrenActiveInHierarchy().DestroyTInTransforms<T>();
////
////
////        public static void DestroyInActiveHierarchy<T>(this GameObject input) where T : MonoBehaviour => 
////            input.transform.root.GetAllChildrenActiveInHierarchy().DestroyTInTransforms<T>();
////
////
////        public static void DestroyOnImmediateInactiveChildren<T>(this GameObject input) where T : MonoBehaviour => 
////            input.transform.GetImmediateInactiveChildren().DestroyTInTransforms<T>();
////
////
////        public static void DestroyOnAllChildrenInactiveInHierarchy<T>(this GameObject input) where T : MonoBehaviour => 
////            input.transform.GetAllChildrenInactiveInHierarchy().DestroyTInTransforms<T>();
////
////
////        public static void DestroyInInactiveHierarchy<T>(this GameObject input) where T : MonoBehaviour => 
////            input.transform.GetAllChildrenInactiveInHierarchy().DestroyTInTransforms<T>();
////
////
////        public static void DestroyTInTransforms<T>(this IEnumerable<Transform> transforms) where T : MonoBehaviour
////        {
////            if (!( transforms.Select(t => t.GetComponent<T>()).Where(c => c != null) is T[] targets )) return;
////            
////            for (var i = targets.Length - 1; i > 0; i--)
////            {
////                Object.Destroy(obj: targets[i]);
////            }
////        }
////
////
////        public static void DestroyImmediateChildrenWithT<T>(this GameObject input)
////            where T : MonoBehaviour
////        {
////            var childCount = input.transform.childCount;
////
////            for (var i = childCount - 1; i >= 0; i--)
////            {
////                if (!input.transform.GetChild(i).GetComponent<T>()) return;
////
////                input.transform.GetChild(i).gameObject.Destroy();
////            }
////        }
////
////
////        public static void DestroyAllChildrenWithT<T>(this GameObject input)
////            where T : MonoBehaviour
////        {
////            var targets = input.GetComponentsInChildren<T>();
////
////            for (var i = targets.Length - 1; i >= 0; i++) { targets[i].gameObject.Destroy(); }
////        }
//
//
//        // ----------------------------------------------------------------------------------------------- Transform //
//
//        #region Logs
//
//
//
//        public static void LogPose(this Transform input)
//        {
//
//            var sb = new StringBuilder(capacity: 128);
//
//            sb.Append(value: $"[{input.name}] Pose:\n\n");
//
//            sb.Append(value: $"Global Position [{input.position}]\n");
//            sb.Append(value: $"Local Position [{input.localPosition}]\n\n");
//
//            sb.Append(value: $"Global Rotation [{input.rotation}]\n");
//            sb.Append(value: $"Local Rotation [{input.localRotation}]\n\n");
//
//            sb.Append(value: $"Global Eulers [{input.eulerAngles}]\n");
//            sb.Append(value: $"Local Eulers [{input.localEulerAngles}]\n\n");
//
//            sb.Append(value: $"Global Scale [{input.lossyScale}]\n");
//            sb.Append(value: $"Local Scale [{input.localScale}]\n\n");
//
//            sb.Append(value: $"Parent [{input.parent}]");
//
//            Debug.Log(message: sb.ToString());
//
//        }
//
//
////        public static void LogLineage(this Transform input)
////        {
////            var sb = new StringBuilder(capacity: 64);
////
////            sb.Append(value: $"Performing transform hierarchy trace:\n\n{input.name}\n");
////
////            var t = input;
////
////            while (t != null)
////            {
////                sb.Append(value: $"{t.name}\n");
////
////                t = t.parent;
////            }
////
//////            Vanilla.Log(culprit: input.name,
//////                        message: sb.ToString());
////        }
//
//
////        public static void LogHierarchyPoses(this Transform input)
////        {
////            var output = $"Performing transform hierarchy pose trace:\n\n";
////            var t      = input;
////
////            while (t != null)
////            {
////                output += "// ------- //\n\n" +
////                          $"Name [{t.name}]\n\nGlobal Position [{t.position}]\nLocal Position [{t.localPosition}]\nGlobal Eulers [{t.eulerAngles}]\nLocal Eulers [{t.localEulerAngles}]\nGlobal Rotation [{t.rotation}]\nLocal Rotation [{t.localRotation}]\nGlobal Scale [{t.lossyScale}]\nLocal Scale [{t.localScale}]\n\n";
////
////                t = t.parent;
////            }
////
////            Vanilla.Log(culprit: input.gameObject.name,
////                        message: output);
////        }
//
//
//
//        #endregion
//
////        /// <summary>
////        ///     Returns a List<Transform> containing every single child object in the hierarchy downwards.
////        /// </summary>
////        public static List<Transform> GetAllChildren
////            (this Transform input) =>
////            input.GetComponentsInChildren<Transform>(true).Where(c => c.transform != input).ToList();
////
////        /// <summary>
////        ///     Returns an array of all child Transforms active according to the hierarchy.
////        /// </summary>
////        public static Transform[] GetAllChildrenActiveInHierarchy
////            (this Transform input) =>
////            input.GetComponentsInChildren<Transform>(true).Where(c => c.transform != input && c.gameObject.activeInHierarchy) as Transform[];       
////        
////        /// <summary>
////        ///     Returns an array of all child Transforms active according to themselves.
////        /// </summary>
////        public static Transform[] GetAllChildrenActiveSelves
////            (this Transform input) =>
////            input.GetComponentsInChildren<Transform>(true).Where(c => c.transform != input && c.gameObject.activeSelf) as Transform[];       
//
//
////        /// <summary>
////        ///     Returns a List<Transform> containing every single transform in the hierarchy that this
////        ///     transform is a part of.
////        /// </summary>
////        public static List<Transform> GetHierarchy(this Transform input)
////        {
////            var children = new List<Transform> {input};
////
////            HierarchyDrill(current: input.root,
////                           children: children);
////
////            return children;
////        }
//
//
//
////        #endregion
//
//        // Hierarchy accessors - active only //
//
////        #region Hierarchy accessors - active
//
////        /// <summary>
////        ///     Returns a List<Transform> containing only the active objects in the hierarchy from this
////        ///     point onward.
////        /// </summary>
////        public static List<Transform> GetAllActiveChildren(this Transform input)
////        {
////            var children = new List<Transform>();
////
////            if (!input.gameObject.activeSelf) return children;
////
////            children.Add(input);
////
////            ActiveHierarchyDrill(current: input,
////                                 children: children);
////
////            return children;
////        }
//
//
//        /// <summary>
//        ///     Returns a List<Transform> containing all active objects in the hierarchy that this
//        ///     transform belongs to.
//        /// </summary>
//
////        public static List<Transform> GetActiveHierarchy(this Transform input)
////        {
////            var children = new List<Transform> {input};
////
////            ActiveHierarchyDrill(current: input.root,
////                                 children: children);
////
////            return children;
////        }
//
//
//
////        #endregion
//
//        // Hierarchy accessors - Inactive only //
//
//        #region Hierarchy accessors - inactive
//
//
//
////        /// <summary>
////        ///     Returns a List<Transform> containing all inactive child objects from this point in the
////        ///     hierarchy onwards.
////        /// </summary>
////        public static List<Transform> GetAllInactiveChildren(this Transform input)
////        {
////            var children = new List<Transform>();
////
////            if (!input.gameObject.activeSelf)
////            {
////                children.Add(input);
////            }
////
////            InactiveHierarchyDrill(current: input,
////                                   children: children);
////
////            return children;
////        }
//
//
////        /// <summary>
////        ///     Returns a List<Transform> containing only the inactive objects in the entire hierarchy
////        ///     of the given transform.
////        /// </summary>
////        public static List<Transform> GetInactiveHierarchy(this Transform input)
////        {
////            var children = new List<Transform> {input};
////
////            InactiveHierarchyDrill(current: input.root,
////                                   children: children);
////
////            return children;
////        }
//
//
//
//        #endregion
//
//        // Hierarchy Drills - Loops that add children to a given list //
//
//        #region Hierarchy drills
//
//
//
////        /// <summary>
////        ///     This function will add all successive children of a given transform to a list.
////        /// </summary>
////        /// <param name="current">The target transform to start from. It will not be included in the list.</param>
////        /// <param name="children">The list to add children to.</param>
////        private static void HierarchyDrill
////        (
////            Transform              current,
////            ICollection<Transform> children)
////        {
////            for (var i = 0; i < current.childCount; i++)
////            {
////                children.Add(current.GetChild(i));
////
////                HierarchyDrill(current: current.GetChild(i),
////                               children: children);
////            }
////        }
//
//
////        /// <summary>
////        ///     This function will add all successive active children of a given transform to a list.
////        /// </summary>
////        /// <param name="current">The target transform to start from. It will not be included in the list.</param>
////        /// <param name="children">The list to add children to.</param>
////        private static void ActiveHierarchyDrill
////        (
////            Transform              current,
////            ICollection<Transform> children)
////        {
////            for (var i = 0; i < current.childCount; i++)
////            {
////                if (!current.GetChild(i).gameObject.activeSelf) continue;
////
////                children.Add(current.GetChild(i));
////
////                ActiveHierarchyDrill(current: current.GetChild(i),
////                                     children: children);
////            }
////        }
//
////
////        /// <summary>
////        ///     This function will add all successive inactive children of a given transform to a list.
////        /// </summary>
////        /// <param name="current">The target transform to start from. It will not be included in the list.</param>
////        /// <param name="children">The list to add children to.</param>
////        private static void InactiveHierarchyDrill
////        (
////            Transform              current,
////            ICollection<Transform> children)
////        {
////            for (var i = 0; i < current.childCount; i++)
////            {
////                if (!current.GetChild(i).gameObject.activeSelf)
////                {
////                    children.Add(current.GetChild(i));
////                }
////
////                ActiveHierarchyDrill(current: current.GetChild(i),
////                                     children: children);
////            }
////        }
//
//
//
//        #endregion
//
//        // Lerps //
//
////        #region Lerps
////
////
////
////        public static void LerpPosition
////            (this Transform t,
////            Vector3        from,
////            Vector3        to,
////            float          factor) => t.position = Vector3.Lerp(a: from,
////                                                                b: to,
////                                                                t: factor);
////
////
////        public static void LerpLocalPosition
////            (this Transform t,
////             Vector3        from,
////             Vector3        to,
////             float          factor) => t.localPosition = Vector3.Lerp(a: from,
////                                                                     b: to,
////                                                                     t: factor);
////
////
////        public static void LerpRotation
////            (this Transform t,
////             Quaternion     from,
////             Quaternion     to,
////             float          factor) => t.rotation = Quaternion.Lerp(a: from,
////                                                                   b: to,
////                                                                   t: factor);
////
////
////        public static void LerpLocalRotation
////            (this Transform t,
////             Quaternion     from,
////             Quaternion     to,
////             float          factor) => t.localRotation = Quaternion.Lerp(a: from,
////                                                                        b: to,
////                                                                        t: factor);
////
////
////        public static void LerpLocalScale
////            (this Transform t,
////             Vector3        from,
////             Vector3        to,
////             float          factor) => t.localScale = Vector3.Lerp(a: from,
////                                                                  b: to,
////                                                                  t: factor);
////
////
////
////        #endregion
//
//        // Dirty Lerps //
//
//
//        // Look At - X Only - Global //
//
//        #region Look Ats - X Only
//
//
//
//        public static void LookAtXOnly(this Transform input,
//                                       Transform      target)
//        {
//            var dir = target.position - input.position;
//
//            dir.x = 0.0f;
//
//            input.rotation = Quaternion.LookRotation(forward: dir);
//        }
//
//
//        public static void LookAtXOnlyFlipped(this Transform input,
//                                              Transform      target)
//        {
//            var dir = input.position - target.position;
//
//            dir.x = 0.0f;
//
//            input.rotation = Quaternion.LookRotation(forward: dir);
//        }
//
//
//        public static void LookAtXOnly(this Transform input,
//                                       Vector3        target)
//        {
//            var dir = target - input.position;
//
//            dir.x = 0.0f;
//
//            input.rotation = Quaternion.LookRotation(forward: dir);
//        }
//
//
//        public static void LookAtXOnlyFlipped(this Transform input,
//                                              Vector3        target)
//        {
//            var dir = input.position - target;
//
//            dir.x = 0.0f;
//
//            input.rotation = Quaternion.LookRotation(forward: dir);
//        }
//
//
//        // Look At - X Only - Local //
//
//
//        public static void LookAtXOnlyLocal(this Transform input,
//                                            Transform      target)
//        {
//            var dir = target.localPosition - input.localPosition;
//
//            dir.x = 0.0f;
//
//            input.localRotation = Quaternion.LookRotation(forward: dir);
//        }
//
//
//        public static void LookAtXOnlyLocalFlipped(this Transform input,
//                                                   Transform      target)
//        {
//            var dir = input.localPosition - target.localPosition;
//
//            dir.x = 0.0f;
//
//            input.localRotation = Quaternion.LookRotation(forward: dir);
//        }
//
//
//        public static void LookAtXOnlyLocal(this Transform input,
//                                            Vector3        target)
//        {
//            var dir = target - input.localPosition;
//
//            dir.x = 0.0f;
//
//            input.localRotation = Quaternion.LookRotation(forward: dir);
//        }
//
//
//        public static void LookAtXOnlyLocalFlipped(this Transform input,
//                                                   Vector3        target)
//        {
//            var dir = input.localPosition - target;
//
//            dir.x = 0.0f;
//
//            input.localRotation = Quaternion.LookRotation(forward: dir);
//        }
//
//
//        // Look At - Y Only - Global //
//
//
//        public static void LookAtYOnly(this Transform input,
//                                       Transform      target)
//        {
//            var dir = target.position - input.position;
//
//            dir.y = 0.0f;
//
//            input.rotation = Quaternion.LookRotation(forward: dir);
//        }
//
//
//        public static void LookAtYOnlyFlipped(this Transform input,
//                                              Transform      target)
//        {
//            var dir = input.position - target.position;
//
//            dir.y = 0.0f;
//
//            input.rotation = Quaternion.LookRotation(forward: dir);
//        }
//
//
//        public static void LookAtYOnly(this Transform input,
//                                       Vector3        target)
//        {
//            var dir = target - input.position;
//
//            dir.y = 0.0f;
//
//            input.rotation = Quaternion.LookRotation(forward: dir);
//        }
//
//
//        public static void LookAtYOnlyFlipped(this Transform input,
//                                              Vector3        target)
//        {
//            var dir = input.position - target;
//
//            dir.y = 0.0f;
//
//            input.rotation = Quaternion.LookRotation(forward: dir);
//        }
//
//
//        // Look At - Y Only - Local //
//
//
//        public static void LookAtYOnlyLocal(this Transform input,
//                                            Transform      target)
//        {
//            var dir = target.localPosition - input.localPosition;
//
//            dir.y = 0.0f;
//
//            input.localRotation = Quaternion.LookRotation(forward: dir);
//        }
//
//
//        public static void LookAtYOnlyLocalFlipped(this Transform input,
//                                                   Transform      target)
//        {
//            var dir = input.localPosition - target.localPosition;
//
//            dir.y = 0.0f;
//
//            input.localRotation = Quaternion.LookRotation(forward: dir);
//        }
//
//
//        public static void LookAtYOnlyLocal(this Transform input,
//                                            Vector3        target)
//        {
//            var dir = target - input.localPosition;
//
//            dir.y = 0.0f;
//
//            input.localRotation = Quaternion.LookRotation(forward: dir);
//        }
//
//
//        public static void LookAtYOnlyLocalFlipped(this Transform input,
//                                                   Vector3        target)
//        {
//            var dir = input.localPosition - target;
//
//            dir.y = 0.0f;
//
//            input.localRotation = Quaternion.LookRotation(forward: dir);
//        }
//
//
//        // Look Rotation - Y Only //
//
//
//        /// <summary>
//        ///     Returns a new Quaternion expressing a rotation that faces target only on the Y axis.
//        /// </summary>
//        public static Quaternion LookRotationYOnly(this Transform input,
//                                                   Transform      target)
//        {
//            var dir = target.position - input.position;
//
//            dir.y = 0.0f;
//
//            return Quaternion.LookRotation(forward: dir);
//        }
//
//
//        // Look Rotation - X Only //
//
//
//        /// <summary>
//        ///     Returns a new Quaternion expressing a rotation that faces target only on the Y axis.
//        /// </summary>
//        public static Quaternion LookRotationXOnly(this Transform input,
//                                                   Transform      target)
//        {
//            var dir = target.position - input.position;
//
//            dir.x = 0.0f;
//
//            return Quaternion.LookRotation(forward: dir);
//        }
//
//
//
//        #endregion
//
//        // Dirty Look At //
//
//        #region Dirty Look-Ats
//
//
//
//        public static void DirtyLookAt(this Transform input,
//                                       Transform      target,
//                                       float          factor)
//        {
//            input.rotation = Quaternion.Lerp(a: input.rotation,
//                                             b: Quaternion.LookRotation(forward: target.position - input.position),
//                                             t: factor);
//        }
//
//
//        public static void DirtyLookAtYOnly(this Transform input,
//                                            Transform      target,
//                                            float          factor)
//        {
//            var dir = target.position - input.position;
//
//            dir.y = 0.0f;
//
//            input.rotation = Quaternion.Lerp(a: input.rotation,
//                                             b: Quaternion.LookRotation(forward: dir),
//                                             t: factor);
//        }
//
//
//        public static void DirtyLookAtXOnly(this Transform input,
//                                            Transform      target,
//                                            float          factor)
//        {
//            var dir = target.position - input.position;
//
//            dir.x = 0.0f;
//
//            input.rotation = Quaternion.Lerp(a: input.rotation,
//                                             b: Quaternion.LookRotation(forward: dir),
//                                             t: factor);
//        }
//
//
//
//        #endregion
//
//        // Distance Checks //
//
//        #region Distance Checks
//
//
//
//        public static float GetSqrMagTo(this Transform input,
//                                        Transform      target)
//        {
//            return input.GetSqrMagTo(target: target.position);
//        }
//
//
//        public static float GetSqrMagTo(this Transform input,
//                                        Vector3        target)
//        {
//            return ( input.position - target ).sqrMagnitude;
//        }
//
//
//        public static bool IsCloseTo(this Transform input,
//                                     Transform      target,
//                                     float          distance)
//        {
//            return input.GetSqrMagTo(target: target) < distance * distance;
//        }
//
//
//        public static bool IsCloseTo(this Transform input,
//                                     Vector3        target,
//                                     float          distance)
//        {
//            return input.GetSqrMagTo(target: target) < distance * distance;
//        }
//
//
//
//        #endregion
//
//        // Angle Checks //
//
//        #region Compare Directions
//
//
//
//        /// <summary>
//        ///     This function will compare the angular difference between two transforms on the global y axis and return the signed/absolute (positive-only) difference in degrees. 
//        /// </summary>
//        public static float SignedAngularDistanceBetweenYEulers(this Transform input,
//                                                                Transform      target)
//        {
//            return Mathf.Abs(f: Mathf.DeltaAngle(current: input.eulerAngles.y,
//                                                 target: target.eulerAngles.y));
//        }
//
//
//        /// <summary>
//        ///     This function will compare the angular difference between two transforms on the global y axis and return the unsigned (positive or negative) difference in degrees. 
//        /// </summary>
//        public static float UnsignedAngularDistanceBetweenYEulers(this Transform input,
//                                                                  Transform      target)
//        {
//            return Mathf.DeltaAngle(current: input.eulerAngles.y,
//                                    target: target.eulerAngles.y);
//        }
//
//
//
//        #endregion
//
//        // -------------------------------------------------------------------------------------------------- String //
//
//        #region Strings
//
//
//
//        public static string ExtractFileNameFromPath(this string input)
//        {
//            var lastSlashIndex = input.LastIndexOf(value: "/",
//                                                   comparisonType: StringComparison.Ordinal);
//
//            var extensionStartIndex = input.LastIndexOf(value: ".",
//                                                        comparisonType: StringComparison.Ordinal);
//
//            return input.Substring(startIndex: lastSlashIndex,
//                                   length: input.Length   -
//                                           lastSlashIndex -
//                                           ( input.Length - extensionStartIndex ));
//        }
//
//
//        public static bool StartsWith(this string a,
//                                      string      b)
//        {
//            var aLen = a.Length;
//            var bLen = b.Length;
//
//            var ap = 0;
//            var bp = 0;
//
//            while (ap < aLen && bp < bLen && a[index: ap] == b[index: bp])
//            {
//                ap++;
//                bp++;
//            }
//
//            return bp == bLen;
//        }
//
//
//        public static bool EndsWith(this string a,
//                                    string      b)
//        {
//            var ap = a.Length - 1;
//            var bp = b.Length - 1;
//
//            while (ap >= 0 && bp >= 0 && a[index: ap] == b[index: bp])
//            {
//                ap--;
//                bp--;
//            }
//
//            return bp < 0;
//        }
//
//
////        public static JSONObject ToJSONObject(this string input)
////        {
////            return (JSONObject) JSONNode.Parse(input);
////        }
////
////
////        public static JSONNode ToJSONNode(this string input)
////        {
////            return JSONNode.Parse(input);
////        }
//
//
//        public static string XOREncrypt(this string input,
//                                        int         key) => new string(value: input.Select(selector: c => (char) ( c ^ key )).ToArray());
//
//
//
//        #endregion
//
//        // -------------------------------------------------------------------------------------------------- Camera //
//
//        #region Camera
//
//
//
//        // Layers //
//
//
//        public static void ShowLayer(this Camera camera,
//                                     string      layerName) => camera.cullingMask |= 1 << LayerMask.NameToLayer(layerName: layerName);
//
//
//        public static void HideLayer(this Camera camera,
//                                     string      layerName) => camera.cullingMask &= ~( 1 << LayerMask.NameToLayer(layerName: layerName) );
//
//
//        public static void ToggleLayer(this Camera camera,
//                                       string      layerName) => camera.cullingMask ^= 1 << LayerMask.NameToLayer(layerName: layerName);
//
//
//        /// Determines if the Bounds intersect with a camera's frustrum
//        public static bool CanSee(this Camera input,
//                                  Bounds      target) => GeometryUtility.TestPlanesAABB(planes: GeometryUtility.CalculateFrustumPlanes(camera: input),
//                                                                                        bounds: target);
//
//
//        public static bool CanSee(this Camera input,
//                                  Transform   target)
//        {
//            var p = input.WorldToViewportPoint(position: target.position);
//
//            return p.x >= 0 && p.x <= 1.0f && p.y >= 0 && p.y <= 1.0f && p.z > 0;
//        }
//
//
//        public static bool CanSee(this Camera input,
//                                  Vector3     position)
//        {
//            var p = input.WorldToViewportPoint(position: position);
//
//            return p.x.IsNormalized() && p.y.IsNormalized() && p.z > 0;
//        }
//
//
//
//        #endregion
//
//        // --------------------------------------------------------------------------------------------------- Touch //
//
////        #region Touch
//
//
////
////        public static bool IsOverUIElement(ref this Touch input) => EventSystem.current.IsPointerOverGameObject(pointerId: input.fingerId);
////
//
//
////        #endregion
//
//        // --------------------------------------------------------------------------------------------------- Lists //
//
//        #region Lists
//
//
//
//        /// <summary>
//        ///     This method will run a for loop that starts at the end of the List iterating backwards
//        ///     checking each entry with Vanilla.SilentNullCheck(). If a null entry is detected, RemoveAt
//        ///     is used to take the entry out, shortening the List. Be aware that this will change the
//        ///     current indices of other entries!
//        /// </summary>
////        public static void RemoveNullEntries<T>(this List<T> input)
////        {
////            for (var i = input.Count - 1; i >= 0; i--)
////            {
////                if (!Vanilla.SilentNullCheck(thing: input[index: i])) continue;
////
////                input.RemoveAt(index: i);
////
////                i--;
////
////            }
////        }
//
//
//        public static int GetNearestTransform(this List<Transform> input,
//                                              Vector3              target)
//        {
//            var nearest     = -1;
//            var nearestDist = float.MaxValue;
//
//            for (var i = 0; i < input.Count; i++)
//            {
//                var thisDist = input[index: i].GetSqrMagTo(target: target);
//
//                if (!( thisDist < nearestDist )) continue;
//
//                nearestDist = thisDist;
//                nearest     = i;
//            }
//
//            return nearest;
//        }
//
//
//        public static void CopyTo<T>(this List<T> input,
//                                     List<T>      target)
//        {
//            target.Clear();
//
//            target.AddRange(collection: input);
//        }
//
//
//        /// <summary>
//        ///     If the lists length exceeds maxLength, this function removes all items in the
//        ///     list at maxLength and beyond.
//        /// </summary>
//        /// 
//        /// <param name="maxLength">
//        ///     The maximum allowed length of the list.
//        /// </param>
//        public static void Trim<T>(this List<T> input,
//                                   int          maxLength)
//        {
//            if (input.Count <= maxLength) return;
//
//            input.RemoveRange(index: maxLength,
//                              count: input.Count - maxLength);
//        }
//
//
////        /// <summary>
////        ///     This function is similar to the standard List<T>.Add() except that it ensures that the new item
////        ///     will only be added once the target list has removed enough elements (always at index 0) to remain
////        ///     under its capacity by one (effectively ensuring that it is never resized).
////        /// 
////        ///     This function assumes that .Capacity has already been set to a desired maximum.
////        /// </summary>
////        /// 
////        /// <param name="input"></param>
////        /// 
////        /// <param name="newItem">
////        ///     The item to add to the list.
////        /// </param>
////        public static void Cycle<T>
////        (
////            this List<T> input,
////            T            newItem)
////        {
//////            Vanilla.Log("Debugging Cycle()");
////
////            while (!input.CanFitAnother())
////            {
//////                Vanilla.Log("Couldn't fit another! Removing element 0...");
////
////                input.RemoveAt(0);
////            }
////
//////            Vanilla.Log("Adding newItem");
////
////            input.Add(newItem);
////
//////            Vanilla.Log($"input.Count is now [{input.Count}]");
////
//////            while (input.Count > maxLength)
//////            {
//////                input.RemoveAt(0);
//////            }
////        }
//
//
////        public static bool CanFitAnother<T>(this List<T> input)
////        {
//////            Vanilla.Log("Debugging CanFitAnother()");
////
//////            Vanilla.Log($"input.Count [{input.Count}] < input.Capacity [{input.Capacity}] ? [{input.Count < input.Capacity}]");
////
////            return input.Count < input.Capacity;
////        }
//
//
//        public static bool AtOrOverCapacity<T>(this List<T> input)
//        {
//            return input.Count >= input.Capacity;
//        }
//
//
//        // Use a Stack<T> like everyone else!
//
////        /// <summary>
////        ///     This function inserts newItem to the start of the list.
////        /// </summary>
////        /// 
////        /// <param name="newItem">
////        ///     The item to add to the list.
////        /// </param>
////        public static void Push<T>(this List<T> input, T newItem)
////        {
////            input.Insert(0, newItem);
////        }
////        
////        /// <summary>
////        ///     This function inserts newItem to the start of the list and trims the end if it exceeds maxLength.
////        /// </summary>
////        /// 
////        /// <param name="newItem">
////        ///     The item to add to the list.
////        /// </param>
////        public static void Push<T>(this List<T> input, T newItem, int maxLength)
////        {
////            input.Insert(0, newItem);
////
////            input.Trim(maxLength);
////        }
////
////        /// <summary>
////        ///     This functions gets the first item from the list, removes it from the list and returns it as an out.
////        /// </summary>
////        /// 
////        /// <param name="returnedItem">
////        ///     The item returned from the start of the list.
////        /// </param>
////        public static void Pop<T>(this List<T> input, out T returnedItem)
////        {
////            returnedItem = input[0];
////            
////            input.RemoveAt(0);
////        }
////        
////        public static T Pop<T>(this List<T> input)
////        {
////            T returnedItem = input[0];
////            
////            input.RemoveAt(0);
////
////            return returnedItem;
////        }
//
//
//
//        #endregion
//
//        // ---------------------------------------------------------------------------------------------- Networking //
//
//
////        public static void CopyFrom<T>
////        (
////            this T[] input,
////            int      from,
////            int      to,
////            T[]      target)
////        {
////            if (!input.Length.IsWithinInclusiveRange(min: from,
////                                                     max: to) ||
////                !target.Length.IsWithinInclusiveRange(min: from,
////                                                      max: to))
////                return;
////
////            for (var i = from; i < to; i++)
////            {
////                input[i] = target[i];
////            }
////        }
////
////
////        public static void CopyTo<T>
////        (
////            this T[] input,
////            int      from,
////            int      to,
////            T[]      target)
////        {
////            if (!input.Length.IsWithinInclusiveRange(min: from,
////                                                     max: to) ||
////                !target.Length.IsWithinInclusiveRange(min: from,
////                                                      max: to))
////                return;
////
////            for (var i = from; i < to; i++)
////            {
////                target[i] = input[i];
////            }
////        }
//
//
//        public static void SetAll<T>(this T[] input,
//                                     T        value)
//            where T : struct
//        {
//            for (var i = 0; i < input.Length; i++) { input[i] = value; }
//        }
//
//
//        public static void SetRange<T>(this T[] input,
//                                       int      from,
//                                       int      to,
//                                       T        value)
//            where T : struct
//        {
//            for (var i = from; i < to; i++) { input[i] = value; }
//        }
//
//
//        public static void SetAmount<T>(this T[] input,
//                                        int      from,
//                                        int      amount,
//                                        T        value)
//            where T : struct
//        {
//            for (var i = from; i < from + amount; i++) { input[i] = value; }
//        }
//
//
//        // ---------------------------------------------------------------------------------------------------- Byte //
//
//
//        /// <summary>
//        ///     Written by Sana at PHORIA
//        /// 
//        ///     This decompresses a byte into a Color32.
//        ///     This color range is limited to 256 colors.
//        /// </summary>
//        private static Color32 ToColor32(this byte input) => new Color32(r: (byte) ( input          & 0xE0 ),
//                                                                         g: (byte) ( ( input << 3 ) & 0xE0 ),
//                                                                         b: (byte) ( input << 6 ),
//                                                                         a: 255);
//
//
//        // --------------------------------------------------------------------------------------------------- UInt //
//
//
//        /// <summary>
//        ///     This decompresses a uint into a Color32.
//        /// 
//        ///     This provides a color range of over 4 billion colours, i.e. HDR.
//        /// </summary>
//        public static Color32 ToColor32(this uint input) => new Color32(r: (byte) ( input >> 0 ),
//                                                                        g: (byte) ( input >> 8 ),
//                                                                        b: (byte) ( input >> 16 ),
//                                                                        a: (byte) ( input >> 24 ));
//
//
//        // --------------------------------------------------------------------------------------------------- Color //
//
//
//        /// <summary>
//        ///     This compresses a Color32 into a single uint (4 bytes).
//        /// 
//        ///     This provides a color range of over 4 billion colours, i.e. HDR.
//        /// </summary>
//        public static uint ToUInt(this Color32 input) => (uint) ( ( input.a << 0 )  |
//                                                                  ( input.r << 8 )  |
//                                                                  ( input.g << 16 ) |
//                                                                  ( input.b << 24 ) );
//
//
//        //            return (uint) ((input.a << 24) | (input.r << 16) | (input.g << 8) | (input.b << 0));
//        //            return (uint)(((input.a << 24) | (input.r << 16) | (input.g << 8)  | input.b) & 0xffffffffL);
//        //            return (uint)((input.a << 24) | (input.r << 16) | (input.g << 8)  | input.b);
//
//
//        /// <summary>
//        ///     Written by Sana at PHORIA
//        ///
//        ///     This compresses a Color32 into a single byte.
//        /// 
//        ///     This color range is limited to 256 colors.
//        /// </summary>
//        public static byte ToByte(this Color32 input) => (byte) ( ( input.r & 0xE0 )          |
//                                                                  ( ( input.g & 0xE0 ) >> 3 ) |
//                                                                  ( input.b            >> 6 ) );
//
//
//        // ------------------------------------------------------------------------------------------- RenderTexture //
//
//
//        public static Texture2D ToTexture2D(this RenderTexture input,
//                                            int                xResolution,
//                                            int                yResolution)
//        {
//            var output = new Texture2D(width: xResolution,
//                                       height: yResolution,
//                                       textureFormat: TextureFormat.RGB24,
//                                       mipChain: false);
//
//            RenderTexture.active = input;
//
//            output.ReadPixels(source: new Rect(x: 0,
//                                               y: 0,
//                                               width: input.width,
//                                               height: input.height),
//                              destX: 0,
//                              destY: 0);
//
//            output.Apply();
//
//            return output;
//        }
//
//
//        // ---------------------------------------------------------------------------------------------- Networking //
//
//        // Move these to VNet!
//
//
//        public static void CopyToByteArray(this Vector3 input,
//                                           byte[]       target,
//                                           int          startIndex,
//                                           float        scalar)
//        {
//            input.Scale(scale: Vector3.one * scalar);
//
//            BitConverter.GetBytes(value: (short) input.x)
//                        .CopyTo(array: target,
//                                index: startIndex);
//
//            BitConverter.GetBytes(value: (short) input.y)
//                        .CopyTo(array: target,
//                                index: startIndex + 2);
//
//            BitConverter.GetBytes(value: (short) input.z)
//                        .CopyTo(array: target,
//                                index: startIndex + 4);
//        }
//
//
//        public static Vector3 ToVector3(this byte[] input,
//                                        int         startIndex,
//                                        float       scalar) => new Vector3(x: BitConverter.ToInt16(value: input,
//                                                                                                   startIndex: startIndex) /
//                                                                              scalar,
//                                                                           y: BitConverter.ToInt16(value: input,
//                                                                                                   startIndex: startIndex + 2) /
//                                                                              scalar,
//                                                                           z: BitConverter.ToInt16(value: input,
//                                                                                                   startIndex: startIndex + 4) /
//                                                                              scalar);
//
//
//        public static Quaternion ToQuaternion(this byte[] input,
//                                              int         startIndex,
//                                              float       scalar) => Quaternion.Euler(x: BitConverter.ToInt16(value: input,
//                                                                                                              startIndex: startIndex) /
//                                                                                         scalar,
//                                                                                      y: BitConverter.ToInt16(value: input,
//                                                                                                              startIndex: startIndex + 2) /
//                                                                                         scalar,
//                                                                                      z: BitConverter.ToInt16(value: input,
//                                                                                                              startIndex: startIndex + 4) /
//                                                                                         scalar);
//
//
//        // -------------------------------------------------------------------------------------------------- Object //
//
//
////        public static int GetBuildIndexFromSceneAsset
////        (
////            this Object input)
////        {
////            if (input == null)
////            {
////                return -1;
////            }
////
////            #if UNITY_EDITOR
////            for (var i = 0; i < EditorBuildSettings.scenes.Length; i++)
////            {
////                if (string.Equals(a: input.name,
////                                  b: EditorBuildSettings.scenes[i].path.ExtractFileNameFromPath(),
////                                  comparisonType: StringComparison.Ordinal))
////                {
////                    return i;
////                }
////            }
////            #endif
////
////            Debug.LogWarning($"This asset file [{input.name}] could not be found in the build settings scene index. Ensure the scene is present first!");
////
////            return -1;
////
////        }
//
//
//        // ---------------------------------------------------------------------------------------------- SimpleJSON //
//
//        // Inserting To JSONObject //
//
//
////        public static void AddVector2
////        (
////            this JSONObject input,
////            Vector2         vector,
////            string          name)
////        {
////            var o = new JSONObject();
////
////            o.Add(aKey: "x",
////                  aItem: vector.x.ToString("0.0000"));
////
////            o.Add(aKey: "y",
////                  aItem: vector.y.ToString("0.0000"));
////
////            input.Add(aKey: name,
////                      aItem: o);
////        }
//
//
////        public static void AddVector3
////        (
////            this JSONObject input,
////            Vector3         vector,
////            string          name)
////        {
////            var o = new JSONObject();
////
////            o.Add(aKey: "x",
////                  aItem: vector.x.ToString("0.0000"));
////
////            o.Add(aKey: "y",
////                  aItem: vector.y.ToString("0.0000"));
////
////            o.Add(aKey: "z",
////                  aItem: vector.z.ToString("0.0000"));
////
////            input.Add(aKey: name,
////                      aItem: o);
////        }
//
//
////        public static void AddTransform
////        (
////            this JSONObject input,
////            Transform       t,
////            string          name)
////        {
////            var o = new JSONObject();
////
////            o.AddVector3(vector: t.localPosition,
////                         name: "position");
////
////            o.AddVector3(vector: t.localEulerAngles,
////                         name: "rotation");
////
////            o.AddVector3(vector: t.localScale,
////                         name: "scale");
////
////            input.Add(aKey: name,
////                      aItem: o);
////        }
//
//
//        // Extracting from JSONObject //
//
////
////        public static Vector2 AsVector2(this JSONNode input)
////        {
////            return new Vector2
////                   {
////                       x = input["x"],
////                       y = input["y"]
////                   };
////        }
////
////
////        public static Vector2 AsVector3(this JSONNode input)
////        {
////            return new Vector3
////                   {
////                       x = input["x"],
////                       y = input["y"],
////                       z = input["z"]
////                   };
////        }
////
////
////        public static void SetFromJSONObject
////        (
////            this Transform input,
////            JSONObject     o)
////        {
////            input.localPosition    = o["position"].AsVector3();
////            input.localEulerAngles = o["rotation"].AsVector3();
////            input.localScale       = o["scale"].AsVector3();
////        }
//
//    }
//
//}