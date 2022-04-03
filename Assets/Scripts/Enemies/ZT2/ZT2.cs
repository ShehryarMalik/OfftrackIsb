using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZT2 : MonoBehaviour
{
    public int MaxHealth = 100;
    private int currentHealth;

    private NavMeshAgent agent;
    private Transform player;
    [SerializeField] private LayerMask whatIsGround, whatIsPlayer;

    //Patroling
    [SerializeField] private Vector3 walkPoint;
    [SerializeField] private bool walkPointSet;
    [SerializeField] private float walkPointRange;

    //Attacking
    [SerializeField] private float timeBetweenAttacks, timeBetweenHitReaction;
    [SerializeField] private bool alreadyAttacked;

    //States
    [SerializeField] private float sightRange, attackRange;

    [Header("Type Setting")]
    public bool isRunner = false;
    [SerializeField] private float runSpeed = 4, walkSpeed = 2;

    [SerializeField] private bool playerInSightRange, playerInAttackRange;
    private ParticleSystem hitParticles;
    Transform AttackChecker;
    [SerializeField] float attackCheckerRadius = 0.53f, timeBeforeAttackCheck = 0.6f;
    private int damageAmount = 25;
    private Animator animator;
    private ThirdPersonCC playerCC;
    public bool detectedTarget = false;

    public bool takingBreak = false;

    public bool footStepSFX, footStepsWait;
    public float footStepWaitTime = 0.8f, footStepWaitTimeRunning = 0.4f;

    private float currentType = 1;
    public bool dead = false;
    public bool playGruntOnWake = true;
    [SerializeField] AudioSource zombieGrunt, zombieDie, ZombieSteps;

    private void Awake()
    {
        AttackChecker = gameObject.transform.GetChild(0);
        hitParticles = GetComponentInChildren<ParticleSystem>();
        player = GameObject.Find("Character").transform;
        playerCC = player.gameObject.GetComponent<ThirdPersonCC>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        currentHealth = MaxHealth;
        
        if(zombieGrunt && playGruntOnWake)
            zombieGrunt.Play();
    }

    private void Update()
    {
        CheckType();

        //check for sight and attack range
        if(!detectedTarget)
        {
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            detectedTarget = playerInSightRange;
        }
        
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if(!dead && !takingBreak)
        {
            //if (!playerInSightRange && !playerInAttackRange) Patroling();
            if (!detectedTarget && !playerInSightRange && !playerInAttackRange) Idiling();
            if (playerInSightRange && !playerInAttackRange) ChasePlayer();
            if (playerInAttackRange && playerInSightRange && playerCC.getCurrentHealth() > 0) AttackPlayer();
        }
        else
        {
            Idiling();
        }

        SetAnimations();
    }

    private void CheckType()
    {
        if (isRunner && currentType != 2)
        {
            animator.SetBool("isWalking", false);
            agent.speed = runSpeed;
            currentType = 2;
        }

        else if(!isRunner && currentType != 1)
        {
            animator.SetBool("isRunning", false);
            agent.speed = walkSpeed;
            currentType = 1;
        }

    }

    IEnumerator waitForNextFootStep()
    {
        if (isRunner)
            yield return new WaitForSeconds(footStepWaitTimeRunning);

        else
            yield return new WaitForSeconds(footStepWaitTime);

        footStepsWait = false;
    }

    private void SetAnimations()
    {
        if(currentType == 1)
        {
            bool isWalking = animator.GetBool("isWalking");

            //if he is moving
            if (agent.velocity.magnitude >= 0.1f)
            {
                if (!isWalking)
                    animator.SetBool("isWalking", true);

                if(footStepSFX && ZombieSteps && !footStepsWait)
                {
                    ZombieSteps.Play();
                    footStepsWait = true;
                    StartCoroutine("waitForNextFootStep");
                }
                    
            }
            //if he is not moving
            else
            {
                if (isWalking)
                    animator.SetBool("isWalking", false);
            }
        }
        else
        {
                bool isRunning = animator.GetBool("isRunning");

                //if he is moving
                if (agent.velocity.magnitude >= 0.1f)
                {
                    if (!isRunning)
                        animator.SetBool("isRunning", true);
                
                    if (footStepSFX && ZombieSteps && !footStepsWait)
                    {
                        ZombieSteps.Play();
                        footStepsWait = true;
                        StartCoroutine("waitForNextFootStep");
                    }
            }
                //if he is not moving
                else
                {
                    if (isRunning)
                        animator.SetBool("isRunning", false);
                }
        }
    }

    private void Idiling()
    {
        agent.destination = transform.position;
    }

    private void Patroling()
    {
        if (!walkPointSet) searchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //walkPoint reached
        if(distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    private void searchWalkPoint()
    {
        //Calculate Random Point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, 0, transform.position.z + randomZ);

        if(Physics.Raycast(walkPoint, Vector3.down,10f,whatIsGround))
        {
            walkPointSet = true;
        }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        //makes sure enemy doesnt move
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        if(!takingBreak)
        {
            //Attack code here
            animator.SetTrigger("Attack");
            Invoke(nameof(CheckHit), timeBeforeAttackCheck);
            ////

            //alreadyAttacked = true;
            takingBreak = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        takingBreak = false;
        //alreadyAttacked = false;
    }

    public void takeDamage(int damageAmount)
    {
        if(!dead)
        {
            if (zombieDie)
                zombieDie.Play(); //When taking damage not dying

            detectedTarget = true;
            playerInSightRange = true;
            currentHealth -= damageAmount;
            hitParticles.Play();
            takingBreak = true;
            if (currentHealth <= 0)
            {
                Die();
            }
            else
            {
                animator.SetTrigger("HitReaction");
                Invoke(nameof(ResetAttack), timeBetweenHitReaction);
            }
        }
    }

    void Die()
    {
        animator.SetTrigger("Dead");
        dead = true;
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
    }

    void CheckHit()
    {
        if(Physics.CheckSphere(AttackChecker.position, attackCheckerRadius, whatIsPlayer))
        {
            player.gameObject.GetComponent<ThirdPersonCC>().takeDamage(damageAmount);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        //Gizmos.color = Color.blue;
        //Gizmos.DrawWireSphere(AttackChecker.position, attackCheckerRadius);
    }

}
