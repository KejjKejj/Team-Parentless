using UnityEngine;
using System.Collections;

public class EnemyBullet : MonoBehaviour {
	
	public float speed = 20;
	
	private Rigidbody2D Bulletbody2d = new Rigidbody2D();
	
	public GameObject WallHit;
    public GameObject BodyHit;
    public GameObject Blood;
	
	public AudioClip FirmWall;
	public AudioClip Body;
	
	public float Recoil;
	public float AngleShot = 0;
	
	// Use this for initialization
	void Start () {
		Bulletbody2d = GetComponent<Rigidbody2D> ();
		Vector3 PlayerPos = GameObject.FindGameObjectWithTag ("Player").transform.position;
		
		float deltaX = -(transform.position.x - PlayerPos.x);
		float deltaY = -(transform.position.y - PlayerPos.y);
		
		float angle = Mathf.Atan2(deltaY, deltaX);
		
		Bulletbody2d.velocity = new Vector2(Mathf.Cos(angle) * speed, Mathf.Sin(angle) * speed);
	}
	
	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "FirmWall")
		{
			AudioSource.PlayClipAtPoint(FirmWall, transform.position, 0.02f);
			Instantiate(WallHit, transform.position, Quaternion.identity);
		}
		if (coll.gameObject.tag == "Player")
		{
			AudioSource.PlayClipAtPoint(Body, transform.position, 0.1f);
            Instantiate(BodyHit, transform.position, transform.rotation);
            Instantiate(Blood, transform.position, transform.rotation);
            coll.gameObject.SendMessage("ApplyDamage", 5);
		}
		Destroy(gameObject); 
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
