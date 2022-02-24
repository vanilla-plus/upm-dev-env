using System;

using Cysharp.Threading.Tasks;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

using Vanilla.Easing;

namespace Vanilla.MediaLibrary.Samples
{

	[Serializable]
	public class SampleLibraryItem : ArrangedLibraryItem2D<SampleLibraryItem, SampleCatalogueItem>
	{

		public Image   image;

//		public Text nameText;
//		public Text durationText;
//		public Text latLongText;

		public Texture2D thumbnail;

		public float hoverExpansion  = 0.25f;
		public float selectExpansion = 1.0f;

		public float imageSlideEffect = 1.0f;

		public CanvasRenderer[] slideInElements = new CanvasRenderer[0];
		
		public float slideInSeconds  = 0.5f;
		public float slideInDistance = 20.0f;

		public float slideInWaitBetweenElements = 0.1f;
		
		private Vector2 originalSize;
		
		protected override void Awake()
		{
			base.Awake();
			
//			var canvasRenderers = GetComponentsInChildren<CanvasRenderer>();

			foreach (var c in slideInElements)
			{
				c.SetAlpha(alpha: 0.0f);
			}

			originalSize = Transform.sizeDelta;

//			PointerHover.Normal.Empty.onFalse += () => ArrangementDirty.State = true;
//			PointerHover.Normal.Full.onFalse  += () => ArrangementDirty.State = true;
//
//			PointerHover.Normal.Full.onTrue  += () => ArrangementDirty.State = false;
//			PointerHover.Normal.Empty.onTrue += () => ArrangementDirty.State = false;

			PointerHover.Toggle.onTrue += SlideImageAround;

//			PointerHover.Toggle.onFalse += () => image.rectTransform.anchoredPosition = Vector2.zero;
			
//			PointerHover.Normal.OnChange += n => Transform.sizeDelta = new Vector2(x: originalSize.x + (originalSize.x * hoverExpansion) * n.InOutPower(power: 1.25f),
//			                                                                       y: originalSize.y);
//			
//			PointerSelected.Normal.OnChange += n => Transform.sizeDelta = new Vector2(x: originalSize.x + (originalSize.x * selectExpansion) * n.InOutPower(power: 1.25f),
//			                                                                          y: originalSize.y);
			
			PointerHover.Normal.OnChange += n => UpdateSize(n: n);

			PointerSelected.Normal.OnChange += n => UpdateSize(n: n);

			PointerHover.Normal.OnChange += n => image.color = Color.Lerp(a: Color.white,
			                                                              b: new Color(r: 0.9f,
			                                                                           g: 0.9f,
			                                                                           b: 0.9f,
			                                                                           a: 1.0f),
			                                                              t: n.InOutPower(power: 1.25f));

			PointerDown.Normal.OnChange += n => image.color = Color.Lerp(a: new Color(r: 0.9f,
			                                                                          g: 0.9f,
			                                                                          b: 0.9f,
			                                                                          a: 1.0f),
			                                                             b: new Color(r: 0.8f,
			                                                                          g: 0.8f,
			                                                                          b: 0.8f,
			                                                                          a: 1.0f),
			                                                             t: n.InOutPower(power: 1.25f));
		}


		public override UniTask OnGet()
		{
			Debug.Log(message: "[SampleLibraryItem] I was plucked out of the ether!");

			return default;
		}


		public override UniTask OnRetire()
		{
			Debug.Log(message: "[SampleLibraryItem] I'm outta here");

			return default;
		}


		public override async UniTask Populate(SampleCatalogueItem item)
		{
			base.Populate(item: item);
			
			Debug.LogWarning(message: $"[SampleLibraryItem] I've been populated with [{item.Name}]");

//			nameText.text       = item.Name;
//			durationText.text   = $"{item.duration}s";
//			latLongText.text    = $"[{item.latLong[0]}, {item.latLong[1]}]";

			using var uwr = UnityWebRequestTexture.GetTexture(uri: $"https://vanilla-plus.neocities.org/unity/{item.Name.ToLower()}.jpeg");

			await uwr.SendWebRequest();

			if (uwr.result != UnityWebRequest.Result.Success)
			{
				Debug.Log(message: uwr.error);
			}
			else
			{
				thumbnail = DownloadHandlerTexture.GetContent(www: uwr);

				image.sprite = Sprite.Create(texture: thumbnail,
				                             rect: new Rect(x: 0,
				                                            y: 0,
				                                            width: thumbnail.width,
				                                            height: thumbnail.height),
				                             pivot: Vector2.one * 0.5f,
				                             pixelsPerUnit: 100F,
				                             extrude: 0,
				                             meshType: SpriteMeshType.FullRect);
			}

//			var canvasRenderers = GetComponentsInChildren<CanvasRenderer>();

			foreach (var c in slideInElements)
			{
				await SlideElementIn(c);

//				await UniTask.Delay(millisecondsDelay: (int) (slideInWaitBetweenElements * 100));
			}
		}


		private async UniTask SlideElementIn(CanvasRenderer c)
		{
			Debug.Log($"Sliding in element [{c.gameObject.name}]");
			
			var rect   = c.transform as RectTransform;
			var endPos = rect.anchoredPosition;

			var startPos = endPos;
			startPos.x -= slideInDistance;

			var i    = 0.0f;
			var rate = 1.0f / slideInSeconds;

			while (i < 1.0f)
			{
				i += Time.deltaTime * rate;

				rect.anchoredPosition = Vector2.Lerp(a: startPos,
				                                     b: endPos,
				                                     t: i);
					
				c.SetAlpha(alpha: i);
					
				await UniTask.Yield();
			}
		}

		private async void SlideImageAround()
		{
			var hoverState = PointerHover.Toggle;

			while (hoverState.State)
			{
				image.rectTransform.anchoredPosition = Vector2.Lerp(a: Vector2.zero,
				                                                    b: Transform.position - Input.mousePosition,
				                                                    t: imageSlideEffect);
				
				await UniTask.Yield();
			}
		}


		private void UpdateSize(float n)
		{
			var newSize = originalSize;

			newSize.x += originalSize.x * hoverExpansion  * PointerHover.Normal.Value.InOutPower(power: 3f);
			newSize.x += originalSize.x * selectExpansion * PointerSelected.Normal.Value.InOutPower(power: 3f);

			Transform.sizeDelta = newSize;
			
//			Transform.sizeDelta = new Vector2(x: originalSize.x + (originalSize.x * selectExpansion) * n.InOutPower(power: 1.25f),
//			                                  y: originalSize.y);
		}

	}

}