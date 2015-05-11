using UnityEngine;
using System.Collections;

public class MyCamera : MonoBehaviour {

	private Rigidbody2D Charrigidbody2d;
	public int DistFromPlayer = 10;
    public int Ammo;
    public Texture tex;
    bool Shake;
    int CamShakeMove = 100;
    public float CamSize = 10;
    private int ShakeAmount;

    private Vector3 CenterCamera;
    private float yVelocity = 0.0F;
    private float xVelocity = 0.0F;

    public Color color1 = Color.yellow;
    public Color color2 = Color.red;
    public float duration = 1.0f;
    public Camera camera1;


    public GUIStyle gui;
    void Start () {
        gameObject.GetComponent<Camera>().orthographicSize = CamSize;
        camera1 = gameObject.GetComponent<Camera>();
	    camera1.clearFlags = CameraClearFlags.SolidColor;
    }

    int GetHealth()
    {
        return GameObject.Find("Character").GetComponent<Movement>().Health;
    }

    
    void OnGUI()
    {
        if (GetHealth() <= 0)
        {
            GUI.TextField(new Rect(Screen.width / 2, Screen.height / 2, 85, 20), "PRESS R TO RESTART!", gui);
        }
        //GUI.TextField(new Rect(0, 570, 85, 20), "Health: " + GetHealth().ToString());  
        GUI.TextField(new Rect(0, Screen.height-20, 85, 20), "Health: " + GetHealth().ToString(),gui);

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
        Vector3 MousePos = Input.mousePosition;
        MousePos.z = 3f;
        Vector3 player = GameObject.FindGameObjectWithTag("Player").transform.position;
	    Vector3 crosshair = GameObject.Find("Crosshair").transform.position;
        player.z -= DistFromPlayer;


        float camx = (player.x + crosshair.x) / 2;
        float camy = (player.y + crosshair.y) / 2;

        float NewCamx = Mathf.SmoothDamp(camx, camx, ref xVelocity, 3f);
        float NewCamy = Mathf.SmoothDamp(camy, camy, ref yVelocity, 3f);

	    transform.position = new Vector3(NewCamx, NewCamy, player.z);

        var t = Mathf.PingPong(Time.time, duration) / duration;
        camera1.backgroundColor = Color.Lerp(color1, color2, t);
        CameraShake();
	}
}
