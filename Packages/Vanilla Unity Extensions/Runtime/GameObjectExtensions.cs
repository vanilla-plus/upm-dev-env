using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace Vanilla.UnityExtensions
{

    public static class GameObjectExtensions
    {

        // ----------------------------------------------------------------------------------- Changing Active State //

        public static void Activate(this GameObject input) =>
            input.SetActive(true);

        public static void Deactivate(this GameObject input) =>
            input.SetActive(false);

        public static void ToggleActive(this GameObject input) =>
            input.SetActive(!input.activeSelf);

        
        // ---------------------------------------------------------------------------------------- Self-Destruction //

        public static void Destroy(this GameObject input)
        {
            #if UNITY_EDITOR
                if (Application.isPlaying) 
                    Object.Destroy(obj: input);
                else 
                    Object.DestroyImmediate(obj: input);
            #else
                Object.Destroy(input);
            #endif
        }
        
        // --------------------------------------------------------------------------- GetComponents From Collection //


        public static IEnumerable<T> GetComponentsFromCollection<T>(this IEnumerable<GameObject> input)
            where T : Component =>
            input.Select(selector: g => g.GetComponent<T>()).Where(predicate: c => c != null);


        // ------------------------------------------------------------------------------------ GetComponent Dynamic //


        public static T GetComponentDynamic<T>(this GameObject   input,
                                               GetComponentStyle style,
                                               bool              includeInactive = false) =>
            style switch
            {
                GetComponentStyle.InChildren => input.GetComponentInChildren<T>(includeInactive: includeInactive),
                GetComponentStyle.InParent   => input.GetComponentInParent<T>(),
                _                            => input.GetComponent<T>()
            };


        public static T[] GetComponentsDynamic<T>(this GameObject   input,
                                                  GetComponentStyle style,
                                                  bool              includeInactive = false) =>
            style switch
            {
                GetComponentStyle.InChildren => input.GetComponentsInChildren<T>(includeInactive: includeInactive),
                GetComponentStyle.InParent   => input.GetComponentsInParent<T>(includeInactive: includeInactive),
                _                            => input.GetComponents<T>()
            };


        // ------------------------------------------------------------------------------------ GetComponent Queries //

        


//        public static void GetOnImmediateChildren<T>(this GameObject input)
//            where T : Component => input.transform.GetImmediateChildren().


//
//        public static void GetOnAllChildren<T>(this GameObject input)
//            where T : Component =>
//            input.transform.GetAllChildren().DestroyTInTransforms<T>();
//
//
//        public static void GetInHierarchy<T>(this GameObject input)
//            where T : Component =>
//            input.transform.root.GetAllChildren().DestroyTInTransforms<T>();
//
//
//        public static IEnumerable<T> GetComponentsOnImmediateChildrenActiveInHierarchy<T>(this GameObject input)
//            where T : Component =>
//
//            input.transform.GetImmediateChildren().ActiveInHierarchy().GetComponentsFromCollection<T>();
////            input.GetComponentsInChildren<T>().Where(c => c.transform.parent == input.transform && c.gameObject.activeInHierarchy);
//
//        public static void GetOnAllActiveChildren<T>(this GameObject input)
//            where T : Component =>
//            input.transform.GetAllChildrenActiveInHierarchy().DestroyTInTransforms<T>();
//
//
//        public static void GetInActiveHierarchy<T>(this GameObject input)
//            where T : Component =>
//            input.transform.root.GetAllChildrenActiveInHierarchy().DestroyTInTransforms<T>();
//
//
//        public static void GetOnImmediateInactiveChildren<T>(this GameObject input)
//            where T : Component =>
//            input.transform.GetImmediateInactiveChildren().DestroyTInTransforms<T>();
//
//
//        public static void GetOnAllChildrenInactiveInHierarchy<T>(this GameObject input)
//            where T : Component =>
//            input.transform.GetAllChildrenInactiveInHierarchy().DestroyTInTransforms<T>();
//
//
//        public static void GetInactiveInHierarchy<T>(this GameObject input)
//            where T : Component =>
//            input.transform.GetAllChildrenInactiveInHierarchy().DestroyTInTransforms<T>();

        // -------------------------------------------------------------------------------------- Collection Filters //
        
        // By active state
        
        public static IEnumerable<GameObject> ActiveInHierarchy(this IEnumerable<GameObject> input) => input.Where(predicate: g => g.activeInHierarchy);

        public static IEnumerable<GameObject> InactiveInHierarchy(this IEnumerable<GameObject> input) => input.Where(predicate: g => !g.activeInHierarchy);
		
        public static IEnumerable<GameObject> ActiveSelves(this IEnumerable<GameObject> input) => input.Where(predicate: g => g.activeSelf);

        public static IEnumerable<GameObject> InactiveSelves(this IEnumerable<GameObject> input) => input.Where(predicate: g => !g.activeSelf);

        // By common parent
        
        public static IEnumerable<GameObject> WithCommonParent(this IEnumerable<GameObject> input, Transform parent) => input.Where(predicate: g => ReferenceEquals(objA: g.transform.parent,
                                                                                                                                                                    objB: parent));

        // -------------------------------------------------------------------------------------- Collection Actions //


        public static void Destroy(this IEnumerable<GameObject> input)
        {
            #if UNITY_EDITOR
            if (Application.isPlaying)
                foreach (var g in input) Object.Destroy(obj: g);
            else
                foreach (var g in input) Object.DestroyImmediate(obj: g);
            #else
                foreach (var g in input) Object.Destroy(g);
            #endif
        }


        // -------------------------------------------------------------------------------------- Destroy Components //

        #region Destroy Components via query






//        public static void DestroyOnImmediateChildren<T>(this GameObject input)
//            where T : Component =>
//            input.transform.GetImmediateChildren().DestroyTInTransforms<T>();
//
//
//        public static void DestroyOnAllChildren<T>(this GameObject input)
//            where T : Component =>
//            input.transform.GetAllChildren().DestroyTInTransforms<T>();
//
//
//        public static void DestroyInHierarchy<T>(this GameObject input)
//            where T : Component =>
//            input.transform.root.GetAllChildren().DestroyTInTransforms<T>();
//
//
//        public static void DestroyOnImmediateActiveChildren<T>(this GameObject input)
//            where T : Component =>
//            input.transform.GetImmediateActiveChildren().DestroyTInTransforms<T>();
//
//
//        public static void DestroyOnAllActiveChildren<T>(this GameObject input)
//            where T : Component =>
//            input.transform.GetAllChildrenActiveInHierarchy().DestroyTInTransforms<T>();
//
//
//        public static void DestroyInActiveHierarchy<T>(this GameObject input)
//            where T : Component =>
//            input.transform.root.GetAllChildrenActiveInHierarchy().DestroyTInTransforms<T>();
//
//
//        public static void DestroyOnImmediateInactiveChildren<T>(this GameObject input)
//            where T : Component =>
//            input.transform.GetImmediateInactiveChildren().DestroyTInTransforms<T>();
//
//
//        public static void DestroyOnAllChildrenInactiveInHierarchy<T>(this GameObject input)
//            where T : Component =>
//            input.transform.GetAllChildrenInactiveInHierarchy().DestroyTInTransforms<T>();
//
//
//        public static void DestroyInactiveInHierarchy<T>(this GameObject input)
//            where T : Component =>
//            input.transform.GetAllChildrenInactiveInHierarchy().DestroyTInTransforms<T>();


        /// <summary>
        ///     Destroys all valid instances of T found on the given transform collection.
        /// </summary>
        public static void DestroyTInTransforms<T>(this IEnumerable<GameObject> input)
            where T : Component
        {
            // Get an array of all valid (non-null) instances of T found on these transforms.
            // It'll drop out early if none are found.
            if (!(input.Select(selector: t => t.GetComponent<T>()).Where(predicate: c => c != null) is T[] targets)) return;

            // Move backwards through this collection destroying them.
            for (var i = targets.Length - 1;
                 i > 0;
                 i--) Object.Destroy(obj: targets[i]);
        }


        public static void DestroyTransforms(this IEnumerable<Transform> input)
        {
            foreach (var t in input)
            {
                Object.Destroy(obj: t);
            }
        }


        public static void DestroyImmediateChildrenWithT<T>(this GameObject input)
            where T : Component
        {
            var childCount = input.transform.childCount;

            for (var i = childCount - 1;
                 i >= 0;
                 i--)
            {
                if (!input.transform.GetChild(index: i).GetComponent<T>()) return;

                input.transform.GetChild(index: i).gameObject.Destroy();
            }
        }


        public static void DestroyAllChildrenWithT<T>(this GameObject input)
            where T : Component
        {
            var targets = input.GetComponentsInChildren<T>();

            for (var i = targets.Length - 1;
                 i >= 0;
                 i++) targets[i].gameObject.Destroy();
        }



        #endregion

    }

}