    using UnityEngine;
using System.Collections;

public class BossShot2 : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.transform.tag == "Player")
        {
            GameObject.Find("Character").SendMessage("ApplyDamage", 5);


        }
        Destroy(gameObject);
    }
	// Update is called once per frame
	void Update () {
	
	}
}
