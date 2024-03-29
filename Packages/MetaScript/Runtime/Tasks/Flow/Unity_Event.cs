using System;

using Cysharp.Threading.Tasks;

using UnityEngine.Events;

namespace Vanilla.MetaScript.Flow
{

	[Serializable]
	public class Unity_Event : MetaTask
	{

		public UnityEvent unityEvent = new UnityEvent();


		protected override bool CanAutoName()
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


		protected override string CreateAutoName()
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


		protected override UniTask<Scope> _Run(Scope scope)
		{
			unityEvent.Invoke();

			return UniTask.FromResult(scope);
		}

	}

}