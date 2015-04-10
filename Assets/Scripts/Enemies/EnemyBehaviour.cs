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
			EnemyRigid2D.velocity = new Vector2(0,0);
		}
		EnemyPos = new Vector2(EnemyRigid2D.position.x, EnemyRigid2D.position.y);
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
	}
}
