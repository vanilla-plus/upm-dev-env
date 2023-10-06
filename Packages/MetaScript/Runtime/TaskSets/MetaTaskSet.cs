using System;
using System.Linq;

using UnityEngine;

using Vanilla.TypeMenu;

namespace Vanilla.MetaScript.TaskSets
{

	[Serializable]
	public abstract class MetaTaskSet : MetaTask
	{

		[SerializeReference]
		[TypeMenu]
		[Only(typeof(MetaTask))]
		protected MetaTask[] _tasks = Array.Empty<MetaTask>();
		public MetaTask[] Tasks => _tasks;


		public override void OnValidate()
		{
			#if UNITY_EDITOR
			foreach (var t in _tasks) t?.OnValidate();
			#endif

			base.OnValidate();
		}


		protected override bool CanAutoName() => Tasks != null && Tasks.Length > 0 && Tasks.All(task => task != null);

	}

}