# Unity Extensions

Unity Extensions is part of the Vanilla Unity SDK.

Unity Extensions is a collection of extension methods for all the data types and classes found in the Unity GameObject ecosystem. The purpose is to offer a number of shorthand methods for common or useful behaviour.

Some examples include:

+ GameObject.GetComponentDynamic - Get a component on the GameObject itself or upwards through its parents or downwards through its children by changing the parameter.
+ Transform.GetAllChildren - Returns a list of all child transforms without using GetComponentsOnChildren<Transform>
+ T[].Shuffle - Shuffles the contents of an array using the Fisher-Yates algorithm.

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
public class LovingParent : MonoBehaviour 
{

	void Start()
	{
		foreach (var c in transform.GetAllChildren()) 
		{
			Debug.Log($"I have a child and their name is [{c}]");
		}
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