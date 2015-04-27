using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

    public bool Exploded = false;

    public float ExplosionTime = 0f;
    public float BombTimer = 0;

    public Animator Anim;
	// Use this for initialization
	void Start () {
        Anim = GetComponent<Animator>();
	}

    void OnTriggerEnter2D(Collider2D collissionobject)
    {
       
        if (collissionobject.tag == "Enemy" || collissionobject.tag == "Boss" && BombTimer >= ExplosionTime)
        {
            //if (!Exploded)
            //{
                collissionobject.gameObject.SendMessage("ApplyDamage", 20);
                Exploded = true;
            //}
        }
    }
	// Update is called once per frame
	void Update () {

        BombTimer += Time.deltaTime;
        if (BombTimer >= ExplosionTime)
        {
            Anim.SetBool("Explosion", true);
            
        }
        if (BombTimer >= ExplosionTime + 0.5f)
        {
            Destroy(gameObject);
        }
	}
}
