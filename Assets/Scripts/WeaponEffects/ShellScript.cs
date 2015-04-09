using UnityEngine;
using System.Collections;

public class ShellScript : MonoBehaviour {

    public float rot, PosX, PosY;
    public Color CurrColor;
    float i = 1f;
	// Use this for initialization
	void Start () {
        CurrColor = gameObject.GetComponent<Renderer>().material.color;
        rot = Random.Range(0f, 360f);
        PosX = Random.Range(-1f, 1f);
        PosY = Random.Range(-1f, 1f);

        transform.position = new Vector3(transform.position.x + PosX, transform.position.y + PosY, 0);
        transform.rotation = Quaternion.Euler(0, 0, rot);
	}

    void Disappear()
    {

        CurrColor.a = i;
        gameObject.GetComponent<Renderer>().material.color = CurrColor;
        i -= 0.001f;


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
