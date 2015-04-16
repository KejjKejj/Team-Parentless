using UnityEngine;
using System.Collections;

public class Knife : Weapon
{

    public AudioClip Slash;


    public GameObject obj;
    GameObject Player;
    public Color CurrColor;
    private float i = 1f;
	// Use this for initialization
    void Start()
    {
        FireRate = 1.0f;
        Player = GameObject.Find("Character");
        CurrColor = gameObject.GetComponent<Renderer>().material.color;
        Vector3 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 CharPos = new Vector3(Player.transform.position.x, Player.transform.position.y);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, MousePos - CharPos);
        
        float deltaX = -((Screen.width / 2) - Input.mousePosition.x);
        float deltaY = -((Screen.height / 2) - Input.mousePosition.y);

        float angle = Mathf.Atan2(deltaY, deltaX);
        
        transform.position = new Vector3(Player.transform.position.x + (Mathf.Cos(angle)), Player.transform.position.y + (Mathf.Sin(angle)), Player.transform.position.z);

        AudioSource.PlayClipAtPoint(Slash, transform.position, 0.5f);
    }


    void Disappear()
    {

        CurrColor.a = i;
        gameObject.GetComponent<Renderer>().material.color = CurrColor;
        i -= 0.1f;


        if (i < 0)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log(coll.transform.tag);
        if (coll.transform.tag == "Boss" || coll.transform.tag == "Enemy")
        {
            coll.gameObject.SendMessage("ApplyDamage", 5);
        }

    }
    void Update()
    {
        Disappear();
    }
}
