# CyberStories
A serious game in virtual reality to learn about good practices for cybersecurity in companies.

## Requirements
- A computer
- A virtual reality head set : HTC Vive or Oculus Rift

## Features
- Office environment
- Dialogues with NPCs
- Quests & choices
- A mark is given to you at the end of the game
- A website which allows you to :
	- download the game
	- compare your score with other players
	- master a game session

## Development
### Folders organization
#### Top level organization
- ```~/CyberStories/Asset Packages```: a backup for each downloaded asset package is saved in this folder.
It is not visible in the Unity project view, then if you need to grab something in there you may just
copy-paste it to where you need it. Each new downloaded asset package should be backed up in this folder.
-  ```~/Cyberstories/Assets```: contains all game assets.

#### Folder naming conventions
Since most libraries use it, the folder naming convention for this project is :
> words in lower case beginning with a capital letter, separated by spaces.

Examples:
- Base Office
- Clock
- Office Supplies

Note that naming a folder 'Resources' anywhere will make all of its content to be shiped with the build, whether it is used in a scene or not.
Use it only for dynamically loaded assets. See : https://docs.unity3d.com/Manual/SpecialFolders.html
#### Assets folder organization
This organization is based on recommendations from Unity's website: https://unity3d.com/fr/learn/tutorials/topics/tips/large-project-organisation
Assets folder contains two kinds of subfolders :
- Scene folders
- Special folders

##### Scene folders
A scene folder contains a scene file and all its dependencies.
Dependencies are organized in folders with the following names and file types:
- **Audio**: contains audio files
- **Materials**: contains .mat files
- **Models**: contains 3D models files (.blend, .fbx, .obj)
- **Prefabs**: contains .prefab files
- **Scripts**: contains .cs script files
- **Shaders**: contains shader files
- **Textures**: contains textures and any type of image file (light maps, directional light maps, specular maps, normal maps, cube maps, ...)

Note that they are all plural names.
Additional folders may be added to group related assets.
Here are some examples of typical scene folder organization :
<pre>
Office/
	+-Office.unity
	+-Base Office/
	|   +-Materials/
	|   |   +-wood.mat
	|   |   +-carpet.mat
	|   |   +-glass.mat
	|   +-Models/
	|   |   +-window.fbx
	|   |   +-steel_chair.fbx
	|   |   +-floor.fbx
	|   +-Prefabs/
	|   |   +-office.prefab
	|   |   +-window.prefab
	|   |   +-floor.prefab
	|   |   +-steel_chair.prefab
	|   +-Textures/
	|	    +-wood.png
	|	    +-window.png
	+-Cars/
		+-Materials/
		|	+-iron.mat
		+-Models/
		|	+-car_1.fbx
		|	+-car_2.fbx
		+-Prefabs/
		|	+-car_1.prefab
		|	+-car_2.prefab
		+-Scripts/
		|	+-Path/
		|	|	+-path.cs
		|	+-Car/
		|		+-car_wheel.cs
		|		+-car_engine.cs
		+-Textures/
			+-car_1.png
			+-car_2.png
</pre>
<pre>
Menu/
    +-Menu.unity
    +-Materials/
	|	+-transparent.mat
	+-Prefabs/
	|	+-menu.prefab
	+-Texutes/
		+-background.png
</pre>
##### Special folders
Additionaly, Assets folder contains the following special folders:
- **Prototypes**: stores scene folders that are not included [yet] in production and asset packages that are likely to be used in the future.
- **Shared**: stores scene folders that are/will be shared among multiple scenes, and virtual reality plugins.

### Unity version
2019.1.6f1

## Licence
Unlicensed.

## Authors
- Maxime Lacombe
- Emmanuel Meric de Bellefon
- Laxman Thayalan
- Axel Vigny
