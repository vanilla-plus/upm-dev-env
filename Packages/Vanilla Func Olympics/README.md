# Vanilla Func Olympics

Func Olympics is part of the Vanilla For Unity SDK.

Func Olympics is a package for comparing the performance metrics of different functions. Simply pass in two methods you want to compare into FuncOlympics.Sprint(someFunction, someOtherFunction) and a console log will tell you which one performed faster and by how much.

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
public class FuncOlympicsTest : MonoBehaviour
{

    public void Start() => FuncOlympics.Sprint(a: Function_A,
                                                  b: Function_B,
                                                  input: "squats");


    public void Function_A(string somethingFunny) => Debug.Log("Hey there, " + gameObject.name + " here! What a fantastic day to be doing " + somethingFunny + ".");

    public void Function_B(string somethingFunny) => Debug.Log($"Hey there, {gameObject.name} here! What a fantastic day to be doing {somethingFunny}.");

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