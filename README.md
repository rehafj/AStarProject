# AStarProject
![](/Images/main.gif)

##Overview
For our first project, we propose to implement modified pathfinding using A* in a 3 dimensional space.
Our project will include both open paths that allow multiple routes to reach a given point, along with  gravity considerations that will affect pathfinding via the introduction of more “realistic” travel costs.
In addition, we’d like to include (potentially adjustable) transporters in order to see how the shifted heuristic estimatesOur approach will use a static set of pathfinding nodes throughout the gameworld grid
##Design and Technical Approach
Initially we will have a basic A* implantation done in 3D space.The GameObject/agent implementing the A* algorithm will have four  basic movements: left, right, forward and backward to reach the desired Goal.  As an added feature the agent will move upwards and downwards with a modified cost heuristic due to gravity. In the Game world obstacles will be present, and the agent will avoid all of these obstacles in its journey to the goal. 

#brief summary 
The path will be determined by estimating whatever returns a shorter path based on the grid’s node locations. If the total distance from the bot to the teleporter t1, and teleporter 2 t2 to the goal is shorter then it will take this route as its main path by calling the path a* finding method. 

teleporter one and two are set up by their approximate locations to the bot or goal, where t1 is closer to the bot. 

![](AStarProject/Images/porter.gif)

the other route will be calculated only if the route directly to the goal was estimated as the shorter path vs teleportation route
![](/Images/optionPlanet.gif)

##constraints
## Instructions
## Contributors

