using System;
using System.Collections;
using System.Collections.Generic;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace MagicalProject
{
    
	[Serializable]
	public class JumpToScope : SomeTask
	{

		[SerializeField]
		public ScopeSource target;
        
		protected override string AutoName => $"Jump to {target.gameObject.name}";


		public override async UniTask<Scope> Run(Scope scope)
		{
			await target.HandleJump(scope);

			return scope;
		}

	}
}