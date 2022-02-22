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

		public bool hover;
		public bool down;

		private void Awake() => Target ??= GetComponentInParent<IPointerRedirectTarget>();

		public void OnPointerEnter(PointerEventData eventData)
		{
			hover = true;
			
			Target.PointerRedirectEnter(eventData);
		}


		public void OnPointerExit(PointerEventData eventData)
		{
			hover = false;
			
			Target.PointerRedirectExit(eventData);
		}


		public void OnPointerDown(PointerEventData eventData)
		{
			down = true;
			
			Target.PointerRedirectDown(eventData);
		}


		public void OnPointerUp(PointerEventData eventData)
		{
			down = false;
			
			Target.PointerRedirectUp(eventData);
		}

	}

}