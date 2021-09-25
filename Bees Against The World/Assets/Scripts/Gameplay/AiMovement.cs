using UnityEngine;
using UnityEngine.AI;

public class AiMovement : MonoBehaviour
{
    #region PUBLIC_VARS
    public NavMeshAgent agent;
    public StateType state;
    [Header("Range")]
    public float walkPointRange;
    public float sightRange;
    public float attackRange;
    [Header("Check Vars")]
    public LayerMask targetMask, groundMask;
    public bool targetInSightRange, targetInAttackRange, isBeingAttacked;
    public bool isBee;
    [Header("Abilities Vars")]
    public float timeBetweenAttack;
    public float damageAmount;
    #endregion

    #region PRIVATE_VARS
    private Transform target;
    private Vector3 targetPos;
    private bool isAlreadyAttacked;
    private bool isWalkpointSet;
    private Transform fixedTarget;
    #endregion

    #region UNITY_CALLBACKS
    private void Start()
    {
        if(!isBee)
        {
            fixedTarget = GameManager.Instance.queenBee;
        }
    }

    private void Update()
    {
        targetInSightRange = Physics.CheckSphere(transform.position, sightRange, targetMask);
        targetInAttackRange = Physics.CheckSphere(transform.position, attackRange, targetMask);


        if (GameManager.Instance.phaseType == PhaseType.DEFENCE && !targetInSightRange)
        {
            if(!isBee)
            {
                target = null;
                state = StateType.CHASING;
            }
            else
            {
                state = StateType.ROAMING;
            }
        }
        else if(!targetInSightRange && !targetInAttackRange)
        {
            state = StateType.ROAMING;
        }

        if(targetInSightRange && !targetInAttackRange)
        {
            if(target == null)
            {
                RaycastHit[] targets = Physics.SphereCastAll(transform.position, sightRange, Vector3.up, sightRange, targetMask);
                target = targets[0].transform;
            }

            state = StateType.CHASING;
        }
        if(targetInSightRange && targetInAttackRange)
        {
            state = StateType.ATTACK;
        }

        switch(state)
        {
            case StateType.ROAMING:
                Patroling();
                break;

            case StateType.CHASING:
                Chase();
                break;

            case StateType.ATTACK:
                Attack();
                break;

            default:
                break;
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, walkPointRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
#endif
    #endregion

    #region PUBLIC_FUNCTIONS
    #endregion

    #region PRIVATE_FUNCTIONS
    private void Patroling()
    {
        target = null;
        if(!isWalkpointSet)
        {
            SearchWalkPoint();
        }

        agent.SetDestination(targetPos);

        if((transform.position - targetPos).magnitude < 1f)
        {
            isWalkpointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        var randomZ = Random.Range(-walkPointRange, walkPointRange);
        var randomX = Random.Range(-walkPointRange, walkPointRange);

        targetPos = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if(Physics.Raycast(targetPos, -transform.up, 5f, groundMask))
        {
            isWalkpointSet = true;
        }
    }

    private void Chase()
    {
        if(target == null)
        {
            agent.SetDestination(fixedTarget.position);
        }
        else
        {
            agent.SetDestination(target.position);
        }
    }

    private void Attack()
    {
        agent.SetDestination(transform.position);
        if(target == null)
        {
            target = fixedTarget;
        }

        transform.LookAt(target);

        if (!isAlreadyAttacked)
        {
            //Attack

            bool isDied = target.GetComponent<Health>().takeDamage(damageAmount);
            if(isDied)
            {
                target = null;
                state = StateType.ROAMING;
            }

            isAlreadyAttacked = true;
            Invoke("ResetAttack", timeBetweenAttack);
        }
    }

    private void ResetAttack()
    {
        isAlreadyAttacked = false;
    }
    #endregion

    #region CO-ROUTINES
    #endregion

    #region EVENT_HANDLERS
    #endregion

    #region UI_CALLBACKS
    #endregion
}

public enum StateType
{
    ROAMING,
    CHASING,
    ATTACK
}