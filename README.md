<h1>Hyperclick</h1>

<i>Integrated Design Document</i>

<a name="_ozhlzon0evnb"></a>Developed by Jayden Everest

## <a name="_h7hmv3r4f0hb"></a>Executive Summary
#### <a name="_fm9s6lkyb1n1"></a>Mission Statement
Create an engaging, mechanically simple game that combines elements from high-speed point-and-click games and bullet hells.
#### <a name="_ioc8n72yyv3i"></a>High Concept
Bullet hells and clicking games are mechanically similar genres, and tend to attract similar audiences; players wanting reflex driven games with incredibly high skill ceilings. However, especially with clicking games, players tend to burn out quickly, due to the intensive clicking and high-precision movements needed.

That’s where Hyperclick comes in. By alternating between dodging bullets with a keyboard and clicking targets with a mouse, the gameplay stays fresh for longer and gives each interaction type short pauses to recover, instead of staying at maximum intensity throughout the game session. This will increase player engagement, by lowering fatigue while keeping the difficulty high.
#### <a name="_aas5kf9742lw"></a>Unique Selling Points
Hyperclick’s core selling point is in how it alternates between its two combat systems. This is a unique mechanic, as most bullet hell games tend to focus purely on dodging projectiles. So, by introducing a target combat system as well, the game gains another layer of depth and mastery. 

Hyperclick’s story will also help differentiate it, by having a uniquely mysterious story that’ll stand out in comparison to other bullet hells, which tend to opt for more simplistic and direct storytelling.
#### <a name="_tr9s2vphzvgr"></a>Brief Gameplay Overview
Hyperclick will take place in a single rectangular room, with a stationary boss in the center. The gameplay loop will involve clicking targets as they appear around the room, while bullets slowly fire from the boss. Clicking a target will increase the action bar, while getting hit by a bullet or missing a target will decrease it. If the bar hits 0 the player dies, and if it reaches a certain threshold the player will advance to the next level, gaining points and increasing the difficulty. Occasionally, the boss will also enter an attack phase, where targets appear much more infrequently, but bullets are fired much faster.

The goal of the game will be to get the highest score possible, by surviving and leveling up as much as possible.

Also, since the game will focus on feature minimalism, through having only two mechanics (Moving and Clicking), adding dialogue would only clutter the gameplay, so won’t be included, except for flavor text at the start of each round (I.E having the boss randomly chose a taunt to say to the player).
#### <a name="_syhm6aqb64ib"></a>Story
Since as previously mentioned, dialogue would only clutter the game, the story elements will instead be told much more subtly, by unlocking secrets through reaching certain highscores. This way, only players actively wanting lore will find it, while the players just wanting to play the game aren’t distracted.

The story will aim to give a backstory to “Schrodinger”, a character from another game being developed by me. So, the lore will consist of brief glimpses and hints into his past. However, even without the other game’s context, the subtle lore hints will help add an air of mystery to the game, which players tend to enjoy. An example of this is Yumi Nikki, which while lacking much text, content, or quality, manages to have a large player base simply through mysterious environments.

## <a name="_sqagekbzmtwa"></a>Project Parameters
#### <a name="_ogdwy3i9nxqq"></a>Constraints
Since the project will be developed in a game development environment that the developer is unfamiliar with, focus will be spent on high-level mechanical refinement, through polishing the difficulty curve and game play aesthetics, instead of low-level improvements, such as adding a large selection of features.
#### <a name="_9oxh84uulxul"></a>Project Duration
27/2/23 - 21/4/23 (8 weeks)
#### <a name="_83zy3l92g7x7"></a>Alpha Due
7/4/23, chosen since it gives 2 extra weeks to fix any bugs or in case there are any other delays in production.
#### <a name="_ktnn5jx9jkzm"></a>Game Engine
Unity 2021 LTS
#### <a name="_x8jk9i149tdw"></a>Target Platform
Windows 10
#### <a name="_wm8vjs72703y"></a>Target Recommended Specifications
Ram: 8 GB

CPU: Intel I7 3770

GPU: RTX 2060 6GB

Resolution: 1080p @ 60hz
#### <a name="_r8ma8ry75977"></a>Primary Programming Language
C#
#### <a name="_pw39nuyvrq9b"></a>Team Size
1 (Jayden Everest)
#### <a name="_tnnms3ni1crg"></a>Project Methodology & Frameworks
The project will be developed using the Agile project management technique, by first developing the entire game to a functional level, and then iteratively improving it for the rest of the project timeline. This method was chosen since the project deadline cannot be changed, making it vital to ensure that the game is at a playable level before refining it.

Since the game will be relatively simple, it won’t be necessary to use any additional programming frameworks, aside from the base Unity framework.
#### <a name="_2zb03va0rtvm"></a>Budget 
All art and programming will be done in-house, meaning the budget will be made up of the total art and programming hours. Music will be sourced from CC0 repositories, meaning it won’t factor into the budget. For the art, it’ll take approximately 20 hours to create everything, which at $30.00 an hour (The NZ average for artists), will cost $600.00. For the programming, it’ll take approximately 50 hours to program everything, which at $35.00 an hour, will cost $1750.00.

This means in total, the game will cost approximately $2350.00 to develop.
#### <a name="_gsbmdiez8x97"></a>Software Used
Unity Game Engine (Development Platform)

Github Desktop (Version Control)

Visual Studio (Programming IDE)

Aseprite (Art)
#### <a name="_ayd20uri5eqp"></a>Target Audience
The game’s target audience will be young adults (14-25 years) already experienced with pc gaming, as this will reduce the need for an in-depth tutorial in the game, since we can assume that the players will already be familiar with keyboard WASD movement. It will target both the bullet-hell and clicker communities, as these both have huge followings, increasing the potential outreach of Hyperclick. Also, games in these genres typically are more simplistic and unpolished, meaning Hyperclick will have lower expectations, and will therefore be able to compete better compared to other genres.

## <a name="_1swsoat75yli"></a>Gameplay Overview
#### <a name="_90km9o675njp"></a>Core Functionality
At its core, Hyperclick is about long lasting, high-intensity gameplay that is easy to learn yet hard to master. Typically, games in this genre have sessions lasting only several minutes, such as in Osu or Touhou, the two case studies for this game.

Hence, the unique hook for this game will be its much longer sessions, with the aim of keeping the players “In the zone” for much longer, ideally for 10+ minutes. It will accomplish this by alternating focus between the player’s two hands. The hand resting on the keyboard will be the focus during dodging intense gameplay, and the mouse hand will be the focus during point-and-click gameplay. This will give each hand low-intensity breaks without compromising the intensity of the rest of the gameplay.
#### <a name="_20a5a8n55yi7"></a>Point-and-click Targets
This mechanic will involve spawning glowing targets randomly throughout the environment, which the player will have to click on within a given timeframe, indicated by a ring around the target. Initially, only one target will be on the field at any given time, and there will be a long timeframe to hit it. However, as the player levels up more targets will appear at any given time, with shorter timeframes. Given that there will be many bullets scattered throughout the environment as well, it will be important to add lots of visual emphasis on each target, so that the player notices them.
#### <a name="_jx6watm5vwnk"></a>Movement
This mechanic will involve using WASD keyboard movement to move the player up, down, left, and right throughout the environment. It will need to be tight and responsive to control, and the player should be able to traverse the environment quickly using this mechanic.
#### <a name="_61fnzu1jpdbl"></a>Leaderboard
Since there is no clear story in the game, a leaderboard will be used instead to motivate the player. On the leaderboard will be the player (Highlighted in blue and given a generic name), as well as 9 challengers. Each challenger will have a different level assigned to them, which can be used to add different challenge difficulties (E.G beating challenger 1 could require the user to get to level 4, which could be relatively easy, while challenger 9 could require level 11, which could be made much harder to get). This adds an optional challenge to the player, by encouraging them to level up enough to rank higher than the challengers. 
#### <a name="_gfvcjouknoes"></a>Dodging (Bullet Firing System)
This mechanic will involve spawning bullets from the bosses position, either towards the player or in hardcoded patterns. As the player levels up, bullets will move quicker and spawn in larger quantities. This mechanic can also be used to force the player to move throughout the environment, or lock off certain areas, by creating long streams of bullets in a given direction.

There will also be several varieties of bullets. Yellow bullets will fire in a straight line, and rebound off walls twice. Pink bullets have a slight homing effect applied to them, so they move towards the player's direction. These bullet types will be intermixed throughout the levels, to add variety to the gameplay.
#### <a name="_aiv25gl89qpf"></a>Action Bar & Levels
Throughout the gameplay session, the player will have an action bar visible to them, which dictates both their health and level. Initially, the player will spawn with a set action score, e.g 3. Missing targets, or getting hit by bullets will decrease the score, and if it reaches 0 the player dies. Contrarily, every target hit will increase the score by 1, and if it reaches a given threshold, the player will level up, reducing their action score and increasing the gameplay difficulty. This threshold will increase with each level. The action bar will also control whether the game is in dodging intense mode, or target intense mode, by changing between them once certain score thresholds are met.

Note that the action score has to reduce every level up, as bullets won’t scale in damage, meaning eventually the player would become a bullet sponge with the amount of score they’d have built up.
#### <a name="_wx34361tqzvo"></a>Story
When the player progresses on the leaderboard, a hidden button will become available on the start menu, which will display any unlocked lore to the player. This means to unlock the entire game’s story, the player will need to become no.1 on the leaderboard.
#### <a name="_qjhzfz46hv"></a>Schedule
So that the project is completed on time, each task will be given a deadline (Seen below), with the most important tasks, such as movement, being developed first, and less important tasks, like the story, being developed last. Each task has also been given a priority level, so if the project starts to run behind schedule, it’ll be easier to identify which tasks can be delayed. Lastly, each task has a department assigned to it, which indicates what aspect the feature relates to, such as “Art” or “Programming”.


|**Feature**|**Deadline**|**Priority**|**Department**|
| :-: | :-: | :-: | :-: |
|Movement|4/3/23|HIGH|Programming|
|Boss Sprites|7/3/23|MEDIUM|Art|
|Bullet Firing System|12/3/23|HIGH|Programming|
|Arena Sprite|13/3/23|MEDIUM|Art|
|Action Bar|14/3/23|HIGH|Programming|
|Levels|17/3/23|MEDIUM|Programming|
|Menus|21/3/23|MEDIUM|Art|
|Player Sprites|22/3/23|MEDIUM|Art|
|Targets|24/3/23|HIGH|Programming|
|Story|26/3/23|LOW|Writing|
|Leaderboard|29/3/23|LOW|Programming|
###### <a name="_qbi9summb7q"></a>*Deadlines intentionally ~2 weeks extra time till the alpha due date, to account for expected production delays due to the developer having no experience with the Unity Game Engine.*
## <a name="_y1hy8ccz193x"></a>UI
#### <a name="_x6kio1y5odhl"></a>Philosophy
Bullet hell games tend to have arcade-esc designs, with thick buttons, vibrant colors, and stylized splash texts. As such, Hyperclick will have a similar design, with each menu page containing a splash text, along with thick buttons and pixelated fonts. All text and buttons will also use vibrant colors matching the current splash text color scheme. All menu screens will also be non-diegetic, with a much bigger emphasis on readability and speed of use. This is because, since the game is a bullet hell, this means the player will die much more frequently. As such, making the menu navigation from death screen to respawning as quick as possible is important.

For the in-game HUD, since a large aspect of the game will involve clicking targets that spawn anywhere in the arena, having any UI elements that obstruct that interaction would be a massive problem. As such, the level counter and action bar will be positioned outside of the arena environment.

## <a name="_2y2xxf70p6lp"></a>References
###### <a name="_mojptol1m19j"></a>*Games*
Osu - github.com/ppy/osu

Touhou Project - en.wikipedia.org/wiki/Touhou\_Project

Yumi Nikki - store.steampowered.com/app/650700/Yume\_Nikki
###### <a name="_8uwnp64ch4hx"></a>*Software*
Unity - unity.com

Aseprite - aseprite.org

Github Desktop - github.com

Visual Studio - visualstudio.microsoft.com
