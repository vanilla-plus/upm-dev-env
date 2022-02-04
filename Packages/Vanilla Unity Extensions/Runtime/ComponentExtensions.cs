using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace Vanilla.UnityExtensions
{

    public static class ComponentExtensions
    {
        
        // ---------------------------------------------------------------------------------------- Self-Destruction //

        public static void Destroy(this Component input)
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
        
        // ------------------------------------------------------------------------------------ GetComponent Dynamic //


        public static T GetComponentDynamic<T>(this Component    input,
                                               GetComponentStyle style,
                                               bool              includeInactive = false) =>
            style switch
            {
                GetComponentStyle.InChildren => input.GetComponentInChildren<T>(includeInactive: includeInactive),
                GetComponentStyle.InParent   => input.GetComponentInParent<T>(),
                _                            => input.GetComponent<T>()
            };


        public static T[] GetComponentsDynamic<T>(this Component    input,
                                                  GetComponentStyle style,
                                                  bool              includeInactive = false) =>
            style switch
            {
                GetComponentStyle.InChildren => input.GetComponentsInChildren<T>(includeInactive: includeInactive),
                GetComponentStyle.InParent   => input.GetComponentsInParent<T>(includeInactive: includeInactive),
                _                            => input.GetComponents<T>()
            };


        // --------------------------------------------------------------------------------------- Component Queries //

        // In Parents

        public static IEnumerable<T> GetComponentsInParentsActiveInHierarchy<T>(this Component input)
            where T : Component =>
            input.GetComponentsInParent<T>().Where(s => s.gameObject.activeInHierarchy);
        
        public static IEnumerable<T> GetComponentsInParentsInactiveInHierarchy<T>(this Component input)
            where T : Component =>
            input.GetComponentsInParent<T>().Where(s => !s.gameObject.activeInHierarchy);
        
        public static IEnumerable<T> GetComponentsInParentsActiveSelves<T>(this Component input)
            where T : Component =>
            input.GetComponentsInParent<T>().Where(s => s.gameObject.activeSelf);
        
        public static IEnumerable<T> GetComponentsInParentsInactiveSelves<T>(this Component input)
            where T : Component =>
            input.GetComponentsInParent<T>().Where(s => !s.gameObject.activeSelf);

        // In Children
        
        // Immediate

        public static IEnumerable<T> GetComponentsInImmediateChildren<T>(this Component input)
            where T : Component =>
            input.transform.GetImmediateChildren().GetComponentsFromCollection<T>();

        public static IEnumerable<T> GetComponentsInImmediateChildrenActiveSelf<T>(this Component input)
            where T : Component =>
            input.transform.GetImmediateChildren().ActiveSelves().GetComponentsFromCollection<T>();
        
        public static IEnumerable<T> GetComponentsInImmediateChildrenInactiveSelf<T>(this Component input)
            where T : Component =>
            input.transform.GetImmediateChildren().InactiveSelves().GetComponentsFromCollection<T>();

//        public static IEnumerable<T> GetComponentsInImmediateChildrenActiveInHierarchy<T>(this Component input)
//            where T : Component =>
//            input.transform.GetImmediateChildren().ActiveInHierarchy().GetComponentsFromCollection<T>();
        
        public static IEnumerable<T> GetComponentsInImmediateChildrenInactiveInHierarchy<T>(this Component input)
            where T : Component =>
            input.transform.GetImmediateChildren().InactiveInHierarchy().GetComponentsFromCollection<T>();
        
        // All

        public static IEnumerable<T> GetComponentsInAllChildrenActiveInHierarchy<T>(this Component input)
            where T : Component =>
            input.GetComponentsInChildren<T>().Where(s => s.gameObject.activeInHierarchy);
        
        public static IEnumerable<T> GetComponentsInAllChildrenInactiveInHierarchy<T>(this Component input)
            where T : Component =>
            input.GetComponentsInChildren<T>().Where(s => !s.gameObject.activeInHierarchy);
        
        public static IEnumerable<T> GetComponentsInAllChildrenActiveSelves<T>(this Component input)
            where T : Component =>
            input.GetComponentsInChildren<T>().Where(s => s.gameObject.activeSelf);
        
        public static IEnumerable<T> GetComponentsInAllChildrenInactiveSelves<T>(this Component input)
            where T : Component =>
            input.GetComponentsInChildren<T>().Where(s => !s.gameObject.activeSelf);
     
        // --------------------------------------------------------------------------------- Component Query Actions //

        
        
    }

}