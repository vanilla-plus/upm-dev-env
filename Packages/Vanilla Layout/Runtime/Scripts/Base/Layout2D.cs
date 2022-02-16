using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Vanilla.Layout
{

	[Serializable]
	public abstract class Layout2D : LayoutBase<LayoutItem2D, RectTransform, Vector2>
	{

		public override void ArrangeItem(RectTransform target,
		                                 Vector2 position) => target.anchoredPosition = position;


		public override Vector2 GetInitialPosition() => _transforms[0].anchoredPosition;


		public override Vector2 GetNewPosition(RectTransform prev,
		                                       RectTransform current) => prev.anchoredPosition + GetPreviousOffset(prev: prev) + GetCurrentOffset(current: current);




		public async UniTask AnimateTest()
		{
			var i = 0.0f;

			while (i < 1.0f)
			{
				i += Time.deltaTime * 0.25f;

				_transforms[1].sizeDelta = new Vector2(x: Mathf.Lerp(a: 0,
				                                                     b: 100,
				                                                     t: i),
				                                       y: 100);

				await UniTask.Yield();
			}

			i = 1.0f;


		}

	}

}