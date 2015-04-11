using UnityEngine;
using System.Collections;

public class MyCamera : MonoBehaviour {

	private Rigidbody2D Charrigidbody2d;
	public int DistFromPlayer = 10;
    public int Ammo;
    public Texture tex;
    bool Shake;
    int CamShakeMove = 100;

    private int ShakeAmount;

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
        
        
        
    }
  
  

    void CameraShake()
    {
        GameObject[] Weapontype = GameObject.FindGameObjectsWithTag("Weapon");
        for (int i = 0; i < Weapontype.Length; i++)
        {
            if (Weapontype[i].GetComponent<Weapon>().Shake == true)
            {
                Shake = Weapontype[i].GetComponent<Weapon>().Shake;
                ShakeAmount = Weapontype[i].GetComponent<Weapon>().ShakeAmount;
            }
        }
        float deltaX = -((Screen.width / 2) - Input.mousePosition.x);
        float deltaY = -((Screen.height / 2) - Input.mousePosition.y);
        float angle = Mathf.Atan2(deltaY, deltaX);
        
        if (Shake && CamShakeMove > 0)
        {
            transform.position = transform.position + new Vector3(-Mathf.Cos(angle)/ShakeAmount, -Mathf.Sin(angle)/ShakeAmount, -1);
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
