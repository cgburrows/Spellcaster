# Spellcaster
Crazy Wiz-Biz in VR

## Setting Up SteamVR
This experience requires an HTC Vive, SteamVR, and a VR capable computer. Please follow the official setup instructions on the Steam Support website to maximize compatability:
https://support.steampowered.com/steamvr/HTC_Vive/

## Instructions
Once SteamVR and Spellcaster are running, put on your headset and pick up your right controller. The left controller will track, but is not necessary in-game. Your right controller will appear as a white "magic wand," and spells will spawn from the tip of the wand in the direction your are pointing once you fire them. To cast spells, start by placing your thumb on the right controller's trackpad to bring up the school selection whell, which is divided into six slices that each represent a certain area of magic. To switch into a school, place your thumb on that slice of the trackpad and click down. While there is no immediate feedback for a successfull switch, you may cast a basic "beam" spell by squeezing the controller's grip buttons to verify that you are in the correct school.

Each school has three spells available: The grip-activated beam spell as well as two spells unique to that school that require you to "cast" them by drawing a symbol in the air before they activate. Posters to the left of the spawn area detail all of your available spells and the gesture required to cast them. To start casting, hold down the trigger on the right controller and follow the gesture that corresponds to the spell you want to cast. Start with the controller in the approximate location of the open circle on the gesture diagram, and follow the line to the arrow at the end. Note that every gesture is one solid line and does not require youo let go of the trigger until the end. Once you have completed the gesture, some spells require you to pull the trigger a second time to activate its effect. Try not to switch schools while casting spells, as this may result in VFX objects not despawning which requires a game restart to fix.

## Video demonstration
<a href="http://www.youtube.com/watch?feature=player_embedded&v=2Tr5sjjO_sk&" target="_blank"><img src="http://img.youtube.com/vi/2Tr5sjjO_sk&/0.jpg" alt="Watch a demonstration of Spellcaster here." width="240" height="180" border="10"/></a>

## Poster
![alt text][poster]

[poster]: https://i.imgur.com/L74SpXD.png "Poster displayed on the OC"

## Implementation

## Asset Packs

**Power Books** was an asset that did not pan out. It worked nearly flawlessly, we had a working book with all the information inside of it. The problem with it was the package did not have a way to manipulate the book within VR, it had options for touchscreen, keyboard, mouse, and UI inputs.  We attempted to create code that would utilize the Vive controller inputs as a manipulator, and based it on the 'keyboard controller' script (which manipulated the book via keyboard input).  In the end the script wasn't completed and we created posters to help the user through the spellcasting experience.  
Unity Asset Store: [Power Books](https://assetstore.unity.com/packages/3d/props/interior/power-books-95500 "Power Books - Asset Store]")

Note: You do not need any of these assets to run the game, only to correctly view the build in Unity.
