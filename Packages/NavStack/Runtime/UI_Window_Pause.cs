//using System;
//
//using Cysharp.Threading.Tasks;
//
//using UnityEngine;
//using UnityEngine.UI;
//
//using Vanilla.TimeManagement;
//
//namespace PHORIA.Studios.Showcase
//{
//
//	[Serializable]
//	public class UI_Window_Pause : UI_Window
//	{
//
//		[SerializeField]
//		public Button lobbyButton;
//
//		protected override void Start()
//		{
//			base.Start();
//			
//			Hourglass.Paused.Active.OnValueChanged += HandlePause;
//			
//			lobbyButton.onClick.AddListener(HandleLobbyButton);
//		}
//		
//		private void HandleLobbyButton()
//		{
//			ShowcaseManager.i.CancelExperience();
////			ShowcaseManager.i.ResetApp().Forget();
//			
//			Hourglass.TryPause();
//		}
//
//
//		// Lets synchronize Hourglass.Paused.Active with the windows Active DeltaState
//		// So when the app is Paused, this window will transition with it.
//		private void HandlePause(bool outgoing,
//		                         bool incoming) => Active.Active.Value = incoming;
//		
//		protected override void OnDestroy()
//		{
//			base.OnDestroy();
//			
//			Hourglass.Paused.Active.OnValueChanged -= HandlePause;
//			
//			lobbyButton.onClick.RemoveListener(HandleLobbyButton);
//		}
//
//	}
//
//}