# AStarProject
![alt tag](AStarProject/Images/optionPlanet.gif)

##Overview
For our first project, we propose to implement modified pathfinding using A* in a 3 dimensional space.
Our project will include both open paths that allow multiple routes to reach a given point, along with  gravity considerations that will affect pathfinding via the introduction of more “realistic” travel costs.
In addition, we’d like to include (potentially adjustable) transporters in order to see how the shifted heuristic estimatesOur approach will use a static set of pathfinding nodes throughout the gameworld grid
##Design and Technical Approach
Initially we will have a basic A* implantation done in 3D space.The GameObject/agent implementing the A* algorithm will have four  basic movements: left, right, forward and backward to reach the desired Goal.  As an added feature the agent will move upwards and downwards with a modified cost heuristic due to gravity. In the Game world obstacles will be present, and the agent will avoid all of these obstacles in its journey to the goal. 

##constraints
## Instructions
## Contributors

