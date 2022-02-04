using UnityEngine;
using UnityEngine.Events;

using Vanilla.QuitUtility;

namespace Vanilla.DataAssets
{

	[CreateAssetMenu(fileName = "New Event Data Asset",
	                 menuName = "Vanilla/Data Assets/Event")]
	public class EventAsset : BaseAsset
	{

		protected UnityAction _onBroadcast;
		public UnityAction onBroadcast
		{
			get => _onBroadcast;
			set
			{
				#if DEBUG_DATA_ASSETS
				if (!Quit.InProgress)
				{
					DataAssetsUtility.LogSubscriptions(name: name,
					                                   currentInvocations: _onBroadcast?.GetInvocationList(),
					                                   newInvocations: value?.GetInvocationList());
				}
				#endif

				_onBroadcast = value;
			}
		}


		[ContextMenu(itemName: "Broadcast")]
		public override void Broadcast() => _onBroadcast?.Invoke();

	}

}