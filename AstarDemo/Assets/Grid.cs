using UnityEngine;
using System.Collections;

//please note, as a basis for our project we have used the tutorials mentioned and linked in class 
// we have improved upon them to meet our needs. and to futher clerfy the task at hand. 
public class Grid : MonoBehaviour {
    public Vector3 gridWorldSize; 
    public float nodeRadius;
    public LayerMask unwalkableMask;
    Node[,,] grid; // three grid array
    float nodeDiameter;

    int gridSizeX;
    int gridSizeY;
    public int gridSizeZ; //this si what determins the layers we will move in ... 
    public int x, y, z;

    public Transform Bot;

    void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter); //creates half the grid / so 10 / 2 = 5 box on x and 5 on y 
		gridSizeZ = Mathf.RoundToInt(gridWorldSize.z / nodeDiameter);//added z pos 
        CreateGrid();
    }

    void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY, gridSizeZ];//increased space
        Vector3 worldBottomLeft = 
        transform.position - Vector3.right * gridWorldSize.x / 2 
        - Vector3.forward * gridWorldSize.y / 2
			;//  - Vector3.up * gridWorldSize.z / 2;

			//loop through all postions 
        for (int x = 0; x < gridSizeX; x++) {
            for (int y= 0; y < gridSizeY;  y++) {
            	for ( z = 0 ; z< gridSizeZ; z++ ){
					Vector3 worldPzoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) +
					 Vector3.forward * (y * nodeDiameter + nodeRadius) + Vector3.up * (z* nodeDiameter + nodeRadius); 
					 //collision check with the world point( vector 2 pos) and the node radous - and defined by unwalkable mask

               		 	 bool walkable = !(Physics.CheckSphere(worldPzoint, nodeRadius,unwalkableMask));
               		 // added a node z to detect 
               		 //populate the grid with nodes 
               		  grid[x, y, z] = new Node(walkable, worldPzoint);

            }}
        }
    }
     
    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, gridWorldSize.z, gridWorldSize.y));
        // draw cubes for refrence 
        if (grid != null){
        	//draw the player/ bot 
			Node botNode = GetNodeLocation( Bot.position);
            foreach (Node node in grid)
            {
            	//short hand for if - is the nide walkbale? yes white else red 
                Gizmos.color = (node.walkable) ? Color.white : Color.red;
               // if the node we are looping through is the bot/player change the color 
               if( botNode == node){
              			Gizmos.color = Color.black;

           	 		  }
                Gizmos.DrawCube(node.worldPosition, Vector3.one * (nodeDiameter - .1f));
            }
        }
    }

    //16 min.
    //retuns te postion of the node in the x,z,y axsis plane //i.e.  in the world 
    public Node GetNodeLocation(Vector3 worldPos)

    //need to get thw postion of the player/ bot 
    //the formual is used from the tutorial series as refrenced by prof.Maccoy 
    //modifed upon .. need to check values and test 
    {	
    //calcakte the precentage and make sure we are in the worl d
    float xprec = (worldPos.x +gridWorldSize.x/2) / gridWorldSize.x;
	float yPrex = (worldPos.z +gridWorldSize.y/2) / gridWorldSize.y;
	float ZPrex = (worldPos.y +gridWorldSize.y/2) / gridWorldSize.y;
	//clamp values between 0 an d1 
		xprec = Mathf.Clamp01(xprec);
		yPrex = Mathf.Clamp01(yPrex);
		ZPrex = Mathf.Clamp01(ZPrex);
	// 
	int x = Mathf.RoundToInt((gridSizeX-1) * xprec);
	int y = Mathf.RoundToInt((gridSizeY-1) * yPrex);
	int z = Mathf.RoundToInt((gridSizeZ-1) * ZPrex);
	//return the postion 
	return grid[x,y,z];

	//yaaaas finnaly fgot it to relize both nodes 
    }

}
