# Ray Detectors ReadMe

Ray Detectors is part of the Vanilla Unity SDK.

Ray Detectors is a simple system for performing ray-based collider detection and tracking.

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
using Vanilla.RayDetectors;

public class RayDetectorTest : MonoBehaviour
{

    public GameObjectDetector detector;
    
    void Update()
    {
        detector.Detect(rayOrigin: transform.position,
        		                   rayDirection: transform.forward);
    }
}
```

You can enable Ray Detectors debugging by adding the preprocessor DEBUG_RAY_DETECTORS to the Unity Editor scripting define symbols.

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