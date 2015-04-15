using UnityEngine;
using System.Collections;

public class Boss2Script : MainBossScript {

    public bool Run;
    public float Speed = 5;
    public float ChargeSpeed = 20;
    private Vector3 EnemyPos;
    Rigidbody2D Player = new Rigidbody2D();
    Rigidbody2D Boss = new Rigidbody2D();
    private Ray2D BossSight;
    public Transform SightStart, SightEnd;
    public bool Spotted = false;
    RaycastHit2D hit,hitupperright,hitupperleft;
    public bool left, right, charge;
    Vector3 upperright,upperleft;
    Vector2 chargedir;

	// Use this for initialization
	void Start () {
        
        EnemyRigid2D = GetComponent<Rigidbody2D>();
        EnemyPos = new Vector3(EnemyRigid2D.position.x, EnemyRigid2D.position.y, 0);
        Health = 30;
	}

    void ChargeAttack()
    {
        charge = true;
        EnemyRigid2D.velocity = chargedir;
        
        
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.transform.tag == "FirmWall")
        {
            charge = false;
        }
        if (coll.transform.tag == "Player")
        {
            GameObject.Find("Character").SendMessage("ApplyDamage",5);
            charge = false;

        }
   
    }
    void Raycasting()
    {

        upperright = transform.up + transform.right /2;
        upperleft = transform.up - transform.right /2;
        hit = Physics2D.Raycast(SightStart.position, transform.up, 1000, 1 << LayerMask.NameToLayer("Player"));
        hitupperright = Physics2D.Raycast(SightStart.position,upperright ,1000, 1 << LayerMask.NameToLayer("Player"));
        hitupperleft = Physics2D.Raycast(SightStart.position,upperleft,1000, 1 << LayerMask.NameToLayer("Player"));
    

        Debug.DrawLine(SightStart.position, hit.point, Color.black);
     
        Debug.DrawLine(SightStart.position, hitupperleft.point, Color.green);
        Debug.DrawLine(SightStart.position, hitupperright.point, Color.magenta);

    }

    void Rotate()
    {
        Raycasting();
        Player = GameObject.Find("Character").GetComponent<Movement>().ReturnPlayerPos();
        float deltaX = -(EnemyRigid2D.transform.position.x - Player.position.x);

        float deltaY = -(EnemyRigid2D.transform.position.y - Player.position.y);

        float angle = Mathf.Atan2(deltaY, deltaX);


        if (hitupperright.collider != null && hitupperright.transform.tag == "Player")
        {
            
            left = false;
            right = true;
        }
        if (hitupperleft.collider != null && hitupperleft.transform.tag == "Player")
        {
            
            left = true;
            right = false;
        }

        if (!charge)
        {
            if (hitupperright.collider != null && hitupperright.transform.tag == "Player")
            {
                EnemyRigid2D.velocity = new Vector2(Mathf.Cos(angle) * Speed, Mathf.Sin(angle) * Speed);
                left = false;
                right = true;
            }
            if (hitupperleft.collider != null && hitupperleft.transform.tag == "Player")
            {
                EnemyRigid2D.velocity = new Vector2(Mathf.Cos(angle) * Speed, Mathf.Sin(angle) * Speed);
                left = true;
                right = false;
            }
            if (hit.collider != null && hit.transform.tag == "Player")
            {
                chargedir = new Vector2(Mathf.Cos(angle) * ChargeSpeed, Mathf.Sin(angle) * ChargeSpeed);
                ChargeAttack();
                

                //EnemyRigid2D.velocity = new Vector2(Mathf.Cos(angle) * Speed, Mathf.Sin(angle) * Speed);
            }

            else if (left)
            {
                transform.Rotate(new Vector3(0, 0, 3));
                EnemyRigid2D.velocity = new Vector2(0, 0);
            }
            else if (right)
            {
                transform.Rotate(new Vector3(0, 0, -3));
                EnemyRigid2D.velocity = new Vector2(0, 0);
            }

        }
    }
	// Update is called once per frame
	void FixedUpdate () {
       
        Rotate();
	}
}
