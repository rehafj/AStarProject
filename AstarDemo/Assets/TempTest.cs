using UnityEngine;
using System.Collections;


public class TempTest : MonoBehaviour {

public GameObject bot;
public Transform destination;

	void OnTriggerEnter(Collider other) {

		Debug.Log("entred on trigger");

		bot.transform.position = destination.transform.position;
		//code to end path finding and start it again 

    }
}
