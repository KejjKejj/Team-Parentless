using UnityEngine;
using System.Collections;

public class Boss5Script : MonoBehaviour {

    public Transform BossSightStart, BossSightEnd;
    public GameObject Shield;

    private int _phase = 1;

    private float Timer = 0;
    private float RemoveShieldTime = 5f;

	// Use this for initialization
	void Start ()
	{
	    //Shield.transform.parent = transform;
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        
            Debug.Log(coll.gameObject.name + " Hitboxnamn");
            Debug.Log(coll.gameObject.tag + " Hitboxtag");
        
    }


    public void PhaseOne()
    {
        LookAtPlayer();
    }

    public void LookAtPlayer()
    {
        Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, playerPos - transform.position);
        Debug.DrawLine(BossSightStart.position, BossSightEnd.position, Color.blue);
    }
	
	// Update is called once per frame
	void Update () 
    {
	    if (_phase == 1)
	    {
	        PhaseOne();
	    }

	    Timer += Time.deltaTime;

	    if (Timer >= RemoveShieldTime)
	    {
	        Shield.GetComponent<Boss5ShieldScript>().ShieldCollider.enabled = false;
	        Shield.GetComponent<SpriteRenderer>().enabled = false;
	    }

        if (Timer >= 7)
        {
            Shield.GetComponent<Boss5ShieldScript>().ShieldCollider.enabled = true;
            Shield.GetComponent<SpriteRenderer>().enabled = true;
        }

    }
}
