using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {

	private Rigidbody2D EnemyRigid2D;
    public GameObject Blood;
    public GameObject AmmoCrate;
    public GameObject[] Bloodspatter;
    private int RandAmmo;
	private Vector3 EnemyPos;
	private float Distance;
	private float Vinkel;

    public int Health = 10;


    public float patrolSpeed;
    public int CurWayPoint;
    public bool doPatrol = true;
    public Transform[] patrolWayPoints;
    public Vector3 Target;
    public Vector3 MoveDirection;
    public Vector3 Velocity;


	public Transform SightEnemy1, SightPlayer1;
	public bool Spotted = false;
	public bool Detected = false;

	public GameObject EnemyBullet;
	private float timer = 1f;
	public Transform Player;



	// Use this for initialization
	void Start ()
	{
      
		EnemyRigid2D = GetComponent<Rigidbody2D> ();
		EnemyPos = new Vector3 (EnemyRigid2D.position.x, EnemyRigid2D.position.y, 0);
        
	}

	void OnTriggerStay2D(Collider2D collissionObject)
	{
		Vector3 PlayerPos = GameObject.FindGameObjectWithTag ("Player").transform.position;
		float PlayerX = PlayerPos.x - EnemyRigid2D.position.x;
		float PlayerY = PlayerPos.y - EnemyRigid2D.position.y;
		Vector2 TowardsPlayer = new Vector2 (PlayerX, PlayerY);

		if (collissionObject.tag == "Player") 
		{
			EnemyRigid2D.velocity = TowardsPlayer * 0;
			Direction();
			Spotted = true;
			Detected = true;
			Attack();
		}
	}

	void OnTriggerExit2D(Collider2D collissionObject)
	{
		if (collissionObject.tag == "Player")
			Spotted = false;
	}

	void Attack()
	{
		timer -= Time.deltaTime;
		if (timer <= 0) 
		{
			Instantiate(EnemyBullet,transform.position,transform.rotation);
			timer = 1f;
		}
	}

	void Move()
	{
		Vector3 PlayerPos = GameObject.FindGameObjectWithTag ("Player").transform.position;
		float PlayerX = PlayerPos.x - EnemyRigid2D.position.x;
		float PlayerY = PlayerPos.y - EnemyRigid2D.position.y;
		Vector2 TowardsPlayer = new Vector2 (PlayerX, PlayerY);
		Distance = Vector3.Distance (EnemyPos, PlayerPos);

		Debug.DrawLine (SightEnemy1.position, SightPlayer1.position, Color.blue);


		if (Detected) 
		{
			EnemyRigid2D.velocity = TowardsPlayer * 1 * Time.deltaTime;
			Direction();
			if(Physics2D.Linecast (SightEnemy1.position, SightPlayer1.position, 1 << LayerMask.NameToLayer ("Default")) &&
			   !Physics2D.Linecast (SightEnemy1.position, SightPlayer1.position, 1 << LayerMask.NameToLayer ("FirmWall")))
			{
				EnemyRigid2D.velocity = TowardsPlayer * 0;
				Attack();
			}
		}
		 if (!Spotted && !Detected) 
		{
            Patrol();
		}
		EnemyPos = new Vector2(EnemyRigid2D.position.x, EnemyRigid2D.position.y);
	}

    void Patrol()
    {

        if (CurWayPoint < patrolWayPoints.Length)
        {
            Target = patrolWayPoints[CurWayPoint].position;
            MoveDirection = Target - transform.position;
            Velocity = EnemyRigid2D.velocity;

            if (MoveDirection.magnitude < 1)
            {
                CurWayPoint++;
            }
            else
            {
                Velocity = MoveDirection.normalized * patrolSpeed;
            }
        }

        else
        {
            if (doPatrol)
            {
                CurWayPoint = 0;
            }
            else
            {
                Velocity = Vector3.zero;
            }
        }
        EnemyRigid2D.velocity = new Vector2(Velocity.x, Velocity.y);

        //transform.rotation = Quaternion.);
        //transform.LookAt(Target);

        

    }
	// Roterar fienden mot spelarens håll
	void Direction()
	{
		Vector3 PlayerPos = GameObject.FindGameObjectWithTag ("Player").transform.position;
		transform.rotation = Quaternion.LookRotation (Vector3.forward, PlayerPos - EnemyPos);
	}
    
   


    void ApplyDamage(int damage)
    {

        Health -= damage;
 
        if (Health <= 0)
        {
            Destroy(gameObject);
            SpawnCrate();
            SprayBlood();
        }

    }
    

    public void SprayBlood()
    {
        Bloodspatter = new GameObject[20];
        for (int i = 0; i < Bloodspatter.Length; i++)
        {
            GameObject clone = (GameObject)Instantiate(Blood, new Vector3(EnemyRigid2D.position.x,EnemyRigid2D.position.y), Quaternion.identity);
            Bloodspatter[i] = clone;

        }
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
    void Update()
    {
       
		Move ();


	}
}
