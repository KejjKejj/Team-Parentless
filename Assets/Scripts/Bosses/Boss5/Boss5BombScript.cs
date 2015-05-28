using UnityEngine;


public class Boss5BombScript : MonoBehaviour
{
    public bool Exploded = false;

    public float ExplosionTime = 4f;
    public float BombTimer = 0;

    public Animator Anim;

	// Use this for initialization
	void Start ()
	{
	    Anim = GetComponent<Animator>();
	}

    void OnTriggerStay2D(Collider2D collissionobject)
    {
        if (collissionobject.tag == "Player" && BombTimer >= ExplosionTime)
        {
            if (!Exploded)
            {
                collissionobject.gameObject.SendMessage("ApplyDamage", 20);
                Exploded = true;
            }
        }
    }

	
	// Update is called once per frame
	void Update ()
	{
	    BombTimer += Time.deltaTime;
	    if (BombTimer >= ExplosionTime)
	    {
            Anim.SetBool("Explode", true);
	    }
	    if (BombTimer >= ExplosionTime + 0.5f)
	    {
	        Destroy(gameObject);
	    }
	}
}
