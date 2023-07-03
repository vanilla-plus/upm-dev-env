# Vanilla Core

Vanilla Core is part of the Vanilla For Unity SDK.

Vanilla Core is a very basic and somewhat messy Unity library, consistently updated for compatibility since 2017 to match Unity's development patterns.

This package contains Vanilla Core which includes a library of basic extension methods for common Unity interactions.

## Installation

Vanilla packages are installed through Unity's Package Manager using a [scoped registry](https://docs.unity3d.com/Manual/upm-scoped.html). Open your Unity Project of choice and select:

	Edit menu ->
		Project settings ->
			Package Manager -> 
				Scoped Registries -> 
					Plus button

Then add:

	name:      Vanilla Plus
	url:       https://registry.npmjs.com/
	Scopes:    vanilla.plus

## Usage

Instead of beginning new classes from MonoBehaviour, you can try the inherited default class VanillaBehaviour.

```csharp
public class MyNewClass : VanillaBehaviour 
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

## Contributing
Please don't. I have no idea what a pull request is and at this point I'm too afraid to ask.

If you hated this package, let me know:

[Gmail](mailto:lucas@vanilla.plus)

## Author

Lucas Hehir

## License
[The Unlicense](https://unlicense.org/)