using UnityEngine;
using System.Collections;

public class EnemyTutorial : MonoBehaviour
{
    public GameObject Blood;
    public GameObject[] Bloodspatter;
    public GameObject[] PatrolPath;
    public Vector3 _target;
    public float Health = 10;
    private float _walkSpeed = 2f;
    private Rigidbody2D _rigid;

    public bool Alive = true;
    public bool StaticEnemy = false;
 
    // Use this for initialization
    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _target = PatrolPath[0].transform.position;
    }

    void Patrol()
    {
        if (Vector3.Distance(transform.position, _target) < 0.5f)
        {
            _target = SelectRandomPatrolNode();
        }
        else
        {
            MoveTowards(_target);
        }
    }

    Vector3 SelectRandomPatrolNode()
    {
        for (int i = 0; i < PatrolPath.Length; ++i)
        {
            Vector3 node = PatrolPath[Random.Range(0, PatrolPath.Length)].transform.position;

                return node;
            
        }
        return PatrolPath[Random.Range(0, PatrolPath.Length)].transform.position;
    }
    void MoveTowards(Vector3 target)
    {
        LookDirection(target);
        Vector3 moveDirection = target - transform.position;
        Vector3 velocity = moveDirection.normalized * _walkSpeed;
        _rigid.velocity = new Vector2(velocity.x, velocity.y);
    }
    void LookDirection(Vector3 target)
    {
        transform.rotation = Quaternion.LookRotation(Vector3.forward, target - transform.position);
    }

    void ApplyDamage(int damage)
    {
        Health -= damage;
        CheckIfDead();
    }

    void CheckIfDead()
    {
        if (Health <= 0)
        {
            Alive = false;
            Destroy(gameObject);
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

    // Update is called once per frame
    void Update()
    {
        if(!StaticEnemy)
            Patrol();
    }
}
