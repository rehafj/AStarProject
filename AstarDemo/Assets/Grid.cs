using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {
    public Vector3 gridWorldSize; // can this be a vector 3? 
    public float nodeRadius;
    public LayerMask unwalkableMask;
    Node[,,] grid; // three grid array 
    float nodeDiameter;

    int gridSizeX;
    int gridSizeY;

    public int gridSizeZ; //this si what determins the layers we will move in ... 

    public int x, y, z;

    void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
		//gridSizeZ = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);//added z pos 

        CreateGrid();
    }

    void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY, gridSizeZ];//increased space
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2
			;//  - Vector3.up * gridWorldSize.z / 2;

        for (int x = 0; x < gridSizeX; x++) {
            for (int y= 0; y < gridSizeY;  y++) {
            	for ( z = 0 ; z< gridSizeZ; z++ ){
					Vector3 worldPzoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) +
					 Vector3.forward * (y * nodeDiameter + nodeRadius) + Vector3.up * (z* nodeDiameter + nodeRadius); 
               		 bool walkable = !(Physics.CheckSphere(worldPzoint, nodeRadius,unwalkableMask));
               		 // added a node z to detect 
               		  grid[x, y, z] = new Node(walkable, worldPzoint);

            }}
        }
    }
     
    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));

        if (grid != null){
            foreach (Node n in grid)
            {
                Gizmos.color = (n.walkable) ? Color.white : Color.red;
                Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - .1f));
            }
        }
    }
}
