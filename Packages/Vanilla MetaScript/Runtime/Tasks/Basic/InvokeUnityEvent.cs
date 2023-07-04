using System;
using System.Threading;

using Cysharp.Threading.Tasks;

using UnityEngine.Events;

namespace Vanilla.MetaScript
{

	[Serializable]
	public class InvokeUnityEvent : MetaTask
	{

		public UnityEvent unityEvent = new UnityEvent();


		protected override bool CanDescribe()
		{
			var count = unityEvent.GetPersistentEventCount();

			if (count == 0) return false;

			for (var i = 0;
			     i < count;
			     i++)
			{
				if (unityEvent.GetPersistentTarget(i) == null
				 || string.IsNullOrEmpty(unityEvent.GetPersistentMethodName(i)))
				{
					return false;
				}
			}

			return true;
		}


		protected override string DescribeTask()
		{
			var output = "Call";

			for (var i = 0;
			     i < unityEvent.GetPersistentEventCount();
			     i++)
			{
				var target = unityEvent.GetPersistentTarget(i);
			
				output += $" [{target.name}.{target.GetType().Name}.{unityEvent.GetPersistentMethodName(i)}]";
			}

			return output;
		}


		protected override UniTask<Tracer> _Run(Tracer tracer)
		{
			unityEvent.Invoke();

			return UniTask.FromResult(tracer);
		}

	}

}