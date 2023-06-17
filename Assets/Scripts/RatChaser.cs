using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatChaser : MonoBehaviour
{
    public enum FSMStates
    {
        Idle,
        Patrol,
        Chase,
        Attack,
        Hit,
        Dead
    }

    public FSMStates currentState;

    public float chaseDistance = 2;
    public float enemySpeed = 5;
    public float attackDistance = 1.5f;
    public GameObject player;

    GameObject[] wanderPoints;
    Vector3 nextDestination;
    Animator anim;
    float distanceToPlayer;

    int currentDestinationIndex = 0;

    public static bool lightHit;

    // Start is called before the first frame update
    void Start()
    {
        lightHit = false;
        wanderPoints = GameObject.FindGameObjectsWithTag("WanderPoint");
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");

        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (!lightHit && !LevelManager.isGameOver)
        {
            switch (currentState)
            {
                case FSMStates.Hit:
                    UpdateHitState();
                    break;
                case FSMStates.Patrol:
                    UpdatePatrolState();
                    break;
                case FSMStates.Chase:
                    UpdateChaseState();
                    break;
                case FSMStates.Attack:
                    UpdateAttackState();
                    break;
                case FSMStates.Dead:
                    UpdateDeadState();
                    break;
            }
        }
        else
        {
            currentState = FSMStates.Hit;
            anim.SetInteger("animState", 0);
        }

    }

    private void Initialize()
    {
        currentState = FSMStates.Patrol;
        FindNextPoint();
    }

    void UpdateHitState()
    {
        currentState = FSMStates.Patrol;
    }

    void UpdatePatrolState()
    {
        anim.SetInteger("animState", 1);

        if (Vector3.Distance(transform.position, nextDestination) < 1.5)
        {
            FindNextPoint();
        }
        else if (distanceToPlayer <= chaseDistance)
        {
            currentState = FSMStates.Chase;
        }

        FaceTarget(nextDestination);
    }

    void UpdateChaseState()
    {
        anim.SetInteger("animState", 2);

        nextDestination = player.transform.position;

        if (distanceToPlayer <= attackDistance)
        {
            currentState = FSMStates.Attack;
        }
        else if (distanceToPlayer > chaseDistance)
        {
            currentState = FSMStates.Patrol;
        }

        FaceTarget(nextDestination);
    }

    void UpdateAttackState()
    {
        nextDestination = player.transform.position;

        if (distanceToPlayer <= attackDistance)
        {
            currentState = FSMStates.Attack;
        }
        else if (distanceToPlayer > attackDistance && distanceToPlayer <= chaseDistance)
        {
            currentState = FSMStates.Chase;
        }
        else if (distanceToPlayer > chaseDistance)
        {
            currentState = FSMStates.Patrol;
        }

        FaceTarget(nextDestination);
        anim.SetInteger("animState", 3);
        FindObjectOfType<LevelManager>().LevelLost();
    }

    void UpdateDeadState()
    {
        anim.SetInteger("animState", 4);

        Destroy(gameObject, 3);
    }

    void FindNextPoint()
    {
        nextDestination = wanderPoints[currentDestinationIndex].transform.position;

        currentDestinationIndex = (currentDestinationIndex + 1) % wanderPoints.Length;
    }

    void FaceTarget(Vector3 target)
    {
        Vector3 directionTarget = (target - transform.position).normalized;
        directionTarget.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(directionTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 10 * Time.deltaTime);
    }

    public void LightHit(bool hit)
    {
        lightHit = hit;
    }

    private void OnDrawGizmos()
    {
        //attack
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);

        //attack
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);
    }
}
