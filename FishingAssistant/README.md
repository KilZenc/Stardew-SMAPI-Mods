## Fishing Assistant
Simple ```Stardew Valley Mod``` which allows you to ```automatically catching fish``` and customize fishing mechanics by making it easier or harder even cheating it. this mod comes with additional features like fish and treasure preview, auto-stop fishing at night or low stamina, and more to come.

## Requirements
[SMAPI - Stardew Modding API](https://www.nexusmods.com/stardewvalley/mods/2400)

## Feature
List of currently available mod features
- Automation mode (cast fishing rod, hook fish, play mini-games, loot treasure).
- Prevent players from fishing when stamina is low.
- Auto-stop fishing at night (time can be changed in the config file).
- Preview current fish and show you if this round has a treasure.
- Configurable cheat in a config file.

## Configuration
The `configuration file` is located on the mod's folder under the StardewValley installation directory, and its automatically created the first time the Game is run with this mod installed.

### EnableModButton 
**Default value:** `F5`
**Possible values:** See `Modding:Player Guide/Key Bindings` on Stardew valley wiki. [Here](https://stardewcommunitywiki.com/Modding:Player_Guide/Key_Bindings#Button_codes)

```
A button for enable this mod, press again for disable.
```

### CastPowerButton  
**Default value:** `F6`
**Possible values:** See `Modding:Player Guide/Key Bindings` on Stardew valley wiki. [Here](https://stardewcommunitywiki.com/Modding:Player_Guide/Key_Bindings#Button_codes)

```
A button for force fishing ro cast power to max, press again for disable.
```

### CatchTreasureButton   
**Default value:** `F7`
**Possible values:** See `Modding:Player Guide/Key Bindings` on Stardew valley wiki. [Here](https://stardewcommunitywiki.com/Modding:Player_Guide/Key_Bindings#Button_codes)

```
A button for toggle catch or ignore treasure when treasure appeare.
```

### ReloadConfigButton    
**Default value:** `F8`
**Possible values:** See `Modding:Player Guide/Key Bindings` on Stardew valley wiki. [Here](https://stardewcommunitywiki.com/Modding:Player_Guide/Key_Bindings#Button_codes)

```
A button for reloading the new configuration value.
```

### AlwaysPerfect     
**Default value:** `false`
**Possible values:** `true | false`

```
Making the result of fishing always perfect, even if it wasn't.
```

### AlwaysFindTreasure      
**Default value:** `false`
**Possible values:** `true | false`

```
Causing you to find a treasure every time you fish.
```

### InstantFishBite       
**Default value:** `false`
**Possible values:** `true | false`

```
Fish will instantly bite when you throw fishing rod.
```

### InstantCatchFish       
**Default value:** `false`
**Possible values:** `true | false`

```
Instantly catch fish when fish hooked.
```

### InstantCatchTreasure        
**Default value:** `false`
**Possible values:** `true | false`

```
Instantly catch treasure when treasure appeare.
```

### InfiniteTackle         
**Default value:** `false`
**Possible values:** `true | false`

```
Make your fishing tackle last long forever.
```

### InfiniteBait          
**Default value:** `false`
**Possible values:** `true | false`

```
Make your fishing bait last long forever.
```

### FishDifficultyMultiplier           
**Default value:** `1`
**Possible values:** `0 - 1 to lower difficulty, or more than 1 to increase it.`

```
A multiplier applied to the fish difficulty.
```

### FishDifficultyAdditive            
**Default value:** `0`
**Possible values:** `< 0 to lower difficulty, or > 0 to increase it.`

```
A value added to the fish difficulty.
```

### AlwaysCatchDoubleFish             
**Default value:** `false`
**Possible values:** `true | false`

```
Make you catch double fish every time
```

### FishDisplayPosition             
**Default value:** `"UpperRight"`
**Possible values:** `"Top" | "UpperRight" | "UpperLeft" | "Bottom" | "LowerRight" | "LowerLeft"`

```
Position to display fish info when playing fishing minigame.
```

### ShowFishName             
**Default value:** `true`
**Possible values:** `true | false`

```
Shows the text of the fish name under the icon.
```

### ShowTreasure              
**Default value:** `true`
**Possible values:** `true | false`

```
Show treasure icon with fish info.
```

### ShowUncaughtFishSpecies               
**Default value:** `false`
**Possible values:** `true | false`

```
Show preview for all fish species, even ones you have never caught.
```

### AlwaysShowLegendaryFish                
**Default value:** `false`
**Possible values:** `true | false`

```
Show preview for legendary fish.
```

### PauseFishingTime                 
**Default value:** `2400`
**Possible values:** `front two-digit represent as an hour in-game.`

```
Time to stop fishing.
```

## Open-Source commitment
This mod is **Open-Source**, that means its code is public, freely-available and covered by an open-source license.

For more details about why OpenSourcing is important on StardewValley mods see the Open-Source Wiki entry on [Stardew Valley Wiki](https://stardewvalleywiki.com/Modding:Open_source).

# Licensing
This mod is licensed under the GNU Lesser General Public License v3.0 License. For more information see [LGPL License details](https://github.com/KilZenc/Stardew-SMAPI-Mods/blob/main/LICENSE)
