# ElleRealTime
Unity project using [MagicOnion](https://github.com/Cysharp/MagicOnion/), some World of Warcraft models and .Net Core 3

Supports multiplayer and realtime engine.

## Setup to start developing
- Clone the repo
- Open with Unity3D "MagicOnionTest" folder.
- Double click on a .cs file under the "Project" tab in unity. (ex. Assets/Scripts/InitClient.cs)
- If you modify/add an Interface in the ServerShared folder, make sure to generate/update some files by doing so: Click "MagicOnion" in the upper menù of Unity, then click "CodeGenerate". Check in the "Console" tab when it's finished.
- The starting scene is named "LoginForm" and you can find it under "Assets/Scenes"

## Start Playing
- .Net Standard 2.0 .Net Core 3 are required.
- Compile the solution with Visual Studio (i'm using Visual Studio 2019 Community edition).
- Execute .sql files inside Database folder. You can choose between MySql & SqlServer, execute their schema.sql and then execute InitData.sql
- Write config.json file with your data (it's inside "ElleRealTime/ElleRealTime/bin/Debug(or Release)/netcoreapp3.0/") [reference](https://github.com/LuigiElleBalotta/ElleRealTime/wiki/Configuration-file)
- Start the server console 
- Start unity game
- Use your credential by creating an account (see server command [here](https://github.com/LuigiElleBalotta/ElleRealTime/wiki/Commands) or by using test/test or admin/admin)
- Enjoy


## What works
- Registration via server command ( .createaccount username password )
- Login
- Warcraft Camera
- Player movement with animations
- Minimap
- Player Info save (position) + load at next login
- Npc spawn
- Send commands from chat to server

## Planned todos
- Support SqlLite DB (for offline play mode)
- Health 
- Chat


## Links
[MagicOnion](https://github.com/Cysharp/MagicOnion/): Unified Realtime/API Engine for .NET Core and Unity.

[Available commands](https://github.com/LuigiElleBalotta/ElleRealTime/wiki/Commands)


[Warcraft Arena Unity](https://github.com/Reinisch/Warcraft-Arena-Unity/) some models & scripts

## License
All character models, textures and sound are copyrighted by ©2004 Blizzard Entertainment, Inc. All rights reserved. World of Warcraft, Warcraft and Blizzard Entertainment are trademarks or registered trademarks of Blizzard Entertainment, Inc. in the U.S. and/or other countries.

The project itself is [GPL-3](LICENSE).
