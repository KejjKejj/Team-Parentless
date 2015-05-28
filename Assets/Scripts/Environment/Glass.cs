using UnityEngine;
using System.Collections;

public class Glass : MonoBehaviour
{
    public AudioClip Shatter;
    //public Sprite Broken;
    public GameObject GlassShard1;
    public GameObject GlassShard2;
    public GameObject GlassShard3;
    private BoxCollider2D GlassCollider;
    private AudioSource Audio;
    
    

	// Use this for initialization
	void Start () {
        
        GlassCollider = gameObject.GetComponent<BoxCollider2D>();
        Audio = GetComponent<AudioSource>();
        
            

	}

    void OnCollisionEnter2D(Collision2D collisionobject)
    {
        if (collisionobject.gameObject.tag == "Shot1")
        {
            Audio.PlayOneShot(Shatter);
            GetComponent<SpriteRenderer>().enabled = false;
            for (int i = 0; i < 30; i++)
            {
                Instantiate(GlassShard1, transform.position, transform.rotation);
                Instantiate(GlassShard2, transform.position, transform.rotation);
                Instantiate(GlassShard3, transform.position, transform.rotation);
            }
            GlassCollider.enabled = false;
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
