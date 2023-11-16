#if unity_services_core && unity_services_analytics
using System;

using Cysharp.Threading.Tasks;

using Unity.Services.Core;

using Unity.Services.Analytics;

namespace Vanilla.MetaScript
{

	[Serializable]
	public class Initialize_Analytics : MetaTask
	{

		protected override bool CanAutoName() => true;

		protected override string CreateAutoName() => "Initialize Unity Analytics";

		protected override async UniTask<Scope> _Run(Scope scope)
		{
			if (scope.Cancelled) return scope;

			await UnityServices.InitializeAsync();

			AnalyticsService.Instance.StartDataCollection();
				
			return scope;
		}

	}

}
#endif