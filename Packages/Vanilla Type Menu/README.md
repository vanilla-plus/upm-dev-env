# Type Menu

Type Menu is part of the Vanilla For Unity SDK.

Type Menu allows users to use a menu to populate fields using the SerializeReference attribute.

Type Menu is a heavily stripped down variation of SerializeReferenceUI by Textus Games.

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

Instead of beginning new classes from MonoBehaviour, you can try the inherited default class VanillaBehaviour.

```csharp
public class MyClass : MonoBehaviour
{

    [Serializable]
    public class Fruit
    {

        [SerializeField]
        public Color color;

    }

    [Serializable]
    public class Apple : Fruit
    {

        public Apple() => color = Color.red;

    }

    [Serializable]
    public class Banana : Fruit
    {

        public Banana() => color = Color.yellow;

    }

    [Serializable]
    public class Grape : Fruit
    {

        public Grape() => color = Color.green;

    }
    
    
    [SerializeReference]
    [TypeMenu]
    [TypeMenuOnlyFilter(typeof(Fruit))]
    public Fruit fruit;

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