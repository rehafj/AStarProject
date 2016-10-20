using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathFinding : MonoBehaviour {

	Grid mygrid ; 
	public Transform target, Findingbot, t1, t2;
	public List<Node> openList;
	public HashSet<Node> closedList;

	 void  Awake(){

	 mygrid = FindObjectOfType<Grid>();//get a refrence to the grid componenet 

	}

	public void Update(){
		if(Input.GetKeyDown(KeyCode.Space)){
		AstarPathFinding(Findingbot.position, target.position);}
	}
	//, Vector3   tranOne, Vector3 transTwo t1.position, t2.position 
	public void AstarPathFinding( Vector3 start_pos, Vector3 target_pos){

		//Debug.Log("entred startpath");

		Node startNode = mygrid.GetNodeLocation(start_pos);
		Node goalNode = mygrid.GetNodeLocation(target_pos);
		//Node tran1 = mygrid.GetNodeLocation(;
		//kNode tran2;

		//Debug.Log("start node "+ start_pos.ToString());
		//Debug.Log("goal node node "+ target_pos.ToString());
		//Priority queues Open and Closed begin empty. 

		//create a list of open nodes 
		openList = new List<Node>();
		//used hashset as it eliminates duplicates? check this further for better ds 
		 closedList = new HashSet<Node>();
		//Put S into Open with priority f(s) = g(s) + h(s)
		openList.Add(startNode);// add the start node to the list 

		while( openList.Count> 0){
			//Debug.Log("entred openlist loop");
		//i.e. while our open list is not empty 
		//assign the current node to the first element in the open list
			Node temp = openList[0];
			//Debug.Log("assigned temp");
			//go through the open list
			 
			//cehck elemnt in open list's  f cost is less than the cirrent node's fcost 
			// or it is equal to it AND it has less h cost 
			for(int i = 1 ; i < openList.Count ;i++){
				
				//Debug.Log("entred main for loop for open list ");
				//Debug.Log("main loop counter nd the ioen list has "+ openList.Count.ToString());
				if(openList[i].fCost < temp.fCost || openList[i].fCost == temp.fCost 	
				&&  openList[i].hCost < temp.hCost){

					temp = openList[i];
				//	Debug.Log("assigned temp to current!!!");

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

			//Debug.Log("removed temp");

			//check if the current node is = to the  target node
			if(temp == goalNode){

				//return the path!and exit the loop 
				Debug.Log("Found goal node  ");
				Debug.Log("before steoong into get pach back sending starting nide as  ");
				getPathBack(startNode, goalNode);
				return;

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
				//Debug.Log("entred nehghnoot loop in main loop in astar  ");
				if(! nei.walkable || closedList.Contains(nei)){
					//Debug.Log("neigbot is not walkable or in closed list ");
					continue;
				}
				//cost from cuttent - neighbor 
				int newMoveCost = temp.gCost + getDistance(temp, nei);
					Debug.Log("temp initila move cost has value of " + temp.gCost.ToString() + " abd the new movment cost is " + newMoveCost.ToString());
					Debug.Log("intiial naighboors cisr prioe ro new move is "+ nei.fCost);
				if( newMoveCost < nei.gCost || !openList.Contains(nei)){

					nei.gCost = newMoveCost;
					//old h cost calculaitons 
					//**********old working one  nei.hCost = getDistance(nei, goalNode);
					//caluclate new h cost based on nei and goal node 
			//		nei.hCost = calculateNewHcost( nei, goalNode ,  tran1,  tran2);


					//define h
					nei.parentNode = temp;
					//Debug.Log("assigned parent node to temp ");
					if(! openList.Contains(nei)){
						//Debug.Log("adding neighboor to open list  ");
						openList.Add(nei); 

						
					}


				} 

			}
 
//	
		}
		//cretae a list of closed nodes / visted
	}

	public void getPathBack(Node startingNode, Node EndingNode){

		//Debug.Log("entred get path back ");
		List<Node> _path = new List<Node>();
		Node temp = EndingNode;
		Debug.Log("Temp is "+ temp.worldPosition.ToString());
		Debug.Log("endign node is "+ EndingNode.worldPosition.ToString());
		Debug.Log("starting node is   "+ startingNode.worldPosition.ToString());


		while(temp != startingNode){
			Debug.Log("adding nodes as twmo != starting node ");

			_path.Add(temp);
			Debug.Log("adding node   "+ temp.parentNode.worldPosition.ToString() + " added to the list ");
			temp = temp.parentNode;

			}

		//apth is revered so need o reverse it 
		//Debug.Log("Reversing path... ");
		//Debug.Log("the path is "+ path);
		_path.Reverse();

		//need gizmos to visulize it 
		mygrid.path = _path;
		if(mygrid.path!=null){

		Debug.Log("PATH HAS HITEMS ");
		foreach( Node n in _path){

				Debug.Log("the path has node:  "+ n.worldPosition.ToString() + " added to it ");
		}
		Debug.Log("the path has "+ _path.Count.ToString() + "in the list");
		}
		mygrid.DrawPath();


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

					//if( x== 0 && y == 0 )
						//continue; 

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
						//neighboors.Add(mygrid.grid[checkX,checky]);

					}//end 
			}//end of z inner loop 
		}
	}		//reutn list of neghboors 
			//Debug.Log("return neighboors");
			return neighboors;
	}//end of neghboor method 


	public int getDistance( Node a, Node b){


		//Debug.Log("entred get distance");
		//get the gcost  distances 

		int xDistance = (Mathf.Abs(a.xGridLocation - b.xGridLocation))*2; //(weight of horizontal movement is standard)
		int temp = Mathf.Abs(a.yGridLocation - b.yGridLocation);
		int yDistance ;


		//gravity modifications 
		if (b.yGridLocation > a.yGridLocation){
			 yDistance = temp * 4; //Weight of moving up (most expensive)
		} 
		else {
			 yDistance = temp ; //Weight of moving down (cheapest)
		}

		int zDistance = (Mathf.Abs(a.ZGridLocation - b.ZGridLocation))*2;

		/*
		if(xDistance > yDistance)
			return 12 * yDistance + 10 * ( xDistance - yDistance);
		return 14 * xDistance + 10 * (yDistance - xDistance);
		*/
		if(xDistance > zDistance) {
		//	Debug.Log("returned distance calucaltiond from node a to b x > z ");
			return 14 * zDistance + 10 * ( xDistance - zDistance) + 10 * yDistance;
			//Debug.Log("returned distance calucaltiond from node a to b  ");
		}
		//Debug.Log("returned distance calucaltiond from node a to b  ");
		return 14 * xDistance + 10 * ( zDistance - xDistance) + 10 * yDistance;
		//Debug.Log("returned distance calucaltiond from node a to b  ");
	}
	///end of 3d option 


	public int  calculateNewHcost(Node current, Node goal , Node _tran1, Node _tran2){

		int  tranOneDistance, transTwoDistance;

		Node closenode;
		Node farnode ;

		int y1 = Mathf.Abs(current.yGridLocation - _tran1.yGridLocation);
		int x1 = Mathf.Abs(current.xGridLocation - _tran1.xGridLocation);
		int z1= Mathf.Abs(current.ZGridLocation - _tran1.ZGridLocation);

		tranOneDistance= x1+y1+z1;
		int y2 = Mathf.Abs(current.yGridLocation - _tran2.ZGridLocation);
		int x2 = Mathf.Abs(current.xGridLocation - _tran2.ZGridLocation);
		int z2= Mathf.Abs(current.ZGridLocation - _tran2.ZGridLocation);

		tranOneDistance= x1+y1+z1;
		transTwoDistance= x2+y2+z2;

		if (tranOneDistance < transTwoDistance) {
			closenode=_tran1;
			farnode=_tran2;
			}
		else {
			closenode=_tran2;
			farnode=_tran1;}

		int goald, tran1d, tran2d;
		int yc = Mathf.Abs(current.yGridLocation - goal.ZGridLocation);
		int xc = Mathf.Abs(current.xGridLocation - goal.ZGridLocation);
		int zc= Mathf.Abs(current.ZGridLocation - goal.ZGridLocation);

		goald= yc+xc+zc;

		int yt1 = Mathf.Abs(current.yGridLocation - closenode.ZGridLocation);
		int xt1 = Mathf.Abs(current.xGridLocation - closenode.ZGridLocation);
		int zt1= Mathf.Abs(current.ZGridLocation - closenode.ZGridLocation);

		tran1d= yt1+xt1+zt1;

		int yt2 = Mathf.Abs(current.yGridLocation - farnode.ZGridLocation);
		int xt2 = Mathf.Abs(current.xGridLocation - farnode.ZGridLocation);
		int zt2= Mathf.Abs(current.ZGridLocation - farnode.ZGridLocation);

		tran2d= yt2+xt2+zt2;

		if (goald <(tran1d+tran2d) ){
			return goald;
			}
		else{
				return tran1d+tran2d;
			}
    }


	//overloaded method for distance if node is teleported 


	}//end of classs 

