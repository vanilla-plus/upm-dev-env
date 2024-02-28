using System;

using UnityEngine;

namespace Vanilla.MetaScript.DataSources
{
 
	[Serializable]
	public abstract class Vec3Source : IDataSource<Vector3>
	{

//		[SerializeField]
//		private string _name = "Unnamed Vec3Source";
//		public string Name
//		{
//			get => _name;
//			set => _name = value;
//		}
        
		public abstract Vector3 Value
		{
			get;
			set;
		}

		[NonSerialized]
		private Action<Vector3> _onValueChange;
		public Action<Vector3> OnSet
		{
			get => _onValueChange;
			set => _onValueChange = value;
		}

		[NonSerialized]
		private Action<Vector3, Vector3> _onSetWithHistory;
		public Action<Vector3, Vector3> OnSetWithHistory
		{
			get => _onSetWithHistory;
			set => _onSetWithHistory = value;
		}

		public abstract void OnBeforeSerialize();

		public abstract void OnAfterDeserialize();

	}
}