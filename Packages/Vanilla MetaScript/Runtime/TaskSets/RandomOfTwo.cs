using System;

using UnityEngine;

namespace Vanilla.MetaScript.TaskSets
{

	[Serializable]
	public class RandomOfTwo : Switch
	{

		[Range(0, 1)]
		[SerializeField]
		public float threshold = 0.5f;
	
		protected override bool CanDescribe() => true;
	
		protected override string DescribeTask() => $"[{threshold *100}%]?";

		public override void OnValidate()
		{
			base.OnValidate();

			#if UNITY_EDITOR
			if (Tasks.Length != 2)
			{
				var newTasks = new MetaTask[2];
    
				var elementsToCopy = Math.Min(_tasks.Length, 2);

				Array.Copy(_tasks,
				           newTasks,
				           elementsToCopy);

				_tasks = newTasks;
			}
			#endif
		}


		public override int Evaluate() => UnityEngine.Random.value < threshold ? 0 : 1;

	}

}