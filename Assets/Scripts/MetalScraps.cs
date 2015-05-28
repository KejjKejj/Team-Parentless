using UnityEngine;
using System.Collections;

public class MetalScraps : MonoBehaviour
{

    public Vector3 enemy;
    private float i = 1f;
    public float size;
    public float rot;
    public float PosX;
    public float PosY;
    public Color CurrColor;
    // Use this for initialization
    void Start()
    {
        CurrColor = gameObject.GetComponent<Renderer>().material.color;
        size = Random.Range(2f, 4f);
        rot = Random.Range(0f, 360f);
        PosX = Random.Range(-1f, 1f);
        PosY = Random.Range(-1f, 1f);

        transform.position = new Vector3(transform.position.x + PosX, transform.position.y + PosY, 0);
        transform.rotation = Quaternion.Euler(0, 0, rot);
        transform.localScale = new Vector3(size, size, 0);

    }


    void Disappear()
    {

        CurrColor.a = i;
        gameObject.GetComponent<Renderer>().material.color = CurrColor;
        i -= 0.005f;


        if (i < 0)
        {
            Destroy(gameObject);
        }
    }
    void OnApplicationQuit()
    {
        Destroy(this);
    }

    // Update is called once per frame
    void Update()
    {
        //Disappear();
    }
}
