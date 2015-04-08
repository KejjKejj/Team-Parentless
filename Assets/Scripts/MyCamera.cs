using UnityEngine;
using System.Collections;

public class MyCamera : MonoBehaviour {

	private Rigidbody2D Charrigidbody2d;
	public int DistFromPlayer = 10;
    public int Ammo;
    public Texture tex;
    



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

    int GetBossHealth()
    {
        if (GameObject.Find("Boss"))
        {
            return GameObject.Find("Boss").GetComponent<BossScript>().BossHealth;
        }
        else
            return 0;
    }
    void OnGUI()
    {
        GUI.TextField(new Rect(0, 570, 85, 20), "Health: " + GetHealth().ToString());
        
        
        showBossHealth();
        
    }
    bool PlayerinRangeofBoss()
    {
        GameObject.Find("Progressbar").GetComponent<Renderer>().enabled = false;
        return GameObject.Find("BossZone").GetComponent<BossZone>().OpenFire;
    }
    void showBossHealth()
    {
        if(PlayerinRangeofBoss() && GetBossHealth() >= 0)
        {

            GameObject.Find("Progressbar").GetComponent<Renderer>().enabled = true;
            GUI.DrawTexture(new Rect(0, 0, GetBossHealth()*(Screen.width/30), 50), tex);
            //UnityEditor.EditorGUI.ProgressBar(new Rect(0, 0, Screen.width, 50), (float)GetBossHealth()/30, "Boss Health");
            
            
        }
        
    }
	// Update is called once per frame
	void Update () {
        
		Vector3 player = GameObject.FindGameObjectWithTag ("Player").transform.position;
		player.z -= DistFromPlayer;
		transform.position = player;
	}
}
