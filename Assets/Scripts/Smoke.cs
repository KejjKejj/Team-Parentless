using UnityEngine;
using System.Collections;

public class Smoke: MonoBehaviour
{
    
    public float speed = 20;
    private float disapperRate = 1f;
    private Rigidbody2D Bulletbody2d = new Rigidbody2D();



    public float AngleShot = 0;

    public float Recoil;
    
    public float DecreaseRate;
    public Color CurrColor;

    // Use this for initialization
    void Start()
    {
        CurrColor = gameObject.GetComponent<Renderer>().material.color;
        DecreaseRate = Random.Range(0.94f, 0.97f);
        

        Bulletbody2d = GetComponent<Rigidbody2D>();
        

        Recoil = Random.Range(-Recoil, Recoil);

        //float deltaX = -((Screen.width / 2) - Input.mousePosition.x);
        //float deltaY = -((Screen.height / 2) - Input.mousePosition.y);

        //float angle = Mathf.Atan2(deltaY, deltaX);

        

    }

    
    void Disappear()
    {

        CurrColor.a = disapperRate;
        gameObject.GetComponent<Renderer>().material.color = CurrColor;
        disapperRate -= 0.02f;

        if (disapperRate < 0)
        {
            
            Destroy(gameObject);
        }
    }

   



    // Update is called once per frame
    void Update()
    {
        //Bulletbody2d.velocity = Bulletbody2d.velocity * DecreaseRate;
        
        Disappear();
    }
}
