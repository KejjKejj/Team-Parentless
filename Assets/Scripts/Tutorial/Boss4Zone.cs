using UnityEngine;
using System.Collections;

public class Boss4Zone : MonoBehaviour {
    public bool InsideBossRoom;
	// Use this for initialization
	void Start () {
	
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            InsideBossRoom = true;
        }
    }
    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            InsideBossRoom = false;
        }
    }

	// Update is called once per frame
	void Update () {
	
	}
}
