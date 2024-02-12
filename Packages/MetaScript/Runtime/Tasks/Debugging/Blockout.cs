using System;
using Cysharp.Threading.Tasks; // Assuming UniTask is within this namespace
using UnityEngine;
using UnityEngine.UI; // For the Text component

using Vanilla.MetaScript; // For the TextMeshPro component

namespace MyCompany.MyProject
{
	[Serializable]
	public class Blockout : MetaTask
	{
		[SerializeField]
		private string message = "Default message";

		[SerializeField]
		private float lengthInSeconds = 5.0f;

		// SerializedField attribute allows you to choose between Text or TextMeshPro in the Unity Inspector.
		// It's assumed that only one of these will be used at a time, as per your project's needs.
		[SerializeField]
		private Text uiTextElement;

//		[SerializeField]
//		private TextMeshPro tmpTextElement;

		protected override bool CanAutoName() => true;

		protected override string CreateAutoName() => $"Blockout '{message}' for {lengthInSeconds} seconds";

		protected override async UniTask<Scope> _Run(Scope scope)
		{
			// Set the text of the chosen UI element
			if (uiTextElement != null)
			{
				uiTextElement.text = message;
			}
//			else if (tmpTextElement != null)
//			{
//				tmpTextElement.text = message;
//			}
			else
			{
				Debug.LogWarning("Blockout MetaTask: No UI element assigned.");
				
				return scope; // Early exit if no UI element is assigned
			}

			var elapsedTime = 0.0f;

			// Wait for the specified duration, checking for cancellation each frame
			while (elapsedTime < lengthInSeconds)
			{
				if (scope.Cancelled) return scope;

				elapsedTime += Time.deltaTime;
				
				await UniTask.Yield(PlayerLoopTiming.Update);
			}

			return scope;
		}
	}
}