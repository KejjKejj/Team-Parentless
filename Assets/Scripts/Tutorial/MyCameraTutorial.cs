using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class MyCameraTutorial : MonoBehaviour
{
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

    public bool InsideRoom1, InsideRoom2, InsideRoom3, InsideRoom41, InsideRoom42, InsideRoom5;
    

    public GUIStyle gui;
    void Start()
    {
        gameObject.GetComponent<Camera>().orthographicSize = CamSize;

    }

    protected bool OpenDoor(){ return GameObject.Find("DoorOne").GetComponent<TutorialDoorScript>().ReadyToOpen; }
    protected bool PlayerInsideRoom1(){ return GameObject.Find("RoomOneZone").GetComponent<RoomOneZone>().InsideRoom1; }
    protected bool PlayerInsideRoom2(){ return GameObject.Find("RoomTwoZone").GetComponent<RoomTwoZone>().InsideRoom2; }
    protected bool PlayerInsideRoom3(){ return GameObject.Find("RoomThreeZone").GetComponent<RoomThreeZone>().InsideRoom3; }
    protected bool PlayerInsideRoom4(){ return GameObject.Find("RoomFourZone").GetComponent<RoomFourZone>().InsideRoom4; }
    protected bool PlayerInsideRoom5(){ return GameObject.Find("RoomFiveZone").GetComponent<RoomFiveZone>().InsideRoom5; }


    void OnGUI()
    {
        if (OpenDoor())
            GUI.TextField(new Rect(Screen.width/2, Screen.height/2, 20, 50), "Press 'E' to open and close doors.", gui);
        if (PlayerInsideRoom1())
            GUI.TextField(new Rect(Screen.width/3, 0, 100, 20), "Use 'W' 'A' 'S' 'D' keys to move. \nUse your mouse to aim. \n" +
            "Press mouse2 to use your knife. \nTry everything above to unlock next room", gui);
        if (PlayerInsideRoom2())
            GUI.TextField(new Rect(Screen.width / 3, 0, 100, 20), "Kill the enemy with your knife to unlock next room", gui);
        if (PlayerInsideRoom3())
            GUI.TextField(new Rect(Screen.width / 3, 0, 100, 20), "Pick up the gun and try it out to unlock next room. \n" + 
            "Pick up and drop weapons on 'G'. \nPress mouse1 to fire. \nBe careful so you don´t run out of ammo! You can see your current ammo down to the left.", gui);
        if (PlayerInsideRoom4())
            GUI.TextField(new Rect(Screen.width / 3, 0, 100, 20), "Kill the enemies to unluck next door. ", gui);
        if (PlayerInsideRoom5())
            GUI.TextField(new Rect(Screen.width / 3, 0, 100, 20), "Kill the enemy machines to complete tutorial. \n" +
            "Hide behind walls to avoid bullets. \n Press 'SPACE' slide and dodge bullets. \n If you are low on ammo, walk over a ammocrate", gui);


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
            transform.position = transform.position + new Vector3(-Mathf.Cos(angle) / ShakeAmount, -Mathf.Sin(angle) / ShakeAmount, -1);
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
    void Update()
    {
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

        CameraShake();
    }
}
