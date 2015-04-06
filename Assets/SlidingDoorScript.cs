using UnityEngine;
using System.Collections;

public class SlidingDoorScript : MonoBehaviour
{

    private bool IsOpen;
    private bool Opening;
    private bool Closing;

    private Vector3 MoveVector;

    private int MoveMeter = 0;

	// Use this for initialization
	void Start () {
        if (gameObject.name == "Sliding_Door_Left") { MoveVector = new Vector3(-0.1f, 0, 0); }
        if (gameObject.name == "Sliding_Door_Right") { MoveVector = new Vector3(0.1f, 0, 0); }
	}

    void OnTriggerEnter2D(Collider2D collissionobject)
    {
        if (collissionobject.gameObject.tag == "Player")
        {
            Debug.Log("Player Entered - Sliding Doors");
        }
        if (!IsOpen)
        {
            Opening = true;
        }
    }

    void OnTriggerStay2D(Collider2D collissionobject)
    {
        if (collissionobject.gameObject.tag == "Player")
        {
            Debug.Log("Player Staying - Sliding Doors");
        }
        
    }

    void OnTriggerExit2D(Collider2D collissionobject)
    {
        if (collissionobject.gameObject.tag == "Player")
        {
            Debug.Log("Player Leaving - Sliding Doors");
        }
        if (IsOpen)
        {
            Closing = true;
           
        }
    }
	
	// Update is called once per frame
	void Update () {
	    if (Opening)
	    {
	        transform.position += MoveVector;
	        MoveMeter += 1;
	        if (MoveMeter >= 10)
	        {
	            Opening = false;
                IsOpen = true;
	        }
	    }
        if (Closing)
        {
            transform.position -= MoveVector;
            MoveMeter -= 1;
            if (MoveMeter <= 0)
            {
                Closing = false;
                IsOpen = false;
                
            }
        }
	}
}
