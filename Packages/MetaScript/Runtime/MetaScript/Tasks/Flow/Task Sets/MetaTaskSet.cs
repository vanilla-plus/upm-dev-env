using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

using Vanilla.TypeMenu;

namespace Vanilla.MetaScript.Flow
{

	[Serializable]
	public abstract class MetaTaskSet : MetaTask
	{
		
		[SerializeReference]
		[TypeMenu("blue")]
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