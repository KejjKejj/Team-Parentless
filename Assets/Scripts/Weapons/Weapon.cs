using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
    protected bool IsPickedUp = false;
    protected bool SetPositionToPlayer;

    protected float DropDelay = 0.2f;
    protected float DropDelayTimer;
    protected float PickUpDelayTimer;

    protected float FireRate;
    protected float FireRateTimer;

    protected bool Automatic;

    protected int MagSize;

	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
