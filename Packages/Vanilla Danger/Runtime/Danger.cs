using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vanilla.Danger
{

    public static class Danger
    {

        /// <summary>
        /// Super performant bitwise unmanaged type comparison by https://stackoverflow.com/users/8675026/alsein
        ///
        /// Checks if two values of an unmanaged type are the same by comparing them byte-for-byte.
        ///
        /// In a Stopwatch test using BitwiseEquals w Quaternion vs default Quaternion.Equals 1000 times (excluding an extra data warm-up)
        /// This performed, on average, 200-220 ticks faster (i.e. 402 vs 623)
        /// </summary>
        public static unsafe bool BitwiseEquals<Unmanaged>(in Unmanaged a,
                                                           in Unmanaged b)
            where Unmanaged : unmanaged
        {
            fixed (Unmanaged* px = &a)
                fixed (Unmanaged* py = &b)
                {
                    var p1 = (byte*) px;
                    var p2 = (byte*) py;

                    for (var i = 0;
                         i < sizeof(Unmanaged);
                         i++)
                    {
                        if (p1[i] != p2[i]) return false;
                    }
                }

            return true;
        }

    }
}
