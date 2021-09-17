using UnityEngine;

public class PlayerMovement : MonoBehaviour
 {
    #region PUBLIC_VARS
    public float moveSpeed;
    public float rotationSpeed;
    #endregion

    #region PRIVATE_VARS
    private float horizontalMovement;
    private float verticalMovement;
    private Vector3 limit;
    private Vector3 targetPos;
    #endregion

    #region UNITY_CALLBACKS
    private void Update()
    {
        horizontalMovement = Input.GetAxis("Horizontal");
        verticalMovement = Input.GetAxis("Vertical");
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