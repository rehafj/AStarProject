using UnityEngine;
using System.Collections;

public class Node {
    public bool walkable;
    public Vector3 worldPosition; // instead of game object ? 

    //costs used in a star calulations for node values 
    public float hCost; //huristic cost 
	public float gCost;
	public float Fcost;

	public Node parentNode;

	public enum  NodeType  { groundNode, airNode, porterNode }
	NodeType mynode;

	//to calculate the fcost 
	public float getFCost(){
		Fcost=  gCost + hCost; //basic cost - add method to ovverride it 
		return Fcost;
	}

	//constuctor of a node class - sets it to walkable / world postions 
    public Node(bool _walkable, Vector3 _worldPos)
    {
        walkable = _walkable;
        worldPosition = _worldPos;
    }
	
}
