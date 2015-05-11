using UnityEngine;
using System.Collections;

public class Boss3Script : MainBossScript {


    private Rigidbody2D BossRigidbody2D;
    private Vector3 BossPos;
    public float Distance;
    public Transform Target;
    private float lookAtDistance = 10.0f;
    private float attackRange = 5.0f;
    private float moveSpeed_ = 5.0f;
    private float Damping = 6.0f;

    private float patrolSpeed;
    private int CurWayPoint;
    public bool doPatrol = true;
    public Transform[] patrolWayPoints;
    private Vector3 Targets;
    private Vector3 MoveDirection;
    private Vector3 Velocity;

    public int state = 2;
    

    //public Transform Enemy;
    //private bool IsAttacking = false;
    //public Rigidbody2D bullet;
    //private Vector3 Distances;
    //private float DistanceFrom;
    //public float fireRate = 0.5f;
    //public float nextFire = 0.0f;

    // Shooting
    public GameObject bullets;
    public Rigidbody2D bullet = new Rigidbody2D();
    public float fireRate = 0.5f;
    public float DistanceFrom;
    private float timer = 1f;

    private float curTime;
    public float pauseDuration = 0;


    void Start()
    {
        Health = 100;
        EnemyRigid2D = GetComponent<Rigidbody2D>();
        bullet = GetComponent<Rigidbody2D>();
        patrolSpeed = 5;
        //EnemyRigid2D = GetComponent<Rigidbody2D>();Health = 30;
        //BossRigidbody2D = GetComponent<Rigidbody2D>();
        //BossPos = new Vector3(BossRigidbody2D.position.y, BossRigidbody2D.position.x, 0);
    }

    void lookAt()
    {
        var rotation = Quaternion.LookRotation(Vector3.forward, Target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * Damping);
    }




    void attack()
    {
        //transform.Translate(Vector3.forward * moveSpeed_ * Time.deltaTime);
        if (timer <= 0)
        {
            Instantiate(bullet, transform.position, transform.rotation);
            timer = 1f;
        }
    }





    void BossMovement()
    {
        Distance = Vector3.Distance(Target.position, transform.position);

        BossRigidbody2D = GetComponent<Rigidbody2D>();

        if (CurWayPoint < patrolWayPoints.Length)
        {
            Targets = patrolWayPoints[CurWayPoint].position;
            MoveDirection = Targets - transform.position;
            Velocity = BossRigidbody2D.velocity;

            if (MoveDirection.magnitude < 0.5)
            {
                if (state == 1)
                    CurWayPoint = Random.Range(0, 3);
                if (state == 2)
                {
                    //if (curTime == 0)
                    //{
                    //    curTime = Time.time;
                    //}
                
                        //if ((Time.time - curTime) >= pauseDuration)
                        //{
                            CurWayPoint++;
                          //  curTime = 0;
                        //}
                        //if (CurWayPoint <= 7)
                        //{
                        //    //CurWayPoint += Random.Range(3,7);
                        //    CurWayPoint--;
                        //    //if (CurWayPoint == 3)
                        //    //    CurWayPoint++;
                        //}
                }
                    //CurWayPoint = Random.Range(3, 7);
                //CurWayPoint += Random.Range(0, patrolWayPoints.Length);
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
                if (state == 1)
                    CurWayPoint = Random.Range(0, 3);
                if (state == 2)
                {
                    CurWayPoint = 3;
                }
                //CurWayPoint = Random.Range(3,7);
            }
            else
            {
                Velocity = Vector3.zero;
            }
        }
        if (Distance > lookAtDistance)
        {
            BossRigidbody2D.velocity = new Vector2(Velocity.x, Velocity.y);
            var rotation = Quaternion.LookRotation(Vector3.forward, Targets - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * Damping);

            //BossRigidbody2D.velocity = new Vector2(Velocity.x, Velocity.y);
            //transform.rotation = Quaternion.LookRotation(Vector3.forward, Targets - transform.position);
        }
        if (Distance < lookAtDistance)
        {
            BossRigidbody2D.velocity = new Vector2(Velocity.x, Velocity.y);
            lookAt();
        }
    }

    void Update()
    {
        timer -= Time.deltaTime;
        BossMovement();
        if (Health <= 30)
        {
            patrolSpeed = 10;
            //CurWayPoint = 3;
            Debug.Log("Aj");
            state = 2;
        }
    }








    //public Transform target; // The enemy's target
    //public Transform myTransform; // Current transform of this enemy
    //public float moveSpeed = 3; // Move speed
    //public float rotationSpeed = 3; // Speed of turning
    //private Vector3 EnemyPos;

    //void awake()
    //{
    //    myTransform = transform;
    //}

    //Rigidbody2D Player = new Rigidbody2D();
    //Rigidbody2D Boss = new Rigidbody2D();
    //// Use this for initialization
    //void Start () 
    //{
    //    target = GameObject.FindGameObjectWithTag("Player").transform; // Target the player
    //}

    //void Direction()
    //{
    //    // Rotate to look at the player
    //    Vector3 PlayerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
    //    Vector3 EnemyPos = GameObject.FindGameObjectWithTag("Boss").transform.position;
    //    //transform.rotation = Quaternion.LookRotation(Vector3.forward, PlayerPos - EnemyPos);
    //    //transform.rotation = Quaternion.Slerp(target.rotation, myTransform.rotation, rotationSpeed * Time.deltaTime);
    //    //transform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(PlayerPos - myTransform.position), 0); //rotationSpeed * Time.deltaTime);
    //    myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(target.position - myTransform.position), rotationSpeed * Time.deltaTime);
    //    myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;
    //}
    //// Update is called once per frame
    //void Update () {
    //    Direction();
    //}
}
