using System;

using UnityEngine;

namespace Vanilla.TypeMenu
{

    [AttributeUsage(validOn: AttributeTargets.Field)]
    public class Except : PropertyAttribute
    {

        public readonly Type[] Types;

        public Except(params Type[] types) => Types = types;

    }

}