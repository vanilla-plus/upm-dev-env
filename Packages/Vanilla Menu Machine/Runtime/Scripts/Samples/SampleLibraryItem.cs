using System;

using Cysharp.Threading.Tasks;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
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
		
		protected override void Awake()
		{
			base.Awake();

			PointerHover.Normal.Empty.onFalse += () => ArrangementDirty.State = true;
			PointerHover.Normal.Full.onFalse  += () => ArrangementDirty.State = true;

			PointerHover.Normal.Full.onTrue  += () => ArrangementDirty.State = false;
			PointerHover.Normal.Empty.onTrue += () => ArrangementDirty.State = false;

			var originalSize = Transform.sizeDelta;

			PointerHover.Toggle.onTrue += SlideImageAround;

//			PointerHover.Toggle.onFalse += () => image.rectTransform.anchoredPosition = Vector2.zero;
			
//			PointerHover.Normal.OnChange += n => Transform.sizeDelta = new Vector2(x: originalSize.x + (originalSize.x * hoverExpansion) * n.InOutPower(power: 1.25f),
//			                                                                       y: originalSize.y);
//			
//			PointerSelected.Normal.OnChange += n => Transform.sizeDelta = new Vector2(x: originalSize.x + (originalSize.x * selectExpansion) * n.InOutPower(power: 1.25f),
//			                                                                          y: originalSize.y);

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
			Debug.Log("[SampleLibraryItem] I was plucked out of the ether!");

			return default;
		}


		public override UniTask OnRetire()
		{
			Debug.Log("[SampleLibraryItem] I'm outta here");

			return default;
		}


		public override async UniTask Populate(SampleCatalogueItem item)
		{
			base.Populate(item: item);
			
			Debug.Log($"[SampleLibraryItem] I've been populated with [{item.Name}]");

//			nameText.text       = item.Name;
//			durationText.text   = $"{item.duration}s";
//			latLongText.text    = $"[{item.latLong[0]}, {item.latLong[1]}]";

			using var uwr = UnityWebRequestTexture.GetTexture($"https://vanilla-plus.neocities.org/unity/{item.Name.ToLower()}.jpeg");

			await uwr.SendWebRequest();

			if (uwr.result != UnityWebRequest.Result.Success)
			{
				Debug.Log(uwr.error);
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
		}


		private async void SlideImageAround()
		{
			var hoverState = PointerHover.Toggle;

			while (hoverState.State)
			{
				var mouseDir = Transform.position - Input.mousePosition;

//				Vector3.ClampMagnitude(vector: mouseDir,
//				                       maxLength: imageSlideEffect);

				image.rectTransform.anchoredPosition = Vector2.Lerp(a: Vector2.zero,
				                                                    b: mouseDir,
				                                                    t: imageSlideEffect);

//				image.rectTransform.anchoredPosition = Vector2.Lerp(a: Vector2.zero,
//				                                                    b: -Input.mousePosition,
//				                                                    t: imageSlideEffect);
				
				await UniTask.Yield();
			}
		}

	}

}