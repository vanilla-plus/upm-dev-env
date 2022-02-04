# String Formatting

String Formatting is part of the Vanilla For Unity SDK.

String Formatting is a collection of extension methods for string manipulation including human-readable data conversions, directory path creation and some simple encryption. For example...

long byteTotal = 5000;

Debug.Log(byteTotal.AsDataSize());

...will print out 5Kb.

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
public class MyNewClass : VanillaBehaviour 
{

	public long bytes = 1000;

	void Start() => Debug.Log(bytes.AsBytes); // Prints out "1kb"

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