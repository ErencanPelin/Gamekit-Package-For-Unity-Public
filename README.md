# 2D Gamekit Complete Documentation

Notes:<br>
<u>DO NOT MODIFY ANYTHING WITHIN THE PACKAGE FOLDER</u>.
The package was designed to only expose the code & features which should be edited. Changing anything else may cause unexpected behaviours and stop the package from working properly.

For more information, please refer to the [documentation](https://erensoftworks.wordpress.com/documentation/).

<br>Basic setup documentation can be found below, refer to the following links for more detailed documentation.
 - Third party License info can be found in [Third Party Notices](Third%20Party%20Notices.md)
 - Recent changes can be viewed in the [Change Log](CHANGELOG.md).
 - For support, please contact <u>[erenp.business@gmail.com](https://erenp.business@gmail.com)</u>
 - Full documentation can be found at [www.erensoftworks/documentation](https://erensoftworks.wordpress.com/documentation/)
<br><br>

>### What's inside?
>- This package contains ready-made assets & over 50 script components 
which you can easily drag & drop onto GameObjects. Every component has been set up to
automatically adjust relevant settings on the GameObject to reduce errors.
<br><br>
>- This package also contains dozens of custom editors and scene-visualising tools
to make your development process even more streamlined.
<br><br>
>- Custom toolbars & menus have been written to allow you to simply right click and add
template objects into your scene e.g. collectables, checkpoints & enemies.

## Setup
Ensure you have installed the following package dependencies from the Unity Package Manager in case the package does not install these at the same time.
These packages are required by the Game kit in order to function properly. Please read the documentation associated with these independent
packages for further information.
- 2D Pixel Perfect (com.unity.2d.pixel-perfect)
- 2D Tilemap Extras (com.unity.2d.tilemap.extras)
- 2D Tilemap Editor (com.unity.2d.tilemap)
- 2D Sprite (com.unity.2d.sprite)
- Input System (com.unity.inputsystem)

Before you use the package, you should set a few things up to make sure everything works as intended! The steps have been documented below:
2. Go to Edit > Project Settings > Tags & Layers
3. On 'User Layer 6' type: "Standable"
4. Then, on 'User Layer 7' type: "Actor"
5. Your settings should look like this:
   ![alt text](/Docs/tagsLayers.png)
6. Then, Go to Edit > Project Settings > Physics 2D
7. Uncheck 'Queries Hit Triggers'
8. [Optional] Scroll down to the 'Layer Collision Matrix' dropdown
9. Make sure to uncheck 'Actor x Actor' collisions to stop actors (characters) from being able to push or bump into each other
10. Your settings should look like this:
    ![alt text](/Docs/physics2D.png)