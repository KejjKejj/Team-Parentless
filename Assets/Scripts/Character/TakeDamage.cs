using UnityEngine;
using System.Collections;

public class TakeDamage : CharacterMain {
    
	// Use this for initialization
	void Start () {
        //Health = GetComponent<Movement>().Health;
	}
    void ApplyDamage(int damage)
    {
        GetComponent<Movement>().Health -= damage;
        Anim = GetComponent<Animator>();
        charRigid2D = GetComponent<CharacterMain>().charRigid2D;
    }


     public void CheckIfDead()
    {

        if (Health <= 0)
        {

            
           
            Anim.Play("Death");
            GetComponent<CharacterMain>().Alive = false;
            charRigid2D.velocity = new Vector2(0, 0);
            if (Input.GetKeyDown(KeyCode.R))
            {
                Application.LoadLevel(Application.loadedLevel);
            }
            GameObject w = gameObject.GetComponentInChildren<Weapon>().gameObject;
            
            Destroy(w);
        }
    }

     //void OnFireDamage()
     //{
     //    if (OnFire)
     //    {
     //        Health -= 1;

     //    }

     //}
	// Update is called once per frame
	void Update () {
        Health = GetComponent<Movement>().Health;
        CheckIfDead();
	}
}
