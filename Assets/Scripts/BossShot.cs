using UnityEngine;
using System.Collections;

public class BossShot : MonoBehaviour {
    Rigidbody2D Bulletbody2d = new Rigidbody2D();
    Rigidbody2D Player = new Rigidbody2D();
    Rigidbody2D Boss = new Rigidbody2D();
    public int speed = 1;
    
	// Use this for initialization
	void Start () {
        Player = GameObject.Find("Character").GetComponent<Movement>().ReturnPlayerPos();
        Boss = GameObject.Find("Boss").GetComponent<BossScript>().ReturnBossPos();
        Bulletbody2d = GetComponent<Rigidbody2D>();
        float deltaX = -(Boss.position.x - Player.position.x);
        
        float deltaY = -(Boss.position.y - Player.position.y);
        Debug.Log("x: " + deltaX + "    y: " + deltaY + "   Player x: " + Player.position.x + " Player y: " + Player.position.y);
        float angle = Mathf.Atan2(deltaY, deltaX);
       
        Bulletbody2d.velocity = new Vector2(Mathf.Cos(angle) * speed, Mathf.Sin(angle) * speed);
	}


    void OnCollisionEnter2D(Collision2D coll)
    {

        Destroy(gameObject);
    }
	// Update is called once per frame
	void Update () {
	
	}
}
