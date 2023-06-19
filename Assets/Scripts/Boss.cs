using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss : MonoBehaviour
{
    public enum FSMStates
    {
        Idle,
        Chase,
        Attack,
        Hit,
        Dead
    }

    public GameObject player;
    public FSMStates currentState;
    public GameObject cheese;
    public GameObject throwCheese;

    public float attackDistance = 15;
    public float enemySpeed = 10;
    public float attackRate = 3;
    public int damageAmount = 20;

    NavMeshAgent agent;
    Animator anim;
    Vector3 nextDestination;

    float distanceToPlayer;
    bool isDead;
    PlayerHealth playerHealth;

    float elapsedTime = 0;
    float animTimer = 0;
    bool animStart = false;
    

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();

        anim = GetComponent<Animator>();

        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = attackDistance;

        currentState = FSMStates.Chase;
        isDead = false;

    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        switch (currentState)
        {
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

        elapsedTime += Time.deltaTime;
        if (animStart)
        {
            animTimer += Time.deltaTime;
        }
    }

    void UpdateChaseState()
    {
        anim.SetInteger("animState", 1);

        agent.speed = enemySpeed;
        nextDestination = player.transform.position;

        if (distanceToPlayer <= attackDistance)
        {
            currentState = FSMStates.Attack;
        }

        FaceTarget(nextDestination);
        agent.SetDestination(nextDestination);
    }

    void UpdateAttackState()
    {
        if (distanceToPlayer <= attackDistance)
        {
            currentState = FSMStates.Attack;
        }
        else if (distanceToPlayer > attackDistance)
        {
            currentState = FSMStates.Chase;
        }

        FaceTarget(nextDestination);
        Attack();
    }

    void UpdateDeadState()
    {
        isDead = true;
    }

    void Attack()
    {
        if (!isDead)
        {
            if (elapsedTime >= attackRate)
            {
                int attack = Random.Range(2, 5);
                anim.SetInteger("animState", attack);
                var animDuration = anim.GetCurrentAnimatorStateInfo(0).length;

                if (animTimer >= animDuration)
                {
                    if (distanceToPlayer <= attackDistance)
                    {
                        playerHealth.TakeDamage(damageAmount);
                    }
                    animStart = false;
                }

                animStart = true;
                if (attack == 1)
                {
                     //AudioSource.PlayClipAtPoint(regularAttackSFX, transform.position);

                }
                else if (attack == 2)
                {
                    //AudioSource.PlayClipAtPoint(StompingSFX, transform.position);
                }
                else
                {
                    //AudioSource.PlayClipAtPoint(ThrowSFX, transform.position);
                    
                    cheese.SetActive(true);

                    Invoke("ThrowCheese", animDuration);
                }
                
                elapsedTime = 0.0f;
            }
        }
        
    }

    void ThrowCheese()
    {
        Instantiate(throwCheese, cheese.transform.position, cheese.transform.rotation);
        cheese.SetActive(false);
    }

    void FaceTarget(Vector3 target)
    {
        Vector3 directionTarget = (target - transform.position).normalized;
        directionTarget.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(directionTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 10 * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        //attack
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
    }
}
