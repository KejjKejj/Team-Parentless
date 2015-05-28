using UnityEngine;
using System.Collections;

public class Boss4Script : MonoBehaviour {

    public GameObject Boss4Bullet;
    public Transform BossSight, PlayerSight;
    private Rigidbody2D BossRigid2D;
    private GameObject[] ChasePoints;
    private float timer = 0;
    private float ShotTimer = 0;

	// Use this for initialization
    void Start()
    {
        BossRigid2D = GetComponent<Rigidbody2D>();
        ChasePoints = GameObject.FindGameObjectsWithTag("Boss4Points");
    }

    void Load()
    {
        Vector3 StaticGunPos = GameObject.Find("Turret").transform.position;
        float GunX = StaticGunPos.x - transform.position.x;
        float GunY = StaticGunPos.y - transform.position.y;
        Vector2 TowardsGun = new Vector2(GunX, GunY);

        transform.rotation = Quaternion.LookRotation(Vector3.forward, StaticGunPos - new Vector3(BossRigid2D.position.x, BossRigid2D.position.y, 0));

        BossRigid2D.velocity = TowardsGun * 1;
    }

    void PickUpAmmo()
    {
        Vector3 Ammocrate = GameObject.FindGameObjectWithTag("AmmoBoxes").transform.position;
        float playerx =  Ammocrate.x - transform.position.x;
        float playery =  Ammocrate.y - transform.position.y;
        Vector2 towardsammo = new Vector2(playerx, playery);

        BossRigid2D.velocity = towardsammo * 1;
    }

    void RandomSpot(float x)
    {
        Vector3 Waypoint1 = GameObject.Find("BossPoint1").transform.position;
        Vector3 Waypoint2 = GameObject.Find("BossPoint2").transform.position;

        if (x == 1)
        {
            Vector2 TowardsPoint1 = new Vector2(Waypoint1.x, Waypoint1.y);
            BossRigid2D.velocity = TowardsPoint1 * 5 * Time.deltaTime;
        }
        else if(x == 0){
            Vector2 TowardsPoint2 = new Vector2(Waypoint2.x, Waypoint2.y);
            BossRigid2D.velocity = TowardsPoint2 * 5 * Time.deltaTime;
        }
    }

    void Shot(bool Spotted, bool Hide)
    {
        Vector3 PlayerPos = GameObject.FindGameObjectWithTag("Player").transform.position;

        if (Spotted && !Hide)
        {
            if (ShotTimer > 0.5)
            {
                Instantiate(Boss4Bullet, transform.position, transform.rotation);
                ShotTimer = 0;
                transform.rotation = Quaternion.LookRotation(Vector3.forward, PlayerPos - new Vector3(BossRigid2D.position.x, BossRigid2D.position.y, 0));
            }
        }
    }

    void Fight()
    {
        bool Spotted = Physics2D.Linecast(BossSight.position, PlayerSight.position, 1 << LayerMask.NameToLayer("Player"));
        bool Hide = Physics2D.Linecast(BossSight.position, PlayerSight.position, 1 << LayerMask.NameToLayer("FirmWall"));


            if (Spotted && !Hide && timer < 5)
            {
                float x = Random.Range(0, 1) - BossRigid2D.position.x;
                RandomSpot(x);
                Shot(Spotted, Hide);
            }

            if (timer > 2 && timer < 3)
            {
                float BossX = BossRigid2D.position.x;
                float BossY = BossRigid2D.position.y;
                Shot(Spotted, Hide);
                PickUpAmmo();
            }
            if (timer > 5 && timer < 6)
            {
                float BossX = BossRigid2D.position.x;
                float BossY = BossRigid2D.position.y;
                Load();
            }
            if (timer > 8)
            {
                float x = Random.Range(0, 1) - BossRigid2D.position.y;
                RandomSpot(x);
                Shot(Spotted, Hide);
            }
            if (timer > 10)
                timer = 0;
        
    }

    protected bool InsideBossRoom() { return GameObject.Find("Boss4Zone").GetComponent<Boss4Zone>().InsideBossRoom; }
	// Update is called once per frame
	void Update () 
    {
        Debug.DrawLine(BossSight.position, PlayerSight.position, Color.blue);
        timer += Time.deltaTime;
        ShotTimer += Time.deltaTime;
        if(InsideBossRoom())
            Fight();
        gameObject.GetComponent<MainBossScript>().CheckIfDead();
	}
}
