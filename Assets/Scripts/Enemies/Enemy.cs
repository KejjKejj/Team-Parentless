using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public GameObject Blood;
    public GameObject AmmoCrate;
    public GameObject[] Bloodspatter;

    private int RandAmmo;
    public float Health = 10;
    public bool Onfire = false;

    // Use this for initialization
    void Start()
    {

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
            GameObject.FindGameObjectWithTag("Player").SendMessage("ApplyScore", 100);
            Destroy(gameObject);
            SpawnCrate();
            SprayBlood();
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

    }
}
