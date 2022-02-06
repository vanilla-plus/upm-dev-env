using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Vanilla.TypeMenu;

namespace Vanilla
{

    public class NormalSample : MonoBehaviour
    {

        [SerializeReference]
        [TypeMenu]
        public INormal swappableNormal = new EasingNormal();

        [SerializeField]
        public Normal normal;

        [SerializeField]
        public UnsafeNormal unsafeNormal;

        [SerializeField]
        public EasingNormal easingNormal;

        [SerializeField]
        public UnsafeEasingNormal unsafeEasingNormal;


        void Start()
        {
            if (!ReferenceEquals(objA: swappableNormal,
                                 objB: null))
            {
                swappableNormal.OnChange += n => Debug.Log(message: $"The SwappableNormal changed to [{n}]");

                swappableNormal.Fill(conditional: null);
            }

            normal.OnChange             += n => Debug.Log(message: $"The regular normal changed to [{n}]");
            unsafeNormal.OnChange       += n => Debug.Log(message: $"The unsafe normal changed to [{n}]");
            easingNormal.OnChange       += n => Debug.Log(message: $"The easing normal changed to [{n}]");
            unsafeEasingNormal.OnChange += n => Debug.Log(message: $"The unsafe easing normal changed to [{n}]");

            normal.Full.onTrue += () => Debug.Log(message: "The normal is full!");

            unsafeNormal.Full.onFalse += () => Debug.Log(message: "The unsafe normal is no longer full!");

            easingNormal.OnIncrease += n => Debug.Log(message: $"The easing Normal normal increased to [{n}]!");

            unsafeEasingNormal.Empty.onChange += isEmpty => Debug.Log(message: $"The unsafe easing normal is {(isEmpty ? string.Empty : "not ")}empty");

        }

    }

}