using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	public float speed = 20;
	Rigidbody2D Bulletbody2d = new Rigidbody2D();
	// Use this for initialization
	void Start () {
		Bulletbody2d = GetComponent<Rigidbody2D> ();
		Vector3 player = GameObject.FindGameObjectWithTag ("Player").transform.position;
//		float deltaX = Input.mousePosition.x - player.x;
//		float deltaY = Input.mousePosition.y - player.y;
		Bulletbody2d.velocity = new Vector2 (Input.mousePosition.x,Input.mousePosition.y);

	}



	// Update is called once per frame
	void Update () {

	
	}
}
