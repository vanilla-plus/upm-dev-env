# DotNet Extensions

DotNet Extensions is part of the Vanilla Unity SDK.

DotNet Extensions is a collection of extension methods for .NET.

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
public class Card
{

    public Suite suite;

    public Rank rank;

    public Card(Suite suite,
                Rank  rank)
    {
        this.suite = suite;
        this.rank  = rank;
    }

}

public class Deck
{

    public Card[] cards;

    public void Initialize()
    {
        cards = new Card[52];

        for (int s = 0,
                 i = 0;
             s < 4;
             s++)
        {
            for (var r = 1;
                 r < 14;
                 r++, i++)
            {
                cards[i] = new Card(suite: (Suite) s,
                                    rank: (Rank) r);
            }
        }

        cards.Shuffle(); // Randomizes the cards without affecting the array.
    }

}

public enum Suite { Clubs, Spades, Hearts, Diamonds }

public enum Rank { One = 1, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King }
```

---

## Contributing
Please don't. I have no idea what a pull request is and at this point I'm too afraid to ask.

If you hated this package, let me know:

[Gmail](mailto:lucas@vanilla.plus)

---

## Author

Lucas Hehir

---

## License
[The Unlicense](https://unlicense.org/)