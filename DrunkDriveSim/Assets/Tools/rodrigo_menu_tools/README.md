Created by Rodrigo Lopez Moreira Mazzacotte and Sergei Voronov

Credits: 
Rodrigo Lopez Moreira Mazzacote: Initial main menu and pause menu structure (Main canvas, buttons, submenus, prefabs);
Sergei Voronov: project organising, music randomizer feature (script, scene hierarchy, prefab, readme), documentation. 

How to use: 
1) Create Unity project;
2) Import main menu tool package using Unity "import custom package" feature;
3) To use main menu tool create new main menu scene (or feel free to make a copy of main menu sample scene), add StartMenu prefab from Runtime/Prefabs folder. Customize the title of game (and other titles as well) by finding "<...>title" object in StartMenu prefab hierarchy. Feel free to change any assets you would like to. Package is fully flexible. Add also LevelLoader Prefab in case if you want to use fade effect for more polished look of your game;'
4) To use PauseMenu tool add the PauseMenu prefab from Runtime/Prefabs folder to your actual level scene (so to all your level scenes if you have multiple). Same as StartMenu tool, it is fully flexible so feel free to edit it as you want; 
5) Bonus feature: Music_randomizer tool which exists as part of MainMenu prefab and individual Music_Randomizer prefab both. To use Music_Randomizer tool copy the Music_randomizer prefab from Runtime/Prefabs folder to any scene you want to use the tool in. To randomize your music that would play during the gameplay add as many tracks as you want to "List of music tracks" list in Random Music Player subwindow in Music_Randomizer Inspector window.
6) Don't forget to add all scenes you would to open to the Scene List in the Unity Build Profiles menu. 
