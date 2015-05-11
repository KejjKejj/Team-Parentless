using UnityEngine;
using System.Collections;


public class Movement : CharacterMain
{
    //private Animator Anim;

    //private Rigidbody2D charRigid2D;

    public int Speed = 1000;
    //public int Health = 100;

    public int JumpSpeed = 3;
    public bool Jumped;
    public float JumpTime = 0;
    public float SetJumpTime = 12f;
    public float JumpDelay = 0;

    private float KnifeTimer = 0;
    private const float KnifeDelay = 0.25f;

    public int AxisSpeed = 3;

    public bool PickUpFirstWeapon = true;
    public float PickUpFirstTimer;
    public int WeaponDamage;

    public bool CarryingWeapon = false;
    public bool IsHandgun = false;

    private float Angle;
    //public bool Alive = true;

    protected bool OnFire = false;

	public GameObject Bullet;

    public GameObject Knife;

    public GameObject[] Weapons;
    public GameObject SelectedWeapon;
    public GameObject WeaponInstance;
   

    public Rigidbody2D ReturnPlayerPos()
    {
        return charRigid2D;
    }

    public PolygonCollider2D HitBox;

    // Use this for initialization
    void Start()
    {
        charRigid2D = GetComponent<Rigidbody2D>();
        HitBox = GetComponent<PolygonCollider2D>();
        Anim = GetComponent<Animator>();
        CarryingWeapon = false;
        //Weapons = GameObject.FindGameObjectsWithTag("Weapon");
        foreach (var w in Weapons)
        {
            if (w.GetComponent<Weapon>().WeaponId == PlayerPrefs.GetInt("WeaponSelected"))
            {
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
                Anim.SetBool("Sliding", true);
                Jumped = true;
                JumpDelay = 0;
            }
        }

        if (Jumped) // Har man tryckt hoppa
        {
            Physics2D.IgnoreLayerCollision(18, 20, true);
            movement = movement * 2; // Dubbla hastigheten på spelaren
            JumpTime += Time.deltaTime; // Räkna tiden som hoppet hållt på
            if (JumpTime >= SetJumpTime) // Om tiden är större än tiden hoppet ska hålla på, sätt hoppet till false och hopptiden till 0
            {
                Jumped = false;
                JumpTime = 0;
            }
        }

        if (!Jumped)
        {
            Anim.SetBool("Sliding", false);
            Physics2D.IgnoreLayerCollision(18, 20, false);
        }

        if (KnifeTimer >= KnifeDelay)
        {      
            if (Input.GetMouseButtonDown(1))
            {
                KnifeAttack();
                KnifeTimer = 0;
            }
        }

        charRigid2D.velocity = movement * Speed;

    }


    //void ApplyDamage(int damage)
    //{
    //    Health -= damage;
    //}

  
    //void CheckIfDead()
    //{
        
    //    if (Health <= 0)
    //    {
    //        Anim.Play("Death");
    //        Alive = false;
    //        charRigid2D.velocity = new Vector2(0, 0);
    //        if (Input.GetKeyDown(KeyCode.R))
    //        {
    //            Application.LoadLevel(Application.loadedLevel);
    //        }
    //        GameObject w = gameObject.GetComponentInChildren<Weapon>().gameObject;
    //        Destroy(w);
    //    }
    //}

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
        if (CarryingWeapon)
        {
            if (IsHandgun)
            {
                Anim.SetBool("Strafing", false);
                Anim.SetBool("Walking", false);
                Anim.SetBool("StrafingUnarmed", false);
                Anim.SetBool("WalkingUnarmed", false);
                Anim.SetBool("Pistol", true);
                Anim.SetBool("Armed", false);
                if (Input.GetAxisRaw("Horizontal") != 0 && ((Angle > 45 && Angle < 135) || (Angle > 225 && Angle < 325)))
                {
                    Anim.SetBool("StrafingPistol", false);
                    Anim.SetBool("WalkingPistol", true);
                }
                else if (Input.GetAxisRaw("Vertical") != 0 && ((Angle < 45 || Angle > 325) || (Angle > 135 && Angle < 225)))
                {
                    Anim.SetBool("StrafingPistol", false);
                    Anim.SetBool("WalkingPistol", true);
                }
                else if (Input.GetAxisRaw("Horizontal") != 0)
                {
                    Anim.SetBool("WalkingPistol", false);
                    Anim.SetBool("StrafingPistol", true);
                }

                else if (Input.GetAxisRaw("Vertical") != 0)
                {
                    Anim.SetBool("WalkingPistol", false);
                    Anim.SetBool("StrafingPistol", true);
                }
                else
                {
                    Anim.SetBool("StrafingPistol", false);
                    Anim.SetBool("WalkingPistol", false);
                }
            }
            else
            {
                Anim.SetBool("StrafingPistol", false);
                Anim.SetBool("WalkingPistol", false);
                Anim.SetBool("StrafingUnarmed", false);
                Anim.SetBool("WalkingUnarmed", false);
                Anim.SetBool("Pistol", false);
                Anim.SetBool("Armed", true);
                if (Input.GetAxisRaw("Horizontal") != 0 && ((Angle > 45 && Angle < 135) || (Angle > 225 && Angle < 325)))
                {
                    Anim.SetBool("Walking", false);
                    Anim.SetBool("Strafing", true);
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
        }
        if (!CarryingWeapon)
        {
            Anim.SetBool("Strafing", false);
            Anim.SetBool("Walking", false);
            Anim.SetBool("StrafingPistol", false);
            Anim.SetBool("WalkingPistol", false);
            Anim.SetBool("Pistol", false);
            Anim.SetBool("Armed", false);
            if (Input.GetAxisRaw("Horizontal") != 0 && ((Angle > 45 && Angle < 135) || (Angle > 225 && Angle < 325)))
            {
                Anim.SetBool("StrafingUnarmed", false);
                Anim.SetBool("WalkingUnarmed", true);
            }
            else if (Input.GetAxisRaw("Vertical") != 0 && ((Angle < 45 || Angle > 325) || (Angle > 135 && Angle < 225)))
            {
                Anim.SetBool("StrafingUnarmed", false);
                Anim.SetBool("WalkingUnarmed", true);
            }
            else if (Input.GetAxisRaw("Horizontal") != 0)
            {
                Anim.SetBool("WalkingUnarmed", false);
                Anim.SetBool("StrafingUnarmed", true);
            }

            else if (Input.GetAxisRaw("Vertical") != 0)
            {
                Anim.SetBool("WalkingUnarmed", false);
                Anim.SetBool("StrafingUnarmed", true);
            }
            else
            {
                Anim.SetBool("StrafingUnarmed", false);
                Anim.SetBool("WalkingUnarmed", false);
            }
        }
    }
    
    //void OnFireDamage()
    //{
    //    if (OnFire)
    //    {
    //        Health -= 1;

    //    }

    //}
    void KnifeAttack()
    {       
        Instantiate(Knife, transform.position, Quaternion.identity);
    }

    void Direction()
    {
        Vector3 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 CharPos = new Vector3(charRigid2D.position.x, charRigid2D.position.y);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, MousePos - CharPos);
        Angle = transform.localEulerAngles.z;
    }


    // Update is called once per frame

    private void Update()
    {
        if (Alive)
        {
            Move();
            Direction();
            SetAnimation();
        }
       

        PickUpFirstTimer += Time.deltaTime;
        KnifeTimer += Time.deltaTime;
        if (PickUpFirstWeapon && PickUpFirstTimer >= 0.2f)
        {
            PickUp();
        }

        //CheckIfDead();     
	}

}

