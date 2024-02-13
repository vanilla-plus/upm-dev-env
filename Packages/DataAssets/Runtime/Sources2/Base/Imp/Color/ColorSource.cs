using System;

using UnityEngine;

namespace Vanilla.DataSources
{
 
	[Serializable]
	public abstract class ColorSource : IDataSource<Color>
	{

		[SerializeField]
		private string _name = "Unnamed ColorSource";
		public string Name
		{
			get => _name;
			set => _name = value;
		}
        
		public abstract Color Value
		{
			get;
			set;
		}

		[NonSerialized]
		private Action<Color> _onValueChange;
		public Action<Color> OnSet
		{
			get => _onValueChange;
			set => _onValueChange = value;
		}

		[NonSerialized]
		private Action<Color, Color> _onSetWithHistory;
		public Action<Color, Color> OnSetWithHistory
		{
			get => _onSetWithHistory;
			set => _onSetWithHistory = value;
		}

		public abstract void OnBeforeSerialize();

		public abstract void OnAfterDeserialize();

	}
}