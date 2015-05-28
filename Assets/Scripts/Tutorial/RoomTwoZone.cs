using UnityEngine;
using System.Collections;

public class RoomTwoZone : MonoBehaviour {
    public bool InsideRoom2 = false;
	// Use this for initialization
	void Start () {
	
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            InsideRoom2 = true;
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            InsideRoom2 = false;
        }
    }
	// Update is called once per frame
	void Update () {
	
	}
}
