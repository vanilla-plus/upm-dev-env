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


//			internal void OnValidate()
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

		protected override bool Valid => false;

//		public override void OnValidate()
		public override void OnValidate()
		{
			#if UNITY_EDITOR
//			base.OnValidate();
			base.OnValidate();

			foreach (var m in mappings)
			{
//				m?.OnValidate();
				m?.OnValidate();
			}
			#endif
		}


		protected override         string CreateAutoName() => "If running in UnityEditor...";


		protected override async UniTask<Scope> _Run(Scope scope)
		{
			

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