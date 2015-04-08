using UnityEngine;
using System.Collections;

public class BossScript : MonoBehaviour {

    Rigidbody2D EnemyRigid2D;
    private Vector3 EnemyPos;
    public GameObject BossShot;
    public float TimeBetweenShots = 0.5f;
    public float Timer = 0;
    public Rigidbody2D ReturnBossPos()
    {
        return EnemyRigid2D;
    }

   
    

	// Use this for initialization
	void Start () {
        
        EnemyRigid2D = GetComponent<Rigidbody2D>();
        EnemyPos = new Vector3 (EnemyRigid2D.position.x, EnemyRigid2D.position.y, 0);
        
	}

    bool GetPlayerInRange()
    {
        return GameObject.Find("BossZone").GetComponent<BossZone>().OpenFire;
    }
    void Shot()
    {
        
        if (GetPlayerInRange())
        {
            Direction();
            if (Timer >= TimeBetweenShots)
            {
                ShotPattern();
                Timer = 0;
            }
         
            Timer += Time.deltaTime;
        }


    }
    void ShotPattern()
    {
        Instantiate(BossShot, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
        Instantiate(BossShot, transform.position, Quaternion.Euler(new Vector3(0, 0, 5)));
        Instantiate(BossShot, transform.position, Quaternion.Euler(new Vector3(0, 0, -5)));
    }
    void Direction()
    {
        Vector3 PlayerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, PlayerPos - EnemyPos);
        
    }

   

    
	// Update is called once per frame
	void Update () {
        Shot();
        
	}
}
