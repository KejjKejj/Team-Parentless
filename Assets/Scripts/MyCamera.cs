using UnityEngine;
using System.Collections;

public class MyCamera : MonoBehaviour {

	private Rigidbody2D Charrigidbody2d;
	public int DistFromPlayer = 10;
    public int Ammo;
	// Use this for initialization
	void Start () {
        
	}
    int GetAmmo()
    {
        return GameObject.Find("Character").GetComponent<Movement>().MaxNumberOfShots - GameObject.Find("Character").GetComponent<Movement>().NumberOfShots;
    }

    int GetHealth()
    {
        return GameObject.Find("Character").GetComponent<Movement>().Health;
    }
    void OnGUI()
    {
        GUI.TextField(new Rect(0, 570, 85, 20), "Health: " + GetHealth().ToString());
        GUI.TextField(new Rect(100, 570, 85, 20),"Ammo: " + GetAmmo().ToString() + "/30");
    }

	// Update is called once per frame
	void Update () {
        
		Vector3 player = GameObject.FindGameObjectWithTag ("Player").transform.position;
		player.z -= DistFromPlayer;
		transform.position = player;
	}
}
