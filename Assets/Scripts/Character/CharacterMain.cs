using UnityEngine;
using System.Collections;

public class CharacterMain : MonoBehaviour {
    public int Health;

   public Animator Anim;

    public Rigidbody2D charRigid2D;

    public bool Alive = true;
	// Use this for initialization
	void Start () {
        Anim = GetComponent<Animator>();
	}
	



	// Update is called once per frame
	void Update () {
	
	}
}
