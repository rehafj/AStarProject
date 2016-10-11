using UnityEngine;
using System.Collections;

public class Node {
    public bool walkable;
    public Vector3 worldPosition; // (x,y,z )  postion of the object, reminder our y is our z and vise versa 

    //costs used in a star calulations for node values 
    public int hCost; //huristic cost 
	public int gCost;
//	public float fcost;
	//

	public int xGridLocation, yGridLocation, ZGridLocation;

	public int fCost{
	//getter method to set the fcost for a givin node 
		get {
			return hCost+ gCost;
		}
	}

	public Node parentNode; //used to retrace the path 

	public enum  NodeType  { groundNode, airNode, porterNode }
	public NodeType mynode;

	//to calculate the fcost 

	//constuctor of a node class - sets it to walkable / world postions 
    public Node(bool _walkable, Vector3 _worldPos)
    {
        walkable = _walkable;
        worldPosition = _worldPos;
    }

    //overloaded constructor 

	public Node(bool _walkable, Vector3 _worldPos, int _X, int _Y,  int _Z)
    {
        walkable = _walkable;
        worldPosition = _worldPos;
        xGridLocation  = _X;
		yGridLocation  = _Y;
		ZGridLocation = _Z;
	


    }





	public float getFCost(){
	 //basic cost - add method to ovverride it 
		return  gCost + hCost;
	}


	
}
