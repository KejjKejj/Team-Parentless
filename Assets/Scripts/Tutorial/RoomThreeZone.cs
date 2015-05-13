using UnityEngine;
using System.Collections;

public class RoomThreeZone : MonoBehaviour {
    public bool InsideRoom3 = false;
	// Use this for initialization
	void Start () {
	
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            InsideRoom3 = true;
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            InsideRoom3 = false;
        }
    }
	// Update is called once per frame
	void Update () {
	
	}
}
