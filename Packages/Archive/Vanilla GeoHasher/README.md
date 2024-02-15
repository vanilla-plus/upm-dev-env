# GeoHasher

GeoHasher is part of the Vanilla For Unity SDK.

GeoHasher is a small library for converting co-ordinates from latitude/longitude to [geohashes](https://en.wikipedia.org/wiki/Geohash) and vice versa. 

---

## Installation

Vanilla Plus packages are installed through Unity's Package Manager using a [scoped registry](https://docs.unity3d.com/Manual/upm-scoped.html). Open your Unity Project of choice and select:

> Edit menu > Project settings > Package Manager > Scoped Registries > Plus button

Then add:


	name:      Vanilla Plus
	url:       http://35.231.76.113:4873/
	Scopes:    vanilla.plus

---

## Usage

Pop example code markup below:

```csharp
public class MyNewClass : MonoBehaviour 
{

	public Text tapCountText;

	void Start() => tapCountText = GetComponentDynamic<Text>(GetComponentStyle.InParent);

	void Update() 
	{
		if (!AnyTouchBegan()) return;

		Log("Can't you hear me knockin'?");
	}

}
```

---

## Debugging

You can enable debugging for this package by including the keywords DEBUG and <PACKAGE_NAME_HERE> in the Unity Editor scripting define symbols.

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
