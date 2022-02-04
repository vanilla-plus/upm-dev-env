//using System;
//using System.Collections;
//using System.Collections.Generic;
//
//using UnityEngine;
//
//namespace Vanilla.Comparisons
//{
////    public interface IComparison<Type>
////    {
////
////        bool Compare(Type a, Type b);
////
////    }
////
////    [Serializable]
////    public struct FloatComparison : IComparison<float>
////    {
////
////        public bool Compare(float a,
////                            float b) => false;
////
////    }
//
//	public interface IComparison<Type>
//		where Type : IEquatable<Type>
//	{
//
//		public Type what
//		{
//			get;
//			set;
//		}
//
//	}
//
//	[Serializable]
//	public struct Comparison_Equals : IComparison<float>
//	{
//
//		private float _what;
//		public float what
//		{
//			get => _what;
//			set => _what = value;
//		}
//
//	}
//}
