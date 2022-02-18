using UnityEngine;
using UnityEngine.EventSystems;

namespace Vanilla.PointerRedirect
{

	public class PointerRedirectSource : MonoBehaviour,
	                                      IPointerEnterHandler,
	                                      IPointerExitHandler,
	                                      IPointerDownHandler,
	                                      IPointerUpHandler
	{

		public IPointerRedirectTarget Target;

		private void Awake() => Target ??= GetComponentInParent<IPointerRedirectTarget>();

		public void OnPointerEnter(PointerEventData eventData) => Target.PointerRedirectEnter(eventData);

		public void OnPointerExit(PointerEventData eventData) => Target.PointerRedirectExit(eventData);

		public void OnPointerDown(PointerEventData eventData) => Target.PointerRedirectDown(eventData);

		public void OnPointerUp(PointerEventData eventData) => Target.PointerRedirectUp(eventData);

	}

}