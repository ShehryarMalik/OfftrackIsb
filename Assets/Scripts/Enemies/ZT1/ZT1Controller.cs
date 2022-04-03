using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class ZT1Controller : MonoBehaviour
{
    public int zombieID;
    [SerializeField] private float lookRadius = 10f, nearRadius = 10f;
    [SerializeField] private Transform target;
    [SerializeField] private ThirdPersonCC targetCC;
    [SerializeField] private Transform pointA, pointB;
    [SerializeField] int currentPoint = 1;
    [SerializeField] float waitingTimeBetweenPoints = 2f;
    [SerializeField] float detectsDelay = 1.5f;
    [SerializeField] GameObject detectionMark;
    [SerializeField] UpperFloorAreaCinematics upperFloorCinematics;
    [SerializeField] DownStairsAreaCinematicManager downStairsCinematics;
    [SerializeField] MainFreeLookController MFLC;

    public bool canDetect = false, canMoveToPoints = false, isEating = false;
    public bool targetFound = false, isAttacking = false;
    bool running, footStepWait = false;

    public bool detectsAnimationPlayed = false;
    public AudioSource detectionSound, eatingSFX, footStepSFX, zombieGruntSFX;
    NavMeshAgent agent;
    Animator anim;
    public float footStepWaitTime = 0.8f, footStepWaitTimeRunning = 0.4f;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isAttacking)
        {
            if (canDetect)
            {
                float distance = Vector3.Distance(target.position, transform.position);

                //if the character is already found or is in range and is not sneaking
                if (targetFound || (distance <= lookRadius && !targetCC.isSneakPressed))
                {
                    running = true;
                    agent.speed = 5f;
                    agent.acceleration = 40;

                    targetFound = true;

                    if (!detectsAnimationPlayed)
                    {
                        if(detectionSound) detectionSound.Play();
                        if (zombieGruntSFX) zombieGruntSFX.Play();
                        agent.isStopped = true;
                        anim.SetTrigger("detects");
                        detectsAnimationPlayed = true;
                        StartCoroutine("detectionDelay");
                        MFLC.FocusTransformForSeconds(this.transform, 1.5f);
                    }

                    agent.SetDestination(target.position);

                    //if (agent.isStopped)
                    //    agent.isStopped = false;

                    if (isEating)
                        isEating = false;

                    if (canMoveToPoints)
                        canMoveToPoints = false;

                    if (distance <= (agent.stoppingDistance + 0.5f))
                    {
                        //face target
                        faceTarget();

                        //attack
                        isAttacking = true;

                        if (upperFloorCinematics != null)
                            upperFloorCinematics.TriggerInstaKillCinematic();

                        else if (downStairsCinematics != null)
                        {
                            if(zombieID == 0)
                            {
                                downStairsCinematics.TriggerFirstZombieInstaKillCinematic();
                            }

                            if(zombieID == 1)
                            {
                                downStairsCinematics.TriggerSecondZombieInstaKillCinematic();
                            }
                        }

                        else
                            Debug.Log("No cinematic managerFound!!");
                    }
                }

                //if player comes too close
                if(!targetFound && distance <= nearRadius)
                {
                    targetFound = true;
                }
            }

            if (canMoveToPoints)
            {
                running = false;

                agent.speed = 1.5f;
                agent.acceleration = 20;

                if (agent.isStopped)
                    agent.isStopped = false;

                if (isEating)
                    isEating = false;

                if (!targetFound)
                {
                    float distanceToPointA = Vector3.Distance(pointA.position, transform.position);
                    float distanceToPointB = Vector3.Distance(pointB.position, transform.position);

                    if (currentPoint == 1)
                    {
                        if (distanceToPointA > agent.stoppingDistance)
                        {
                            agent.SetDestination(pointA.position);
                        }

                        else
                        {
                            StartCoroutine("waitAndChangePointToB");
                        }
                    }

                    else if (currentPoint == 2)
                    {
                        if (distanceToPointB > agent.stoppingDistance)
                        {
                            agent.SetDestination(pointB.position);
                        }

                        else
                        {
                            StartCoroutine("waitAndChangePointToA");
                        }
                    }

                }
            }

            if (isEating)
            {
                if (!anim.GetBool("isEating"))
                {
                    anim.SetBool("isEating", true);
                    eatingSFX.loop = true;
                    eatingSFX.Play();
                }
                    
            }
            else
            {
                if (anim.GetBool("isEating"))
                {
                    anim.SetBool("isEating", false);
                    eatingSFX.Stop();
                }

            }

            //set moving animation
            if (agent.velocity.magnitude >= 0.1f)
            {
                if(!footStepWait && footStepSFX)
                {
                    footStepSFX.Play();
                    footStepWait = true;
                    StartCoroutine("waitForNextFootStep");
                }

                if (running)
                {
                    if (!anim.GetBool("isRunning"))
                    {
                        anim.SetBool("isRunning", true);
                    }
                }

                if (!anim.GetBool("isWalking"))
                    anim.SetBool("isWalking", true);
            }
            else
            {
                if (anim.GetBool("isWalking"))
                {
                    anim.SetBool("isWalking", false);
                }

                if (anim.GetBool("isRunning"))
                {
                    anim.SetBool("isRunning", false);
                }

            }
        }

    }

    IEnumerator waitForNextFootStep()
    {
        if (running)
            yield return new WaitForSeconds(footStepWaitTimeRunning);

        else
            yield return new WaitForSeconds(footStepWaitTime);

        footStepWait = false;
    }

    IEnumerator detectionDelay()
    {
        detectionMark.SetActive(true);

        yield return new WaitForSeconds(detectsDelay);
        agent.isStopped = false;
    }

    public void TriggerTargetFound()
    {
        targetFound = true;
    }

    public void StopAgent()
    {
        agent.isStopped = true;
    }

    IEnumerator waitAndChangePointToA()
    {
        yield return new WaitForSeconds(waitingTimeBetweenPoints);
        currentPoint = 1;
    }

    IEnumerator waitAndChangePointToB()
    {
        yield return new WaitForSeconds(waitingTimeBetweenPoints);
        currentPoint = 2;
    }

    void faceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,lookRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, nearRadius);
    }

    public void FullyStopZombie()
    {
        canDetect = false;
        canMoveToPoints = false;
        isEating = false;
        agent.isStopped = true;
        anim.SetBool("isWalking", false);
        anim.SetBool("isRunning", false);
    }
}
