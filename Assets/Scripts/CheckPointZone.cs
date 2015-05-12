using UnityEngine;
using System.Collections;

public class CheckPointZone : MonoBehaviour {

    public bool CheckPointEntered = false;
	// Use this for initialization
    void Awake()
    {
        DontDestroyOnLoad(this);
    }
	void Start () {
	
	}
	void OnTriggerEnter2D(Collider2D coll)
    { 
        if(coll.tag == "Player")
        {
            CheckPointEntered = true;
        }
    }
	// Update is called once per frame
	void Update () {
	
	}
}
