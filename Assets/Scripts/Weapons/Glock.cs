using UnityEngine;
using System.Collections;

public class Glock : Weapon
{

    public int CurrentAmmo;

    public GameObject Bullet;

    public GameObject obj;
    protected Movement Player;

	// Use this for initialization
	void Start ()
	{
	    FireRate = 0.5f;
	    MagSize = 12;
	    CurrentAmmo = MagSize;
	    Automatic = false;
        Player = obj.GetComponent<Movement>();
	}

    void OnTriggerEnter2D(Collider2D collissionobject)
    {
        if (collissionobject.gameObject.tag == "Player")
        {
            Debug.Log("Player Entered - Weapon pickuparea - M4");
        }
        if (collissionobject.gameObject.tag == "Ammocrate")
        {
            CurrentAmmo = MagSize;
        }
    }

    void OnTriggerStay2D(Collider2D collissionobject)
    {
        if (collissionobject.gameObject.tag == "Player")
        {
            Debug.Log("Player Staying - Weapon - M4");

        }
        if (Input.GetButton("Weapon") && !IsPickedUp && gameObject.tag == "Weapon" && PickUpDelayTimer >= DropDelay && !Player.CarryingWeapon)
        {
            Debug.Log("Player pressed E - Weapon");
            IsPickedUp = true;
            SetPositionToPlayer = true;
            PickUpDelayTimer = 0;

            Player.CarryingWeapon = true;
            Debug.Log(Player.CarryingWeapon + " M4");
        }
    }

    void OnTriggerExit2D(Collider2D collissionobject)
    {
        if (collissionobject.gameObject.tag == "Player")
        {
            Debug.Log("Player Leaving - Weapon");
        }
    }

    void Position()
    {
        GameObject Player = GameObject.FindGameObjectWithTag("Player");

        Vector3 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 CharPos = new Vector3(Player.transform.position.x, Player.transform.position.y);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, MousePos - CharPos);

        float deltaX = -((Screen.width / 2) - Input.mousePosition.x);
        float deltaY = -((Screen.height / 2) - Input.mousePosition.y);

        float angle = Mathf.Atan2(deltaY, deltaX);

        transform.position = new Vector3(Player.transform.position.x + (Mathf.Cos(angle - 90) / 3), Player.transform.position.y + (Mathf.Sin(angle - 90) / 3), Player.transform.position.z);

        transform.parent = GameObject.FindGameObjectWithTag("Player").transform;


    }

    void OnGUI()
    {
        if (IsPickedUp)
        {
            GUI.TextField(new Rect(100, 570, 100, 20), "Ammo: " + CurrentAmmo.ToString() + " / " + MagSize.ToString());
        }
    }

    // Update is called once per frame
    void Update()
    {

        // För att plocka upp vapen
        if (IsPickedUp && SetPositionToPlayer)
        {
            Position();
            SetPositionToPlayer = false;
            Debug.Log("Weapon Picked Up By Player!");
        }

        if (IsPickedUp)
        {
            DropDelayTimer += Time.deltaTime;
        }

        if (Input.GetButton("Weapon") && IsPickedUp && gameObject.tag == "Weapon" && DropDelayTimer >= DropDelay && Player.CarryingWeapon)
        {
            Debug.Log("Player pressed Weapon - Drop Weapon");
            IsPickedUp = false;
            DropDelayTimer = 0;

            Player.CarryingWeapon = false;
        }

        if (!IsPickedUp)
        {
            transform.parent = null;
            PickUpDelayTimer += Time.deltaTime;
        }

        // För att skjuta
        FireRateTimer += Time.deltaTime;
        if (!Automatic)
        {
            if (IsPickedUp && FireRateTimer >= FireRate && CurrentAmmo > 0)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Instantiate(Bullet, transform.position, transform.rotation);
                    FireRateTimer = 0;
                    CurrentAmmo--;
                }
            }
        }
        if (Automatic)
        {
            if (IsPickedUp && FireRateTimer >= FireRate && CurrentAmmo > 0)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Instantiate(Bullet, transform.position, transform.rotation);
                    FireRateTimer = 0;
                    CurrentAmmo--;
                }
            }
            if (IsPickedUp && FireRateTimer >= FireRate && CurrentAmmo > 0)
            {
                if (Input.GetMouseButton(0))
                {
                    Instantiate(Bullet, transform.position, transform.rotation);
                    FireRateTimer = 0;
                    CurrentAmmo--;
                }
            }
        }
        
    }
}
