using UnityEngine;
using System.Collections;

public class Crosshair : MonoBehaviour
{
    private GameObject _weapon;

    private Vector2 MouseDistance;

    private float RecoilLast;

	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update () {
        Vector3 player = GameObject.FindGameObjectWithTag("Player").transform.position;
	    GameObject[] weapon = GameObject.FindGameObjectsWithTag("Weapon");

        for (int i = 0; i < weapon.Length; i++)
        {
            if (weapon[i].GetComponent<Weapon>().IsPickedUp)
            {
                player = weapon[i].transform.position;
                _weapon = weapon[i];
            }           
        }

        Vector3 MousePos = Input.mousePosition;
        Vector3 CursorPos = Camera.main.ScreenToWorldPoint(MousePos);

	    float distance = Mathf.Sqrt(Mathf.Pow((player.x - CursorPos.x) / 2, 2) + Mathf.Pow((player.y - CursorPos.y) / 2, 2));

        float deltaX = -((Screen.width / 2) - Input.mousePosition.x);
        float deltaY = -((Screen.height / 2) - Input.mousePosition.y);

        float angle = Mathf.Atan2(deltaY, deltaX);

	    if (_weapon != null)
	    {
	        if (_weapon.GetComponent<Weapon>().Recoil > 0 && Input.GetMouseButton(0))
	        {
	            if (transform.localScale.x < 1.8f)
	            {
	                transform.localScale += new Vector3(0.05f, 0.05f, 0.05f);
	            }
	        }

	        if (!Input.GetMouseButton(0))
	        {
	            if (transform.localScale.x > 1)
	            {
	                transform.localScale -= new Vector3(0.05f, 0.05f, 0.05f);
	            }
	        }
	    }

	    transform.position = player + new Vector3(Mathf.Cos(angle) * distance, Mathf.Sin(angle) * distance, 0);
	}
}
