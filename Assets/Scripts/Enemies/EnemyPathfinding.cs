using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyPathfinding : MonoBehaviour
{

    private GameObject[] ListOfWaypoints;
    private Rigidbody2D EnemyRigid2D;

    public Transform SightEnemy1, SightPlayer1;
    public GameObject Shot;

    private bool GoalFound;
    private bool MovingToTarget;
    private bool TargetSpotted;

    private float Timer = 0f;

    private Vector3 Goal = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
    private Vector3 Target = new Vector3(0, 0, 0);
    private Vector3 Spawn;

	// Use this for initialization
	void Start () {
        ListOfWaypoints = GameObject.FindGameObjectsWithTag("Waypoint");
        EnemyRigid2D = GetComponent<Rigidbody2D>();
	    Spawn = transform.position;
	}
	
    Vector3 FindClosest(Vector3 goal)
    {
        Vector3 Closest = new Vector3(float.MaxValue, float.MaxValue, 0);
        Vector3 Temp;
        for(int i = 0; i < ListOfWaypoints.Length; i++)
        {
            Temp = ListOfWaypoints[i].GetComponent<Transform>().position;
            if (Physics2D.Linecast(transform.position, Temp, 1 << LayerMask.NameToLayer("FirmWall")) ||
                Physics2D.Linecast(transform.position, Temp, 1 << LayerMask.NameToLayer("SoftWall"))) 
            {
                
            }
            else
            {
                if (Vector3.Distance(goal, Temp) <= Vector3.Distance(Temp, Closest))
                {
                    Closest = Temp;
                    Debug.Log(Closest + " Closest");
                    Debug.Log(goal);
                }
            }
        }
        
        return Closest;
    }

    Vector3 FindClosestToPlayer()
    {
        Vector3 PlayerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        Vector3 Closest = new Vector3(float.MaxValue, float.MaxValue, 0);
        Vector3 Temp;
        float TempDistance;
        for (int i = 0; i < ListOfWaypoints.Length; i++)
        {
            
            Temp = ListOfWaypoints[i].GetComponent<Transform>().position;
                TempDistance = Vector3.Distance(Temp, PlayerPos);
                if (TempDistance < Vector3.Distance(Closest, PlayerPos))
                {
                    Closest = Temp;
                }
            
        }
        return Closest;
    }

    void Chase()
    {
        if (!GoalFound)
        {
            Goal = FindClosestToPlayer();
            GoalFound = true;
            Debug.Log(Goal);
        }
        if (!MovingToTarget)
        {
            Target = FindClosest(Goal);
            MovingToTarget = true;
        }

        Vector3 MoveDirection = Target -transform.position;
        Vector3 Velocity = EnemyRigid2D.velocity;
        Velocity = MoveDirection.normalized * 5;
        EnemyRigid2D.velocity = new Vector2(Velocity.x, Velocity.y);        

        if (Vector3.Distance(transform.position, Target) < 0.1f)
        {
            MovingToTarget = false;
        }
    }

    void Patrol()
    {
        if (!MovingToTarget)
        {
            Target = FindClosest(Spawn);
            MovingToTarget = true;
        }

        Vector3 MoveDirection = Target - transform.position;
        Vector3 Velocity = EnemyRigid2D.velocity;
        Velocity = MoveDirection.normalized * 5;
        EnemyRigid2D.velocity = new Vector2(Velocity.x, Velocity.y);

        if (Vector3.Distance(transform.position, Target) < 0.1f)
        {
            MovingToTarget = false;
        }
    }

    void StopMoving()
    {
        EnemyRigid2D.velocity = new Vector2(0, 0);
    }

    private void Direction()
    {
        if (TargetSpotted)
        {
            Vector3 PlayerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
            transform.rotation = Quaternion.LookRotation(Vector3.forward, PlayerPos - transform.position);
        }
        else
        {
            transform.rotation = Quaternion.LookRotation(Vector3.forward, Target - transform.position);
        }
    }

    private void OnTriggerEnter2D(Collider2D collissionObject)
    {
        if (collissionObject.tag == "Player")
        {
            TargetSpotted = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collissionObject)
    {
        if (collissionObject.tag == "Player")
        {
            GoalFound = false;
        }
    }

    bool LineOfSight()
    {
        return Physics2D.Linecast(SightEnemy1.position, SightPlayer1.position, 1 << LayerMask.NameToLayer("Player")) &&
               !Physics2D.Linecast(SightEnemy1.position, SightPlayer1.position, 1 << LayerMask.NameToLayer("FirmWall"));
    }

    void Attack()
    {
        if (Timer <= 0)
        {
            Instantiate(Shot, transform.position, transform.rotation);
            Timer = 1f;
        }
    }

    // Update is called once per frame
	void Update() 
    {
        Direction();
	    if (!TargetSpotted)
	    {
	        Patrol();
	    }
	    else
	    {
	        if (LineOfSight())
	        {
	            StopMoving();
	            Attack();
	        }
	        else
	        {
	            Chase();
	        }
	    }
	    Timer -= Time.deltaTime;
        Debug.Log(TargetSpotted + " Spotted");
        Debug.Log(LineOfSight() + " LOS");
        Debug.DrawLine(SightEnemy1.position, SightPlayer1.position, Color.red);
    }

}
