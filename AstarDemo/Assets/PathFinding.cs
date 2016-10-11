using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathFinding : MonoBehaviour {

	Grid mygrid ; 
	public Transform target, Findingbot;

	 void  Awake(){

	 mygrid = FindObjectOfType<Grid>();//get a refrence to the grid componenet 

	}

	public void Update(){

		AstarPathFinding(Findingbot.position, target.position);
	}

	public void AstarPathFinding( Vector3 start_pos, Vector3 target_pos){

		Debug.Log("entred startpath");

		Node startNode = mygrid.GetNodeLocation(start_pos);
		Node goalNode = mygrid.GetNodeLocation(target_pos);

		Debug.Log("start node "+ start_pos.ToString());
		Debug.Log("goal node node "+ target_pos.ToString());
		//Priority queues Open and Closed begin empty. 

		//create a list of open nodes 
		List<Node> openList = new List<Node>();
		//used hashset as it eliminates duplicates? check this further for better ds 
		HashSet<Node> closedList = new HashSet<Node>();
		//Put S into Open with priority f(s) = g(s) + h(s)
		openList.Add(startNode);// add the start node to the list 

		while( openList.Count> 0){
			Debug.Log("entred openlist loop");
		//i.e. while our open list is not empty 
		//assign the current node to the first element in the open list
			Node temp = openList[0];
			Debug.Log("assigned temp");
			//go through the open list
			 
			//cehck elemnt in open list's  f cost is less than the cirrent node's fcost 
			// or it is equal to it AND it has less h cost 
			for(int i = 1 ; i < openList.Count ;i++){
				
				Debug.Log("entred main for loop for open list ");
				Debug.Log("main loop counter nd the ioen list has "+ openList.Count.ToString());
				if(openList[i].fCost < temp.fCost || openList[i].fCost == temp.fCost 	
				&&  openList[i].hCost < temp.hCost){

					temp = openList[i];
					Debug.Log("assigned temp to current!!!");

					//if condition holds - assign current/temp node to new current node
					//if it not the 'start' node itself assign our current node/ tmep to it 
//					if( !temp.Equals(openList[i])){
//
//						temp = openList[i];
//						}
			}

			}
			// remove current node from the open list and add it to the closed list 
			openList.Remove(temp);
			closedList.Add(temp);

			Debug.Log("removed temp");

			//check if the current node is = to the  target node
			if(temp == goalNode){

				//return the path!and exit the loop 
				Debug.Log("Found goal node  ");
				getPathBack(temp, goalNode);

			}

		//for each loop through the neigboots of our temp if ( path is not found) 
			//chec kif the neighboot is not walkable or naibor is in closed list 
			//then move to other neighbor 

			//of the parh to n is shorter OR tthe neighboor not in open list 
			//set f cost 
			// set parrent of n to temp/current pointer 

				//if the  neigbor is not in open list 
				//add it to open 
				//loop back
			foreach (Node nei in getNeighbuors(temp)){
				Debug.Log("entred nehghnoot loop in main loop in astar  ");
				if(! nei.walkable || closedList.Contains(nei)){
					Debug.Log("neigbot is not walkable or in closed list ");
					continue;
				}
				//cost from cuttent - neighbor 
				int newMoveCost = temp.gCost + getDistance(temp, nei);

				if( newMoveCost < nei.gCost || !openList.Contains(nei)){

					nei.gCost = newMoveCost;
					nei.hCost = getDistance(nei, goalNode);
					nei.parentNode = temp;
					Debug.Log("assigned parent node to temp ");
					if(! openList.Contains(nei)){
						Debug.Log("adding neighboor to open list  ");
						openList.Add(nei); 

						
					}


				} 

			}
 
//	
		}
		//cretae a list of closed nodes / visted
	}

	public void getPathBack(Node startingNode, Node EndingNode){

		Debug.Log("entred get path back ");
		List<Node> path = new List<Node>();
		Node ttemp = EndingNode;

		while(ttemp != startingNode){

			path.Add(ttemp);
			ttemp = ttemp.parentNode;
		}

		//apth is revered so need o reverse it 
		Debug.Log("Reversing path... ");
		path.Reverse();

		//need gizmos to visulize it 
		mygrid.path = path;


	}



	// to check neighbors of the array - using list 
	public  List<Node> getNeighbuors(Node node){
		Debug.Log("enred get neghboors method  ");
		//create an empty list for the neghboors 
		List<Node> neighboors = new List<Node>();
		for (int x = -1 ; x<=1 ; x++){

			for (int y = -1 ; y <=1 ; y++){
				for (int z = -1 ; z <=1 ; z++){

					Debug.Log("in neghboots loop  ");

					if( x== 0 && y == 0 && z == 0)
						continue; 

					Debug.Log("managed to continue  ");
					int checkX = node.xGridLocation + x;
					int checky = node.yGridLocation + y;
					int checkz = node.ZGridLocation + z;

					Debug.Log("got temp xyz postions  ");
					if(checkX >= 0 && checkX < mygrid.gridSizeX 
						 && checky >= 0 && checky < mygrid.gridSizeY 
						&& checkz >= 0 && checkz < mygrid.gridSizeZ ){

							//add node to neighboor 
							Debug.Log("managed to add postions to neighboors  ");
							neighboors.Add(mygrid.grid[checkX,checky,checkz]);

					}//end 
			}
		}
	}		//reutn list of neghboors 
			Debug.Log("return neighboors");
			return neighboors;
	}//end of neghboor method 


	int getDistance( Node a, Node b){

		Debug.Log("entred get distance");

		int xDistance = Mathf.Abs(a.xGridLocation = b.xGridLocation);
		int yDistance = Mathf.Abs(a.yGridLocation = b.yGridLocation);
		int zDistance = Mathf.Abs(a.ZGridLocation = b.ZGridLocation);


		if(xDistance > zDistance) {
			Debug.Log("returned distance calucaltiond from node a to b x > z ");
			return 14 * zDistance + 10 * ( xDistance - zDistance) + 10 * yDistance;
			//Debug.Log("returned distance calucaltiond from node a to b  ");
		}
		Debug.Log("returned distance calucaltiond from node a to b  ");
		return 14 * xDistance + 10 * ( zDistance - xDistance) + 10 * yDistance;
		//Debug.Log("returned distance calucaltiond from node a to b  ");
	}



	}//end of classs 




//disclaimer 
//please note, as a basis for our project we have used the tutorials mentioned and linked in class 
// we have improved upon them to meet our needs.  to futher clerfy the task at hand. 