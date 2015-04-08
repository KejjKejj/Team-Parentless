using UnityEngine;
using System.Collections;

public class BossShot : MonoBehaviour {
    Rigidbody2D Bulletbody2d = new Rigidbody2D();
    Rigidbody2D Player = new Rigidbody2D();
    Rigidbody2D Boss = new Rigidbody2D();
    private float timer = 0;
    public float speed = -1f;
    public float angleshot;
    
	// Use this for initialization
	void Start () {
        Player = GameObject.Find("Character").GetComponent<Movement>().ReturnPlayerPos();
        Boss = GameObject.Find("Boss").GetComponent<BossScript>().ReturnBossPos();
        Bulletbody2d = GetComponent<Rigidbody2D>();
        if (GetBossState() == 1)
        {
            float deltaX = -(Boss.position.x - Player.position.x);

            float deltaY = -(Boss.position.y - Player.position.y);

            float angle = Mathf.Atan2(deltaY, deltaX);

            Bulletbody2d.velocity = new Vector2(Mathf.Cos(angle + angleshot) * speed, Mathf.Sin(angle + angleshot) * speed);
        }
        if (GetBossState() == 2)
        {
            Bulletbody2d.velocity = new Vector2(Mathf.Cos(GameObject.Find("Boss").GetComponent<BossScript>().BossRottimer + angleshot) * 10,
                                                Mathf.Sin(GameObject.Find("Boss").GetComponent<BossScript>().BossRottimer + angleshot) * 10);
           
            
        }
	}
    
    int GetBossState()
    {
        return GameObject.Find("Boss").GetComponent<BossScript>().state;
    }
    void OnCollisionEnter2D(Collision2D coll)
    {

        Destroy(gameObject);
    }
	// Update is called once per frame
	void Update () {
	
	}
}
