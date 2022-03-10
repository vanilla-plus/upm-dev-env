# JNode

JNode is part of the Vanilla UPM SDK.

JNode offers extra support for C# data classes constructed from Json, especially those intended to represent catalogues.

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

Here's a small example application. Note that JNodeCollection itself inherits from JNode as well.

### Earth.json

```json
{
  "_name": "Earth",
  "_radius": 6371,
  "_population": 7592000000,
  "_surfaceAreaKm": 510100000,
  "_continents": [
    {
      "_name": "Africa",
      "_population": 1300000000,
      "_surfaceAreaKm": 30365000
    },
    {
      "_name": "Asia",
      "_population": 4600000000,
      "_surfaceAreaKm": 44614000
    },
    {
      "_name": "Europe",
      "_population": 750000000,
      "_surfaceAreaKm": 10000000
    },
    {
      "_name": "North America",
      "_population": 580000000,
      "_surfaceAreaKm": 24230000
    },
    {
      "_name": "South America",
      "_population": 420000000,
      "_surfaceAreaKm": 17814000
    },
    {
      "_name": "Antarctica",
      "_population": 0,
      "_surfaceAreaKm": 14200000
    },
    {
      "_name": "Oceania",
      "_population": 42000000,
      "_surfaceAreaKm": 8510900
    }
  ]
}
```

### [Continent_name].json

```json
{
    "_countries":
    [
        ...
    ]
}
```

### MyApp.cs


```csharp
public class Earth : JNodeCollection<Continent>
{

    private string _name;
    public string Name => _name;

    private float _radius;
    public float Radius => _radius;

    private float _population;
    public float Population => _population;

    private float _surfaceAreaKm;
    public float SurfaceAreaKm => _surfaceAreaKm;
    
    private Continent[] _continents = Array.Empty<Continent>();
    public override Continent[] Items => _continents;

    protected override async UniTask ItemAdded(Continent item) =>
    await item.FromWebRequest($"https://vanilla.plus/{item.Name}.json");
}

public class Continent : JNodeCollection<Country>
{
    private string _name;
    public string Name => _name;

    private float _population;
    public float Population => _population;

    private float _surfaceAreaKm;
    public float SurfaceAreaKm => _surfaceAreaKm;
    
    private Country[] _countries = Array.Empty<Country>();
    public override Country[] Items => _countries;
    
    protected override async UniTask ItemAdded(Country item) => Debug.Log($"[{Name}] New country found! [{item.Name}]");
    
}

public class Country : JNode
{
    private string _name;
    public string Name => _name;

    private float _population;
    public float Population => _population;

    private float _surfaceAreaKm;
    public float SurfaceAreaKm => _surfaceAreaKm;
}

public class MyApp : MonoBehaviour
{
    public Earth earth;

    void Awake()
    {
        earth.OnItemAdded += item =>
            Debug.Log($"The continent of [{item.Name}] has been discovered!");

        earth.OnItemRemoved += item =>
            Debug.Log($"The continent of [{item.Name}] has completely disappeared!");
    }

    async void Start() =>
        await earth.FromWebRequest("https://vanilla.plus/Earth.json");
}
```

---

## Debugging

You can enable debugging for this package by including the keywords DEBUG and JNODE in the Unity Editor scripting define symbols.

---

## Contributing
Please don't. I have no idea what a pull request is and at this point I'm too afraid to ask.

If you took this package personally, [let me know](mailto:lucas@vanilla.plus).

---

## Author

Lucas Hehir

---

## License
[The Unlicense](https://unlicense.org/)
