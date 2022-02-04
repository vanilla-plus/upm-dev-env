# Quit Utility

Quit Utility is part of the Vanilla Unity SDK.

Quit Utility is a basic tool for detecting when a Unity application is quitting.

Believe it or not, there is currently no simple way to ask Unity if the application is in the process of quitting without organizing your own callback. Quit Utility handles this simply and automatically without any extra work, just check if Quit.InProgress is true.

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

Simply check if Quit.InProgress is true or not before doing anything that might fail during a quit.

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