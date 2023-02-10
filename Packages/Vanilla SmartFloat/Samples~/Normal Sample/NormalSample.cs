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
        public EasingNormal easingNormal;

        void Start()
        {
            if (!ReferenceEquals(objA: swappableNormal,
                                 objB: null))
            {
                swappableNormal.OnChange += n => Debug.Log(message: $"The SwappableNormal changed to [{n}]");

                swappableNormal.Fill(conditional: null);
            }

            normal.OnChange             += n => Debug.Log(message: $"The normal changed to [{n}]");
            easingNormal.OnChange       += n => Debug.Log(message: $"The eased normal changed to [{n}]");

            normal.Full.onTrue += () => Debug.Log(message: "The normal is full!");

            easingNormal.OnIncrease += n => Debug.Log(message: $"The easing Normal normal increased to [{n}]!");
            
        }

    }

}