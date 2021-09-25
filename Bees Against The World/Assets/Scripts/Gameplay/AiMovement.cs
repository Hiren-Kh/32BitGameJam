using UnityEngine;
using UnityEngine.AI;

public class AiMovement : MonoBehaviour
{
    #region PUBLIC_VARS
    public NavMeshAgent agent;
    public StateType state;
    public float walkPointRange;
    public float sightRange;
    public float attackRange;
    public float minCloseDistance;
    public LayerMask bee, ground;
    public float timeBetweenAttack;
    public bool playerInSightRange, playerInAttackRange;
    #endregion

    #region PRIVATE_VARS
    public Transform target;
    private Vector3 targetPos;
    private bool isAlreadyAttacked;
    private bool isWalkpointSet;
    #endregion

    #region UNITY_CALLBACKS
    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, bee);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, bee);

        if (GameManager.Instance.phaseType == PhaseType.DEFENCE)
        {
            target = GameManager.Instance.queenBee;
            state = StateType.CHASING;
        }
        else if(!playerInSightRange && !playerInAttackRange)
        {
            state = StateType.ROAMING;
        }

        if(playerInSightRange && !playerInAttackRange)
        {
            state = StateType.CHASING;
        }
        if(playerInSightRange && playerInAttackRange)
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
    #endregion

    #region PUBLIC_FUNCTIONS
    #endregion

    #region PRIVATE_FUNCTIONS
    private void Patroling()
    {
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

        if(Physics.Raycast(targetPos, -transform.up, 5f, ground))
        {
            isWalkpointSet = true;
        }
    }

    private void Chase()
    {
        agent.SetDestination(target.position);
    }

    private void Attack()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(target);

        if (!isAlreadyAttacked)
        {
            //Attack
            Debug.Log("Attack");

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