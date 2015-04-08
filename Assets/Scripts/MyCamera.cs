using UnityEngine;
using System.Collections;

public class MyCamera : MonoBehaviour {

	private Rigidbody2D Charrigidbody2d;
	public int DistFromPlayer = 10;

	// Use this for initialization
	void Start () {
        
	}

	// Update is called once per frame
	void Update () {
        
		Vector3 player = GameObject.FindGameObjectWithTag ("Player").transform.position;
		player.z -= DistFromPlayer;
		transform.position = player;
	}
}
