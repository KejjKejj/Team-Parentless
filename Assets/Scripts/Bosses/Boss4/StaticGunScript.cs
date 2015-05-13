using UnityEngine;
using System.Collections;

public class StaticGunScript : MonoBehaviour {

    private Rigidbody2D GunRigid2D;
    private SpriteRenderer SR;
    private float AmmoTimer = 0;
    private float ShotTimer = 0;
    private bool Loaded = false;
    public GameObject Boss4Shot;

    public Sprite Ready;
    public Sprite Shooting;
    public Sprite Empty;

	// Use this for initialization
	void Start () {
        GunRigid2D = GetComponent<Rigidbody2D>();
        SR = GetComponent<SpriteRenderer>();
	}

    void OnTriggerEnter2D(Collider2D CollisionObject)
    {
        if (CollisionObject.tag == "Boss")
        {
            Loaded = true;
            AmmoTimer = 0;
            SR.sprite = Ready;
        }
    }


    void Shot()
    {
        SR.sprite = Shooting;
        if (ShotTimer > 0.05)
        {
            Vector3 PlayerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
            transform.rotation = Quaternion.LookRotation(Vector3.forward, PlayerPos - new Vector3(GunRigid2D.position.x, GunRigid2D.position.y, 0));
            Instantiate(Boss4Shot, transform.position + new Vector3(0,0,0), transform.rotation);
            ShotTimer = 0;
        }
    }

    void Attack()
    {
        SR.sprite = Empty;
        if (Loaded && AmmoTimer > 5)
            Shot();
        if (AmmoTimer > 10)
            Loaded = false;

    }
	// Update is called once per frame
	void Update () {
        AmmoTimer += Time.deltaTime;
        ShotTimer += Time.deltaTime;
        Attack();

	}
}
