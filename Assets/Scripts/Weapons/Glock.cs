using UnityEngine;
using System.Collections;

public class Glock : Weapon
{

    public int CurrentAmmo;

    public GameObject Bullet;
    public GameObject ShellObj;
    public GameObject obj;
    public GameObject GunFlashLight;
    public Movement Player;

    private AudioSource[] AudioSources;
    private AudioSource Audio1;
    private AudioSource Audio2;

    public AudioClip Shot;
    public AudioClip Shell;
    public AudioClip PickUp;

    public GUIStyle GuiFont;
	// Use this for initialization
	void Start ()
	{
	    FireRate = 0.5f;
	    MagSize = 12;
        Recoil = 0f;
        MaxRecoil = 0f;
        damage = 7;
	    CurrentAmmo = MagSize;
	    Automatic = false;
	    AudioSources = GetComponents<AudioSource>();
	    Audio1 = AudioSources[0];
	    Audio2 = AudioSources[1];
        gameObject.GetComponent<Weapon>().WeaponId = 0;
	}

    void OnTriggerEnter2D(Collider2D collissionobject)
    {
        if (collissionobject.gameObject.tag == "Ammocrate")
        {
            CurrentAmmo = MagSize;
        }
    }

    void OnTriggerStay2D(Collider2D collissionobject)
    {
        if (collissionobject.tag == "Player")
        {
            if (Input.GetButton("Weapon") && !IsPickedUp && gameObject.tag == "Weapon" && PickUpDelayTimer >= DropDelay &&
                !collissionobject.GetComponent<Movement>().CarryingWeapon)
            {
                Player = collissionobject.GetComponent<Movement>();
                PickUpWeapon();
            }
        }
    }

    void OnTriggerExit2D(Collider2D collissionobject)
    {

    }

    public void PickUpWeapon()
    {
        IsPickedUp = true;
        SetPositionToPlayer = true;
        PickUpDelayTimer = 0;
        gameObject.GetComponent<Weapon>().IsPickedUp = true;
        GameObject.Find("Character").GetComponent<Movement>().WeaponDamage = damage;
        Player.CarryingWeapon = true;
        Player.IsHandgun = true;
        Audio1.PlayOneShot(PickUp);
    }

    public void DropWeapon()
    {
        IsPickedUp = false;
        DropDelayTimer = 0;
        gameObject.GetComponent<Weapon>().IsPickedUp = false;
        Player.CarryingWeapon = false;
        Player.IsHandgun = false;
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
            GUI.TextField(new Rect(130, Screen.height-20, 120, 20), "Ammo: " + CurrentAmmo.ToString() + " / " + MagSize.ToString(),GuiFont);
        }
    }

    void Shellspread()
    {
        Instantiate(ShellObj, transform.position, Quaternion.identity);
    }
    void GunFlash()
    {
        gameObject.GetComponent<Weapon>().Shake = true;
        Instantiate(GunFlashLight, transform.position, Quaternion.identity);

    }
    // Update is called once per frame
    void Update()
    {

        // För att plocka upp vapen
        if (IsPickedUp && SetPositionToPlayer)
        {
            Position();
            SetPositionToPlayer = false;
        }

        if (IsPickedUp)
        {
            DropDelayTimer += Time.deltaTime;
        }

        if (Input.GetButton("Weapon") && IsPickedUp && gameObject.tag == "Weapon" && DropDelayTimer >= DropDelay && Player.CarryingWeapon)
        {
            DropWeapon();
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
                    Audio1.PlayOneShot(Shot);
                    GunFlash();
                    Audio2.clip = Shell;
                    Shellspread();
                    Audio2.PlayDelayed(0.2f);
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
                    Audio1.PlayOneShot(Shot);
                    GunFlash();
                    Audio2.clip = Shell;
                    Shellspread();
                    Audio2.PlayDelayed(0.2f);
                }
            }
            if (IsPickedUp && FireRateTimer >= FireRate && CurrentAmmo > 0)
            {
                if (Input.GetMouseButton(0))
                {
                    Instantiate(Bullet, transform.position, transform.rotation);
                    FireRateTimer = 0;
                    CurrentAmmo--;
                    Audio1.PlayOneShot(Shot);
                    GunFlash();
                    Audio2.PlayOneShot(Shell);
                    Shellspread();
                }
            }
        }
        
    }
}
