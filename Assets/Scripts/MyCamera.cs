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

    float Damping = 5f;
    float Height  = 13.0f;
    float Offset  = 0.0f;
    private Vector3 CenterCamera;
    private float yVelocity = 0.0F;
    private float xVelocity = 0.0F;

    void Start () {
        gameObject.GetComponent<Camera>().orthographicSize = CamSize;
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
        Vector3 MousePos = Input.mousePosition;
        MousePos.z = 3f;
        Vector3 CursorPos = Camera.main.ScreenToWorldPoint(MousePos);
        Vector3 player = GameObject.FindGameObjectWithTag("Player").transform.position;
        player.z -= DistFromPlayer;
        float camx = (player.x + CursorPos.x) /2;
        float camy = (player.y + CursorPos.y) /2;

        float NewCamx = Mathf.SmoothDamp(player.x, CursorPos.x, ref xVelocity, 3f);
        float NewCamy = Mathf.SmoothDamp(player.y, CursorPos.y, ref yVelocity, 3f);
        
        //float angle = Mathf.Atan2(camy, camx);
        //Vector2 range = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        //float NewCamx = Mathf.SmoothDamp(camx, range.x, ref xVelocity, 3f);
        //float NewCamy = Mathf.SmoothDamp(camy, range.y, ref yVelocity, 3f);
        
        //float Stopcamx = camx;
        //float Stopcamy = camy;
        //CenterCamera = new Vector3(camx , camy, player.z);
       
        //transform.position = MousePos;

        
        transform.position = Vector3.Lerp(transform.position, new Vector3(NewCamx, NewCamy, player.z), Time.deltaTime * Damping);
        
        CameraShake();
	}
}
