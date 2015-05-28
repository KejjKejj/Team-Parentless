using UnityEngine;
using System.Collections;

public class testLine : MonoBehaviour {
    public Transform start, end, straight,straight2;
    public float angle;
    Vector3 mousePos;
    RaycastHit2D LineTarget;
    Vector2 deltax, deltay;
	// Use this for initialization
	void Start () {
	
	}
	
    void test()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Physics2D.Linecast(transform.position, Vector3.forward);
        
        
        Debug.DrawLine(start.position,mousePos,Color.blue);
        
        Debug.DrawLine(start.position, end.position, Color.green);
        deltax = start.position - end.position;
        deltay = start.position - mousePos;
        angle = Vector2.Angle(deltax,deltay);
        Debug.Log(angle);
        
        
    }
	// Update is called once per frame
	void Update () {
        test();
	}
}
