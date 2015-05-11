using System;
using UnityEngine;
using System.Collections;
using System.Net;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public class EnemyStateMachine : MonoBehaviour
{
    private enum State
    {
        STATE_PATROLLING,
        STATE_ATTACKING,
        STATE_CHASING,
        STATE_CHASE_ENDED,
        STATE_RETURNING
    }

    private GameObject[] _waypoints;
    private GameObject _player;
    private Rigidbody2D _rigid;
    private Movement _pAlive;

    public GameObject[] PatrolPath;
    public GameObject Shot;
    public Transform EnemySightStart, EnemySightEnd;

    private Vector3 _goal;
    private Vector3 _target;
    private Vector3 _spawn;
    private State _state;

    private bool _chasing;
    private float _attackTimer;
    public float _walkSpeed = 5f;

    public float TimeBetweenAttacks = 1f;
    public bool StaticEnemy;

	// Use this for initialization
	void Start ()
	{
	    _waypoints  = GameObject.FindGameObjectsWithTag("Waypoint");
	    _player     = GameObject.FindGameObjectWithTag("Player");
	    _pAlive     = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();
	    _rigid      = GetComponent<Rigidbody2D>();
        _goal       = new Vector3(0, 0, 0);
	    _target     = PatrolPath[0].transform.position;
	    _spawn      = transform.position;
        _state      = State.STATE_PATROLLING;
	}
	
	// Update is called once per frame
	void Update ()
	{
        if (!StaticEnemy)
        {
            DecideAction();
            Action();
        }
        else
        {
            if (LineOfSightToPlayer())
            {
                Attack();
            }
        }

	    _attackTimer += Time.deltaTime;
        Debug.DrawLine(EnemySightStart.position, EnemySightEnd.position, Color.red);
	}

    #region Line of Sight
    bool LineOfSightToPlayer()
    {
        return
            Physics2D.Linecast(EnemySightStart.position, EnemySightEnd.position, 1 << LayerMask.NameToLayer("Player")) &&
            !Physics2D.Linecast(EnemySightStart.position, EnemySightEnd.position, 1 << LayerMask.NameToLayer("FirmWall")) &&
            !Physics2D.Linecast(EnemySightStart.position, EnemySightEnd.position, 1 << LayerMask.NameToLayer("SoftWall"));
    }

    bool LineOfSightToTarget(Vector3 target)
    {
        return
            !(Physics2D.Linecast(EnemySightStart.position, target,
                1 << LayerMask.NameToLayer("FirmWall")) ||
              Physics2D.Linecast(EnemySightStart.position, target,
                  1 << LayerMask.NameToLayer("SoftWall")));
    }
    #endregion

    #region State Functions
    void DecideAction()
    {
        if (!LineOfSightToPlayer() && _state == State.STATE_PATROLLING) return;

        if (!LineOfSightToPlayer() && _state == State.STATE_ATTACKING) 
            _state = State.STATE_CHASING;
        else if (!LineOfSightToPlayer() && _state == State.STATE_CHASE_ENDED)
            _state = State.STATE_RETURNING;
        else if (LineOfSightToPlayer())
            _state = State.STATE_ATTACKING;
    }

    void Action()
    {
        switch (_state)
        {
            case State.STATE_PATROLLING:
                Patrol();
                break;

            case State.STATE_ATTACKING:
                StandStill();
                Attack();
                break;

            case State.STATE_CHASING:
                ChasePlayerToLastSeenPoint();
                break;

            case State.STATE_CHASE_ENDED:
                StandStill();
                break;

            case State.STATE_RETURNING:
                ReturnToPatrolling();
                break;

            default:
                StandStill();
                break;
        }
    }
    #endregion

    #region Attack Functions
    void Attack()
    {
        _chasing = false;
        LookDirection(_player.transform.position);
        if (_attackTimer >= TimeBetweenAttacks && _pAlive.Alive)
        {
            Instantiate(Shot, transform.position, transform.rotation);
            _attackTimer = 0;
        }
    }
    #endregion

    #region Moving Functions
    void StandStill()
    {
        _rigid.velocity = new Vector2(0, 0);
    }

    void LookDirection(Vector3 target)
    {
        transform.rotation = Quaternion.LookRotation(Vector3.forward, target - transform.position);
    }

    void MoveTowards(Vector3 target)
    {
        LookDirection(target);
        Vector3 moveDirection = target - transform.position;
        Vector3 velocity = moveDirection.normalized * _walkSpeed;
        _rigid.velocity = new Vector2(velocity.x, velocity.y);
    }
    #endregion

    #region Patrolling Functions
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
            if (LineOfSightToTarget(node))
            {
                return node;
            }
        }
        return PatrolPath[Random.Range(0, PatrolPath.Length)].transform.position;
    }

    void ReturnToPatrolling()
    {
        _goal = PatrolPath[0].transform.position;
        if (Vector3.Distance(transform.position, _target) < 0.5f)
        {
            if (_target == _goal)
            {
                _walkSpeed = 5;
                _state = State.STATE_PATROLLING;
            }
            _target = BestFirstSearchToGoalNode();
        }
        else
        {
            MoveTowards(_target);
        }
    }
    #endregion

    #region Chasing Functions
    void ChasePlayerToLastSeenPoint()
    {
        _walkSpeed = 10;
        if (!_chasing)
        {
            SetGoalNode(_player.transform.position);
            _chasing = true;
        }
        if (Vector3.Distance(transform.position, _target) < 0.5f)
        {
            if (_target == _goal && !LineOfSightToPlayer())
            {
                _state = State.STATE_CHASE_ENDED;
                _chasing = false;
            }
            _target = BestFirstSearchToGoalNode();
        }
        else
        {
            MoveTowards(_target);
        }
    }
    #endregion

    #region Best First Search Region
    void SetGoalNode(Vector3 goal)
    {
        Vector3 closest = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
        for (int i = 0; i < _waypoints.Length; ++i)
        {
            if (Vector3.Distance(_waypoints[i].transform.position, goal) < Vector3.Distance(closest, goal))
            {
                closest = _waypoints[i].transform.position;
            }
        }
        _goal = closest;
    }

    Vector3 BestFirstSearchToGoalNode()
    {
        Vector3 closest = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
        for (int i = 0; i < _waypoints.Length; ++i)
        {
            Vector3 node = _waypoints[i].transform.position;
            if (Physics2D.Linecast(transform.position, node, 1 << LayerMask.NameToLayer("FirmWall")) ||
                Physics2D.Linecast(transform.position, node, 1 << LayerMask.NameToLayer("SoftWall")))
            {

            }
            else if (Vector3.Distance(_goal, node) < Vector3.Distance(closest, _goal))
            {              
                closest = _waypoints[i].transform.position;
            }
        }
        
        return closest;
    }
    #endregion
}
