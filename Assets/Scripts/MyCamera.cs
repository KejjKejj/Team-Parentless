using UnityEngine;
using System.Collections;

public class MyCamera : MonoBehaviour {

	private Rigidbody2D Charrigidbody2d;
	public int DistFromPlayer = 10;
    public int Ammo;
    public Texture tex;
    bool Shake;
    int CamShakeMove = 100;
    Quaternion PlayerDir;

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
        
        return GameObject.Find("BossZone").GetComponent<BossZone>().OpenFire;
    }
    void showBossHealth()
    {
        if(PlayerinRangeofBoss() && GetBossHealth() >= 0)
        {

            GameObject.Find("Progressbar").GetComponent<Renderer>().enabled = true;
            GUI.DrawTexture(new Rect(0, 0, GetBossHealth()*(Screen.width/30), 50), tex);
            
            
            
        }
        
    }

    void CameraShake()
    {
        GameObject[] Weapontype = GameObject.FindGameObjectsWithTag("Weapon");
        for (int i = 0; i < Weapontype.Length; i++)
        {
            if (Weapontype[i].GetComponent<Weapon>().Shake == true)
            {
                Shake = Weapontype[i].GetComponent<Weapon>().Shake;
            }
        }
        float deltaX = -((Screen.width / 2) - Input.mousePosition.x);
        float deltaY = -((Screen.height / 2) - Input.mousePosition.y);
        float angle = Mathf.Atan2(deltaY, deltaX);
        PlayerDir = GameObject.Find("Character").GetComponent<Movement>().transform.rotation;
        Debug.Log(PlayerDir);
        if (Shake && CamShakeMove > 0)
        {
            transform.position = transform.position + new Vector3(-Mathf.Cos(angle)/20, -Mathf.Sin(angle)/20, -1);
            CamShakeMove -= 20;
        }
        else if (Shake && CamShakeMove <= 0)
        {
            
            CamShakeMove = 100;
            for (int i = 0; i < Weapontype.Length; i++)
            {
                    Weapontype[i].GetComponent<Weapon>().Shake = false;
                    Shake = false;
                
            }
        }
            
    }
	// Update is called once per frame
	void Update () {

        Vector3 player = GameObject.FindGameObjectWithTag("Player").transform.position;
        player.z -= DistFromPlayer;
        transform.position = player;
        CameraShake();
	}
}
