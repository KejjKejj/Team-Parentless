using UnityEngine;
using System.Collections;

public class TurretScript : MonoBehaviour {

    private Rigidbody2D EnemyRigid2D;
    private Vector3 EnemyPos;
    public GameObject AmmoCrate;
    public GameObject EnemyBullet;
    public Vector3 PlayerPos;
    public float Health = 20;
    private int RandAmmo;
    public bool Onfire = false;
    public int MaxFire = 30;
    private int NumberShots = 0;

    float ShootTimer = 0.1f;
    float Timer;
	// Use this for initialization
	void Start () {
        EnemyRigid2D = GetComponent<Rigidbody2D>();
        EnemyPos = new Vector3(EnemyRigid2D.position.x, EnemyRigid2D.position.y, 0);
	}

    void Raycasting()
    {
        if (Physics2D.Linecast(transform.position,PlayerPos, 1<<LayerMask.NameToLayer("Player")) &&
        !Physics2D.Linecast(transform.position, PlayerPos,1<<LayerMask.NameToLayer("FirmWall")) &&
        !Physics2D.Linecast(transform.position,PlayerPos,1<<LayerMask.NameToLayer("SoftWall")))
        {
            Direction();
            //StartCoroutine(Cooldown());
            if (Timer >= ShootTimer)
            {
                StartCoroutine(Cooldown());
                Timer = 0;
            }
             
        }
        Debug.DrawLine(transform.position,PlayerPos,Color.black);
        
    }

    void ApplyDamage(int damage)
    {

        Health -= damage;
        
    }

   

    IEnumerator Cooldown()
    {


        

        if (NumberShots >= MaxFire)
        {
            yield return new WaitForSeconds(2f);
            NumberShots = 0;
            Debug.Log("Kattt");


        }
        else
        {
            Instantiate(EnemyBullet, transform.position, transform.rotation);

        }
        Debug.Log(NumberShots);
        NumberShots++;
    }
    void Direction()
    {
        Vector3 PlayerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, PlayerPos - EnemyPos);
    }
    void Attack()
    {
        Instantiate(EnemyBullet, transform.position, transform.rotation);
    }
    void CheckIfDead()
    {
        if (Health <= 0)
        {
            GameObject.FindGameObjectWithTag("Player").SendMessage("ApplyScore", 100);
            Destroy(gameObject);
            SpawnCrate();
          
        }
    }
    void ApplyFireDamage(bool Fire)
    {
        Onfire = Fire;

    }
    
    void SpawnCrate()
    {
        RandAmmo = Random.Range(1, 101);
        Debug.Log(RandAmmo);
        if (RandAmmo >= 75)
        {

            Instantiate(AmmoCrate, new Vector3(EnemyRigid2D.position.x, EnemyRigid2D.position.y), Quaternion.identity);
        }
    }
	// Update is called once per frame
	void Update () {
        Raycasting();
        PlayerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        if (Onfire)
        {
            Health -= 4 * Time.deltaTime;
        }
        CheckIfDead();
        Timer += Time.deltaTime;
        
	}
}
