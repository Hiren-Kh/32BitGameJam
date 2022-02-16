using UnityEngine;

public class Player : MonoBehaviour
 {
    #region PUBLIC_VARS
    public Health health;
    public float moveSpeed = 10f;
    public float rotationSpeed = 5f;
    public LayerMask targetMask;
    public float attackRange;
    public float timeBetweenAttacks;
    public float damageAmount;
    #endregion

    #region PRIVATE_VARS
    private float horizontalMovement;
    private float verticalMovement;
    private bool canAttack = true;
    [SerializeField] private Vector3 limit;
    private Vector3 targetPos;
    #endregion

    #region UNITY_CALLBACKS
    private void Update()
    {
        horizontalMovement = Input.GetAxis(Constants.HORIZONTAL);
        verticalMovement = Input.GetAxis(Constants.VERTICAL);
        ClampPosition();

        if(Input.GetKeyDown(KeyCode.Space) && canAttack)
        {
            canAttack = false;
            RaycastHit[] targets = Physics.SphereCastAll(transform.position, attackRange, Vector3.up, 500f, targetMask);
            if(targets.Length > 0)
            {
                Attack(targets[0].transform.GetComponent<Health>());
                Invoke("ResetAttack", timeBetweenAttacks);
            }
        }
    }

    private void FixedUpdate()
    {
        if (verticalMovement != 0)
        {
            Move();
        }
        if (horizontalMovement != 0)
        {
            Rotate();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
    #endregion

    #region PUBLIC_FUNCTIONS
    #endregion

    #region PRIVATE_FUNCTIONS
    private void Move()
    {
        transform.position +=  transform.forward * verticalMovement * moveSpeed * Time.fixedDeltaTime;
        //transform.Translate(Vector3.forward * verticalMovement * moveSpeed * Time.deltaTime);
    }

    private void Rotate()
    {
        transform.Rotate(transform.up * horizontalMovement * rotationSpeed);
    }

    private void Attack(Health helath)
    {
        helath.takeDamage(damageAmount);
    }

    private void ResetAttack()
    {
        canAttack = true;
    }

    private void ClampPosition()
    {
        targetPos = transform.position;

        targetPos.x = Mathf.Clamp(targetPos.x, -limit.x, limit.x);
        targetPos.z = Mathf.Clamp(targetPos.z, -limit.z, limit.z);

        transform.position = targetPos;
    }
    #endregion

    #region CO-ROUTINES
    #endregion

    #region EVENT_HANDLERS
    #endregion

    #region UI_CALLBACKS
    #endregion
}