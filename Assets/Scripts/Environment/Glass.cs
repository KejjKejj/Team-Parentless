using UnityEngine;
using System.Collections;

public class Glass : MonoBehaviour
{
    public AudioClip Shatter;
    public Sprite Broken;

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
            GetComponent<SpriteRenderer>().sprite = Broken;
            GlassCollider.enabled = false;
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
