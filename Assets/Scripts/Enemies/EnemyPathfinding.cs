using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyPathfinding : MonoBehaviour {

    private GameObject[] ListOfWaypoints, ListOfWaypointsPlayer;
    private Rigidbody2D EnemyRigid2D;
	// Use this for initialization
	void Start () {
        ListOfWaypoints = GameObject.FindGameObjectsWithTag("Waypoint");
        ListOfWaypointsPlayer = GameObject.FindGameObjectsWithTag("Waypoint");
        EnemyRigid2D = GetComponent<Rigidbody2D>();
	}
	
    Vector2 FindClosest()
    {
        Vector3 PlayerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        Vector2 Closest = new Vector2(float.MaxValue, float.MaxValue);
        Vector2 Temp;
        float Temp2;
        for(int i = 0; i < ListOfWaypoints.Length; i++)
        {
            Temp = ListOfWaypoints[i].GetComponent<Transform>().position;
            if (Physics2D.Linecast(PlayerPos, Temp, 1 << LayerMask.NameToLayer("FirmWall"))) {
                
            }

            else
            {
                Temp2 = Vector2.Distance(Temp, PlayerPos);
                if (Temp2 < Vector2.Distance(Closest, PlayerPos) && Temp2 > 0.1f)
                    Closest = Temp;
            }
        }
        return Closest;
    }

    Vector2 FindClosestToPlayer()
    {

        Vector2 Closest = new Vector2(float.MaxValue, float.MaxValue);
        Vector2 Temp;
        float Temp2;
        for (int i = 0; i < ListOfWaypointsPlayer.Length; i++)
        {
            Temp = ListOfWaypointsPlayer[i].GetComponent<Transform>().position;
            if (Physics2D.Linecast(GetComponent<Transform>().position, Temp, 1 << LayerMask.NameToLayer("FirmWall")))
            {

            }
            else
            {
                Temp2 = Vector2.Distance(Temp, transform.position);
                if (Temp2 < Vector2.Distance(Closest, transform.position))
                    Closest = Temp;
            }
            
        }
        return Closest;
    }

    void Chase()
    {
        Vector3 Goal = FindClosestToPlayer();
        Vector3 Target = FindClosest();
        Vector3 MoveDirection = Target -transform.position;
        Vector3 Velocity = EnemyRigid2D.velocity;
        Velocity = MoveDirection.normalized * 5;
        EnemyRigid2D.velocity = new Vector2(Velocity.x, Velocity.y);
    }

    void Patrol()
    {

    }


	// Update is called once per frame
	void Update () {

        Chase();
        Debug.Log(ListOfWaypoints[0].GetComponent<Transform>().position);
        Debug.Log(ListOfWaypoints[1].GetComponent<Transform>().position);
        Debug.Log(ListOfWaypointsPlayer[0].GetComponent<Transform>().position);
	}
}
