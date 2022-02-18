# Pointer Redirect

Pointer Redirect is a tiny component combination for redirecting Unitys Event System pointer callbacks to another component automatically.

---

## Installation

Vanilla packages are installed through Unity's Package Manager using a [scoped registry](https://docs.unity3d.com/Manual/upm-scoped.html). Open your Unity Project of choice and select:

> Edit menu > Project settings > Package Manager > Scoped Registries > Plus button

Then add:


	name:      Vanilla Plus
	url:       http://35.231.76.113:4873/
	Scopes:    vanilla.plus

---

## Usage

By default, Unity invokes IPointer~ callbacks only on components directly attached to an object. There are several situations however where we might want to have another component receive these callbacks instead, like maybe a root/parent component. We might want to know when the user has hovered over any part of a menu UI element, but only the background would receive those events. We don't want to attach our Menu script to the background just to facilitate this...

If we attach a PointerRedirectSource component to the background and give our menu the IPointerRedirectTarget interface, this will get handled for us:

```csharp
public class MyMenuClass : MonoBehaviour, IPointerRedirectTarget
{

        void PointerRedirectEnter(PointerEventData eventData) => Debug.Log("I was hovered over!");

        void PointerRedirectExit(PointerEventData eventData) => Debug.Log("I was hovered away from!");

        void PointerRedirectDown(PointerEventData eventData) => Debug.Log("I was clicked down on!");

        void PointerRedirectUp(PointerEventData eventData) => Debug.Log("I was clicked up from!");

}
```

---


---

## Contributing
Please don't. I have no idea what a pull request is and at this point I'm too afraid to ask.

If you took this package personally, [let me know](mailto:lucas@vanilla.plus).

---

## Author

Lucas Hehir

---

## License
[The Unlicense](https://unlicense.org/)
