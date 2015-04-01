using UnityEngine;
using System.Collections;

public class SlidingDoorScript : MonoBehaviour
{

    private bool IsOpen;

    private Vector3 MoveVector;

	// Use this for initialization
	void Start () {
        if (gameObject.name == "Sliding_Door_Left") { MoveVector = new Vector3(-1, 0, 0); }
        if (gameObject.name == "Sliding_Door_Right") { MoveVector = new Vector3(1, 0, 0); }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player Entered");
        }
        if (!IsOpen)
        {
            transform.position += MoveVector;
            IsOpen = true;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player Staying - Sliding Doors");
        }
        
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player Leaving - Sliding Doors");
        }
        transform.position -= MoveVector;
        IsOpen = false;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
