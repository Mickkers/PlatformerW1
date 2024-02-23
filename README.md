# Platformer
[Play The Game On Itch.Io](https://mickkers.itch.io/platformer)
## Documentation Contents
- [Player](#player)
- [Enemy](#enemy)
- [Objects](#objects)
- [Traps](#traps)
- [Other Systems](#other-systems)
## Player
The player has health and is able to move, jump (including double jumping, and wall sliding & wall jumping), dash, and attack.
### Input Manager
The InputManager class acts as a middleman, handling all of the player's inputs and passing values and calling functions in other classes when needed.
### Player Movement
The PlayerMovement class contains code for all of the player's movement capabilities. This includes horizontal movement, jumps & double jumps, wall slides & jumps, and the dash ability.
### Player Health
The PlayerHealth class contains code relevant to the player's health. This includes the player's current health, invincibility frames, and death sequence. THis class also has functions needed to heal the player and calculate health percentage information for the game's UI.
### Player Attack
The PlayerAttack class is responsible for finding an available fireball from the pool of player projectiles and launching it in the direction the player is facing.
### Player Respawn
The PlayerRespawn class is responsible for resetting the player's attributes and position to the last collected checkpoint when the player is out of health and respawns.
## Enemy
Enemies have health and patrol a predefined area, they chase the player if the player is within the patrol area. Enemies attack the player if the player is inside the attack range.
### Enemy Health
The EnemyHealth class contains an enemy object's health attributes and death sequence.
### Enemy Movement
The EnemyMovement class contains logic for the eneme object's patrolling and chasing behvaiours.
### Enemy Attack
Enemies are able to attack the player in two ways, melee and ranged, depending on their variant. The EnemyAttack class is an abstract class mainly containing logic to check if a player is within an enemy's attack range. The melee enemy's attack actions are in the **EnemyMelee** child class. The ranged enemy's attack action are in the **EnemeyRange** child class.
## Objects
### Checkpoint
Checkpoints are activated by the player when passing through them. Checkpoints serve as respawn points when the player runs out of health. Players have to collect all checkpoints to be able to use the goal and continue to the next level.
### Goal
The GOal object moves the player onto the next level when all checkpoints are collected.
### Heal Fruit
The heal fruit is a collectible that heals the player for 1 hitpoint.
### Fan
Fans are eviromental objects that push the player upwards.
## Traps
All traps damage the player through the DamagePlayer class, if a trap uses an additional class it is only to regulate the traps behaviour of when it can damage the player.
### Spikes
Spikes are set on the ground and damage the player if touched by the player.
### Saw
Saws move back and forth within a predefined area.
### Fire Trap
The firetrap damages the enemy when it is on, it has downtime and startup time before it can damage the player.
## Other Systems
### Game Manager
The GameManager class manages amount of respawns left & triggers the gameover state when out of respawns, and the player's progress in collecting checkpoints.
### Audio System
The game's background music and sound effects are controlled by the **AudioController** class, when a sound effect is needed an object can call for it through the audio controller class. 
<br>
Sound settings are handled by the **SoundSettings** class.
### User Interface
Different user interfaces use different classes (i.e. MainMenuUI, GameplayUI, etc.), during gameplay, the UIManager class is used to transition between the gameplay UI, pause UI, and gameover UI.
### Projectiles
Projectiles used by the player (fireballs) and enemies (spikeballs) are both derived from an abstract class Projectile. The PlayerProjeectile and EnemyProjectile child class is used to regulate the projectiles behaviour on collision with a target.
