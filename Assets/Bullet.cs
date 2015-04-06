using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public float speed = 20;

	Rigidbody2D Bulletbody2d = new Rigidbody2D();

	// Use this for initialization
	void Start () {
		Bulletbody2d = GetComponent<Rigidbody2D> ();

	    float deltaX = -((Screen.width / 2) - Input.mousePosition.x);
        float deltaY = -((Screen.height / 2) - Input.mousePosition.y);

	    float angle = Mathf.Atan2(deltaY, deltaX);

        Bulletbody2d.velocity = new Vector2(Mathf.Cos(angle) * speed, Mathf.Sin(angle) * speed);
        Debug.Log(angle.ToString());
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log("Hej");
        Destroy(gameObject);
    }

	// Update is called once per frame
	void Update () {

	
	}
}
