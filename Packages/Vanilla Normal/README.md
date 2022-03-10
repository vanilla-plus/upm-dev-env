# Vanilla Normal

Normal is part of the Vanilla UPM SDK.

Normal is a basic tool class for driving things with a float clamped between 0-1, i.e. a one-dimensional normal. Normal features a Toggle for being 'empty' (0f) and 'full' (1.0f) which can be subscribed to, as well as when the value increases or decreases or changes at all.

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

Instead of beginning new classes from MonoBehaviour, you can try the inherited default class VanillaBehaviour.

```csharp
public class Bomb : MonoBehaviour 
{

	public Normal normal = new Normal(0.0f);
    
	async void Start()
	{
		normal.Empty.onFalse += () => Debug.Log("What's that hissing noise...?");
        
		normal.OnChange += n => transform.localScale = Vector3.one * Mathf.Lerp(1.0f, 2.0f, n);

		normal.Full.onTrue += () =>
		                      {
			                      Debug.Log("Shes gonna blow!!");

			                      Destroy(gameObject);
		                      };

		await normal.Fill(null);
	}

}
```

---

## Debugging

You can enable debugging for this package by including the keywords DEBUG and <___> in the Unity Editor scripting define symbols.

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