using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public float speed = 20;

	private Rigidbody2D Bulletbody2d = new Rigidbody2D();
    private AudioSource Audio;

    public AudioClip FirmWall;
    public AudioClip Body;

	// Use this for initialization
	void Start ()
	{
	    Audio = GetComponent<AudioSource>();
		Bulletbody2d = GetComponent<Rigidbody2D> ();

	    float deltaX = -((Screen.width / 2) - Input.mousePosition.x);
        float deltaY = -((Screen.height / 2) - Input.mousePosition.y);

	    float angle = Mathf.Atan2(deltaY, deltaX);

        Bulletbody2d.velocity = new Vector2(Mathf.Cos(angle) * speed, Mathf.Sin(angle) * speed);
       
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "FirmWall")
        {
            AudioSource.PlayClipAtPoint(FirmWall, transform.position, 0.02f);
        }
        if (coll.gameObject.tag == "Enemy" || coll.gameObject.tag == "Boss")
        {
            AudioSource.PlayClipAtPoint(Body, transform.position, 0.07f);
        }
        Destroy(gameObject); 
    }

	// Update is called once per frame
	void Update () {

	
	}
}
