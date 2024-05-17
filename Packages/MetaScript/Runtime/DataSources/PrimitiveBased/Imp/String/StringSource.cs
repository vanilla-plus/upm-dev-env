using System;

namespace Vanilla.MetaScript.DataSources.Strings
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

		public virtual void OnBeforeSerialize() { }

		public virtual void OnAfterDeserialize() { }

		public override string ToString() => Value;

//		public static implicit operator string(StringSource input) => input != null ?
//			                                                              input.ToString() :
//			                                                              Utility.Unknown;

	}
}