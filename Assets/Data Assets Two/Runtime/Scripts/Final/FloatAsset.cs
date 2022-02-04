using System;

using UnityEngine;

namespace Vanilla.DataAssets // ----------------------------------------------------------------------------------------------------------- Float //
{

	[Serializable]
	public class FloatSocket : ValueSocket<float, FloatSocket, FloatAsset, FloatAccessor> // --------------------------------------------- Socket //
	{ }

	[CreateAssetMenu(fileName = "New Float Data Asset",
	                 menuName = "Vanilla/Data Assets/Value/Float")]
	[Serializable]
	public class FloatAsset : ValueAsset<float, FloatSocket, FloatAsset, FloatAccessor> // ------------------------------------------------ Asset //
	{ }

	[Serializable]
	public abstract class FloatAccessor : ValueAccessor<float, FloatSocket, FloatAsset, FloatAccessor> // ----------------------------- Processors //
	{ }

//	[Serializable]
//	public class FloaTAccessorAdd : FloaTAccessor
//	{
//
//		public FloatSocket amount;
//
//		public override float Apply(float input) => input + amount.Get();
//
//	}
//
//	[Serializable]
//	public class FloaTAccessorSubtract : FloaTAccessor
//	{
//
//		public FloatSocket amount;
//
//		public override float Apply(float input) => input - amount.Get();
//
//	}
//
//	[Serializable]
//	public class FloaTAccessorMultiply : FloaTAccessor
//	{
//
//		public FloatSocket amount;
//
//		public override float Apply(float input) => input * amount.Get();
//
//	}
//
//	[Serializable]
//	public class FloaTAccessorDivide : FloaTAccessor
//	{
//
//		public FloatSocket amount;
//
//		public override float Apply(float input) => input / amount.Get();
//
//	}

}