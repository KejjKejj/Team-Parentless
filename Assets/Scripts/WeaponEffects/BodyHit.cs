using UnityEngine;
using System.Collections;

public class BodyHit : MonoBehaviour {


    private float AnimationTimer;

    public Sprite Sprite1;
    public Sprite Sprite2;
    public Sprite Sprite3;
    public Sprite Sprite4;
    public Sprite Sprite5;

    // Use this for initialization
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = Sprite1;
    }

    // Update is called once per frame
    void Update()
    {
        AnimationTimer += Time.deltaTime;
        if (AnimationTimer >= 0.03f)
        {
            GetComponent<SpriteRenderer>().sprite = Sprite2;
        }
        if (AnimationTimer >= 0.06f)
        {
            GetComponent<SpriteRenderer>().sprite = Sprite3;
        }
        if (AnimationTimer >= 0.09f)
        {
            GetComponent<SpriteRenderer>().sprite = Sprite4;
        }
        if (AnimationTimer >= 0.12f)
        {
            GetComponent<SpriteRenderer>().sprite = Sprite5;
        }
        if (AnimationTimer >= 0.15f)
        {
            Destroy(gameObject);
        }
    }
}
