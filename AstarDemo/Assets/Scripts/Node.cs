using UnityEngine;
using System.Collections;
/// <summary>
/// Node. class 
/// </summary>
public class Node {
    public bool walkable;
    public Vector3 worldPosition;
     // (x,y,z )  postion of the object, reminder our y is our z and vise versa 
    //costs used in a star calulations for node values 
    public int hCost; //huristic cost - estimated
	public int gCost;  //movement cost 
//	public float fcost;
	//

	public int xGridLocation, yGridLocation, ZGridLocation;

	public int fCost{
	//getter method to set the fcost for a givin node and return it 
		get {
			return hCost+ gCost;
		}
	}

	public Node parentNode; //used to retrace the path 

	public enum  NodeType  { groundNode, airNode, porterNode }
	public NodeType mynode;

	//to calculate the fcost 

	// base constuctor of a node class - sets it to walkable / world postions 
    public Node(bool _walkable, Vector3 _worldPos)
    {
        walkable = _walkable;
        worldPosition = _worldPos;
    }

    //overloaded constructor for xyz values 
	public Node(bool _walkable, Vector3 _worldPos, int _X, int _Y,  int _Z)
    {
        walkable = _walkable;
        worldPosition = _worldPos;
        xGridLocation  = _X;
		yGridLocation  = _Y;
		ZGridLocation = _Z;
	


    }

	//overloaded constructor for xy values alone *2d plane)
	public Node(bool _walkable, Vector2 _worldPos, int _X, int _Y)
    {
        walkable = _walkable;
        worldPosition = new Vector3( _worldPos.x, 1 , worldPosition.y);
        xGridLocation  = _X;
		yGridLocation  = _Y;
	
    }

  


	public float getFCost(){
		return  gCost + hCost;
	}


	
}
