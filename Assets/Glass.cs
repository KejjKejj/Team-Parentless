using UnityEngine;
using System.Collections;

public class Glass : MonoBehaviour
{

    public Sprite Broken;
    private BoxCollider2D GlassCollider;

	// Use this for initialization
	void Start () {
        GlassCollider = gameObject.GetComponent<BoxCollider2D>();
	}

    void OnCollisionEnter2D(Collision2D collisionobject)
    {
        if (collisionobject.gameObject.tag == "Shot1")
        {
            Debug.Log("Glass hit");
            GetComponent<SpriteRenderer>().sprite = Broken;
            GlassCollider.enabled = false;
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
