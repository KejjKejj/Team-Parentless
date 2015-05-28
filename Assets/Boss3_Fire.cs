using UnityEngine;
using System.Collections;

public class Boss3_Fire : MonoBehaviour {

    public Transform fireball;
    public Transform spawnPoint;
    private float FireTimer = 0;
    public float speed = 50;

    private Rigidbody2D bulletBody = new Rigidbody2D();
    public GameObject WallHit;
    public AudioClip FirmWall;
    public AudioClip Body;
	// Use this for initialization
	void Start () 
    {
        bulletBody = GetComponent<Rigidbody2D>();
        Vector3 PlayerPos = GameObject.FindGameObjectWithTag("Player").transform.position;  

        float deltaX = -(transform.position.x - PlayerPos.x);
        float deltaY = -(transform.position.y - PlayerPos.y);

        float angle = Mathf.Atan2(deltaY, deltaX);

        bulletBody.velocity = new Vector2(Mathf.Cos(angle) * speed, Mathf.Sin(angle) * speed);
	}

    void Fire()
    {
        if (FireTimer >= 1.0f)
        {
            Transform firebullet = (Transform)Instantiate(fireball, spawnPoint.position, Quaternion.identity);
            firebullet.GetComponent<Rigidbody2D>().AddForce(transform.forward * speed);
            FireTimer = 0;
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "FirmWall")
        {
            AudioSource.PlayClipAtPoint(FirmWall, transform.position, 0.02f);
            Instantiate(WallHit, transform.position, Quaternion.identity);
        }
        if (coll.gameObject.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(Body, transform.position, 0.1f);
            coll.gameObject.SendMessage("ApplyDamage", 20);
        }
        Destroy(gameObject);
    }
	// Update is called once per frame
	void Update () 
    {

        Fire();
        FireTimer += Time.deltaTime;
    }
}
