using UnityEngine;
using System.Collections;

public class RoomFiveZone : MonoBehaviour {
    public bool InsideRoom5 = false;

	// Use this for initialization
	void Start () {
	
	}
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            InsideRoom5 = true;
        }
    }
    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            InsideRoom5 = false;
        }
    }
	// Update is called once per frame
	void Update () {
	
	}
}
