using UnityEngine;
using System.Collections;

public class BossZone : MonoBehaviour {
    public bool OpenFire = false;
	// Use this for initialization
	void Start () {
	
	}
    void OnTriggerEnter2D(Collider2D coll)
    {
        
        if (coll.gameObject.tag == "Player")
        {
            Debug.Log("Inne i bossrange");
            OpenFire = true;
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            Debug.Log("Lämnar bossrange");
            OpenFire = false;
        }
    }
	// Update is called once per frame
	void Update () {
	
	}
}
