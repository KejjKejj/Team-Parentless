using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
    public bool IsPickedUp = false;
    protected bool SetPositionToPlayer;

    protected float DropDelay = 0.2f;
    protected float DropDelayTimer;
    protected float PickUpDelayTimer;

    protected float FireRate;
    protected float FireRateTimer;

    public int damage;

    protected bool Automatic;

    protected int MagSize;
    public float Recoil;
    public float MaxRecoil;
    public bool Shake;
    public int ShakeAmount = 20;

    public int WeaponId;

	// Use this for initialization
	void Start ()
	{
        
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
