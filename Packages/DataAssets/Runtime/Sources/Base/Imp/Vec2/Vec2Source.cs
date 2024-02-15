using System;

using UnityEngine;

namespace Vanilla.DataSources
{
 
	[Serializable]
	public abstract class Vec2Source : IDataSource<Vector2>
	{

//		[SerializeField]
//		private string _name = "Unnamed Vec2Source";
//		public string Name
//		{
//			get => _name;
//			set => _name = value;
//		}
        
		public abstract Vector2 Value
		{
			get;
			set;
		}

		[NonSerialized]
		private Action<Vector2> _onValueChange;
		public Action<Vector2> OnSet
		{
			get => _onValueChange;
			set => _onValueChange = value;
		}

		[NonSerialized]
		private Action<Vector2, Vector2> _onSetWithHistory;
		public Action<Vector2, Vector2> OnSetWithHistory
		{
			get => _onSetWithHistory;
			set => _onSetWithHistory = value;
		}

		public virtual void Init() { }

		public virtual void Deinit() { }

		public abstract void OnBeforeSerialize();

		public abstract void OnAfterDeserialize();

	}
}