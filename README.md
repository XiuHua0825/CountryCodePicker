# CountryCodePicker

### Introduction
Create dropdown options to select the country code and show the country flag in Unity.
There are a few things in this simple project.
1. Get multiple sprites from a sprite in code.
2. Get the country code from a JSON file by country name.
You can update the country's information and flag's icon in the asset following the setting.
Of course, you can change any image and data you need with this template for free.
See more detail below.

### Get multiple sprites
There are two files you need:
1. A PNG with all country's flags.
<img src="https://user-images.githubusercontent.com/45157704/196520746-f7e97734-6421-40d4-9b4c-c7a28535e7b8.png" width="500" height="400">

2. A Json with each country's flag position in 1. 's PNG.
<img src="https://user-images.githubusercontent.com/45157704/196520673-e6de91b0-e07f-4de5-b218-2efab9b591e7.png" width="500" height="400">

Main coding:
```
// Get the value of the country's s flag position value from JSON.
CountryFlagInfoList aFlagInfo = JsonUtility.FromJson<CountryFlagInfoList>(iFlagData.text);

// Find the information about the country you want by comparing the country's abbr.
aCountryFlagInfo = aFlagInfo.data.Find(x => x.name == aCodeInfo.data[i].abbr);

// Create a sprite with the flag's position
// The value of sprite size is the range in your PNG also the y's offset.
Sprite.Create(m_Sprite_Flag, new Rect(-aCountryFlagInfo.x, aCountryFlagInfo.y + 360, 24, 24), new Vector2(0.5f, 0.5f))
```
