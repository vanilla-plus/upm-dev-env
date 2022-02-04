using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace Vanilla.UnityExtensions
{

    public static class MonoBehaviourExtensions
    {
        // ----------------------------------------------------------------------------------- Changing Active State //

        public static void Activate(this MonoBehaviour input) =>
            input.gameObject.Activate();

        public static void Deactivate(this MonoBehaviour input) =>
            input.gameObject.Deactivate();

        public static void ToggleActive(this MonoBehaviour input) =>
            input.gameObject.ToggleActive();
        
        // -------------------------------------------------------------------------------------- Component Enabling //

        public static void Enable(this MonoBehaviour input) =>
            input.enabled = true;
        
        public static void Disable(this MonoBehaviour input) =>
            input.enabled = false;
        
        public static void ToggleEnabled(this MonoBehaviour input) =>
            input.enabled = !input.enabled;
        
        // ---------------------------------------------------------------------------------------- Self-Destruction //

        public static void Destroy(this MonoBehaviour input)
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


        public static IEnumerable<A> GetComponentsFromCollection<A, B>(this IEnumerable<A> input,
                                                                       B                   targetType)
            where A : Component
            where B : A =>
            input.Where(predicate: a => a as B != null);


        // ------------------------------------------------------------------------------------ GetComponent Dynamic //


        public static T GetComponentDynamic<T>(this MonoBehaviour input,
                                               GetComponentStyle  style,
                                               bool               includeInactive = false) =>
            style switch
            {
                GetComponentStyle.InChildren => input.GetComponentInChildren<T>(includeInactive: includeInactive),
                GetComponentStyle.InParent   => input.GetComponentInParent<T>(),
                _                            => input.GetComponent<T>()
            };


        public static T[] GetComponentsDynamic<T>(this MonoBehaviour input,
                                                  GetComponentStyle  style,
                                                  bool               includeInactive = false) =>
            style switch
            {
                GetComponentStyle.InChildren => input.GetComponentsInChildren<T>(includeInactive: includeInactive),
                GetComponentStyle.InParent   => input.GetComponentsInParent<T>(includeInactive: includeInactive),
                _                            => input.GetComponents<T>()
            };


        // ------------------------------------------------------------------------------------ GetComponent Dynamic //

    }

}