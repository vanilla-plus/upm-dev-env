# Schrodinger Assets

Schrodinger Assets are part of the Vanilla For Unity SDK.

Schrödinger Assets are a type of ScriptableObject that have a weakly-typed data payload through questionable use of Unitys SerializeReference and Vanilla Type Menu.

Because of their loose payload typing, you can swap the type if needed making your data potentially very flexible!

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

As long as you know the expected payload type, you can just use asset.payloads[i].Get<Type>() much like GetComponent. This operation, unlike GetComponent, does not generate garbage!

```csharp
public class SampleA : MonoBehaviour
{

    public SchrodingerAsset assetA;
    public SchrodingerAsset assetB;

    public SampleA somethingA;
    public float somethingB;

    [ContextMenu("Get A")]
    public void GetA() => somethingA = assetA.payloads[0].Get<SampleA>();


    [ContextMenu("Set A")]
    public void SetA() => assetA.payloads[0].Set(input: this);


    [ContextMenu("Get B")]
    public void GetB() => somethingB = assetB.payloads[0].Get<float>();


    [ContextMenu("Set B")]
    public void SetB() => assetB.payloads[0].Set(input: somethingB);

}
```

You will need to create new payloads for unsupported data types, however all MonoBehaviour-derived classes work fine as in the above example.

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