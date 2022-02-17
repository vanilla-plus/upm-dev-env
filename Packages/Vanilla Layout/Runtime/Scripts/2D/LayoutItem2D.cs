//using UnityEngine;
//
//
//namespace Vanilla.Layout
//{
//
//	public class LayoutItem2D : LayoutItem<RectTransform>
//	{
//
////		[SerializeField]
////		public Normal expansion;
////
////		[SerializeField]
////		public KeyCode testKey = KeyCode.Alpha1;
////
////		void Awake()
////		{
////			Selected.onTrue += () => expansion.Fill(conditional: Selected);
////
////			Selected.onFalse += () => expansion.Drain(conditional: Selected,
////			                                          targetCondition: false);
////
////			var originalSize = _transform.sizeDelta.x;
////
////			expansion.OnChange += n => _transform.sizeDelta = new Vector2(x: originalSize * n.InOutPower(power: 1.25f),
////			                                                              y: originalSize);
////
////			expansion.Empty.onFalse += () => Dirty.State = true;
////			expansion.Full.onFalse  += () => Dirty.State = true;
////
////			expansion.Full.onTrue  += () => Dirty.State = false;
////			expansion.Empty.onTrue += () => Dirty.State = false;
////
////			expansion.OnChange?.Invoke(obj: 0.0f);
////		}
////
////		void Update()
////		{
////			if (Input.GetKeyDown(key: testKey)) Selected.Flip();
////		}
//
//	}
//
//}