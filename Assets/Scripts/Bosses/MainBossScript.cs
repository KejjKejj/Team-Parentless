using UnityEngine;
using System.Collections;

public class MainBossScript : MonoBehaviour {

    public Rigidbody2D EnemyRigid2D;
    public int Health;
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

    void ApplyDamage(int damage)
    {
        Health -= damage;


        if (Health <= 0)
        {
            Debug.Log("Död!");
            Destroy(gameObject);
            SprayBlood();
            GameObject.FindGameObjectWithTag("Player").SendMessage("ApplyScore", 1000);
            if (PlayerPrefs.GetInt("Slot" + PlayerPrefs.GetInt("CurrentSaveSlot").ToString() + "UnlockedLevels") < 2)
            {
                PlayerPrefs.SetInt("Slot" + PlayerPrefs.GetInt("CurrentSaveSlot").ToString() + "UnlockedLevels", 2);
            }
            PlayerPrefs.SetInt("Slot" + PlayerPrefs.GetInt("CurrentSaveSlot").ToString() + "TotalMoney", 
                                PlayerPrefs.GetInt("Slot" + PlayerPrefs.GetInt("CurrentSaveSlot").ToString() + "TotalMoney") +
                                GameObject.FindGameObjectWithTag("Player").GetComponent<ScoreScript>().CurrentScore);
            Application.LoadLevel(2);
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
