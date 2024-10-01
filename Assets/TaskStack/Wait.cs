using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace MagicalProject
{

	[Serializable]
	public class Wait : SomeTask
	{

		[SerializeField]
		public float seconds = 1.0f;

		protected override string AutoName=> $"Wait for {seconds} seconds";


		public override async UniTask<Scope> Run(Scope scope)
		{
			await UniTask.Delay((int) (seconds * 1000));
            
			return scope;
		}

	}

}