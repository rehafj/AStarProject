# AStarProject
![](/Images/main.gif)

##Overview
Welcome to Astar in space, an implementation of Modified pathfinding using Astar in a 3 dimensional space.
This project includes an open grid-based space that allows multiple routes to reach a given point, along with  gravity considerations that affect pathfinding via the introduction of more “realistic” travel costs. Also included are moveable functional transporters that the bot can pass through.

##Design and Technical Approach
The agent uses Astar and Manhattan distance estimates to move through the gameworld toward its goal. It has an adjustable gravity cost that affects its choice of paths. In the Game world obstacles are present and more can be added, and the agent avoids all of these obstacles in its journey to the goal. All coding was done in C sharp in the Unity environment.

###Brief Summary of Additions 
The agent chooses its path based on the grid, which comprises a framework of nodes. Initially it chooses between a direct path and using the transporter. If the total distance from the bot to the closest teleporter + the other teleporter to the goal is shorter than the distance to the goal from the start, it will take this route as its main path and set the closest transporter as its goal and call the Astar finding method to find the best path to that node. Once it exits the other transporter node it calls the method again with the original ending goal as its new goal.  

![](/Images/porter.gif)

The "more direct" route will be calculated only if it was estimated as the shorter path vs the teleportation route.

![](/Images/optionPlanet.gif)

###Instructions
Move the obstacles, bot, transporters, and goal by selecting the entire folder and using Unity's move tool. Make sure final positions lie within the grid space. To add more obstacles, duplicate whole folders for asteroids, and resize them as desired.
Inside the code document use the inline documentation to find the gravity cost, and adjust if so desired.

To activate it, hit the play button, and then spacebar once.

###Contributors
3D environment setup - Brittany

Initial Astar implementation - Rehaf 

Astar enhancements ( added gravity)  - Rehaf/Samara

Teleportation implementation  -  Brittany/Samara

Inline Doc - Whoever writes the code for that document

Website & management - Samara
