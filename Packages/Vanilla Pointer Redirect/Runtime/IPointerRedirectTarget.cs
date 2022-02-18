using UnityEngine;
using UnityEngine.EventSystems;

namespace Vanilla.PointerRedirect
{

    public interface IPointerRedirectTarget
    {

        void PointerRedirectEnter(PointerEventData eventData);

        void PointerRedirectExit(PointerEventData eventData);

        void PointerRedirectDown(PointerEventData eventData);

        void PointerRedirectUp(PointerEventData eventData);

    }

}