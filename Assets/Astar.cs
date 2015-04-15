using UnityEngine;
using System.Collections;

public class Astar : MonoBehaviour {

	private Rigidbody2D EnemyRigid2D;	

	private ArrayList OpenCords = new ArrayList();
	private ArrayList ClosedCords = new ArrayList();

	// Use this for initialization
	void Start () {
		EnemyRigid2D = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 WallPos = GameObject.FindGameObjectWithTag ("FirmWall").transform.position;
		Vector2 wall = new Vector2 (WallPos.x, WallPos.y);
		Vector2 EnemyPos = new Vector2 (EnemyRigid2D.position.x, EnemyRigid2D.position.y);
		Vector2 Goal = new Vector2 (4, 2);

		float EnemyX = EnemyPos.x - Goal.x;
		float EnemyY = EnemyPos.y - Goal.y;
		Vector2 TowardsPlayer = new Vector2 (EnemyX, EnemyY);

		ClosedCords.Add (wall);




		for (int i = 0; i< 8; i ++)
		{
			for( int j = 0; j < 8; j++){
				OpenCords.Add(new Vector2(i,j));
			}
		}


		if (OpenCords.Contains (Goal)) {
			EnemyRigid2D.velocity = TowardsPlayer * 1;
		}

		//EnemyRigid2D.velocity = Goal * 3;

	}
}
