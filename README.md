# Easy Number

Large number utility for Unity. Serializable struct with inspector-friendly step input and automatic suffix display (K, M, B, T, aa...bz).

## Installation

### Via Unity Package Manager (Git URL)

1. Open **Package Manager** → `+` → **Add package from git URL**
2. Enter:
```
https://github.com/AtilganSak/EasyNumber.git
```

### Via manifest.json

Add to `Packages/manifest.json`:
```json
{
  "dependencies": {
    "com.atilgansak.easynumber": "https://github.com/AtilganSak/EasyNumber.git"
  }
}
```

## Inspector Setup

Set value directly from inspector using the `steps` array. Each element represents a magnitude:

- Index 0 → ones (×1)
- Index 1 → thousands (×1000)
- Index 2 → millions (×1,000,000)

![Inspector](https://github.com/AtilganSak/ProjectImages/blob/main/Easy%20Number/Screenshot_2.png)

> If `steps` array is empty, value defaults to 0.

## Usage

```csharp
// Display
moneyText.text = easyNumber.ToString(); // e.g. "1.5K", "3.2M"

// Arithmetic
easyNumber1 += easyNumber2;
easyNumber1 += 1000;
easyNumber1 *= 2.5;

// Comparison
bool isGreater  = easyNumber1 > easyNumber2;
bool isEnough   = easyNumber1 >= 500;

// Reset
easyNumber.Clear();

// Raw value
double raw = easyNumber.Value;
```

## JSON / Save Support

```csharp
PlayerDB.Instance.money = easyNumber1;
PlayerDB.Instance.Save();

easyNumber2 = PlayerDB.Instance.money;
```

## Supported Suffixes

`""` `K` `M` `B` `T` `aa` → `az` `ba` → `bz` (57 tiers)

## Requirements

- Unity 2019.4+
- No external dependencies

![Example](https://github.com/AtilganSak/ProjectImages/blob/main/Easy%20Number/GIF.gif)
