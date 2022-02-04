using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace Vanilla.UnityExtensions
{

    public static class BehaviourExtensions
    {
        
        // ---------------------------------------------------------------------------------------- Self-Destruction //

        public static void Destroy(this Behaviour input)
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


        public static IEnumerable<T> GetComponentsFromCollection<T>(this IEnumerable<Behaviour> input)
            where T : Component =>
            input.Select(selector: m => m.GetComponent<T>()).Where(predicate: c => c != null);


        // ------------------------------------------------------------------------------------ GetComponent Dynamic //


        public static T GetComponentDynamic<T>(this Behaviour    input,
                                               GetComponentStyle style,
                                               bool              includeInactive = false) =>
            style switch
            {
                GetComponentStyle.InChildren => input.GetComponentInChildren<T>(includeInactive: includeInactive),
                GetComponentStyle.InParent   => input.GetComponentInParent<T>(),
                _                            => input.GetComponent<T>()
            };


        public static T[] GetComponentsDynamic<T>(this Behaviour    input,
                                                  GetComponentStyle style,
                                                  bool              includeInactive = false) =>
            style switch
            {
                GetComponentStyle.InChildren => input.GetComponentsInChildren<T>(includeInactive: includeInactive),
                GetComponentStyle.InParent   => input.GetComponentsInParent<T>(includeInactive: includeInactive),
                _                            => input.GetComponents<T>()
            };


        // ------------------------------------------------------------------------------------ GetComponent Dynamic //

    }

}