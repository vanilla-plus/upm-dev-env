# Vanilla Data Assets

Data Assets is part of the Vanilla For Unity SDK.

Data Assets is a collection of ScriptableObject-based data classes, created with the goal of breaking important values out of your scenes or static scripting. Things like your players health or a reference to your main camera can be easily accessed as a ScriptableObject data asset.

Data Assets was heavily inspired by the Unite Austin 2017 demonstration by Ryan Hipple from Schell Games.

## Installation

Vanilla Plus packages are installed through Unity's Package Manager using a [scoped registry](https://docs.unity3d.com/Manual/upm-scoped.html). Open your Unity pProject and select:

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

# Creating new Data Assets

Simply right-click in your Project panel and choose the kind of 'data asset' you need from the Vanilla/Data Assets sub-menu. For example, you could create an IntAsset to represent an RPG characters level. You can enter a description for the asset as well to keep track of its purpose.

# Sockets

To assist with using data assets, there's another kind of class called a socket, one for each kind of asset. These are useful because sometimes you may  want to unplug an asset temporarily for testing or have a fallback value (just in case).

# Async

Accessing a value from a socket can be done with Get() and Set(), which will preference the assets data over the fallback value if available. Both of these functions are virtual and await-able, meaning you can easily write your own child socket classes to completely intercept how these values are accessed. This is particularly beneficial if your data needs to travel!

# Data Sets

Data Sets are collections of Data Assets which you can then operate on together. For example, PlayerPrefsDataSet has Save and Load, which reads or writes the values for each asset to PlayerPrefs respectively.

```csharp

public class Player : MonoBehaviour 
{
	
	public ValueSocket<int> playerLevelSocket;
	
	private async void Start()
	{
		int currentLevel = await playerLevelSocket.Get();
		
		currentLevel++;
		
		await playerLevelSocket.Set(currentLevel);
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