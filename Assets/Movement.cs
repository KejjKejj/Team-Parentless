using UnityEngine;
using System.Collections;


public class Movement : MonoBehaviour {

	private Rigidbody2D charRigid2D;
	public int Speed = 10;
	public int MaxNumberOfShots = 3;
	public int NumberOfShots = 0;
	public float ShotSpeed = 40;

	public GameObject Bullet;
	public Rigidbody2D ReturnPlayerPos(){
		return charRigid2D;
	}

	// Use this for initialization
	void Start () {
		charRigid2D = GetComponent<Rigidbody2D> ();
	}

	void Shot(){
//	{	GameObject shot = GameObject.FindGameObjectWithTag ("Shot1");
//		bool shoot = false;
//		Rigidbody2D clone2 = new Rigidbody2D();
//		if (Input.GetMouseButtonDown (0) && NumberOfShots < MaxNumberOfShots) {
//			Vector3 CurrentDirectionShot = new Vector3(Input.mousePosition.x,Input.mousePosition.y);
//			NumberOfShots++;
//			clone2 = Instantiate (shot, new Vector3 (charRigid2D.position.x, charRigid2D.position.y), Quaternion.identity) as Rigidbody2D;
//			shoot = true;
//		}
//		if (shoot) {
//			clone2.AddTorque(100);
//
//		}
		if (Input.GetMouseButtonDown (0)) {
			Instantiate(Bullet,transform.position,Quaternion.identity);
		}
	}

	void Move()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		
		Vector2 movement = new Vector2 (moveHorizontal, moveVertical);
		charRigid2D.velocity = movement * Speed;
	}

	void Direction()
	{
//		float MousePosX = Input.mousePosition.x;
//		float MousePosY = Input.mousePosition.y;
//		float CharPosX = charRigid2D.position.x;
//		float CharPosY = charRigid2D.position.y;
//
//		float deltaY = CharPosY - MousePosY;
//		float deltaX = CharPosX - MousePosX;
//
//		float angle = Mathf.Atan2 (deltaY, deltaX) * (180 / Mathf.PI);
//
//		charRigid2D.rotation = angle;
		Vector3 MousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		Vector3 CharPos = new Vector3 (charRigid2D.position.x, charRigid2D.position.y);
		transform.rotation = Quaternion.LookRotation (Vector3.forward, MousePos - CharPos);


	}


	// Update is called once per frame
	void FixedUpdate () {
		Shot ();
		Move ();
		Direction ();
	}
}














