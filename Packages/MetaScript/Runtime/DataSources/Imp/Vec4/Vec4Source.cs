using System;

using UnityEngine;

namespace Vanilla.MetaScript.DataSources
{
 
	[Serializable]
	public abstract class Vec4Source : IDataSource<Vector4>
	{

//		[SerializeField]
//		private string _name = "Unnamed Vec4Source";
//		public string Name
//		{
//			get => _name;
//			set => _name = value;
//		}
        
		public abstract Vector4 Value
		{
			get;
			set;
		}

		[NonSerialized]
		private Action<Vector4> _onValueChange;
		public Action<Vector4> OnSet
		{
			get => _onValueChange;
			set => _onValueChange = value;
		}

		[NonSerialized]
		private Action<Vector4, Vector4> _onSetWithHistory;
		public Action<Vector4, Vector4> OnSetWithHistory
		{
			get => _onSetWithHistory;
			set => _onSetWithHistory = value;
		}

		public abstract void OnBeforeSerialize();

		public abstract void OnAfterDeserialize();

	}
}