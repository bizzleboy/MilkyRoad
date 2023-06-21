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
    public GameObject firstHit;

    public float attackDistance = 8;
    public float enemySpeed = 8;
    public float attackRate = 3;
    public int damageAmount = 10;

    public static bool isHit;
    public float hitDuration = 8;
    public static bool isDead;

    public AudioClip StompSFX;
    public AudioClip regularAttackSFX;
    public AudioClip ThrowSFX;

    NavMeshAgent agent;
    Animator anim;
    Vector3 nextDestination;

    float distanceToPlayer;
    PlayerHealth playerHealth;
    bool attacking;

    float elapsedTime = 0;
    float animTimer = 0;
    float hitTimer = 0;
    bool hitStart = false;
    bool animStart = false;
    public static bool turnOnFirstHit = false;

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
        attacking = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!LevelManager.isGameOver)
        {
            distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

            if (isDead)
            {
                currentState = FSMStates.Dead;
                hitTimer = hitDuration;
            }

            if (isHit)
            {
                cheese.SetActive(false);
                currentState = FSMStates.Hit;
                hitStart = true;
            }

            switch (currentState)
            {
                case FSMStates.Hit:
                    UpdateHitState();
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


            elapsedTime += Time.deltaTime;


            if (attacking)
            {
                Attack();
                var animDuration = anim.GetCurrentAnimatorStateInfo(0).length;
                HandleDamage(animDuration);
            }
            else
            {
                animStart = false;
                animTimer = 0;
            }

            if (animStart)
            {
                animTimer += Time.deltaTime;
            }

            if (hitStart)
            {
                hitTimer += Time.deltaTime;
            }
        }
    }

    void UpdateHitState()
    {
        anim.SetInteger("animState", 6);
        
        if (turnOnFirstHit)
        {
            firstHit.SetActive(true);
            turnOnFirstHit = false;
        }

        if (hitTimer >= hitDuration)
        {
            hitStart = false;
            hitTimer = 0;
            isHit = false;
            currentState = FSMStates.Chase;
        }
    }

    void UpdateChaseState()
    {
        anim.SetInteger("animState", 2);

        agent.speed = enemySpeed;
        nextDestination = player.transform.position;
        agent.stoppingDistance = attackDistance;

        if (distanceToPlayer <= attackDistance)
        {
            currentState = FSMStates.Attack;
        }

        FaceTarget(nextDestination);
        agent.SetDestination(nextDestination);
    }

    void UpdateAttackState()
    {
        nextDestination = player.transform.position;
        agent.stoppingDistance = attackDistance;

        if (distanceToPlayer <= attackDistance)
        {
            attacking = true;
            currentState = FSMStates.Attack;
        }
        else
        {
            attacking = false;
            cheese.SetActive(false);
            currentState = FSMStates.Chase;
        }

        FaceTarget(nextDestination);
    }

    void UpdateDeadState()
    {
        FindObjectOfType<LevelManager>().LevelBeat();
        Destroy(gameObject, 3);
    }

    void Attack()
    {
        if (!isDead && !LevelManager.isGameOver)
        {
            if (elapsedTime >= attackRate)
            {
                int attack = Random.Range(3, 6);
                anim.SetInteger("animState", attack);
                var animDuration = anim.GetCurrentAnimatorStateInfo(0).length;
                animStart = true;

                if (attack == 1)
                {
                     AudioSource.PlayClipAtPoint(regularAttackSFX, transform.position);

                }
                else if (attack == 2)
                {
                    AudioSource.PlayClipAtPoint(StompSFX, transform.position);
                }
                else
                {
                    AudioSource.PlayClipAtPoint(ThrowSFX, transform.position);
                    
                    cheese.SetActive(true);

                    Invoke("ThrowCheese", animDuration);
                }
                
                elapsedTime = 0.0f;
            }
        }
    }

    void HandleDamage(float animDuration)
    {
        bool dealDamage = false;
        if (animTimer >= animDuration)
        {
            dealDamage = true;
        }

        if (dealDamage)
        {
            if (distanceToPlayer <= attackDistance)
            {
                playerHealth.TakeDamage(damageAmount);
                animTimer = 0;
            }
            dealDamage = false;
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
