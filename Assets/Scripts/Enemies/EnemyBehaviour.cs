using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {

	private Rigidbody2D EnemyRigid2D;
    public GameObject Blood;
    public GameObject AmmoCrate;
    public GameObject[] Bloodspatter;
    private int RandAmmo;
	private Vector3 EnemyPos;
	private float Vinkel;

    public float Health = 10;

    public bool Onfire = false;

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


    public Vector2 TempPlayerPos;


    public Transform testPath;



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
            TempPlayerPos = new Vector2(PlayerPos.x, PlayerPos.y);
		}
	}

	void OnTriggerExit2D(Collider2D collissionObject)
	{
		if (collissionObject.tag == "Player")
			Spotted = false;
	}

	void Attack()
	{

		if (timer <= 0) 
		{
			Instantiate(EnemyBullet,transform.position,transform.rotation);
			timer = 1f;
		}
	}


	void Move()
	{
		Vector3 PlayerPos = GameObject.FindGameObjectWithTag ("Player").transform.position;

		//Vector2 TowardsPlayer = new Vector2 (PlayerX, PlayerY);


		Debug.DrawLine (SightEnemy1.position, SightPlayer1.position, Color.blue);


		if (Detected) 
		{



            float LastSeenX = TempPlayerPos.x - EnemyPos.x;
            float LastSeenY = TempPlayerPos.y - EnemyPos.y;
            Vector2 TowardsLastSeen = new Vector2(LastSeenX, LastSeenY);
            EnemyRigid2D.velocity = TowardsLastSeen * 5 * Time.deltaTime;

            transform.rotation = Quaternion.LookRotation(Vector3.forward, TowardsLastSeen);

            //Om det är fri sikt, dvs ingen vägg finns mellan Enemy och player enligt linecasten
			if(Physics2D.Linecast (SightEnemy1.position, SightPlayer1.position, 1 << LayerMask.NameToLayer ("Player")) &&
			   !Physics2D.Linecast (SightEnemy1.position, SightPlayer1.position, 1 << LayerMask.NameToLayer ("FirmWall")))
			{
				EnemyRigid2D.velocity = TowardsLastSeen * 0;
                Direction();
				Attack();
                TempPlayerPos = new Vector2(PlayerPos.x, PlayerPos.y);
                EnemyPos = new Vector2(EnemyRigid2D.position.x, EnemyRigid2D.position.y);
			}
		}
		 if (!Spotted && !Detected) 
		{
            Patrol();
		}
		
	}

    void Hunt()
    {
        Vector3 Target = testPath.position;
        Vector3 MoveDirection = Target - transform.position;
        Vector3 Velocity = EnemyRigid2D.velocity;



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
 
        //if (Health <= 0)
        //{
        //    GameObject.FindGameObjectWithTag("Player").SendMessage("ApplyScore", 100);
        //    Destroy(gameObject);
        //    SpawnCrate();
        //    SprayBlood();
        //}

    }

    void ApplyFireDamage(bool Fire)
    {
        Onfire = Fire;

    }
    void CheckIfDead()
    {
        if (Health <= 0)
        {
            GameObject.FindGameObjectWithTag("Player").SendMessage("ApplyScore", 100);
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

		timer -= Time.deltaTime;


        CheckIfDead();
		Move ();
        if (Onfire)
        {
            Health -= 4 * Time.deltaTime;
        }

	}
}
