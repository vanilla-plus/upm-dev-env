# Vanilla Initializer

Initializer is part of the Vanilla For Unity SDK.

Initializer is a small tool that adds an extra start-up callback to the classic cast of Start/Awake/OnEnable. It runs once per scene load and lets scripts take as long as they need before initialization is considered 'complete'. It even runs on GameObjects that aren't active, a crucial detail missing for Awake/Start/etc.

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

You can make any component initiable by adding the IInitiable interface like so:

```csharp
using Cysharp.Threading.Tasks;

public class MyNewClass : MonoBehaviour, IInitiable
{

	void Start() 
	{
		Debug.Log("Start!");
	}
	
	public async UniTask Initialize() 
	{
		Debug.Log("Initializing! Hm, let me go check that server for something quickly...");
		
		await UniTask.Delay(1000);
		
		Debug.Log("All done! It was just some junk mail.");
	}
	
	public async UniTask DeInitialize()
	{
		Debug.Log("DeInitializing! Saving password to PlayerPrefs in plain text...");
		
		await UniTask.Delay(1000);
		
		Debug.Log("All done! :)");
	}
	
}
```

Initialize will get called on each IInitiable, one by one, whenever a scene is loaded - this process is called initialization. You can check if initialization is occurring with Initializer.state. There are Action hooks for when Initialization starts and ends.

There is also an outgoing process that works the same called DeInitialization.

You can enable debugging for Initializer by adding the preprocessors DEBUG_VANILLA and INITIALIZER to the Unity Editor scripting define symbols.

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