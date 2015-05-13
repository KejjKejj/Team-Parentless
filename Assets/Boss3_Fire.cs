using UnityEngine;
using System.Collections;

public class Boss3_Fire : MonoBehaviour {

    public Transform fireball;
    public Transform spawnPoint;
    private bool openFire = true;
    public float speed;

    private Rigidbody2D bulletBody = new Rigidbody2D();
    public GameObject WallHit;
    public AudioClip FirmWall;
    public AudioClip Body;
	// Use this for initialization
	void Start () 
    {
        StartCoroutine(Timer(2.0F));
        speed = 10;
        bulletBody = GetComponent<Rigidbody2D>();
        Vector3 PlayerPos = GameObject.FindGameObjectWithTag("Player").transform.position;

        float deltaX = -(transform.position.x - PlayerPos.x);
        float deltaY = -(transform.position.y - PlayerPos.y);

        float angle = Mathf.Atan2(deltaY, deltaX);

        bulletBody.velocity = new Vector2(Mathf.Cos(angle) * speed, Mathf.Sin(angle) * speed);
	}


    void Fire()
    {
        Transform firebullet = (Transform)Instantiate(fireball, spawnPoint.position, Quaternion.identity);
        firebullet.GetComponent<Rigidbody>().AddForce(transform.forward * speed);
        openFire = false;
        Timer(2.0f);
    }

    IEnumerator Timer(float waitForTime)
    {
        if (openFire == false)
        {
            yield return new WaitForSeconds(waitForTime);
            openFire = true;
            //Destroy(gameObject);
        }
    }

    void Collision(Collision2D coll)
    {
        if (coll.gameObject.tag == "FirmWall")
        {
            AudioSource.PlayClipAtPoint(FirmWall, transform.position, 0.02f);
            Instantiate(WallHit, transform.position, Quaternion.identity);
            //Destroy(gameObject);
        }
        Destroy(gameObject);
    }
	// Update is called once per frame
	void Update () 
    {
        if (openFire == true)
        {
            Fire();
            //openFire = false;
        }
	
	}
}
