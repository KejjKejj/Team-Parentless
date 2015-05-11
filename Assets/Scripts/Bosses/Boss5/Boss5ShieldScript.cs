using UnityEngine;
using System.Collections;

public class Boss5ShieldScript : MonoBehaviour
{

    public BoxCollider2D ShieldCollider;
    public SpriteRenderer ShieldSpriteRenderer;

	// Use this for initialization
	void Start ()
	{
	    ShieldCollider = GetComponent<BoxCollider2D>();
	    ShieldSpriteRenderer = GetComponent<SpriteRenderer>();
	}

    public void LookAtPlayer()
    {
        Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        Vector3 bossPos = GameObject.FindGameObjectWithTag("Boss").transform.position;

        float deltaX = -(bossPos.x - playerPos.x);
        float deltaY = -(bossPos.y - playerPos.y);

        float angle = Mathf.Atan2(deltaY, deltaX);

        transform.position = bossPos + new Vector3(Mathf.Cos(angle) / 2, Mathf.Sin(angle) / 2, 0);

        transform.rotation = Quaternion.LookRotation(Vector3.forward, playerPos - transform.position);
      
        
    }
	
	// Update is called once per frame
	void Update () {
	    LookAtPlayer();
	}
}
