# Spellcaster
Crazy Wiz-Biz in VR

## Setting Up SteamVR
This experience requires an HTC Vive, SteamVR, and a VR capable computer. Please follow the official setup instructions on the Steam Support website to maximize compatability:  
https://support.steampowered.com/steamvr/HTC_Vive/

## Instructions
Once SteamVR and Spellcaster are running, put on your headset and pick up your right controller. The left controller will track, but is not necessary in-game. Your right controller will appear as a white "magic wand," and spells will spawn from the tip of the wand in the direction your are pointing once you fire them. To cast spells, start by placing your thumb on the right controller's trackpad to bring up the school selection whell, which is divided into six slices that each represent a certain area of magic. To switch into a school, place your thumb on that slice of the trackpad and click down. While there is no immediate feedback for a successfull switch, you may cast a basic "beam" spell by squeezing the controller's grip buttons to verify that you are in the correct school.

Each school has three spells available: The grip-activated beam spell as well as two spells unique to that school that require you to "cast" them by drawing a symbol in the air before they activate. Posters to the left of the spawn area detail all of your available spells and the gesture required to cast them. To start casting, hold down the trigger on the right controller and follow the gesture that corresponds to the spell you want to cast. Start with the controller in the approximate location of the open circle on the gesture diagram, and follow the line to the arrow at the end. Note that every gesture is one solid line and does not require youo let go of the trigger until the end. Once you have completed the gesture, some spells require you to pull the trigger a second time to activate its effect. Try not to switch schools while casting spells, as this may result in VFX objects not despawning which requires a game restart to fix.

## Video demonstration
<a href="http://www.youtube.com/watch?feature=player_embedded&v=NqTP2uY4-wM" target="_blank"><img src="http://img.youtube.com/vi/NqTP2uY4-wM/0.jpg" alt="Watch a demonstration of Spellcaster here." width="240" height="180" border="10"/></a>

## Poster
![alt text][poster]

[poster]: https://i.imgur.com/L74SpXD.png "Poster displayed on the OC"

## Implementation
Please see [../blob/master/Assets/Scripts](../Assets/Scripts) for detailed documentation.

Our program uses AirSig to track user's gestures. When the user pulls down the trigger to record a spell, the AirSig algorithm finds the closest match and runs the corresponding spell we assigned to each gesture. Since there are six schools and we were worried about gesture matching accuracy, every school has it's own gesture manager and a limited number of gestures to check against. To make switching schools easy, we set up the Vive controller touchpads to display a map of where the user needs to click to get to that school.

The spells themselves were written to be as modular as possible so that we could apply them to different schools or pass them different particle and sound effects as needed. Some spells are unique to their school, but most follow a standard procedure:

> 1. Check for a developer defined gesture match  
> 2. Instantiate a new particle effect at the target's or the user's position  
> 3. Play a sound effect  

Most spells are intentionally left in this format to make switching effects easier.

Eight of the eleven gestures used in the game we had to design ourselves, and we asked people with different levels of VR experience to help us record them. We set up our Vive in our residence hall in a way that didn't require people to put on the headset to record and asked them to perform small, medium, and large versions of our gestures at slow, medium, fast, and "excited" speeds. AirSig only required 81 gesture samples to train the algorithm on, but in some cases we provided over 200 samples.

## Spells
Arcane:

Gesture | Spell | Description
--- | --- | ---
TRIANGLE | Magic Missile | Fire a simple yet powerful blast of magical energy.
HOURGLASS | Slow Time| Everything around you moves at half speed for a short time.
BEAM | Magic Beam | Conjure a stream of pure magical energy from your wand.

Fire:  

Gesture | Spell | Description
--- | --- | ---
CLOSEDX|Ignite|Create an alchemical reaction at your target's location to set them ablaze.
C	|Fireball|Purge with fire! Sends a bolt of flame in any direction.
BEAM|Flamethrower|Summon a jet of powerful flames to char anything in its path.

Frost:  

Gesture | Spell | Description
--- | --- | ---
BOTRN|Glacial Impact|Create a fissure along the ground that ends with an icy eruption.
DOWN|Freeze Dry|Eliminates all moisture around the target and freezes them, causing your next spell to deal bonus damage
BEAM|Icicle Crash|A sharp, cold spell that shreds everything in its way. A blizzard in the palm of your hand!

Light:  

Gesture | Spell | Description
--- | --- | ---
C|Illuminate|Command the very light around you to boost the power of your other spells.
BOW|Lightbringer|Summon the Final Spark to seek out and purify your foes.
BEAM|Photonic Ray|Dazzle everyone in front of you with a display of pure chromatic power.

Nature:  

Gesture | Spell | Description
--- | --- | ---
SNAKE|Sidewinder|Send a wave of natural energy along the ground that will also poison your enemy.
HEART|BLOOM!|Mark your target with the Rootcaller's Hex. Fire and Light spells deal increased to Hexed targets.
BEAM|Dragon's Pulse|A beam of mixed macical energy that deals half of its damage as a random school.

Storm:  

Gesture | Spell | Description
--- | --- | ---
LIGHTNING|Call Lightning|Call forth a bolt of the most powerful magic that the storms have to offer.
CLEAVE|Hurricane|Cast forth wheels of enchanted clouds that deal massive damage to anything they touch.
BEAM|Gale Force|Overwhelm everything in front of you with a jet of fierce winds and icy water.

## Asset Packs

**Power Books** was an asset that did not pan out. It worked nearly flawlessly, we had a working book with all the information inside of it. The problem with it was the package did not have a way to manipulate the book within VR, it had options for touchscreen, keyboard, mouse, and UI inputs.  We attempted to create code that would utilize the Vive controller inputs as a manipulator, and based it on the 'keyboard controller' script (which manipulated the book via keyboard input).  In the end the script wasn't completed and we created posters to help the user through the spellcasting experience.  
Unity Asset Store: [Power Books](https://assetstore.unity.com/packages/3d/props/interior/power-books-95500 "Power Books - Asset Store]")

Note: You do not need any of these assets to run the game, only to correctly view the build in Unity.
