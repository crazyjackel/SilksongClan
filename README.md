# Silk Song Clan

Support language: English | [简体中文](https://github.com/crazyjackel/SilksongClan/blob/main/README_zh.md)

Hollow Knight Silksong Mod for Monster Train 2

## Table of Contents
* [Overview](#overview)
* [Core Mechanics](#core-mechanics)
* [Champion: Hornet](#champion-hornet)
* [Units](#units)
* [Spells](#spells)
* [Installation](#installation)
* [Bugs and Feedback](#bugs-and-feedback)
* [Credits](#credits)

## Overview

***They see your beauty, so frail and fine, they see your peace, woven of faith and toil, forget your heart, bound in slumber and servitude, when you wake they shall see your truth, A beast's nature bare to all.***

The Hollow Knight Silksong Clan is a clan based on the recent release of Hollow Knight Silksong. Fight the challenges of Heaven and Hell as Hornet, the ancient protector of Hallownest, and her spunky allies from the kingdom of pharloom. 

Mechanically, the Clan focuses on a few core new mechanics that capture the essence of Hollow Knight's combat and exploration:

## Core Design

- "High Variance; High Reward"
- "Secondary Resource Management"
- "Acrobatic Combat"

## Core Mechanics

### Silk System
Silk is a new resource that represents Hornet's silk-weaving abilities. Cards can channel silk in exchange for great effects.

**How to Gain Silk:**
- **Warrior Trait**: Units with Warrior gain 1 silk when they strike an enemy
- **Silk Spells**: Many spells provide silk when cast
- **Silk Relics**: Various relics can increase your maximum silk capacity
- **Base Capacity**: Start with 12 maximum silk

**How to Spend Silk:**
- **Card Costs**: Many powerful cards channel silk in addition to energy

### Silksong Trigger
**Silksong** is a special trigger that activates whenever you play a card that costs silk. This creates powerful synergies between silk-spending cards and units with Silksong abilities.

### Status Effects

**Imbue**: A temporary buff that provides additional attack at the start of each turn. Lost when the unit takes damage.

**Protection**: A temporary buff that provides additional max health at the start of each turn. Lost when the unit takes damage.

**Permanent Imbue**: A permanent buff that increases attack. Lost when the unit takes damage.

**Permanent Protection**: A permanent buff that increases max health. Lost when the unit takes damage.

**Bind**: Activates a Unit's current Imbue and Protection values.

### Combo System
Certain cards gain additional abilities when played after specific card types:

- **Combo with [Subtype]**: If a card of the specified subtype was played as the previous card, this card gains additional effects
- **Combo with [Card Name]**: If the specific named card was played as the previous card, this card gains additional effects

### Subtypes
- **Bug**: Insectoid creatures from Pharloom
- **Snail**: Wise, spiritual beings that provide support through soul and silk.
- **Flea**: Little critters that have a habit of getting lost. Join them together into a caravan.
- **Pinstress**: Mysterious masters of Combats, guilded throughout pharloom

## Champion: Hornet

Hornet, the princess-protector of Hallownest, serves as the champion for the Silksong Clan. She starts with 20 attack and 12 health, embodying the agile, needle-wielding warrior from the games.

### Upgrade Paths

**Hunter Path**: Focuses on silk-based power scaling and permanent stat growth
- **Hunter I**: +15/+20 stats. Silksong: Gain +5 attack permanently
- **Hunter II**: +30/+30 stats. Silksong: Gain +5 attack and +3 health permanently
- **Hunter III**: +45/+45 stats. Silksong: Gain +7 attack and +5 health permanently

**Cursed Path**: High risk, high reward
- **Cursed**: Cannot equip equipment, reduced max silk, but gains powerful abilities
- **Witch**: +30/+30 stats, Multistrike. On attacking: Apply Melee Weakness 1

**Reaper Path**: Direct damage
- **Reaper**: +30/+10 stats, Trample damage carries over to back units

**Wanderer Path**: Multiple attacks
- **Wanderer**: +10/+10 stats, Multistrike for multiple attacks per turn

**Beast Path**: Lifesteal combat
- **Beast**: +25/+10 stats. Silksong: Gain Lifesteal 2

**Architect Path**: Equipment mastery
- **Architect**: +10/+20 stats, Valor 8. Can equip two additional equipment pieces

**Shaman Path**: Spell power
- **Shaman**: +30/+30 stats, Conduit 10 for spell enhancement

## Units

### Snail Units
- **Chapel Maid**: Combo Snail: Apply Duality. Silksong: Friendly Units get 3 Permanent Protection
- **Caretaker**: Combo Snail: Apply Duality. Ability 2 (Guidance): Channel 2 Silk. Apply Damage Shield 1 to a friendly unit
- **Bell Hermit**: Combo Snail: Apply Duality. Silksong: Friendly Units get 3 Permanent Imbue

### Bug Units
- **Garamond**: Warrior bug with lance ability and combo synergy with Zaza. Combo with Zaza: Gain +10/+10 permanently and Ability 2 (Lance): Attack
- **Zaza**: Humble steed with Valor 6. Combo with Garamond: Gain Ability (Humble Steed): Add a random Equipment to Hand
- **Sherma**: Endless unit that provides protection to friendly units. Resolve: Give Friendly Units 6 Protection. Ability 2 (Caretake): Retreat a friendly unit
- **Flick the Fixer**: Can spend 20 gold to give friendly units 2 Permanent Protection and 2 Permanent Imbue
- **Ballow**: Equipped units give friendly units 7 Imbue
- **Pavo**: Conduit 5. Silksong: Gain Conduit 6
- **Gilly**: Stealth 2, Trample. Silksong: Gain Stealth 2
- **Shakra**: Warrior bug that applies melee weakness to enemies. Strike: Apply Melee Weakness 1 and Advance
- **Pebb**: Can spend 500 gold to gain an Artifact that increases Max Silk by 4. Harvest: Gain 5 gold
- **Grindle**: Ability 2 (Retreat): Retreat. Strike: Gain 10 gold
- **Sprintmaster Swift**: Quick. Resolve: Reduce all Friendly Unit Cooldowns by 1
- **Alchemist Zylotol**: Ability 2 (Inject): Channel 2 Silk. Apply Damage Shield 2 and Permanent Imbue 5 to a friendly unit
- **Green Prince**: Warrior. Silksong: Add an Ephemeral Copy with Fragile to your hand
- **Yarnaby**: Lifesteal 3. Combo Crull and Benjin: Hunger: Front friendly unit gets Lifesteal 1. Strike: Add a random uncommon or rare Morsel to Hand
- **Lumble the Lucky**: Incant: Gain 5 gold. Extinguish: Units get Permanent Protection 3
- **Pinmaster Plinney**: Ability 5 (Sharpen): Spend 30 gold. Give a friendly unit 8 Permanent Imbue
- **Crull and Benjin**: Ability 2 (Feast): Add 2 random uncommon or rare Morsels to Hand. Combo Yarnaby: Hunger: +3/+3
- **Relic Seeker Scrounge**: Equipped: Remove the Equipment. Gain 20 gold. Silksong: Gain 20 gold
- **Nuu**: Lifesteal 2. Harvest: Add an uncommon or rare Morsel to Hand. Hunger: Gain 2 Permanent Imbue and 2 Permanent Protection
- **Twelfth Architect**: Steward. Celebrate: Spend 100 Gold. Gain an Artifact: Units gain Valor 1 on move

### Flea Units
- **Fleamaster Mooshka**: Resolve: Add a random Flea to Hand - Key component for assembling "Fleatopia"
- **Grishkin**: Combo Flea: Shift: Fleas get +1/+1 - A small but mighty flea that grows stronger when working with the Fleamaster
- **Varga**: Combo Fleamaster Mooshka: Rally: Reduce all Friendly Unit Cooldowns by 1. Ability 4 (Wrangle): Channel 2 Silk. Add a random Flea to Hand
- **Kratt**: Ability 1 (Send to Hot Spring): Sacrifice a Flea. Gain 2 Silk. Fleas get +5/+5 and Damage Shield 1
- **Vog**: Harvest: If a Flea Died, Add a random Flea to Hand - A flea that can summon more fleas when others fall in battle

### Pinstress Units
- **Pinstress**: Combo Pinstress: Strike: Gain Conduit 2 - A master weaver who gains magical energy with each strike when working with another Pinstress
- **Seamstress**: Combo Pinstress: Gain Conduit 16 - A skilled weaver who channels magical energy through her work with the Pinstress


## Spells

The Silksong Clan features a diverse array of spells that utilize silk energy and Hollow Knight's signature combat techniques:

### Starter Spells
- **Nail Strike** (1 cost): Deal damage equal to Silk. Gain 1 Silk - A basic attack that channels silk energy into a devastating strike

### Common Spells
- **Swift Step** (1 cost): Give a Unit Protection 10 - A quick defensive maneuver that shields allies from harm
- **Cling Grip** (1 cost): Retreat. Combo: Advance - A defensive grip that can become an offensive advance when combined with other techniques
- **Bind** (0 cost, 6 Silk): Channel 6 Silk. Bind. Retreat. Restore 60 health - A binding technique that restores health and retreats the target

### Uncommon Spells
- **Needle Strike** (2 cost): Deal damage to all enemies equal to Silk. Gain 1 Silk. Combo with Unit: Repeat - A precise strike that pierces through all enemies with silk-enhanced power, repeating if comboed with a unit
- **Clawline** (1 cost): Deal 3x Silk damage to front enemy. Advance the backmost friendly. Gain 1 Silk - A focused strike that channels silk energy into a devastating claw attack
- **Needolin** (2 cost): Give the Front Enemy Unit Dazed 1 - A disorienting needle strike that confuses the enemy

### Rare Spells
- **Silk Soar** (1 cost): Ascend. Combo with Unit: Draw a card - A graceful ascent that draws a card when comboed with a unit
- **Sylphsong** (0 cost, Consume): Set Silk to Max - A powerful song that restores silk to its maximum capacity
- **Elegy of the Deep** (3 cost): Give front friendly unit Dazed 1, Permanent Protection 10, Permanent Imbue 10 - A haunting melody that empowers allies with ancient magic


## Installation
Using a mod manager is recommended. Alternatively, you can perform a manual installation by following the instructions below.

### Dependencies
Make sure you've installed all the dependencies before using the mod. This mod requires:

* [BepInEx-BepInExPack](https://github.com/risk-of-thunder/BepInEx)
* [MT2-Trainworks_Reloaded](https://github.com/Monster-Train-2-Modding-Group/Trainworks-Reloaded)

For each dependency, follow the installation instructions listed there.

### This Mod
Once you've installed all the dependencies, copy this mod (unzipped) into your plugins directory. The file path should look something like `C:\Program Files (x86)\Steam\steamapps\common\Monster Train 2\BepInEx\plugins`.

## Bugs and Feedback
This mod is in active development. As such, you may come across bugs or balance issues. If you do, please report them on the [GitHub Issues page](https://github.com/crazyjackel/SilksongClan/issues).

If you would like to give feedback on the mod (please do!), we'd love to hear your thoughts on:
- Balance and gameplay mechanics
- New unit ideas
- Champion upgrade path suggestions
- Silk system improvements

Any issues or questions, contact us on the [Monster Train 2 Discord channel](https://discord.gg/jfbTdktH).

## Credits
* **Mod Author**: Jackson Levitt / @crazyjackel
* **Hollow Knight**: Team Cherry
* **Monster Train 2**: Shiny Shoe
* **Trainworks Reloaded**: Monster Train 2 Modding Group
* **Simplified Chinese Translation**: lostone-zc (using 磨坊组汉化(Windmill Group) fan translation for Silksong content currently)

### Special Thanks
* The Monster Train 2 modding community for their support
* Team Cherry for creating the incredible Hollow Knight universe
* The Hollow Knight Wiki for providing easy access to game assets for modding
* All playtesters and feedback providers
