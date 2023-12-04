using System;

using UnityEngine;

namespace Vanilla.TypeMenu.Samples
{
    
    [Serializable]
    public class TypeMenu_Sample_A : MonoBehaviour
    {

        [SerializeReference]
        [TypeMenu("blue")]
        [Only(typeof(IHealthy))]
        public Food eatThis;

        [SerializeReference]
        [TypeMenu("blue")]
        [Except(typeof(IHealthy))]
        public Food dontEatThis;

        [SerializeReference]
        [TypeMenu("blue")]
        public Food eatWhateverYouWant;

    }
    
    public interface IHealthy { }
    public interface IForbidden { }

    [Serializable]
    public class Food
    {

        [SerializeField]
        public Color color;

    }

    [Serializable]
    public class Apple : Food, IHealthy
    {

        public Apple() => color = Color.red;

    }

    [Serializable]
    public class Banana : Food, IHealthy
    {

        public Banana() => color = Color.yellow;

    }

    [Serializable]
    public class Grape : Food, IHealthy
    {

        public Grape() => color = Color.green;

    }
    
        
    [Serializable]
    public class Marble : Food, IForbidden
    {

        public Marble() => color = Color.white;

    }
    
    [Serializable]
    public class TidePod : Food, IForbidden
    {

        public TidePod() => color = Color.gray;

    }
    
}
