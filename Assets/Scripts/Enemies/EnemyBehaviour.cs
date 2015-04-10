using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {

	private Rigidbody2D EnemyRigid2D;
    public GameObject Blood;
    public GameObject AmmoCrate;
    public GameObject[] Bloodspatter;
	private float Speed = 0.5f;
    private int RandAmmo;
	private Vector3 EnemyPos;
	private float Distance;
	private float Vinkel;
    private int state = 0;


    public float patrolSpeed;
    public int CurWayPoint;
    public bool doPatrol = true;
    public Transform[] patrolWayPoints;
    public Vector3 Target;
    public Vector3 MoveDirection;
    public Vector3 Velocity;







	// Use this for initialization
	void Start ()
	{
		EnemyRigid2D = GetComponent<Rigidbody2D> ();
		EnemyPos = new Vector3 (EnemyRigid2D.position.x, EnemyRigid2D.position.y, 0);
        
	}
	void Move()
	{
		Vector3 PlayerPos = GameObject.FindGameObjectWithTag ("Player").transform.position;
		float PlayerX = PlayerPos.x - EnemyRigid2D.position.x;
		float PlayerY = PlayerPos.y - EnemyRigid2D.position.y;
		Vector2 TowardsPlayer = new Vector2 (PlayerX, PlayerY);
		Distance = Vector3.Distance (EnemyPos, PlayerPos);

		if (Distance < 5) 
		{
			EnemyRigid2D.velocity = TowardsPlayer * Speed;
			Direction();
		} 
		else if (Distance > 5) 
		{
			//EnemyRigid2D.velocity = new Vector2(Random.Range(5,5),Random.Range(2,2));
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
        transform.LookAt(Target);

        //if (EnemyRigid2D.position.x < -6)
        //{
        //    Vector3 Rand = new Vector3(Random.Range(0, 10), Random.Range(-10, 10), 0);
        //    EnemyRigid2D.velocity = Rand * Speed;
        //    transform.rotation = Quaternion.LookRotation(Vector3.forward, Rand);
        //}
        //else if (EnemyRigid2D.position.x > 25)
        //{
        //    Vector3 Rand = new Vector3(Random.Range(-10, 0), Random.Range(-10, 10), 0);
        //    EnemyRigid2D.velocity = Rand * Speed;
        //    transform.rotation = Quaternion.LookRotation(Vector3.forward, Rand);
        //}
        //else if (EnemyRigid2D.position.y > 12)
        //{
        //    Vector3 Rand = new Vector3(Random.Range(-10, 10), Random.Range(-10, 0), 0);
        //    EnemyRigid2D.velocity = Rand * Speed;
        //    transform.rotation = Quaternion.LookRotation(Vector3.forward, Rand);
        //}
        //if (EnemyRigid2D.position.y < 0)
        //{
        //    Vector3 Rand = new Vector3(Random.Range(-10, 10), Random.Range(0, 10), 0);
        //    EnemyRigid2D.velocity = Rand * Speed;
        //    transform.rotation = Quaternion.LookRotation(Vector3.forward, Rand);
        //}
    }
	// Roterar fienden mot spelarens håll
	void Direction()
	{
		Vector3 PlayerPos = GameObject.FindGameObjectWithTag ("Player").transform.position;
		transform.rotation = Quaternion.LookRotation (Vector3.forward, PlayerPos - EnemyPos);
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log("Träff");
        if (coll.gameObject.tag == "Shot1")
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
        //Test();
	}
}
