using System;

using UnityEngine;

using Vanilla.MetaScript.DataSources;
using Vanilla.TypeMenu;

namespace Vanilla.MetaScript.DataAssets
{
    
	[Serializable]
	[CreateAssetMenu(fileName = "Bool Asset",
	                 menuName = "Vanilla/MetaScript/Data Assets/Bool",
	                 order = 0)]
	public class BoolAsset : BaseAsset,
	                         IGettableSource<bool>, 
	                         ISettableSource<bool>
	{

		[TypeMenu("blue")]
		[SerializeReference] public IGetSetSource<bool> Source;

		public bool Value
		{
			get => Source.Value;
			set => Source.Value = value;
		}

		public void Set(bool newValue) { }

	}
}