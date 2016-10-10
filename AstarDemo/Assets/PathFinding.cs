using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathFinding : MonoBehaviour {

	Grid mygrid ;

	 void  Awake(){

	 mygrid = FindObjectOfType<Grid>();//get a refrence to the grid componenet 

	}

	public void AstarPathFinding( Vector3 start_pos, Vector3 end_pos){

	Node startNode = mygrid.GetNodeLocation(start_pos);
	Node goalNode = mygrid.GetNodeLocation(end_pos);

		//create a list of open nodes 
		List<Node> openList = new List<Node>();
		//used hashset as it eliminates duplicates? check this further for better ds 
		HashSet<Node> closedList = new HashSet<Node>();

		openList.Add(startNode);// add the start node to the list 

		while( openList.Count> 0){
		//i.e. while our open list is not empty 
			Node temp = openList[0]; //assign the current node to the first element in the open list 
			for( int i = 1 ; i < openList.Count ; i++){ //loop throgh all the nodes in the open list 
				//start counting fromt he first element and check the its f cost ( i = 1 untill the list ends) 
				if(openList[i].fCost< temp.fCost){


				}
				//if(openList[i].getFCost()< temp.getFCost()){}
			}


		}
		//cretae a list of closed nodes / visted
	}



}
//disclaimer 
//please note, as a basis for our project we have used the tutorials mentioned and linked in class 
// we have improved upon them to meet our needs. and to futher clerfy the task at hand. 