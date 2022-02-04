# Geodetics

Geodetics is part of the Vanilla Unity SDK.

Geodetics provides two simple methods for easily converting between traditional cartesian positions (x/y/z) and geodetic positions (longitude/latitude/radius).

It doesn't even introduce any new kinds of data, just use Vector3s! Each component maps directly to its axis equivalent - for example, x maps to longitude.

This allows you to have more intuitive and powerful rotational behaviour without trying to make cartesian positions work in ways they weren't built for.

---

## Installation

Vanilla Plus packages are installed through Unity's Package Manager using a [scoped registry](https://docs.unity3d.com/Manual/upm-scoped.html). Open your Unity Project of choice and select:

> Edit menu > Project settings > Package Manager > Scoped Registries > Plus button

Then add:


	name:      Vanilla Plus
	url:       https://registry.npmjs.com
	Scopes:    vanilla.plus

---

## Usage

```csharp
using UnityEngine;

using Vanilla.Geodetics;

public class RotateAroundTarget : MonoBehaviour
{

	public Vector3 position = new Vector3(x: 0.0f, y: 0.0f, z: 3.0f);
	
	public Transform target;
	
	public float rotateSpeed = 10.0f;

	void Update() 
	{
		position.x += Time.deltaTime * rotateSpeed;
		
		transform.position = target.TransformPoint(position.GeodeticToCartesian());
		
		transform.LookAt(target);
	}

}
```

---

## Contributing
Please don't. I have no idea what a pull request is and at this point I'm too afraid to ask.

If you hated this package, [let me know](mailto:lucas@vanilla.plus).

---

## Author

Lucas Hehir

---

## License
[The Unlicense](https://unlicense.org/)