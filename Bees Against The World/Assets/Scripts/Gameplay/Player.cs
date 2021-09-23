using UnityEngine;

public class Player : MonoBehaviour
 {
    #region PUBLIC_VARS
    public Health health;
    public float moveSpeed = 10f;
    public float rotationSpeed = 5f;
    #endregion

    #region PRIVATE_VARS
    private float horizontalMovement;
    private float verticalMovement;
    //private Vector3 limit;
    //private Vector3 targetPos;

    
    #endregion

    #region UNITY_CALLBACKS
    private void Update()
    {
        horizontalMovement = Input.GetAxis(Constants.HORIZONTAL);
        verticalMovement = Input.GetAxis(Constants.VERTICAL);

        if(Input.GetKeyDown(KeyCode.D))
        {
            Damage(15);
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

    /*private void ClampPosition()
    {
        targetPos = transform.position;

        targetPos.x = Mathf.Clamp(targetPos.x, -limit.x, limit.x);
        targetPos.z = Mathf.Clamp(targetPos.z, -limit.z, limit.z);

        transform.position = targetPos;
    }*/

    private void Damage(float value)
    {
        health.takeDamage(value);
    }
    #endregion

    #region CO-ROUTINES
    #endregion

    #region EVENT_HANDLERS
    #endregion

    #region UI_CALLBACKS
    #endregion
}