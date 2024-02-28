# Vanilla DeltaValues

DeltaValues is a library for C# in Unity encapsulates common primitive data types like bool, float and int into their own class instances like DeltaBool, DeltaFloat and so on.

Most importantly, these instances have an Action<T> called OnValueChanged emitting only when the payload value changes.

This allows programming flow to change to a delta-based
pattern like react or observables.

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

```csharp
public class PlayArea : MonoBehaviour 
{

    public Transform UserTransform;
    
    public float PlayAreaRadius = 2.5f;

    public DeltaBool UserIsInsidePlayArea;

    void OnEnable() =>  UserIsInsidePlayArea.OnValueChanged += HandleUserIsInsidePlayArea;
    void OnDisable() => UserIsInsidePlayArea.OnValueChanged -= HandleUserIsInsidePlayArea;

    void HandleUserIsInsidePlayArea(bool oldValue, bool newValue)
    {
        if (newValue)
        {
            Debug.Log("User entered play area");        
        }
        else
        {
            Debug.Log("User exited play area");
        }
    }

    void Update() => UserIsInsidePlayArea.Value = Vector3.Distance(UserTransform.Position, Vector3.zero) < PlayAreaRadius;

}
```

---

## Author

This package was lovingly handcrafted at Vanilla Plus by:

- [Lucas Hehir](mailto:lucas@vanilla.plus)

---

## License
[The Unlicense](https://unlicense.org/)
