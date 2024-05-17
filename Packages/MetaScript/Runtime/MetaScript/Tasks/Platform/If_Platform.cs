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

			[HideInInspector]
			[SerializeField]
			private string Name;
			
			[SerializeField]
			public RuntimePlatform[] platforms = Array.Empty<RuntimePlatform>();

			[SerializeReference]
			[TypeMenu("blue")]
			public MetaTask task;

			public void OnValidate()
			{
				#if UNITY_EDITOR
				Name = platforms.Aggregate(string.Empty,
				                           (s,
				                            platform) => s + (platform + " / "));

				if (!string.IsNullOrEmpty(Name)) Name = Name[..^3];

				task?.OnValidate();
				#endif
			}

		}

		[SerializeField]
		public PlatformTaskMapping[] mappings = Array.Empty<PlatformTaskMapping>();

		protected override bool Validate => mappings is
		                                    {
			                                    Length: > 0
		                                    } &&
		                                    mappings.All(mapping => mapping is
		                                                            {
			                                                            platforms:
			                                                            {
				                                                            Length: > 0
			                                                            },
			                                                            task: not null
		                                                            });

		public override void OnValidate()
		{
			#if UNITY_EDITOR
			base.OnValidate();

			foreach (var m in mappings) m?.OnValidate();
			#endif
		}


		protected override         string CreateAutoName() => "If the platform is...";


		protected override async UniTask<Scope> _Run(Scope scope)
		{
			var targetMapping = mappings.FirstOrDefault(m => m.platforms.Any(p => p == Application.platform));

			if (targetMapping == null)
			{
				Debug.LogError($"Task for platform [{Application.platform}] is null.");
			}
			else
			{
				#if debug
				Debug.Log($"The chosen platform was [{targetMapping.platforms[0].ToString()}]");
				#endif
				
				await targetMapping.task.Run(scope);
			}
			
			return scope;
		}

	}
}