# Vanilla MetaScript

Vanilla MetaScript is part of the Vanilla For Unity SDK.

MetaScript is a system for quickly and easily creating content sequences, not unlike a visual scripting or an animation-based approach. Each piece of logic is independent and known as a MetaTask.

Because MetaScript is script-based, your sequencing lives inside a given Unity scene. The benefit of this approach is that changes to sequencing can be considered an asset change rather than a code change, which means they can be remotely updated without an app update. In fact, Scenes can be treated like downloadable episodes for your project without the needing to adhere to any pre-built paradigms. Users can even create their own interactive content by attaching and serializing Scenes at run-time!

Making your own MetaTask is incredibly simple; create a new script, inherit from MetaTask_Base and override Run(). Thats it!

## Installation

Vanilla For Unity packages are installed through Unity's Package Manager using a [scoped registry](https://docs.unity3d.com/Manual/upm-scoped.html). Open your Unity Project of choice and select:

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

MetaTasks are based upon C# Tasks, mostly because they provide ease-of-use and asynchronicity. If you're new to programming, don't be too alarmed by this! All it means is that you can make the project wait for something to be finished or let it proceed anyway. You can make it wait by writing

await Task.Yield();

On any line that you want to wait. Here's an example of what it might look like to wait for the user to double-tap on a mobile device:

```csharp
public class WaitForDoubleTap : MetaTask_Base
{

	public override async void Run(IMetaTask initiate) 
	{
		while (Input.touchCount == 0 || Input.GetTouch(0).tapCount < 2) 
		{
			await Task.Yield();
		}
		
		Debug.Log("The user double-tapped!");
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