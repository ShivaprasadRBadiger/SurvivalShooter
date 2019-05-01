# SurvivalShooter
Unity's Survival Shooter AI Behavior Modification

This is Unity's Survival shooter project with following modifications:

**Reloading:** Added reloading system. This would allow the designer to specify a *magazine size* and *reload
delay* in the editor. The gun should fire *magazine size* bullets then take *reload delay*
seconds to reload.
**Scoreboard:** The game does not currently keep track of how many enemies you have
downed. Added endgame summary. This scoring should be integrated into the
existing UI.
**Better AI:** The AI currently just sets the player as a target and moves towards the player.
You are tasked with developing different "personalities" for each of the 3 enemy types.
● ZomBunny - will move at you while staying out of your line of fire. They will
strafe left or right to move out of the way of the current direction your character is facing.
They will still move towards the player. They die
in 2 hits.
● ZomBear - it will die in 4 hits.
● Hellephant - will still move straight at the player. Once it gets within a certain radius it will
stop moving for a time and the Enraged Hellephant should speed up.

