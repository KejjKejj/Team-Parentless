using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public GameObject Blood;
    public GameObject AmmoCrate;
    public GameObject[] Bloodspatter;
    public GameObject SmokeCloud;

    private Animator _animator;
    private PolygonCollider2D _collider2D;

    public bool Smoke = false;
    private int RandAmmo;
    public float Health = 10;
    public bool Onfire = false;

    public float SmokeTimer = 0;
    private GameObject Smoke1;
    // Use this for initialization
    void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
        _collider2D = gameObject.GetComponent<PolygonCollider2D>();
    }

    void ApplyDamage(int damage)
    {
        Health -= damage;
        CheckIfDead();
    }

    void ApplyFireDamage(bool Fire)
    {
        Onfire = Fire;
        CheckIfDead();

    }
    void CheckIfDead()
    {
        if (Health <= 0)
        {
            _animator.SetBool("Dead", true);
            GameObject.FindGameObjectWithTag("Player").SendMessage("ApplyScore", 100);
            gameObject.GetComponent<EnemyStateMachine>().IsAlive = false;
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            _collider2D.enabled = false;
            SpawnCrate();
            SprayBlood();
            Smoke = false;
        }
    }

    public void SprayBlood()
    {
        Bloodspatter = new GameObject[20];
        for (int i = 0; i < Bloodspatter.Length; i++)
        {
            GameObject clone = (GameObject)Instantiate(Blood, transform.position, transform.rotation);
            Bloodspatter[i] = clone;
        }
    }
    public void SmokeClouds()
    {
        SmokeTimer += Time.deltaTime;
        if(Smoke)
        {
            if (SmokeTimer >= 2)
            {
                for (int i = 5000; i <= 10000; i += 1000)
                {
                    Smoke1 = (GameObject)Instantiate(SmokeCloud, transform.position, transform.rotation);
                    Smoke1.GetComponent<Transform>().Rotate(-1, 0, 0);
                    Smoke1.GetComponent<Rigidbody2D>().AddForce(Smoke1.transform.forward * i);
                }
                SmokeTimer = 0;
            }
            
        }

    }
    void SpawnCrate()
    {
        RandAmmo = Random.Range(1, 101);
        if (RandAmmo >= 75)
        {
            Instantiate(AmmoCrate, transform.position, transform.rotation);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Onfire)
        {
            Health -= 4 * Time.deltaTime;
        }
        if (gameObject.GetComponent<EnemyStateMachine>()._chasing)
        {
            Smoke = false;
        }
        
        
        SmokeClouds();
    }
}
