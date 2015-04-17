using UnityEngine;
using System.Collections;

public class Boss5Script : MonoBehaviour {

    public Transform BossSightStart, BossSightEnd;

    public GameObject Shield;
    public GameObject Bomb;
    public GameObject Bullet;

    private int Phase = 1;

    private float Timer = 0;
    private float FireTimer = 0;

    private const float EnableShieldTime = 5f;
    private const float BombCooldown = 8f;
    private float FireRate = 0.3f;

	// Use this for initialization
	void Start ()
	{

	}

    public void PhaseOne()
    {
        LookAtPlayer();
        if (CheckLineOfSight())
        {
            if (Timer >= BombCooldown)
            {
                DisableShield();
                CreateBombs();
            }
        }
        else
        {
            OpenFire();
        }

        if (Timer >= EnableShieldTime)
        {
            EnableShield();
        }
    }

    public void PhaseTwo()
    {
        LookAtPlayer();
        DisableShield();
        CreateOneBomb();
        FireRate = 0.1f;
        OpenFire();
    }

    public void OpenFire()
    {
        if (FireTimer >= FireRate)
        {
            Instantiate(Bullet,
                transform.position + new Vector3(Mathf.Cos(Mathf.Deg2Rad * (transform.localEulerAngles.z + 70)) * 1.4f, Mathf.Sin(Mathf.Deg2Rad * (transform.localEulerAngles.z + 70)) * 1.4f, 0), 
                transform.rotation);
            Instantiate(Bullet,
                transform.position + new Vector3(Mathf.Cos(Mathf.Deg2Rad * (transform.localEulerAngles.z + 110)) * 1.4f, Mathf.Sin(Mathf.Deg2Rad * (transform.localEulerAngles.z + 110)) * 1.4f, 0),
                transform.rotation);
            FireTimer = 0;
        }
    }

    public void DisableShield()
    {
        Shield.GetComponent<Boss5ShieldScript>().ShieldCollider.enabled = false;
        Shield.GetComponent<SpriteRenderer>().enabled = false;
    }

    public void EnableShield()
    {
        Shield.GetComponent<Boss5ShieldScript>().ShieldCollider.enabled = true;
        Shield.GetComponent<SpriteRenderer>().enabled = true;
    }

    public void CreateOneBomb()
    {
        if (Timer >= 1f)
        {
            float x = Random.Range(-9.6f, 9.2f);
            float y = Random.Range(0, 12);

            Instantiate(Bomb, new Vector3(x, y, 0), Quaternion.identity);

            Timer = 0;
        }
    }

    public void CreateBombs()
    {
        // Bakom vänster vägg
        Instantiate(Bomb, new Vector3(-9.6f, 1.5f, 0), Quaternion.identity);
        Instantiate(Bomb, new Vector3(-9.6f, 4f, 0), Quaternion.identity);
        // Bakom höger vägg
        Instantiate(Bomb, new Vector3(9.2f, 1.5f, 0), Quaternion.identity);
        Instantiate(Bomb, new Vector3(9.2f, 4f, 0), Quaternion.identity);

        // Slumpa i rummet
        for (int i = 0; i < 30; ++i)
        {
            float x = Random.Range(-9.6f, 9.2f);
            float y = Random.Range(-7, 12);

            Instantiate(Bomb, new Vector3(x, y, 0), Quaternion.identity);
        }

        Timer = 0;
    }

    public bool CheckLineOfSight()
    {
        return Physics2D.Linecast(BossSightStart.position, BossSightEnd.position, 1 << LayerMask.NameToLayer("FirmWall"));
    }

    public void LookAtPlayer()
    {
        Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, playerPos - transform.position);
        Debug.DrawLine(BossSightStart.position, BossSightEnd.position, Color.blue);
    }
	
	// Update is called once per frame
	void Update () 
    {
	    if (Phase == 1)
	    {
	        PhaseOne();
	        if (gameObject.GetComponent<MainBossScript>().Health <= 20)
	        {
	            Phase = 2;
	        }
	    }

	    if (Phase == 2)
	    {
	        PhaseTwo();
	    }

	    Timer += Time.deltaTime;
	    FireTimer += Time.deltaTime;
    }
}
