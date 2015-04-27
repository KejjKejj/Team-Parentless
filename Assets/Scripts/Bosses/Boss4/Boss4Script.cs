using UnityEngine;
using System.Collections;

public class Boss4Script : MonoBehaviour {

    public GameObject Boss4Bullet;
    public Transform BossSight, PlayerSight;
    private Rigidbody2D BossRigid2D;
    private float timer = 0;
    private float ShotTimer = 0;

	// Use this for initialization
	void Start ()
    {
        BossRigid2D = GetComponent<Rigidbody2D>();
        Vector3 GunPos = GameObject.FindGameObjectWithTag("StaticGun").transform.position;
        Vector3 PlayerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
	}

    void Load(float BossX, float BossY)
    {
        Vector3 StaticGunPos = GameObject.FindGameObjectWithTag("StaticGun").transform.position;
        float GunX = StaticGunPos.x - BossX;
        float GunY = StaticGunPos.y - BossY;
        Vector2 TowardsGun = new Vector2(GunX, GunY);

        transform.rotation = Quaternion.LookRotation(Vector3.forward, StaticGunPos - new Vector3(BossRigid2D.position.x, BossRigid2D.position.y, 0));

        BossRigid2D.velocity = TowardsGun * 1;
    }

    void PickUpAmmo(float BossX, float BossY)
    {
        Vector3 Ammocrate = GameObject.FindGameObjectWithTag("Ammocrate").transform.position;
        float playerx =  Ammocrate.x - BossX;
        float playery =  Ammocrate.y - BossY;
        Vector2 towardsammo = new Vector2(playerx, playery);

        BossRigid2D.velocity = towardsammo * 1;
    }

    void RandomSpot(float x, float y)
    {

        Vector2 TowardsStandardSpot = new Vector2(x,y);
        BossRigid2D.velocity = TowardsStandardSpot * 5 * Time.deltaTime;
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
                float x = Random.Range(-5, 5) - BossRigid2D.position.x;
                float y = Random.Range(10, 15) - BossRigid2D.position.y;
                RandomSpot(x, y);
                Shot(Spotted, Hide);
            }

            if (timer > 5 && timer < 10)
            {
                float BossX = BossRigid2D.position.x;
                float BossY = BossRigid2D.position.y;
                Shot(Spotted, Hide);
                PickUpAmmo(BossX, BossY);
            }
            if (timer > 15 && timer < 22)
            {
                float BossX = BossRigid2D.position.x;
                float BossY = BossRigid2D.position.y;
                Load(BossX, BossY);
            }
            if (timer > 22)
            {
                float x = Random.Range(-5, 5) - BossRigid2D.position.x;
                float y = Random.Range(-5, 5) - BossRigid2D.position.y;
                RandomSpot(x, y);
                Shot(Spotted, Hide);
            }
            if (timer > 27)
                timer = 0;
        
    }

	// Update is called once per frame
	void Update () 
    {
        Debug.DrawLine(BossSight.position, PlayerSight.position, Color.blue);
        timer += Time.deltaTime;
        ShotTimer += Time.deltaTime;
        Fight();
	}
}
