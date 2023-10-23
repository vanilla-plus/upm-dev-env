using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Vanilla.MetaScript
{
    
	[Serializable]
	public class Stop : MetaTask
	{

		public int numberOfScopesToStop = 1;
		
		protected override bool CanAutoName() => true;
        
		protected override string CreateAutoName() => $"Cancel the next [{numberOfScopesToStop}] scopes";

		protected override UniTask<Scope> _Run(Scope scope)
		{
			scope.Cancel_Upwards(numberOfScopesToStop);
			
			Debug.Log(scope.Cancelled);

			return UniTask.FromResult(scope);
		}

	}
}