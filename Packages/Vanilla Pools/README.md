# Vanilla Pools

Vanilla Pools is part of the Vanilla For Unity SDK.

Vanilla Pools is a basic GameObject pooling system. Object pooling is a great performance optimization for objects you need to create often like bullets or enemies. Instead of creating new instances all the time, just create a set amount once and re-use them over and over.

The goal of Pools is to make that as easy as possible to implement. Simply make a new script inheriting from Pool (the 'pool') and another one inheriting from PoolItem (the item you need a lot of).

Try the Example Pool sample to see it in action.

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
public class ExamplePool : Pool<ExamplePool, ExamplePoolItem> { }

public class ExamplePoolItem : PoolItem<ExamplePool, ExamplePoolItem> 
{

	void SayHi() => Debug.Log("Hi!");

}

public class MyNewClass : MonoBehaviour 
{

	public ExamplePool pool;

	void Start() 
	{
		pool.sceneParent = transform;
		
		pool.Fill();
		
		pool.Get().SayHi();
	}

}
```

---

## Debugging

You can enable debugging for this package by including the keywords DEBUG_VANILLA and POOLS in the Unity Editor scripting define symbols.

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