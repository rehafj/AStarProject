using UnityEngine;
using System.Collections;
using System.Collections.Generic;


///***********************************************    Disclaimer **********************************///
//***please note, as a basis for our project we have used the tutorials mentioned and linked in class *//
//*** we have improved upon them to meet our needs. and to futher clerfy the task at hand. ***//
//***https://www.youtube.com/watch?v=T0Qv4-KkAUo&list=PLFt_AvWsXl0cq5Umv3pMC9SPnKjfp9eGW&index=6***//


/// <summary>
/// Grid Class. 
/// this class handles the basic layout for the grid and node reprisntaion on the grid /// 
/// </summary>
public class Grid : MonoBehaviour {


	public Transform[] teleporters = new Transform[2];

	public float speed;

    public Vector3 gridWorldSize;  // x y and z  as vector 3 reprisntaion 
    public float nodeRadius;
	float nodeDiameter;

    public LayerMask unwalkableMask;
  	public  Node[,,] grid; // array of nodes for three varibles 

	public int counter = 0;

    //reprisentaion of x, y and z postions 
    public int gridSizeX;
    public int gridSizeY;
    public int gridSizeZ; 

    //for debugging purposes 
    public Transform Bot;
	public Transform GOAL;

	public GameObject myMovingBot;
	// a path that contains the list of nodes 
    public  List<Node> path;
    [SerializeField]
    public List<Vector3> botPath;
    public Vector3 lastItemInPath;

    /// <summary>
    /// Awake this instance.
    ///set up and create the grid initlizations
    /// </summary>
    void Awake()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter); 
		gridSizeZ = Mathf.RoundToInt(gridWorldSize.z / nodeDiameter); 
        CreateGrid();
    }

    /// <summary>
    /// Creates the grid.
    /// </summary>
    void CreateGrid()
    {
    	//initlize the grid 
		grid = new Node[gridSizeX, gridSizeY, gridSizeZ];

        Vector3 worldBottomLeft = 
        transform.position - Vector3.right * gridWorldSize.x / 2 
        - Vector3.forward * gridWorldSize.y / 2   - Vector3.up * gridWorldSize.z / 2;

			//loop through and create the grid  
        for (int x = 0; x < gridSizeX; x++) {
            for (int y= 0; y < gridSizeY;  y++) {
            	for (int z = 0 ; z< gridSizeZ; z++ ){

            		//get the postion and the bool value and send it with the constuctor -- below 

					Vector3 worldPzoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) +
					// Vector3.forward * (y * nodeDiameter + nodeRadius);
					Vector3.forward * (y * nodeDiameter + nodeRadius) + Vector3.up * (z* nodeDiameter + nodeRadius); 

					 //collision check with the world point( vector 3 pos) and the node radiuos - and defined by unwalkable mask
					 	
               		 	 bool walkable = !(Physics.CheckSphere(worldPzoint, nodeRadius,unwalkableMask));

               		 //populate the grid with nodes 
               		 // constructor set it with the postion and bool value and reprresintaion  
					grid[x, y, z] = new Node(walkable, worldPzoint, x, y, z);


            		}
            }
        }
    }
     
    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, gridWorldSize.z, gridWorldSize.y));

        // draw cubes for refrence  and debugging 

        if (grid != null){
        	//draw the player/ bot  for debugging purpises and gets the location 
			Node botNode = GetNodeLocation( Bot.position);
			Node goalnode = GetNodeLocation(GOAL.position);
																				// loop through the nodes in the grid 
            foreach (Node node in grid)
            {
            	//short hand  if - is the nide walkbale? yes white else red 
                Gizmos.color = (node.walkable) ? Color.white : Color.red;

                	//if there is a path and it is not null 
                 if(path!=null) 
                	if(path.Contains(node)){
                		Debug.Log("drwaing node in black");
                		Gizmos.color = Color.black;
                		Gizmos.DrawCube(node.worldPosition, Vector3.one *(nodeDiameter - .1f));
                		}

                																/// for debugging only- node that contains the bot is green 
				if( botNode == node){
              			Gizmos.color = Color.green;

           	 		  }
           	 	if( goalnode == node){											// node that contains the goal is yellow 
					Gizmos.color = Color.yellow;

           	 	}

                Gizmos.DrawCube(node.worldPosition, Vector3.one * (nodeDiameter - .1f));
                }
            }
        }
    

        


    //retuns te postion of the node in the x,z,y axsis plane //i.e.  in the world 
    /// <summary>
    /// Gets the node location.
    /// </summary>
    /// <returns>a  node with  xyz values .</returns>
    /// <param name="worldPos">World position.</param>
    public Node GetNodeLocation(Vector3 worldPos)
    {	
	    //calcakte the precentage and make sure we are in the world
	    float xprec = (worldPos.x +gridWorldSize.x/2) / gridWorldSize.x;
		float yPrex = (worldPos.z +gridWorldSize.y/2) / gridWorldSize.y;
		float ZPrex = (worldPos.y +gridWorldSize.z/2) / gridWorldSize.z;//note to seld test z and y values. 

		//clamp values between 0 an 1 
			xprec = Mathf.Clamp01(xprec);
			yPrex = Mathf.Clamp01(yPrex);
			ZPrex = Mathf.Clamp01(ZPrex);
		
		int x = Mathf.RoundToInt((gridSizeX-1) * xprec);
		int y = Mathf.RoundToInt((gridSizeY-1) * yPrex);
		int z = Mathf.RoundToInt((gridSizeZ-1) * ZPrex);

		//return node in 3d arr of x y and z values
		return grid[x,y,z];

    }

    /// <summary>
    /// for debugging and checking 
    /// </summary>
	public void DrawPath(){	

	foreach( Node n in path){
		Debug.Log("Added path points " + n.worldPosition.ToString() +"to my vetor 3");
		botPath.Add(n.worldPosition);

		}		
		StopCoroutine("MoveObjAlongPath");
		StartCoroutine("MoveObjAlongPath");
		
		//if (myMovingBot.transform.position!= GOAL.position){

	//
		//	}
		//		for (int i = 0 ; i < path.Count ; i++){
//	
//		botPath[i] = path[i].worldPosition;
//				}

	

		/* debbugin g
		Debug.Log("drawing path");
		if(path == null){

			Debug.Log("error path has 0 items");
		}
		else
		foreach( Node n in path){

			Debug.Log("the path has node:  "+ n.worldPosition.ToString() + " added to it ");
		}
			
			*/
	}

	IEnumerator MoveObjAlongPath(){
		Vector3 temp = botPath[0];

	while(true){
		temp = botPath[counter];
		Debug.Log("bot path is "+ temp);
			
		myMovingBot.transform.position = temp;
			//yield return 5;t of range issue 
			//to fix the bug of index ou
			if(counter == botPath.Count -1)
				break;
		counter++;
			yield return 5;

		}
	/*	while( counter < botPath.Count) {

			Debug.Log("isnide loop of moving object ");
			counter++;
			if (myMovingBot.transform.position ==  temp){
				
				temp = botPath[counter];
				Debug.Log("temp is now  "+ temp);


				}

				transform.position = Vector3.MoveTowards(myMovingBot.transform.position, temp, speed);
				yield return 1;
				
		
			//myMovingBot.transform.position = Vector3.MoveTowards(myMovingBot.transform.position, GOAL.transform.position, speed);
			//yield return new WaitForSeconds(.5f);
	}*///end of wile loop 

	}

}



