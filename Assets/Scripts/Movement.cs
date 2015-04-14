using UnityEngine;
using System.Collections;


public class Movement : MonoBehaviour
{

    private Animator Anim;
	private Rigidbody2D charRigid2D;
	public int Speed = 1000;
    public int Health = 100;
    public int JumpSpeed = 3;
    public int AxisSpeed = 3;
	public int MaxNumberOfShots = 30;
	public int NumberOfShots = 0;
	public float ShotSpeed = 40;
    public bool PickUpFirstWeapon = true;
    public float PickUpFirstTimer;
    public bool Jumped;
    public float JumpTime = 0;
    public float SetJumpTime = 0.1f;
    public float JumpDelay = 0;
    public bool CarryingWeapon = false;
    private float Angle;
	public GameObject Bullet;
    public GameObject[] Weapons;
    public GameObject SelectedWeapon;
    
    public GameObject WeaponInstance;
	public Rigidbody2D ReturnPlayerPos(){
		return charRigid2D;
	}

    public PolygonCollider2D HitBox;

	// Use this for initialization
	void Start () {
		charRigid2D = GetComponent<Rigidbody2D> ();
	    HitBox = GetComponent<PolygonCollider2D>();
	    Anim = GetComponent<Animator>();
	    CarryingWeapon = false;
        Weapons = GameObject.FindGameObjectsWithTag("Weapon");
        foreach (var w in Weapons)
        {
            if (w.GetComponent<Weapon>().WeaponId == PlayerPrefs.GetInt("WeaponSelected"))
            {
                Debug.Log(w.GetComponent<Weapon>().WeaponId + " Selected");
                Debug.Log(PlayerPrefs.GetInt("WeaponSelected"));
                SelectedWeapon = w;
            }
        }
        WeaponInstance = (GameObject)Instantiate(SelectedWeapon, transform.position, transform.rotation);
	}

   

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
	        HitBox.enabled = false;
	        movement = movement*2; // Dubbla hastigheten på spelaren
	        JumpTime += Time.deltaTime; // Räkna tiden som hoppet hållt på
            if (JumpTime >= SetJumpTime) // Om tiden är större än tiden hoppet ska hålla på, sätt hoppet till false och hopptiden till 0
	        {
	            Jumped = false;
	            JumpTime = 0;
	        }
	    }
	    if (!Jumped)
	    {
	        HitBox.enabled = true;
	    }

		charRigid2D.velocity = movement * Speed;
	}



    void OnCollisionEnter2D(Collision2D coll)
    {
        
        if (coll.gameObject.tag == "BossShot")
        {
            if (Health < 1)
            {
                Destroy(gameObject);
                Application.LoadLevel(4);
            }

            else
            {
                Health -= 10;
            }
        }
    }

    void PickUp()
    {
        if (WeaponInstance.GetComponent<M4>() != null)
        {
            WeaponInstance.GetComponent<M4>().Player = gameObject.GetComponent<Movement>();
            WeaponInstance.GetComponent<M4>().PickUpWeapon();
        }
        if (WeaponInstance.GetComponent<Glock>() != null)
        {
            WeaponInstance.GetComponent<Glock>().Player = gameObject.GetComponent<Movement>();
            WeaponInstance.GetComponent<Glock>().PickUpWeapon();
        }
        if (WeaponInstance.GetComponent<Shotgun>() != null)
        {
            WeaponInstance.GetComponent<Shotgun>().Player = gameObject.GetComponent<Movement>();
            WeaponInstance.GetComponent<Shotgun>().PickUpWeapon();
        }
        PickUpFirstWeapon = false;
    }

    void SetAnimation()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 && ((Angle > 45 && Angle < 135) || (Angle > 225 && Angle < 325)))
        {
            Anim.SetBool("Strafing", false);
            Anim.SetBool("Walking", true);
        }
        else if (Input.GetAxisRaw("Vertical") != 0 && ((Angle < 45 || Angle > 325) || (Angle > 135 && Angle < 225)))
        {
            Anim.SetBool("Strafing", false);
            Anim.SetBool("Walking", true);
        }
        else if (Input.GetAxisRaw("Horizontal") != 0)
        {
            Anim.SetBool("Walking", false);
            Anim.SetBool("Strafing", true);
        }
        
        else if (Input.GetAxisRaw("Vertical") != 0)
        {
            Anim.SetBool("Walking", false);
            Anim.SetBool("Strafing", true);
        }
        else
        {
            Anim.SetBool("Strafing", false);
            Anim.SetBool("Walking", false);
        }
    }

	void Direction()
	{
		Vector3 MousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		Vector3 CharPos = new Vector3 (charRigid2D.position.x, charRigid2D.position.y);
		transform.rotation = Quaternion.LookRotation (Vector3.forward, MousePos - CharPos);
	    Angle = transform.localEulerAngles.z;
	}

    
	// Update is called once per frame
	void FixedUpdate () {
		Move ();
		Direction ();
	    SetAnimation();
        PickUpFirstTimer += Time.deltaTime;
        if (PickUpFirstWeapon && PickUpFirstTimer >= 0.2f)
        {
            PickUp();
        }
	}
}














