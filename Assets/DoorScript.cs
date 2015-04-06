using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour
{

    private bool DoorOpen; // Är dörren öppen?
    private bool DoorSwinging; // Svänger dörren fortfarande?

    private float ClickDelay; // Timer för att kunna öppna och stänga dörren

    private const float DoorOpenDelay = 0.5f; // Konstanten för att bestämma hur ofta/sällan man kan öppna och stänga dörren
    
    private const int DoorSwingAngle = 10; // Hur många grader per frame som ska öppnas
    private const int DoorOpenAngle = 90; // Vinkeln på dörren när den är öppen
    private const int DoorClosedAngle = 0; // Vinkel på dörren när den är stängd

    private int DoorSwingTimer = 0; // Timer så dörren stannar där den ska

    private BoxCollider2D DoorCollider;

	// Use this for initialization
	void Start ()
	{
	    DoorCollider = gameObject.GetComponent<BoxCollider2D>();
	}

    void OnTriggerEnter2D(Collider2D collissionobject)
    {
        if (collissionobject.gameObject.tag == "Player")
        {
            Debug.Log("Player Entered");
        }
    }

    void OnTriggerStay2D(Collider2D collissionobject)
    {
        if (collissionobject.gameObject.tag == "Player")
        {
            Debug.Log("Player Staying");
            
        }
        if (Input.GetButton("Interact"))
        {
            Debug.Log("Player pressed E");
            if (ClickDelay >= DoorOpenDelay)
            {
                if (DoorOpen)
                {
                    DoorSwinging = true;
                    DoorOpen = false;
                    
                }
                else
                {
                    DoorSwinging = true;
                    DoorOpen = true;
                    
                }
                ClickDelay = 0;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collissionobject)
    {
        if (collissionobject.gameObject.tag == "Player")
        {
            Debug.Log("Player Leaving");
        }
    }

	// Update is called once per frame
	void Update ()
	{
	    ClickDelay += Time.deltaTime;
        if (DoorSwinging && DoorOpen)
        {
            DoorCollider.enabled = false;
            DoorSwingTimer += 10;
            transform.Rotate(new Vector3(0, 0, DoorSwingAngle));
            if (DoorSwingTimer == DoorOpenAngle)
            {
                DoorCollider.enabled = true;
                DoorSwinging = false;
            }
        }
        if (DoorSwinging && !DoorOpen)
        {
            DoorCollider.enabled = false;
            DoorSwingTimer -= 10;
            transform.Rotate(new Vector3(0, 0, -DoorSwingAngle));
            if (DoorSwingTimer == DoorClosedAngle)
            {
                DoorCollider.enabled = true;
                DoorSwinging = false;
            }
        }
	    
	}
}
