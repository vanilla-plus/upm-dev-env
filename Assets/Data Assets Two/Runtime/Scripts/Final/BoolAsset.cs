using System;

using UnityEngine;

namespace Vanilla.DataAssets // ------------------------------------------------------------------------------------------------------------ Bool //
{

	public class BoolSocket : ValueSocket<bool, BoolSocket, BoolAsset, BoolAccessor> // -------------------------------------------------- Socket //
	{ }


	[CreateAssetMenu(fileName = "New Bool Asset",
	                 menuName = "Vanilla/Data Assets/Value/Bool")]
	[Serializable]
	public class BoolAsset : ValueAsset<bool, BoolSocket, BoolAsset, BoolAccessor> // ----------------------------------------------------- Asset //
	{ }

	[Serializable]
	public abstract class BoolAccessor : ValueAccessor<bool, BoolSocket, BoolAsset, BoolAccessor> { } // ------------------------------ Processors //

	[Serializable]
	public class BoolAccessorToggle : BoolAccessor
	{

//		public override void Apply(BoolSocket input) => input.Set(!input.Get());

//		public override bool Apply(bool input) => !input;


//		public override bool Apply(ref bool outgoing,
//		                           ref bool incoming) => incoming = !incoming;

		public override void Get(BoolAsset asset) => asset._value = !asset._value;


		public override bool Set(BoolAsset asset,
		                         ref bool outgoing,
		                         ref bool incoming) => incoming = !outgoing;

	}

}