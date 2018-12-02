 # mb-ai-movement
The steering behaviours were heavily based on the work done by antonpantev here: https://github.com/antonpantev/unity-movement-ai, however since I needed to use it with the 2d physics system instead of the 3d physics system of unity.

My changes:
 * Conversion to use the Box2d physics system that Unity uses for 2d
 * Movement influencers such as Tiles that the 2d object walks over
 * Reacting to circular explosive force.

 ## Usage examples
Under Assets/_mbLibs/mbAiMovement/Examples/Scenes, there are several examples that showcase the steering behaviors in scenes
In the "Moving Tiles" example, left mouse clicks toggle the direction of the tiles, while right mouse clicks create a small pulse that pushes entities away from itself.
Currently I'm developing a game that uses these 2 features here: https://marmaladebacon.com/#/projects/herding-cats

 ## Additional notes
You need the following layers:

User Layer 8:PostProcessing

User Layer 9:mb2dSensors

User Layer 10:mb2dScreenBoundary

User Layer 11:mb2dTiles

User Layer 12:mb2dExtForce

User Layer 13:mb2dObstacle

 ## License
See LICENSE.md

