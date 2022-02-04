using System;

using UnityEngine;

using Vanilla.Swaperators;
using Vanilla.TypeMenu;

namespace Vanilla.DataAssets // ------------------------------------------------------------------------------------------------------------- Int //
{

	[Serializable]
	public class IntSocket : ValueSocket<int, IntSocket, IntAsset, IntAccessor> // ------------------------------------------------------- Socket //
	{

		

	}

	[CreateAssetMenu(fileName = "New Int Data Asset",
	                 menuName = "Vanilla/Data Assets/Value/Int")]
	[Serializable]
	public class IntAsset : ValueAsset<int, IntSocket, IntAsset, IntAccessor> // ---------------------------------------------------------- Asset //
	{ }

	[Serializable]
	public abstract class IntAccessor : ValueAccessor<int, IntSocket, IntAsset, IntAccessor> // --------------------------------------- Processors //
	{ }

//	[Serializable]
//	public class InTAccessor_Add : InTAccessor
//	{

//		[SerializeField]
//		public IntSocket amount;

//		public override void Get(IntAsset asset) => asset._value += amount.Get();


//		public override bool Set(IntAsset asset,
//		                         ref int outgoing,
//		                         ref int incoming)
//		{
//			incoming += amount.Get();

//			return true;
//		}

//	}

//	[Serializable]
//	public class InTAccessor_Subtract : InTAccessor
//	{

//		[SerializeField]
//		public IntSocket amount;

//		public override void Get(IntAsset asset) => asset._value -= amount.Get();


//		public override bool Set(IntAsset asset,
//		                         ref int outgoing,
//		                         ref int incoming)
//		{
//			incoming -= amount.Get();

//			return true;
//		}

//	}

//	[Serializable]
//	public class InTAccessor_Multiply : InTAccessor
//	{

//		[SerializeField]
//		public IntSocket amount;

//		public override void Get(IntAsset asset) => asset._value *= amount.Get();


//		public override bool Set(IntAsset asset,
//		                         ref int outgoing,
//		                         ref int incoming)
//		{
//			incoming *= amount.Get();

//			return true;
//		}

//	}

//	[Serializable]
//	public class InTAccessor_Divide : InTAccessor
//	{

//		[SerializeField]
//		public IntSocket amount;

//		public override void Get(IntAsset asset) => asset._value /= amount.Get();


//		public override bool Set(IntAsset asset,
//		                         ref int outgoing,
//		                         ref int incoming)
//		{
//			incoming /= amount.Get();

//			return true;
//		}

//	}
	
	[Serializable]
	public class IntAccessorModify : IntAccessor
	{

		[SerializeReference]
		[TypeMenu]
		public ISwaperator Swaperator;
		
		[SerializeField]
		public IntSocket amount;

		public override void Get(IntAsset asset) => asset._value = Swaperator.Apply(a: asset._value,
		                                                                          b: amount.Get());


		public override bool Set(IntAsset asset,
		                         ref int outgoing,
		                         ref int incoming)
		{
			incoming = Swaperator.Apply(a: incoming,
			                          b: amount.Get());

			return true;
		}

	}

}