using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public float speed = 20;

	private Rigidbody2D Bulletbody2d = new Rigidbody2D();


    public AudioClip FirmWall;
    public AudioClip Body;

    public float Recoil;

	// Use this for initialization
	void Start ()
	{
		Bulletbody2d = GetComponent<Rigidbody2D> ();
        Recoil = GameObject.Find("M4").GetComponent<M4>().Recoil;
        Recoil= Random.Range(-Recoil, Recoil);
        
	    float deltaX = -((Screen.width / 2) - Input.mousePosition.x);
        float deltaY = -((Screen.height / 2) - Input.mousePosition.y);

	    float angle = Mathf.Atan2(deltaY, deltaX);

        Bulletbody2d.velocity = new Vector2(Mathf.Cos(angle + Recoil) * speed, Mathf.Sin(angle+ Recoil) * speed);
       
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "FirmWall")
        {
            AudioSource.PlayClipAtPoint(FirmWall, transform.position, 0.02f);
        }
        if (coll.gameObject.tag == "Enemy" || coll.gameObject.tag == "Boss")
        {
            AudioSource.PlayClipAtPoint(Body, transform.position, 0.1f);
        }
        Destroy(gameObject); 
    }

	// Update is called once per frame
	void Update () {

	
	}
}
