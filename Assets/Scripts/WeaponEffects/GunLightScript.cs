using UnityEngine;
using System.Collections;

public class GunLightScript : MonoBehaviour {

    public Color color;
    public float i = 1f;
	// Use this for initialization
	void Start () {
        color = gameObject.GetComponent<Renderer>().material.color;
        
	}
    void Disappear()
    {

        color.a = i;
        gameObject.GetComponent<Renderer>().material.color = color;
        i -= 0.2f;


        if (i < 0)
        {
            Destroy(gameObject);
        }
    }
	// Update is called once per frame
	void Update () {
        Disappear();
	}
}
