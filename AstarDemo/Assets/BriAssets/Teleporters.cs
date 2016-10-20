using UnityEngine;
using System.Collections;
// this code uses the basic tutorial for making teleporters found here: https://www.youtube.com/watch?v=jedJyOyelZM but it has been updated for our A* needs
public class Teleporters : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
	
	}
    //Allows for the cooldown period to actually countdown
    void Update()
    {
        if(coolDown > 0)
        {
            coolDown -= Time.deltaTime;
        }
    }
    public int code;
    public float coolDown = 0;
	// Update is called once per frame
    // makes it so that the teleport spheres will activate on trigger
	void OnTriggerEnter(Collider collider)
    {
        //will only activate if the object is the bot that is using A*
        if(collider.gameObject.name == "bot" && coolDown <= 0 )
        {
            //identifies objects that fall under the Teleporters catergory
            foreach(Teleporters spot in FindObjectsOfType<Teleporters>())
            {
                //uses a code number to match teleporters together, and makes sure that it isn't referencing itself
                if (spot.code == code && spot != this)
                {
                    //cooldown time is used to make sure the bot is not constantly teleporting
                    //below that is the actual movement of the object in the teleporter
                    spot.coolDown = 3;
                    Vector3 position = spot.gameObject.transform.position;
                    collider.gameObject.transform.position = position;
                }
            }
        }
    }
}
