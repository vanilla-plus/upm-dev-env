using System;

using UnityEngine;

namespace Vanilla.MetaScript.DataSources.Strings
{
    
	/// <summary>
	///		This type of StringSource allows the getters first-access result to be cached (optionally)
	///		so that getting the result multiple times is not required. 
	/// </summary>
	[Serializable]
	public abstract class StringSource_Bakable : StringSource
	{

		[SerializeField] public bool BakeValueOnFirstAccess = true;
	    
		[NonSerialized] private string _value = null;

		public override string Value
		{
			get => BakeValueOnFirstAccess ? _value ?? GetFreshValue : GetFreshValue;
			set => _value = value; // You can overwrite the contents... JUST IN CASE?
		}

		protected abstract string GetFreshValue
		{
			get;
		}
        
	}
}