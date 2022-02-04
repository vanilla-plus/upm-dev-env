using System;

using UnityEngine;

using Vanilla.TypeMenu;

namespace Vanilla.DataAssets.Three
{

    public enum ComparisonType
    {

        LesserThan  = -1,
        Equal       = 0,
        GreaterThan = 1

    }

    public interface IBoolEvaluation
    {

        bool Evaluate();

    }

    [Serializable]
    public struct Evaluation_StringEquals : IBoolEvaluation
    {

        [SerializeReference]
        [TypeMenu]
        public StringSource a;

        [SerializeReference]
        [TypeMenu]
        public StringSource b;
        
        public bool Evaluate() => string.Equals(a: a.Get(),
                                                b: b.Get());

    }
    
    [Serializable]
    public struct Evaluation_IsEditor : IBoolEvaluation
    {

        public bool Evaluate()
        {
            #if UNITY_EDITOR
            return true;
            #else
            return false;
            #endif
        }

    }
    
    [Serializable]
    public struct Evaluation_DevelopmentBuild : IBoolEvaluation
    {

        public bool Evaluate()
        {
            #if DEVELOPMENT_BUILD
            return true;
            #else
            return false;
            #endif
        }

    }
    
    [Serializable]
    public struct Evaluation_Debug : IBoolEvaluation
    {

        public bool Evaluate()
        {
            #if DEBUG
            return true;
            #else
            return false;
            #endif
        }

    }

    public interface IReferenceEqualsEvaluation<Type, Source> : IBoolEvaluation
        where Type : class
        where Source : RefSource<Type>
    {

        Source a
        {
            get;
            set;
        }
        
        Source b
        {
            get;
            set;
        }

    }

    [Serializable]
    public struct Evaluation_MonoBehaviourEquals : IReferenceEqualsEvaluation<MonoBehaviour, MonoBehaviourSource>
    {

        [SerializeReference]
        [TypeMenu]
        private MonoBehaviourSource _a;
        public MonoBehaviourSource a
        {
            get => _a;
            set => _a = value;
        }

        [SerializeReference]
        [TypeMenu]
        private MonoBehaviourSource _b;
        public MonoBehaviourSource b
        {
            get => _b;
            set => _b = value;
        }
        
        public bool Evaluate() => ReferenceEquals(objA: a.Get(),
                                                  objB: b.Get());

    }
    
//    [Serializable]
//    public abstract class INumericEvaluation<TypeA> : IBoolEvaluation
//        where TypeA : IComparable<TypeA>
//    {
//
//        [SerializeField]
//        private IComparisonType comparisonType = IComparisonType.Equal;
//
//        protected abstract TypeA GetA();
//
//        protected abstract TypeA GetB();
//
//        private int Comparison() => GetA().CompareTo(other: GetB());
//
//        public bool Evaluate() => Comparison() == (int) comparisonType;
//
//    }

    public interface INumericEvaluation<Type, Source> : IBoolEvaluation
        where Type : unmanaged, IComparable<Type>
        where Source : ValueSource<Type>
    {

        ComparisonType ComparisonType
        {
            get;
            set;
        }

        Source a
        {
            get;
            set;
        }

        Source b
        {
            get;
            set;
        }
        
        int Comparison();

    }

//    [Serializable]
//    public struct Evaluation_FloatToFloat : INumericEvaluation<float>
//    {
//
//        [SerializeReference]
//        [TypeMenu]
//        public FloatSource a;
//
//        [SerializeReference]
//        [TypeMenu]
//        public FloatSource b;
//
//        protected override float GetA() => a.Get();
//
//        protected override float GetB() => b.Get();
//
//    }

    [Serializable]
    public struct Evaluation_CompareFloatFloat : INumericEvaluation<float, FloatSource>
    {

        [SerializeField]
        private ComparisonType _comparisonType;
        public ComparisonType ComparisonType
        {
            get => _comparisonType;
            set => _comparisonType = value;
        }

        [SerializeReference]
        [TypeMenu]
        private FloatSource _a;
        public FloatSource a
        {
            get => _a;
            set => _a = value;
        }

        [SerializeReference]
        [TypeMenu]
        private FloatSource _b;
        public FloatSource b
        {
            get => _b;
            set => _b = value;
        }

        public int Comparison() => a.Get().CompareTo(value: b.Get());

        public bool Evaluate() => Comparison() == (int) _comparisonType;

    }

//    [Serializable]
//    public class Evaluation_IntToInt : INumericEvaluation<int>
//    {
//
//        [SerializeReference]
//        [TypeMenu]
//        public IntSource a;
//
//        [SerializeReference]
//        [TypeMenu]
//        public IntSource b;
//
//        protected override int GetA() => a.Get();
//
//        protected override int GetB() => b.Get();
//
//    }

    [Serializable]
    public struct Evaluation_CompareIntInt : INumericEvaluation<int, IntSource>
    {

        [SerializeField] 
        private ComparisonType _comparisonType;
        public ComparisonType ComparisonType
        {
            get => _comparisonType;
            set => _comparisonType = value;
        }

        [SerializeReference] 
        [TypeMenu] 
        private IntSource _a;
        public IntSource a
        {
            get => _a;
            set => _a = value;
        }

        [SerializeReference] 
        [TypeMenu] 
        private IntSource _b;
        public IntSource b
        {
            get => _b;
            set => _b = value;
        }

        public int Comparison() => a.Get().CompareTo(value: b.Get());

        public bool Evaluate() => Comparison() == (int) _comparisonType;

    }


//    [Serializable]
//    public class Evaluation_FloatToVector3 : INumericEvaluation<float>
//    {
//
//        [SerializeReference]
//        [TypeMenu]
//        public FloatSource a;
//
//        [SerializeReference]
//        [TypeMenu]
//        public Vector3Source b;
//
//        [Tooltip("Which bit of the Vector3 do you want to compare against? 0 = X, 1 = Y, 2 = Z")]
//        [Range(min: 0,
//               max: 2)]
//        public int componentIndex = 0;
//
//        protected override float GetA() => a.Get();
//
//        protected override float GetB() => b.Get()[componentIndex];
//
//    }

    [Serializable]
    public struct Evaluation_CompareFloatVector3 : IBoolEvaluation
    {

        [SerializeField]
        public ComparisonType comparisonType;
        
        [SerializeReference]
        [TypeMenu]
        public FloatSource a;

        [SerializeReference]
        [TypeMenu]
        public Vector3Source b;

        [Tooltip("Which bit of the Vector3 do you want to compare against? 0 = X, 1 = Y, 2 = Z")]
        [Range(min: 0,
               max: 2)]
        public int componentIndex;

        public int Comparison() => a.Get().CompareTo(value: b.Get()[componentIndex]);

        public bool Evaluate() => Comparison() == (int) comparisonType;

    }

}