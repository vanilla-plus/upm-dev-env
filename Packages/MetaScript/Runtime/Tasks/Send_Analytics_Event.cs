#if unity_services_core && unity_services_analytics
using System;

using UnityEngine;
using UnityEngine.Analytics;   // Make sure to include the Unity Analytics namespace
using Cysharp.Threading.Tasks; // For async and await functionality

namespace Vanilla.MetaScript
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