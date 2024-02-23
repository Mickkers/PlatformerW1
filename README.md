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

## Objects

## Traps

## Other Systems
