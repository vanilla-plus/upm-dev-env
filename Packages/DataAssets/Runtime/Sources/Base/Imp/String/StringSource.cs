using System;

using UnityEngine;

namespace Vanilla.DataSources
{
    
	[Serializable]
	public abstract class StringSource : IDataSource<string>
	{

//		[SerializeField]
//		private string _name = "Unnamed StringSource";
//		public string Name
//		{
//			get => _name;
//			set => _name = value;
//		}
        
		public abstract string Value
		{
			get;
			set;
		}
        
		[NonSerialized]
		private Action<string> _onSet;
		public Action<string> OnSet
		{
			get => _onSet;
			set => _onSet = value;
		}

		[NonSerialized]
		private Action<string, string> _onSetWithHistory;
		public Action<string, string> OnSetWithHistory
		{
			get => _onSetWithHistory;
			set => _onSetWithHistory = value;
		}

		public abstract void OnBeforeSerialize();

		public abstract void OnAfterDeserialize();

	}
}