using System;
using System.Collections.Generic;

namespace Vanilla.DotNetExtensions
{

    public static class ArrayExtensions
    {

        /// <summary>
        ///     Shuffles all elements of the collection using the Fisher-Yates algorithm.
        /// </summary>
        public static void Shuffle<T>(this T[] input)
        {
            var n = input.Length;

            for (var i = 0;
                 i < n;
                 i++)
            {
                var r = i +
                        UnityEngine.Random.Range(minInclusive: 0,
                                                 maxExclusive: n - i);

                var t = input[r];

                input[r] = input[i];

                input[i] = t;
            }

        }
        
    }

}