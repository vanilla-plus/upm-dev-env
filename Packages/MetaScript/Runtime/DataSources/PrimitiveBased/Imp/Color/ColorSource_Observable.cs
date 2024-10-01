using System;

using UnityEngine;

using Vanilla.MetaScript.DataSources;

namespace Vanilla.MetaScript
{
	
	[Serializable]
    public class ColorSource_Observable : ColorSource, 
                                          IObservableSource<Color>
    {

	    [SerializeField]
	    private Color _value;
	    public override Color Value
	    {
		    get => _value;
		    set
		    {
			    var outgoing = _value;

			    _value = value;

			    OnSet?.Invoke(value);
			    OnSetWithHistory?.Invoke(value, outgoing);
		    }
	    }

        public override void OnBeforeSerialize() { }

        public override void OnAfterDeserialize() { }

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

    }
}
