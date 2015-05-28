using UnityEngine;
using System.Collections;

public class RoomFourZone : MonoBehaviour {
    public bool InsideRoom4 = false;
	// Use this for initialization
	void Start () {
	
	}
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            InsideRoom4 = true;
        }
    }
    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            InsideRoom4 = false;
        }
    }
	// Update is called once per frame
	void Update () {
	
	}
}
