using UnityEngine;
using System.Collections;

public class BossScript : MainBossScript {

    
    
    public int state = 1;
    private Vector3 EnemyPos;
    
    public GameObject BossShot;
    private GameObject shot1,shot2,shot3,shot4,shot5,shot6,shot7,shot8;
    public float TimeBetweenShots = 0.5f;
    public float Timer = 0;
    public float BossRottimer = 0;
    public Vector2 dir;
    public int StateSwitch = 0;
   
    //public Rigidbody2D ReturnBossPos()
    //{
    //    return EnemyRigid2D;
    //}

   
    

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
                
               

                if (state == 3 && StateSwitch >= 10)
                {
                    
                    state = 2;
                    StateSwitch = 0;
                }
                else if (state == 3)
                    StateSwitch++;

                 if (state == 2 && StateSwitch >= 10)
                                {
                                    state = 3;
                                    StateSwitch = 0;
                                }
                                else if(state == 2)
                                    StateSwitch++;
                Timer = 0;
            }
         
            Timer += Time.deltaTime;
        }


    }
    void ShotPattern()
    {
        if (state == 1)
        {
            shot1 = (GameObject)Instantiate(BossShot, transform.position, transform.rotation);
            shot1.GetComponent<Transform>().Rotate(-1f,0, 0);
            shot1.GetComponent<Rigidbody2D>().AddForce(shot1.transform.forward * 30000);
            
            shot2 = (GameObject)Instantiate(BossShot, transform.position, transform.rotation);
            shot2.GetComponent<Transform>().Rotate(-1f, 0.2f, 0);
            shot2.GetComponent<Rigidbody2D>().AddForce(shot2.transform.forward * 30000);

            shot3 = (GameObject)Instantiate(BossShot, transform.position, transform.rotation);
            shot3.GetComponent<Transform>().Rotate(-1f, -0.2f, 0);
            shot3.GetComponent<Rigidbody2D>().AddForce(shot3.transform.forward * 30000);
        }
        if (state == 2)
        {
            TimeBetweenShots = 0.3f;
            
            //shot1 = (GameObject)Instantiate(BossShot, transform.position, Quaternion.identity);
            //shot1.GetComponent<BossShot>().dirr = transform.up;
            //shot1.GetComponent<BossShot>().angleshot =0;
            //shot1.GetComponent<BossShot>().speed = 20;
       
            //shot2 = (GameObject)Instantiate(BossShot, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            //shot2.GetComponent<BossShot>().dirr = transform.right;
            //shot2.GetComponent<BossShot>().angleshot = Mathf.PI / 2;
            //shot2.GetComponent<BossShot>().speed = 20;
           
            //shot3 = (GameObject)Instantiate(BossShot, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            //shot3.GetComponent<BossShot>().dirr = -transform.up;
            //shot3.GetComponent<BossShot>().angleshot = Mathf.PI;
            //shot3.GetComponent<BossShot>().speed = 20;
           
            //shot4 = (GameObject)Instantiate(BossShot, transform.position, Quaternion.Euler(new Vector3(0,0 , 0)));
            //shot4.GetComponent<BossShot>().dirr = -transform.right;
            //shot4.GetComponent<BossShot>().angleshot = Mathf.PI * 1.5f;
            //shot4.GetComponent<BossShot>().speed = 20;
            shot1 = (GameObject)Instantiate(BossShot, transform.position, transform.rotation);
            shot1.GetComponent<Transform>().Rotate(-1f, 0, 0);
            shot1.GetComponent<Rigidbody2D>().AddForce(shot1.transform.forward * 25000);

            shot2 = (GameObject)Instantiate(BossShot, transform.position, transform.rotation);
            shot2.GetComponent<Transform>().Rotate(1f, 0, 0);
            shot2.GetComponent<Rigidbody2D>().AddForce(shot2.transform.forward * 25000);

            shot3 = (GameObject)Instantiate(BossShot, transform.position, transform.rotation);
            shot3.GetComponent<Transform>().Rotate(0, -1f, 0);
            shot3.GetComponent<Rigidbody2D>().AddForce(shot3.transform.forward * 25000);


            shot4 = (GameObject)Instantiate(BossShot, transform.position, transform.rotation);
            shot4.GetComponent<Transform>().Rotate(0, 1f, 0);
            shot4.GetComponent<Rigidbody2D>().AddForce(shot4.transform.forward * 25000);

            shot5 = (GameObject)Instantiate(BossShot, transform.position, transform.rotation);
            shot5.GetComponent<Transform>().Rotate(-1f, -1f, 0);
            shot5.GetComponent<Rigidbody2D>().AddForce(shot5.transform.forward *10000);

            shot6 = (GameObject)Instantiate(BossShot, transform.position, transform.rotation);
            shot6.GetComponent<Transform>().Rotate(1f, 1f, 0);
            shot6.GetComponent<Rigidbody2D>().AddForce(shot6.transform.forward * 10000);

            shot7 = (GameObject)Instantiate(BossShot, transform.position, transform.rotation);
            shot7.GetComponent<Transform>().Rotate(1f, -1f, 0);
            shot7.GetComponent<Rigidbody2D>().AddForce(shot7.transform.forward * 10000);


            shot8 = (GameObject)Instantiate(BossShot, transform.position, transform.rotation);
            shot8.GetComponent<Transform>().Rotate(-1f, 1f, 0);
            shot8.GetComponent<Rigidbody2D>().AddForce(shot8.transform.forward * 10000);
        }
        if (state == 3)
        {

            shot1 = (GameObject)Instantiate(BossShot, transform.position, transform.rotation);
            shot1.GetComponent<Transform>().Rotate(-1f, 0, 0);
            shot1.GetComponent<Rigidbody2D>().AddForce(shot1.transform.forward * 20000);

            shot2 = (GameObject)Instantiate(BossShot, transform.position, transform.rotation);
            shot2.GetComponent<Transform>().Rotate(1f, 0, 0);
            shot2.GetComponent<Rigidbody2D>().AddForce(shot2.transform.forward * 20000);

            shot3 = (GameObject)Instantiate(BossShot, transform.position, transform.rotation);
            shot3.GetComponent<Transform>().Rotate(0, -1f, 0);
            shot3.GetComponent<Rigidbody2D>().AddForce(shot3.transform.forward * 20000);


            shot4 = (GameObject)Instantiate(BossShot, transform.position, transform.rotation);
            shot4.GetComponent<Transform>().Rotate(0, 1f, 0);
            shot4.GetComponent<Rigidbody2D>().AddForce(shot4.transform.forward * 20000);

            shot5 = (GameObject)Instantiate(BossShot, transform.position, transform.rotation);
            shot5.GetComponent<Transform>().Rotate(-1f, -1f, 0);
            shot5.GetComponent<Rigidbody2D>().AddForce(shot5.transform.forward * 10000);

            shot6 = (GameObject)Instantiate(BossShot, transform.position, transform.rotation);
            shot6.GetComponent<Transform>().Rotate(1f, 1f, 0);
            shot6.GetComponent<Rigidbody2D>().AddForce(shot6.transform.forward * 10000);

            shot7 = (GameObject)Instantiate(BossShot, transform.position, transform.rotation);
            shot7.GetComponent<Transform>().Rotate(1f, -1f, 0);
            shot7.GetComponent<Rigidbody2D>().AddForce(shot7.transform.forward * 10000);


            shot8 = (GameObject)Instantiate(BossShot, transform.position, transform.rotation);
            shot8.GetComponent<Transform>().Rotate(-1f, 1f, 0);
            shot8.GetComponent<Rigidbody2D>().AddForce(shot8.transform.forward * 10000);
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
           
            BossRottimer += 0.75f;
            
        }
        if (state == 3)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, BossRottimer));

            BossRottimer -= 1.5f;

        }
    }
    void OnFireDamage()
    {
        if (Onfire)
        {
            Health -= 7 * Time.deltaTime;

        }
    }
	void Update () {
        Shot();
        if (Health > 10)
        { state = 1; }
        if (Health == 10)
        { state = 2; }
        CheckIfDead();
        OnFireDamage();
	}
}
