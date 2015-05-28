using UnityEngine;
using System.Collections;

public class RoomOneZone : MonoBehaviour {
    public bool InsideRoom1 = false;
	// Use this for initialization
	void Start () {	
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            InsideRoom1 = true;
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            InsideRoom1 = false;
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
