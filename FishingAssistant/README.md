## Fishing Assistant
Simple ```Stardew Valley Mod``` which allows you to ```automatically catching fish``` and customize fishing mechanics by making it easier or harder even cheating it. this mod comes with additional features like fish and treasure preview, auto-stop fishing at night or low stamina, and more to come.

## Feature
List of currently available mod features
- Automation mode (cast fishing rod, hook fish, play mini-games, loot treasure).
- Prevent players from fishing when stamina is low.
- Auto-stop fishing at night (time can be changed in the config file).
- Preview current fish and show you if this round has a treasure.
- Configurable cheat in a config file.

## Requirements
SMAPI - Stardew Modding API. [SMAPI Homepage](https://smapi.io/) | [Nexus](https://www.nexusmods.com/stardewvalley/mods/2400)

## Installation

- Decompress the downloaded zip file.
- Move the `FishingAssistant` folder to the `Mods` directory of your StardewValley installation. If you dont have a `Mods` directory, please ensure you have already downloaded and installed **SMAPI** from the [SMAPI Homepage](https://smapi.io/)| [Nexus](https://www.nexusmods.com/stardewvalley/mods/2400) and have launched the game at least once after its installation.

## Usage
When first run, the mod includes a default configuration that disable all cheat. You can enable this mod in-game by pressing `F5` button on keyboard. Force cast power to max by pressing `F6` button on keyboard. Catch or ignore treasure by pressing `F7` button on keyboard and reload the new configuration value by pressing `F8` button on keyboard.

To further tweak the fishing mechanics, you need to edit the `Mod configuration file` located in the `mod's` directory. This file is created automatically once the game has been launched at least once with the mod installed. Please refer to the **Configuration** section for details on how to further tweak the mod.

## Remarks
- This mod doesn't affect the achievements you can get through Steam, fished counts will still increment as normal when a fish is reeled.
- The mod is designed to be compatible with the **Multiplayer/Coop mode** of the game. **This is still being tested**, May have problems playing the animation of the characters on other players.
- Bug report are available at the Nexus mod page [link](https://www.nexusmods.com/stardewvalley/mods/5815?tab=bugs). Bug fixes are based upon availability, priority and problem level 

## Configuration
The `configuration file` is located on the mod's folder under the StardewValley installation directory, and it's automatically created the first time the game runs with this mod installed.

### EnableModButton 
**Default value:** `F5`
**Possible values:** See `Modding:Player Guide/Key Bindings` on Stardew valley wiki. [Here](https://stardewcommunitywiki.com/Modding:Player_Guide/Key_Bindings#Button_codes)

```
A button to enable this mod, press again for disable.
```

### CastPowerButton  
**Default value:** `F6`
**Possible values:** See `Modding:Player Guide/Key Bindings` on Stardew valley wiki. [Here](https://stardewcommunitywiki.com/Modding:Player_Guide/Key_Bindings#Button_codes)

```
A button for force fishing rod cast power to max, press again for disable.
```

### CatchTreasureButton   
**Default value:** `F7`
**Possible values:** See `Modding:Player Guide/Key Bindings` on Stardew valley wiki. [Here](https://stardewcommunitywiki.com/Modding:Player_Guide/Key_Bindings#Button_codes)

```
A button for toggle catch or ignore treasure when treasure appeared.
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
Fish will instantly bite when you throw a fishing rod.
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
Instantly catch treasure when treasure appeared.
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
Make you catch double fish every time.
```

### ModStatusDisplayPosition
**Default value:** `Left`
**Possible values:** `Left | Right`

```
Position of mod status info
```

### DisplayFishInfo
**Default value:** `true`
**Possible values:** `true | false`

```
Should mod show fish info while catching fish?
```

### FishDisplayPosition             
**Default value:** `"UpperRight"`
**Possible values:** `"Top" | "UpperRight" | "UpperLeft" | "Bottom" | "LowerRight" | "LowerLeft"`

```
Position to display fish info when playing a fishing minigame.
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
Show a preview for all fish species, even ones you have never caught.
```

### AlwaysShowLegendaryFish                
**Default value:** `false`
**Possible values:** `true | false`

```
Show a preview for legendary fish.
```

### PauseFishingTime                 
**Default value:** `2400`
**Possible values:** `front two-digit represent as an hour in-game.`

```
Time to stop fishing.
```

## Open-Source commitment
This mod is **Open-Source**, which means its code is public, freely-available, and covered by an open-source license.

For more details about why OpenSourcing is important on StardewValley mods see the Open-Source Wiki entry on [Stardew Valley Wiki](https://stardewvalleywiki.com/Modding:Open_source).

# Licensing
This mod is licensed under the GNU Lesser General Public License v3.0 License. For more information see [LGPL License details](https://github.com/KilZenc/Stardew-SMAPI-Mods/blob/main/LICENSE)
