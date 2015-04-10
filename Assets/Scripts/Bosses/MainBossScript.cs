using UnityEngine;
using System.Collections;

public class MainBossScript : MonoBehaviour {

    public Rigidbody2D EnemyRigid2D;
    protected int Health;
    public Texture Healthbar;
    public GameObject Blood;
    public GameObject[] Bloodspatter;
    
	// Use this for initialization
    
	void Start () {
	
	}

    protected bool GetPlayerInRange()
    {
        return GameObject.Find("BossZone").GetComponent<BossZone>().OpenFire;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Shot1")
        {
            if (Health <= 0)
            {
                Destroy(gameObject);
                SprayBlood();
            }
            Health--;
        }

    }

    void OnGUI()
    {
        showBossHealth();
    }

    void showBossHealth()
    {
        if (GetPlayerInRange() && Health >= 0)
        {

            GameObject.Find("Progressbar").GetComponent<Renderer>().enabled = true;
            GUI.DrawTexture(new Rect(0, 0, Health * (Screen.width / 30), 50), Healthbar);



        }

    }

    public void SprayBlood()
    {
        Bloodspatter = new GameObject[20];
        for (int i = 0; i < Bloodspatter.Length; i++)
        {
            GameObject clone = (GameObject)Instantiate(Blood, new Vector3(EnemyRigid2D.position.x, EnemyRigid2D.position.y), Quaternion.identity);
            Bloodspatter[i] = clone;

        }
    }
	// Update is called once per frame
	void Update () {
	
	}
}
