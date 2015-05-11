using UnityEngine;
using System.Collections;

public class Flame : MonoBehaviour
{
    public bool Onfire = false;
    public float speed = 20;
    private float disapperRate = 1f;
    private Rigidbody2D Bulletbody2d = new Rigidbody2D();

    public GameObject WallHit;

    public AudioClip FirmWall;
    public AudioClip Body;

    public float Recoil;
    public float AngleShot = 0;
    public int damage;
    public float DecreaseRate;
    public Color CurrColor;

    // Use this for initialization
    void Start()
    {
        CurrColor = gameObject.GetComponent<Renderer>().material.color;
        DecreaseRate = Random.Range(0.9f, 0.96f);
        damage = GameObject.Find("Character").GetComponent<Movement>().WeaponDamage;

        Bulletbody2d = GetComponent<Rigidbody2D>();
        GameObject[] Weapontype = GameObject.FindGameObjectsWithTag("Weapon");
        for (int i = 0; i < Weapontype.Length; i++)
        {
            if (Weapontype[i].GetComponent<Weapon>().IsPickedUp)
            {
                Recoil = Weapontype[i].GetComponent<Weapon>().Recoil;
            }
        }

        Recoil = Random.Range(-Recoil, Recoil);

        float deltaX = -((Screen.width / 2) - Input.mousePosition.x);
        float deltaY = -((Screen.height / 2) - Input.mousePosition.y);

        float angle = Mathf.Atan2(deltaY, deltaX);

        Bulletbody2d.velocity = new Vector2(Mathf.Cos(angle + Recoil + AngleShot) * speed, Mathf.Sin(angle + Recoil + AngleShot) * speed);

    }

    //void OnCollisionEnter2D(Collision2D coll)
    //{
    //    if (coll.gameObject.tag == "FirmWall")
    //    {
    //        AudioSource.PlayClipAtPoint(FirmWall, transform.position, 0.02f);
    //        Instantiate(WallHit, transform.position, Quaternion.identity);
    //    }
    //    if (coll.gameObject.tag == "Enemy" || coll.gameObject.tag == "Boss")
    //    {
    //        AudioSource.PlayClipAtPoint(Body, transform.position, 0.1f);

    //        coll.gameObject.SendMessage("ApplyDamage", damage);

    //    }
    //    Destroy(gameObject);
    //}

    void Disappear()
    {

        CurrColor.a = disapperRate;
        gameObject.GetComponent<Renderer>().material.color = CurrColor;
        disapperRate -= 0.03f;


        if (disapperRate < 0)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator OnTriggerStay2D(Collider2D coll)
    {

        //if (coll.gameObject.tag == "Player")
        //{

        //    GameObject.Find("Character").GetComponent<Movement>().OnFire = true;
        //}

        if (coll.gameObject.tag == "Enemy" || coll.gameObject.tag == "Boss")
        {
            coll.gameObject.SendMessage("ApplyFireDamage", true);
            yield return new WaitForSeconds(1);
        }
    }


    void OnTriggerExit2D(Collider2D coll)
    {
        //if (coll.gameObject.tag == "Player")
        //{

        //    GameObject.Find("Character").GetComponent<Movement>().OnFire = false;
        //}
        if (coll.gameObject.tag == "Enemy" || coll.gameObject.tag == "Boss")
        {
            
            coll.gameObject.SendMessage("ApplyFireDamage", false);
        }
    }

    void OnDestroy()
    {
        if (GameObject.FindGameObjectWithTag("Enemy") != null)
        {
            GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>().Onfire = false; 
   
        }
        if (GameObject.Find("Turret") != null)
        {
            GameObject.Find("Turret").GetComponent<TurretScript>().Onfire = false;
        }
        if (GameObject.FindGameObjectWithTag("Boss") != null)
        {
            GameObject.FindGameObjectWithTag("Boss").GetComponent<MainBossScript>().Onfire = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        Bulletbody2d.velocity = Bulletbody2d.velocity * DecreaseRate;
        Disappear();
    }
}
