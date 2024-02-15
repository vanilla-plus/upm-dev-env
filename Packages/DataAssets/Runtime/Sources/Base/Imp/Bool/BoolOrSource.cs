using System;
using System.Linq;

using UnityEngine;

using Vanilla.TypeMenu;

namespace Vanilla.DataSources
{
    
	[Serializable]
	public class BoolOrSource : BoolSource, IProtectedSource<bool>
	{

		[SerializeField]
		private string _Name = "Unnamed BoolOrSource";
		public string Name
		{
			get => _Name;
			set => _Name = value;
		}
		
		[SerializeReference]
		[TypeMenu("red")]
		public BoolSource[] CombinedSources = Array.Empty<BoolSource>();
		
		[SerializeField]
		private bool _value = false;
		public override bool Value
		{
			get => _value;
			set
			{
				if (_value == value) return;

				var outgoing = _value;
                
				_value = value;
                
				#if debug
				Debug.Log($"[{Name}] was changed from [{outgoing}] to [{value}]");
				#endif

				OnSet?.Invoke(_value);

				OnSetWithHistory?.Invoke(_value,
				                         outgoing);
			}
		}

		public override void OnBeforeSerialize() { }

		public override void OnAfterDeserialize() { }



		public override void Init()
		{
			Debug.Log("Init?");

			foreach (var s in CombinedSources)
			{
				Debug.Log("Oh look its a " +s.GetType().Name);
				
				s.OnSet += HandleSourceSet;
			}
		}

		public override void Deinit()
		{
			Debug.Log("Deinit?");
			
			foreach (var s in CombinedSources) s.OnSet -= HandleSourceSet;
		}
		
		private void HandleSourceSet(bool incoming)
		{
			Debug.Log($"One of my CombinedSources was Set to [{incoming}]");
			
			Value = CombinedSources.Any(s => s.Value);
		}

	}
}