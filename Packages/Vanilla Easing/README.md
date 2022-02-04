# Vanilla Easing

Vanilla Easing is part of the Vanilla For Unity SDK.

Vanilla Easing is a collection of simple-to-deploy easing formulas in the form of extension methods.

---

## Installation

Vanilla Plus packages are installed through Unity's Package Manager using a [scoped registry](https://docs.unity3d.com/Manual/upm-scoped.html). Open your Unity Project of choice and select:

> Edit menu > Project settings > Package Manager > Scoped Registries > Plus button

Then add:


	name:      Vanilla Plus
	url:       https://registry.npmjs.com/
	Scopes:    vanilla.plus

---

## Usage

```csharp
public class MyNewClass : MonoBehaviour 
{

	public Transform target;
	
	public Vector3 from;
	public Vector3 to;
	
	public float secondsToTake = 5.0f;

	public IENumerator Start() 
	{
		var i = 0.0f;
		var rate = 1.0f / secondsToTake;
		
		while (i < 1.0f) 
		{
			i += Time.deltaTime * rate;
			
			target.localPosition = Vector3.Lerp(from, to, i.Ease(easingDirection: Easing.EasingDirection.In, polynomicStrength: 6);
		
			yield return null;
		}
		
		target.localPosition = to;
	}

}
```

---

## Contributing
Please don't. I have no idea what a pull request is and at this point I'm too afraid to ask.

If you hated this package, [let me know.](mailto:lucas@vanilla.plus)

---

## Author

Lucas Hehir

---

## License
[The Unlicense](https://unlicense.org/)