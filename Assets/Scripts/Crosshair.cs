using UnityEngine;
using System.Collections;

public class Crosshair : MonoBehaviour
{

    private Vector2 MouseDistance;

	// Use this for initialization
	void Start () {
	
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
                Debug.Log(player);
            }
            Debug.Log(weapon[i].GetComponent<Weapon>().IsPickedUp);
        }

        float deltaX = -((Screen.width / 2) - Input.mousePosition.x);
        float deltaY = -((Screen.height / 2) - Input.mousePosition.y);

        float angle = Mathf.Atan2(deltaY, deltaX);

        transform.position = player + new Vector3(Mathf.Cos(angle) * 4, Mathf.Sin(angle) * 4, 0);
	}
}
