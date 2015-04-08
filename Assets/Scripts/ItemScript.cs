using UnityEngine;
using System.Collections;

public class ItemScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
	// Update is called once per frame
	void Update () {
	
	}
}
