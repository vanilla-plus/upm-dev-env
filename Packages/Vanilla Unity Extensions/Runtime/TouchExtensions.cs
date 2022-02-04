using UnityEngine;
using UnityEngine.EventSystems;

namespace Vanilla.UnityExtensions
{
    public static class TouchExtensions
    {
        
        public static bool IsOverUIElement(ref this Touch input) => EventSystem.current.IsPointerOverGameObject(pointerId: input.fingerId);


    }
}
