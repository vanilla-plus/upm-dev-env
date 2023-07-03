//using System;
//
//using UnityEngine;
//using UnityEngine.EventSystems;
//
//namespace Vanilla.Arrangement
//{
//
//	[Serializable]
//	public abstract class LayoutItem<T> : ILayoutItem<T>
//		where T : Transform
//	{
//
//		[SerializeField]
//		protected T _transform;
//		public T Transform => _transform;
//		
//		[Tooltip("Is this item currently animating its transform? This should be marked as true whenever its transform changes in a way that may affect the rest of the layout.")]
//		[SerializeField]
//		protected Toggle _dirty;
//		public Toggle Dirty => _dirty;
//
////		[Header("Pointer State")]
////		[Tooltip("Is this item currently being hovered?")]
////		[SerializeField]
////		protected Toggle _hover;
////		public    Toggle Hover => _hover;
////
////		[Tooltip("Has a pointer click started on this item?")]
////		[SerializeField]
////		protected Toggle _pointerDown;
////		public Toggle PointerDown => _pointerDown;
////		
////		[Tooltip("Is this item currently selected?")]
////		[SerializeField]
////		private Toggle _selected;
////		public  Toggle Selected => _selected;
////
//////		[Header("Pointer State")]
//////		private Normal _selectionNormal;
////		
////		protected virtual void OnValidate() => _transform = transform as T;
////
////		public void OnPointerEnter(PointerEventData eventData) => _hover.State = true;
////
////		public void OnPointerExit(PointerEventData eventData)
////		{
////			_hover.State = false;
////
////			_pointerDown.State = false;
////		}
////
////
////		public void OnPointerDown(PointerEventData eventData) => _pointerDown.State = true;
////		
////		public void OnPointerUp(PointerEventData eventData)
////		{
////			if (_pointerDown.State)
////			{
////				_pointerDown.State = false;
////				
////				_selected.Flip();
////			}
////		}
//
//	}
//
//}