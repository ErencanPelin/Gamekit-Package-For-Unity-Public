# Changelog
All notable changes to this project will be documented in this file.
<br>
The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).
<br>
<!-- in order of newest -> oldest changes -->

## [1.2.1-2] - 2023-01-10
### Changed
- Made Unity Events as part of the health component public so you can now add listeners via code. If this proves successful, other events part of the kit will also be made public

## [1.2.1-1] - 2023-01-09
### Added
- updated missing meta files
### Fixed
- Editor assembly checking for settings folder when it should have been checking for resources folder - should be fixed!

## [1.2.1] - 2023-01-09
### Changed
- Resources now loaded from within packages, AssetDatabase is no longer utilized, - removing all editor dependencies in the runtime assembly, thus removing build errors
### Removed
- GetAssetAtPath function - in line with changes to remove editor code from runtime assemblies and fix build errors

## [1.2.0] - 2023-01-09
### Fixed
- **Gamebreaking Fix:** Asset search class causing errors due to loading resources from incorrect folder paths on runtime. All Runtime files should now be stored in /Resources Folders
- This version of the Gamekit may be incompatible with previous versions. However it is a necessary update.

## [1.1.20] - 2023-01-09
### Fixed
- Editor namespaces left inside monobehaviour scripts causing builds to fail, builds should now work successfully!

## [1.1.19-8] - 2023-01-09
### Added
- New Utility function for Health component to directly be able to set an Actor's health

## [1.1.19-7] - 2023-01-06
### Fixed
- Null reference exceptions being thrown on combat actors who don't have 'DoesTakeDamage' bool toggled to 'true'

## [1.1.19-6] - 2023-01-06
### Changed
- Destroy method in Actor now has customisable seconds to wait before actually destroying the object

## [1.1.19-5] - 2023-01-06
### Fixed
- Referenced health slider in Health components should now be initialised properly on game start

## [1.1.19-4] - 2023-01-06
### Changed
- no more delay when destroying objects via event call

## [1.1.19-3] - 2023-01-06
### Added
- Fixed missing damage dealt variable in Combat Editor script

## [1.1.19-2] - 2023-01-06
### Added
- Destroy function to actor component to make it invoke-able as an event

## [1.1.19-1] - 2023-01-06
### Fixed
- Uncheckable TakeDamage bool in Combat component

## [1.1.19] - 2023-01-06
### Fixed
- Helper text in Gamekit settings pointing to wrong window path
- OnAttack events not being called on shooter combat actors
- Combat now deals relevant attack damage to health component if doesTakeDamage checkbox is ticked
- Shooter particles sometimes spawning behind all layers - should now always be visible on sprite layer 90

## [1.1.18-3] - 2022-12-20
### Fixed
- Readonly anim controller templates - you should now be able to write to them as well!

## [1.1.18] - 2022-12-20
### Added
- New tab in the Unity Editor for everything related to the gamekit - so now its easier to find and change settings!
- Creating new objects now highlights the object!
- Creating new assets using the templates inside the project window also selects the asset for renaming
- Topdown character can now be animated in a 4 directional or 2 directional style, 6 directional animation is currently not yet supported
### Fixed
- Inconsistencies in Create GameObject menu where some objects spawn with '(clone)' in their name
- Topdown template animator controllers being generated with 'PF' instead of 'TD' in their name even though the file itself was correct, should be more consistent now!
- Creating two of the same object templates inside the Project Window now automatically add numeric suffixes to avoid duplicate naming/path errors

## [1.1.17-3] - 2022-12-20
### Fixed
- Package resolving errors with invalid dependencies

## [1.1.17] - 2022-12-20
### Changed
- Update package dependencies

## [1.1.16] - 2022-12-19
### Changed
- Updated README to work better with itch.io users
- Updated Dependencies to provide more information for itch.io users
- Updated Third party notices licenses

## [1.1.15] - 2022-12-19
### Changed
- Updated README
- Some folders renamed and folder hierarchy changed in default assets directory
### Removed
- Demos directory, only use Samples directory now for demo and sample scenes

## [1.1.14] - 2022-12-15
### Changed
- New third party licenses updated in [Third Party Notices.md](Third%20Party%20Notices.md)

## [1.1.13] - 2022-11-30
### Fixed
- referencedLoopHandling exception when trying to save Vectors using the Custom Save Manager
### Changed
- SaveSystem almost entirely re-written, now it is easier to understand and far more expandable. Please refer to the updated documentation on the website 

## [1.1.12] - 2022-11-22
### Changed
- GetValue function has been merged with the GetArray function in the SaveProfile class. Now it's even easier to save and load data!

## [1.1.11] - 2022-11-19
### Added
- some information as to how to use the game kit settings in the game kit settings window
- Discord support link for the game kit under the Help tab
- Title text to Gamekit Settings Window
- Functions under Motion, Combat and Direction to change the target - these functions can also be called as a UnityEvent from the Inspector
- Function inside AssetSearch to retrieve an Asset from the Asset folder
- More in-code documentation and function summaries to make things easier to understand when extending the code
### Changed
- SaveProfile is now sealed class, can no longer inherit this class - it should do everything you need it to, sealed to reduce errors users may cause
- Moved some functions from AssetSearch to private editor class so that users don't break stuff - these methods should only be used by the game kit in the backend, so no need to expose them
- RotateGun() inside Combat component is no longer protected virtual, set to private now, there should not be a need to override this method anyway
### Fixed
- Fixed onDash events not being invoked after the player dashes

## [1.1.10] - 2022-11-18
### Fixed
- no more errors when reloading the Gamekit settings window after scripts compile or after the game is stopped/start etc.

## [1.1.9] - 2022-11-16
### Added
- events for jumping on actor motion
- some more dependencies which were required
### Changed
- some GUI colours just so they stand out a little more to be epic
- Motion and Combat GUI to be less 'cluttered' - now with header groups!
- Can no longer manually assign and create a custom gun to an Actor's combat component. Instead, create a stand gun via the button, and then modify the created gun as you wish
### Fixed
- created gun transform in scene no longer generate with "(clone)"
- wrong animations on level exit template object
- actor with shooter combat not referencing the bullet particles on manually assigned gun
- no more missingReferenceExceptions on unassigned input bindings when changing or unloading a scene

## [1.1.8] - 2022-11-14
### Added
- Effect actor component to handle all particle effects emitted by objects. Call the Play() method and pass a particle system to spawn the effect and play it automatically
### Changed
- Updated the settings interface for the Gamekit Settings window
- General and SceneTransition settings split up into separate categories in the Gamekit settings editor window
- ISceneEffect interface renamed to IEffect, to be easier to understand, and because the behaviour has been elevated to all effects, not only scene effects
### Removed
- Test assembly and interface to reduce potential build errors

## [1.1.7] - 2022-11-13
### Added
- Scene transition color customisation

## [1.1.6] - 2022-11-13
### Added
- More in-code documentation and update documentation on the website
### Removed
- AssetSearch from Editor Assembly, instead reference the one inside Runtime/Utils

## [1.1.5] - 2022-11-13
### Changed
- Gamekit settings is now stored locally in the asset folder instead of the package folder to fix the immutable file warnings

## [1.1.4] - 2022-11-13
### Fixed
- [package.json](package.json) issues stopping package manager from updating the package

## [1.1.3] - 2022-11-13
### Fixed
- [package.json](package.json) issues stopping package manager from updating the package

## [1.1.2] - 2022-11-13
### Changed
- updated [package.json](package.json) to include release date 

## [1.1.1] - 2022-11-13
### Added
- Customisable input bindings in Gamekit Settings

## [1.1.0] - 2022-11-13
### Added
- Gamekit settings to modify universal settings for the game kit
- You can now modify the scene transition style from the game kit settings in Window/Gamekit Settings
- Gamekit Manual in 'Help' toolbar, to easily open the manual from the Unity inspector
### Fixed
- inverted move smoothing - should now be un-inverted, or inverse of the inverted - its fixed

## [1.0.11] - 2022-11-13
### Added
- added a few missing dependencies to the package manifest
- dependencies to package setup post in the README and the website post
### Fixed
- fixed player not 'sticking' to a moving platform which is moving down wards. Now the player should properly stick to the platform and not switch animations as if they're falling
- in air motion not 'continuing', instead the player was being halted in the air when they had no air control. Now the player motion should behave properly while in the air
- resetting attack component made the incorrect attack ray layer be selected

## [1.0.10] - 2022-11-12
### Added
- Demo scenes which is essentially just a copy of the sample scenes, but instead its a visible scene so that the package manager can validate that we have a sample/demo scene
- the package now passes all validation checks for the asset store

## [1.0.9] - 2022-11-12
### Removed
- platformer & topdown development scenes, they weren't being used anyway & were causing package validation issues for the Unity Asset store

## [1.0.8] - 2022-11-12
### Fixed
- curHealth not being reset on playmode on Health component

## [1.0.7] - 2022-11-12
### Fixed
- missing camera component on sample scenes
- Player not moving in sample scenes
- player set to shooter combat in sample scene
- player template missing configurations
- warnings for scene loading in demo scene
- camera perspective in platformer demo scene
- colliders in top down demo scene

## [1.0.6] - 2022-11-12
### Added
- Links and updated package.json so that correct info and resources are displayed in the package manager

## [1.0.5-preview] - 2022-11-12
### Removed
- Samples~ meta file, since the folder is invisible, no need to have a meta file & it reduces the warnings in console

## [1.0.4-preview] - 2022-11-12
### Fixed
- package manager not finding samples

## [1.0.3-preview] - 2022-11-12
### Fixed
- package manager not finding samples

## [1.0.2-preview] - 2022-11-12
### Added
- downloadable samples to project
### Changed
- package back to preview
- Demos back to 'Samples' and added samples to the package manager
### Fixed
- incorrect pixel perfect camera used in samples
- samples referencing incorrect materials
- missing background in platformer sample
- materials referencing URP materials, now should reference normal default materials to reduce install errors

## [1.0.1] - 2022-11-10
### Changed
- Samples renamed to 'Demos' - they won't be available in this release as downloadable samples
- interactables no longer need to be on 'Ignore Raycast' Layer
### Fixed
- Removed "(clone)" from being appended to template objects in the scene
- Shooting is now dependent on when bullets collide with an actor instead of the raycast - gives more realistic shooting combat
- Updated [README](README.md)

## [1.0.0] - 2022-11-9
### Added
- Platformer sample scene
### Changed
- Edited documentation to point to more detailed docs on the website
- Fixed demo scenes
### Fixed
- trigger colliders blocking raycasts - refer to project setup in [README](README.md)
- Template prefabs not very consistent in sprite layers - they should now all be on the right layers and have the right tags to make everything work
### Removed
- Sample~ scenes removed - will be added in a future release, instead there are demo scenes which aren't set as downloadable samples via the package manager

## Unreleased
## [0.7.7 - preview] - 2022-11-7
### Added
- New Tiles for top-down tile set
### Fixed
- you can now add the demo top down player from the Create Gameobject menu
- default movement settings on TopDown character
- top down character not animating properly
- animation inconsistencies between parameter values
- invert direction bool not actually inverting direction - now it should be working as normal!
- default values on platformer character not set to be able to double jump - can now jump epic

## [0.7.6 - preview] - 2022-11-7
### Added
- more documentation on the GitHub Wiki & new documentation on eren.softworks site for Actor Components
### Changed
- handles for modifying moving platform paths
- animation for player is no longer dependent on velocity, instead it is dependent on the input provided by the user
### Fixed
- knockback is now more consistent
- Ground check raycast no longer detects ground on trigger colliders
- Moving Platforms should now carry the player as expected
- Moving platforms no longer glide to their first position when the game is started - instead their first position is their original transform position

## [0.7.5 - preview] - 2022-11-4
### Added
- LevelExit template to create GameObject menu
- Time Stutter to AddComponent Menu
- Camera Shake to AddComponent Menu
- Collectable to AddComponent Menu
- Custom Editor for CameraFollow script
- Custom Editor for TimeStutter script
- Custom Editor for CameraShake script
- Custom Editor for Collectable script
### Changed
- Perform() in ISceneEffect interface is now Play() to align more with Unity's wording & documentation
### Known Issues
- Moving platforms broken

## [0.7.4 - preview] - 2022-11-4
### Removed
- Stats folder and Motion stats script - not used and won't be included in the first release
- Items folder and ItemClass script - was added for the inventory system, but it won't be included in the 1.0.0 release

## [0.7.3 - preview] - 2022-11-4
### Changed
- SceneManager class is now Scenes so that it doesn't conflict with Unity's SceneManager
- Input class is now Inputs so that it doesn't conflict with Unity's Input
### Fixed
- SceneManager class not being able to load scenes correctly
- SceneManager throwing missing reference exceptions due to scene unloading in the background

## [0.7.2 - preview] - 2022-11-3
### Added
- LevelExit component now appears in AddComponentMenu
### Changed
- Actor components now in their own sub category/drop down in the AddComponentMenu
- Merged old component menu scripts with new component menu scripts. - This was a bug left from a previous version after the Tests and Runtime assembly were merged

## [0.7.1 - preview] - 2022-11-2
### Removed
- sample Saving code in Actor controller - instead will be documented

## [0.7.0 - preview] - 2022-11-2
### Added
- data saving and loading using encrypted JSON files

## [0.6.3 - preview] - 2022-11-2
### Added
- respawning & checkpoints
### Fixed
- dash cooldown not actually changing the cooldown time

## [0.6.2 - preview] - 2022-11-2
### Removed
- removed mouse input Type - wasn't being used and doesn't make sense in terms of practicality

## [0.6.1 - preview] - 2022-11-2
### Fixed
- character scaling on direction component

## [0.6.0 - preview] - 2022-11-2
### Added
- targets are automatically assigned the to object with the "Player" tag, if no player is found, the script is disabled to reduce errors
- more detailed error/warning messages on unassigned reference exceptions
### Removed
- mouse cursor target in the middle of the screen - redundant
### Changed
- merged features in Tests assembly to runtime assembly. Versions from this version forward will be incompatible with previous versions less than v0.5
- template prefabs to use new actor components instead of old scripts
- default knockback value for combat component
### Fixed
- dashing now supported on gamepad/controller devices
- unassigned reference exceptions are now properly disabling relative components to reduce errors
### Known Issues
- checkpoints can be collected, but the checkpoints are not being saved anywhere as of yet

## [0.5.16 - preview] - 2022-11-2
### Fixed
- controllers now fully supported
- dashing fully implemented

## [0.5.15 - preview] - 2022-11-2
### Added
- shooter combat functionality
- ability to click and hold for attack for non-AI combat-actors
### Known Issues
- dashing not working
- aiming not supported on controller input

## [0.5.14 - preview] - 2022-11-1
### Added
- majority controller support
- shooter combat type - shooter combat not yet implemented
### Fixed 
- left arrow/movement input not animating the character
### Known issues
- directional aiming/attacking with controller not supported

## [0.5.13 - preview] - 2022-11-1
### Added
- combat knockback upon getting hit
### Fixed
- default double jump thresholds not working - now they should be!
- Attack aiming direction now works properly without casting the ray in the wrong direction at first
### Changed
- changed facingDirectionX value back to original name as the function now makes sense :D
### Removed
- custom script icons in order to remove the gizmos which also appear with that script
- Animation binding code entirely - the system was too unstable in exported/built applications. Instead the animation system will be more rigid but more stable

## [0.5.12 - preview] - 2022-10-30
### Added
- default values to all components
### Removed
- AttackDirectionXY from AttackDirection enum - was pointless
### Changed
- attacks no longer performed if the object being hit is invulnerable
- facingDirection attack type now based on the direction of the actor instead of the movement velocity
### Fixed
- animator transitions sometimes breaking
- combat between 2 actors was not working properly

## [0.5.11 - preview] - 2022-10-29
### Added
- raycast previews for combat raycast
### Removed
- Most documentation in [README.md](README.md) as the old system will be removed and the new version will be re-written
### Fixed
- Public fields not showing up in animation component as bindable option
- Min max slider for double jump threshold now appears properly in the inspector
### Known Issues
- Dashing not implemented
### Deprecated
- Runtime assembly will be fully removed in the next minor update [0.6.0 - preview]

## [0.5.10 - preview] - 2022-10-29
### Added
- Custom attribute [IsAnimatorBindable] which when applied shows the field in the inspector to be binded to an AnimatorController parameter
### Fixed
- Animation component throwing errors when newly added

## [0.5.9 - preview] - 2022-10-28
### Added
- Animation component now properly connected to values in code - can assign animator values directly to values in code
- More tooltips to the rest of the Movement Component
### Known Issues
- Combat raycast preview handle not appearing in scene view
- Dashing still doesn't work
- Double jump editor (min max slider) not appearing correctly in inspector window

## [0.5.8 - preview] - 2022-10-12
### Added
- Ability to loop attacks on AI combat modules - they now attack repeatedly at set intervals. The intervals are determined by the AttackCooldown variable 
### Fixed
- Actors no longer hit themselves, and can hit multiple overlapping enemies at once

## [0.5.7 - preview] - 2022-10-11
### Added
- Custom editor to override default inspector GUI for [Actor](Editor/ActorEditor.cs) & [Animation](Editor/AnimationEditor.cs)
### Changed
- All public variables back to [SerializeField] private to align with encapsulation principles

## [0.5.6 - preview] - 2022-10-11
### Added
- Animation component to handle all Actor animations
- Custom editor for Animation component
- Partly added combat functionality - main functions
### Fixed
- Default values for Health module
- ground cast preview showing up even in top-down movement selected
### Known Issues
- Combat raycast preview handle not appearing in scene view
- Dashing still doesn't work
- Double jump editor (min max slider) not appearing correctly in inspector window7

## [0.5.5 - preview] - 2022-10-10
### Added
- Functionality for new Direction Module
- Scripted documentation 
### Changed
- Editor GUIs to be more intuitive and cleaner for UX
- Creating Template Platformer animator controller no longer also creates a Topdown folder - and visa versa for TopDown animator controllers
### Known Issues
- Dashing still doesn't work
- Default values for Test Components aren't set

## [0.5.4 - preview] - 2022-10-10
### Added
- multi-object editing support for custom editors
- ctrl+z undo and redo support for custom editors
- Ray start offset to RaySettings struct & to corresponding editors
### Fixed
- Naming incorrectness for [Samples](Samples~) folder
### Known Issues
- Dashing doesn't work - currently disabled
- Missing default values for new components under tests

## [0.5.3 - preview] - 2022-10-10
### Changed
- Optional slider for Health module
- Assemblies and namespaces to be more consistent with the package

## [0.5.2 - preview] - 2022-10-9
### Added
- [CollectableBase](Tests/Interactions/CollectableBase.cs) as a base class for all collectables
- Ray gizmos to MovementEditor
  - Gizmos should go into the relevant editor scripts - not within DrawGizmos - this way scripts can stay pure to functionality
- [Health](Tests/Actor/Health.cs) module
- [Direction](Tests/Actor/Direction.cs) module
- Custom Editor for Direction module
- Summaries for all existing scripts in the [Tests Assembly](Tests)
- Copied [Utils/AssetSearch.cs](Tests/Utils/AssetSearch.cs) into [Tests Assembly](Tests)
### Changed
- Custom editors now clamp float values to avoid errors or potential miscalculations in code

## [0.5.1 - preview] - 2022-10-7
### Added 
- [Combat.cs](Tests/Actor/Combat.cs), [Actor.cs](Tests/Actor/Actor.cs), [Movement.cs](Tests/Actor/Movement.cs) Modules under 'Tests' assembly.
- Relevant custom inspector files location in the [Editor](Editor) folder
- [RaySettings](Tests/Actor/RaySettings.cs) struct to hold all editor -related ray settings
### Changed
- Root namespace for [/Runtime](Runtime) folder & [runtime.asmdef](Runtime/com.Eren.2DGamekitComplete.Runtime.asmdef)
- [package.json](package.json) to reflect current project version & resolve merge conflict
### Removed
- [LICENSE.md](LICENSE.md) - unity asset store doesn't like licenses!
### Deprecated
- 'Runtime' assembly will become redundant once all functionality has been passed to 'Tests' assembly. Tests will override and 
in the future [0.6.0] version update. This will be a massive shift in systems & modules. Previous versions **will not work**

## [0.5.0 - preview] - 2022-10-7
### Added
- /Tests/com.Eren.2DGamekitComplete.Tests to re-do some of the core features of the gamekit in future versions to make it more understandable and straightforward - especially for newer developers.
  - Code should be more expandable, yet also compact so that newer developers can simply drag and drop components/assign events
  - /Tests will test features to be more simple and expandable, rather than as modular as they have been so far
- Aim Mouse X (only) module
- Filled in more documentation in [README.md](README.md)
- Shooter combat components to fire bullets with particle systems & detect hits via particle Collisions
### Fixed
- Enemy actors no longer attack/can hit other enemy actors
### Changed
- Function comments to include proper formatted summaries - same for all module classes
- Overrides/private Update functions in Aiming modules inconsistencies
- Re-did some of the colours and names of existing components to be more consistent with other component structures
### Known Issues
- Unity Asset store does not accept third-party licenses, will need to remove the [License](LICENSE.md) before publishing to the asset store
### Deprecated
- [LICENSE.md](LICENSE.md) will be removed in the next update

## [0.4.2 - preview] - 2022-08-10
### Added
- Camera zoom component which can handle zooming on both orthographic & perspective projection type cameras
Zooms into the scene/player using the scrollwheel, input can be configured via the input actions asset
- Ranged combat on click - to handle shooter-type combat
- Scene transitions (currently no ability to customise transition) & Level loader component
- Knockback force, enemies / player can now be knocked back upon being hit
### Fixed
- Level templates being spawned at scene camera position instead of vector3.zero
- Broken scrips on platformer demo actor prefabs
- Every actor automatically being added with "Player" tag
- Spike tiles should be working again
### Removed
- Effects/Particle effects. Particles should now be added as children of the gameobjects they spawn under, and then played only when necessary (Saves performance - caching)
### Known Issues
- Level transitions still not fully implemented
- Enemy actors can hit each other 

## [0.4.1 - preview] - 2022-08-06
### Added
- More components & modules to 'Add Component' bar under ".Eren Kit"
- LevelLoader to handle scene transition (not fully implemented yet)
- Dashing now works on both platformer & topdown character
### Changed
- Spelling & grammar mistakes in LICENSE.md & Third party Notices.md
- Fixed formatting in Third party notices.md
- Removed '(Component Type)' for each Actor module to clean the inspector up a little
- Organised 'Add Component' hierarchy for actor modules to clean up the component menu
- DirectionVelocity component no longer requires a motion component. 
This is to allow the motion to be easily changed among gameObjects without error windows popping up
### Fixed
- Teleporting now fully functional
- LevelTileBase objects' collider not automatically being set to isTrigger
- Actors not automatically setting their sprite order (now they do)
- Changelog & package.json discrepancies
- Checkpoint components should be working now, with public event functions which can be called to send an actor to the last checkpoint
### Known Issues
- LevelLoader & scene transitions not functional

## [0.4.0 - preview] - 2022-08-06
### Added
- URPSamples sample for projects using URP and would like to test the samples with URP lighting & other effects
- Began writing up documentation under README.md. All docs for the package will be found here once finished
- Health Component module
- Icons to each script module for aesthetics and also ease of use within the inspector
- Transitions module to handle smooth level transitions effects
- Effects & Abilities modules. These modules can be placed on gameObjects (you can place more than one) with public functions that can be called via UnityEvents.
- AimingVelocityXY to manage aiming at both X and Y velocity in cases of TopDown character
- Teleporter in-scene component which teleports an actor to a specified location when the actor enters its trigger
### Changed
- README.md now contains a setup/installation guide to help users setup the basic layers & tags required by the package
- Stats folder removed, StatsV2 folder renamed to "Stats" and StatsV2 namespace also renamed to "Stats" - replacing old deprecated system
- Combat base no longer shows gizmo line, instead AimBase handles gizmo rays to visualise pointing direction
- Aim Velocity no longer dependent on Direction module, instead works off motion base module
- AimingVelocity renamed to AimingVelocityX so that it only aims at velocity on the X axis
- .Tiles namespace changes to make more sense since nothing in tiles is related to Interactables. New LevelTile namespace added to store them
### Fixed
- doubleJump yVelocity "incompatible type" error for PF_Actor animator
- Material not found (pink mat) issues when package is imported into a non-URP project
- Formatting in Changelog for readability (recent version headings had incorrect formatting)
- Top down character animator 
- multi-gameObject characters not flipping properly with Direction Modules
### Removed
- /Documentation folder (wasn't being used, completely useless, documentation now in README.md) - online documentation in the form of videos will be coming soon
- Removed Runtime.Actors namespace (obsolete) - some interactables were still within this namespace, this has been fixed
- /Enums folder since ActorType enum become obsolete after updated actor modules
- /Actors & /Stats folder removed (obsolete)
- /Stats folder in both TopDown & Platformer DefaultAssets since they are obselete
- /Events folder removed temporarily to be replaced with a better system in future
### Known Issues
- Checkpoints, & other scene components currently not working properly with the new actor system

## [0.3.1 - preview] - 2022-08-4
### Added
- Target follow & target aim & face target direction components for NPC characters
- Added healthBase component/module to manage & keep track of health. Optional component
### Changed
- Components/Modules can now be added via the add component menu instead of having to drag and drop them manually
### Fixed
- Added double jumping into new modular script version located in /Actor
### Deprecated
- Some editor scripts will be removed along with their target classes
- /Actors and /Stats folder will be removed within the next patch and replaced with newer modular scripts taking over entirely
- /Events folder will be removed within next patch and a new system will replace it in a future feature update

## [0.3.0 - preview] - 2022-08-3
### Added
- Added back Samples~ folder to hold downloadable sample scenes for users to play around with the package's features
- Added stateMachines for player & enemy controllers to use
### Changed
- Replaced old MotionStats.cs with new version. Motion settings within inspector now fits under a single dropdown, with a nested dropdown to handle platformer-specific settings
- Renamed 'Demo' scenes within DefaultAssets folder to DevEnvironments to make more sense that these scenes are purely to test characters & enemies
- LICENSE.md to be clearer with wording used
- Replaced old Actor/Component hierarchy system, old system has been removed & all actors have been upgraded to use the new system
### Deprecated
- Entire /Stats directory is deprecated and the scripts contained in that directory will be replaced with a newer, more expandable version
- /Actors folder is deprecated, and scripts contained will be replaced by scripts in /Actor as they are much more modular and expandable
### Known Issues
- Combat, health as a whole currently does not work & new version of scripts are not created
- New version of scripts do not allow for Dashing or Double jumping currently

## [0.2.0 - preview] - 2022-08-1
### Added
- LevelEnd component to Door object within the PlatformerDemo. Entering the door now resets the level and reloads the scene
- Tooltips for all variables to show in the Unity inspector
- CombatData scriptable object class to allow customisation of combat/weapon stats
- Top down player attack up animation
- Top down player Prefab in DefaultAssets/Templates/Prefabs/TopDown
### Changed
- LICENSE.md to be clearer with termination clauses & key-word definition terms
- Level for PlatformerDemo
- 'Third Party Noticed.md' to be more explicit regarding material obtained via third party contributors
- README.md to include some brief info regarding the package contents
- Motion.cs renamed to MotionStats.cs
- Contact email under package.json changed to new business email: erenp.business@gmail.com
### Removed
- Sample projects & 'Samples~' folder
### Fixed
- Level/world template prefabs for both TopDown & Platformer demo containing tilemap data upon creation
- Tooltips for properties not appearing in inspector window
- Top down character not able to attacked due to 'not being on ground'
### Deprecated
- MotionStats.cs will be replaced by a new version where the sealed classes are going to be replaced by structs to allow motion control to be more specific for each type of game (top down or platformer)
- Entire motion & actor component hierarchy system will be re-configured to make more sense for developers creating either topdown or platformer games by only displaying/using information required for that specific type of game

## [0.1.0 - preview] - 2022-07-31
### Added
- Initial upload to Git
### Deprecated
- Sample projects/resources. Package will contain full contents of both TopDown & Platformer