using UnityEngine;
using System.Collections;


public class Movement : MonoBehaviour {

	private Rigidbody2D charRigid2D;
	public int Speed = 1000;
    public int Health = 100;
    public int JumpSpeed = 3;
    public int AxisSpeed = 3;
	public int MaxNumberOfShots = 30;
	public int NumberOfShots = 0;
	public float ShotSpeed = 40;
    public bool Jumped;
    public float JumpTime = 0;
    public float SetJumpTime = 0.1f;
    public float JumpDelay = 0;
    public bool CarryingWeapon = false;
	public GameObject Bullet;
	public Rigidbody2D ReturnPlayerPos(){
		return charRigid2D;
	}

	// Use this for initialization
	void Start () {
		charRigid2D = GetComponent<Rigidbody2D> ();
	}

    //void Shot()
    //{

    //    if (Input.GetMouseButtonDown (0) && NumberOfShots <= MaxNumberOfShots)
    //    {

    //        Instantiate(Bullet, transform.position, transform.rotation);
    //        NumberOfShots++;
            
    //    }
    //}

	void Move()
	{
	    var movement = charRigid2D.velocity;
        movement.x = Input.GetAxisRaw("Horizontal") * AxisSpeed;
        movement.y = Input.GetAxisRaw("Vertical") * AxisSpeed;

	    JumpDelay += Time.deltaTime;
	    if (!Jumped && JumpDelay > 0.5f) // Har man inte hoppat och hoppat inom 0.5 sek?
	    {
	        if (Input.GetButton("Jump")) // Trycker man space, sätt Jumped till sant och hopp delayen till 0
	        {
	            Jumped = true;
	            JumpDelay = 0;
	        }
	    }

	    if (Jumped) // Har man tryckt hoppa
	    {
	        movement = movement*2; // Dubbla hastigheten på spelaren
	        JumpTime += Time.deltaTime; // Räkna tiden som hoppet hållt på
            if (JumpTime >= SetJumpTime) // Om tiden är större än tiden hoppet ska hålla på, sätt hoppet till false och hopptiden till 0
	        {
	            Jumped = false;
	            JumpTime = 0;
	        }
	    }

		charRigid2D.velocity = movement * Speed;

        
	}


    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Ammocrate")
        {
            NumberOfShots = 0;
        }
    }
	void Direction()
	{
		Vector3 MousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		Vector3 CharPos = new Vector3 (charRigid2D.position.x, charRigid2D.position.y);
		transform.rotation = Quaternion.LookRotation (Vector3.forward, MousePos - CharPos);


	}


	// Update is called once per frame
	void FixedUpdate () {
		//Shot ();
		Move ();
		Direction ();
	}
}














