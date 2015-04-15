using UnityEngine;
using System.Collections;

public class BossScript : MainBossScript {

    
    
    public int state = 1;
    private Vector3 EnemyPos;
    
    public GameObject BossShot;
    private GameObject shot1,shot2,shot3,shot4;
    public float TimeBetweenShots = 0.5f;
    public float Timer = 0;
    public float BossRottimer = 0;

    public Rigidbody2D ReturnBossPos()
    {
        return EnemyRigid2D;
    }

   
    

	// Use this for initialization
	void Start () {
        
        EnemyRigid2D = GetComponent<Rigidbody2D>();
        EnemyPos = new Vector3 (EnemyRigid2D.position.x, EnemyRigid2D.position.y, 0);
        Health = 30;
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
        if (state == 1)
        {
            shot1 = (GameObject)Instantiate(BossShot, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            shot1.GetComponent<BossShot>().angleshot = 100;
            shot2 = (GameObject)Instantiate(BossShot, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            shot2.GetComponent<BossShot>().angleshot = -100;
            shot3 = (GameObject)Instantiate(BossShot, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            shot3.GetComponent<BossShot>().angleshot = 0;
        }
        if (state == 2)
        {
            TimeBetweenShots = 0.25f;
            shot1 = (GameObject)Instantiate(BossShot, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            shot1.GetComponent<BossShot>().angleshot = 0;
            shot1.GetComponent<BossShot>().speed = 10;
            shot2 = (GameObject)Instantiate(BossShot, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            shot2.GetComponent<BossShot>().angleshot = 3.1f;
            shot2.GetComponent<BossShot>().speed = 10;
            shot3 = (GameObject)Instantiate(BossShot, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            shot3.GetComponent<BossShot>().angleshot = 1.5f;
            shot3.GetComponent<BossShot>().speed = 10;
            shot4 = (GameObject)Instantiate(BossShot, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            shot4.GetComponent<BossShot>().angleshot = 4.7f;
            shot4.GetComponent<BossShot>().speed = 10;

        }

    }

    void Direction()
    {
        if (state == 1)
        {
            Vector3 PlayerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
            transform.rotation = Quaternion.LookRotation(Vector3.forward, PlayerPos - EnemyPos);
        }
        if (state == 2)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, BossRottimer));
           
            BossRottimer += 1f;
            
        }
    }


	// Update is called once per frame
	void Update () {
        Shot();
        if (Health > 10)
        { state = 1; }
        if (Health <= 10)
        { state = 2; }
        
	}
}
