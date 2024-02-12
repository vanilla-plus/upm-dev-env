using System;
using System.Linq;

using Cysharp.Threading.Tasks;

using UnityEngine;

using Vanilla.TypeMenu;

namespace Vanilla.MetaScript.Flow
{
	
	[Serializable]
	public class If_Platform : MetaTask
	{

		[Serializable]
		public class PlatformTaskMapping
		{

			[SerializeField]
			public RuntimePlatform[] platforms = Array.Empty<RuntimePlatform>();

			[SerializeReference]
			[TypeMenu("blue")]
			public MetaTask task;

		}

		[SerializeField]
		public PlatformTaskMapping[] mappings = Array.Empty<PlatformTaskMapping>();

		protected override bool CanAutoName() => false;

		protected override         string CreateAutoName() => "If running in UnityEditor...";


		protected override async UniTask<Scope> _Run(Scope scope)
		{
			if (scope.Cancelled) return scope;

			var targetTask = mappings.FirstOrDefault(m => m.platforms.Any(p => p == Application.platform))?.task;

			if (targetTask == null)
			{
				Debug.LogError($"Task for platform [{Application.platform}] is null.");
			}
			else
			{
				await targetTask.Run(scope);
			}
			
			return scope;
		}

	}
}