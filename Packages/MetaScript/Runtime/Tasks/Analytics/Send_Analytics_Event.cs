#if unity_services_core && unity_services_analytics
using System;

using UnityEngine;

using Cysharp.Threading.Tasks;

using Unity.Services.Analytics; // For async and await functionality

namespace Vanilla.MetaScript.Analytics
{

	[Serializable]
	public class Send_Analytics_Event : MetaTask
	{

		// Custom properties for the analytics event
		[SerializeField]
		public string eventName = "default_event";

		// AutoName for self-documentation
		protected override bool CanAutoName() => true;

		protected override string CreateAutoName() => $"Submit Analytics Event '{eventName}'";


		// The main execution method for the task
		protected override UniTask<Scope> _Run(Scope scope)
		{
			if (scope.Cancelled) return UniTask.FromResult(scope);

			AnalyticsService.Instance.CustomData(eventName);
			
			return UniTask.FromResult(scope);
		}

	}

}
#endif