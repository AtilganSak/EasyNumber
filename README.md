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

Set value from inspector using the `steps` array. Each element represents a magnitude:

- `x1` → ×1
- `K`  → ×1,000
- `M`  → ×1,000,000
- `B`  → ×1,000,000,000

**Decimals** field controls how many decimal places are shown in the preview and `ToString()`.

> If `steps` array is empty, value defaults to 0.

## Creating from Code

```csharp
EasyNumber money = 1500;                        // int
EasyNumber money = 1500f;                       // float
EasyNumber money = 1500.0;                      // double
EasyNumber money = EasyNumber.Create(1500);     // decimals = 1 (default)
EasyNumber money = EasyNumber.Create(1500, 2);  // decimals = 2
EasyNumber money = EasyNumber.Zero;             // 0, decimals = 1
```

## Display

```csharp
moneyText.text = money.ToString();      // "1.5K"  (uses Decimals field)
moneyText.text = money.ToString(2);     // "1.50K" (override)
moneyText.text = money.ToString(0);     // "1K"

Necessary.Convert(1500000, 2);          // "1.50M"
```

Suffixes: `""` `K` `M` `B` `T` `aa` → `az` `ba` → `bz` (57 tiers)

## Arithmetic

```csharp
money += 1000;
money -= other;
money *= 2.5f;
money /= 2;
money = -money;
```

## Comparison

```csharp
bool a = money > other;
bool b = money >= 500;
bool c = money == other;
bool d = money != 0;
```

## Utilities

```csharp
// Clamp
money = EasyNumber.Clamp(money, EasyNumber.Zero, maxMoney);

// Lerp (smooth UI transitions)
displayed = EasyNumber.Lerp(displayed, target, 0.1);

// Percent (progress bars)
float fill = (float)money.Percent(maxMoney) / 100f;

// Reset
money.Clear();

// Raw value
double raw = money.Value;
```

## JSON / Save

Works out of the box with **Unity JsonUtility** and **Newtonsoft Json.NET** — `Value` and `Decimals` are public properties.

```csharp
// JsonUtility
string json = JsonUtility.ToJson(playerData);

// Json.NET
string json = JsonConvert.SerializeObject(playerData);
EasyNumber loaded = JsonConvert.DeserializeObject<EasyNumber>(json);
```

## Sorting

```csharp
List<EasyNumber> numbers = ...;
numbers.Sort(new EasyNumberComparer());
```

## Requirements

- Unity 2019.4+
- No external dependencies

![Example](https://github.com/AtilganSak/ProjectImages/blob/main/Easy%20Number/GIF.gif)
