using UnityEngine;

public class SmoothFollow : MonoBehaviour
 {
	#region PUBLIC_VARS
    public Transform target;
    public Vector3 offset;
    public float moveSpeed;
    public float rotationSpeed;
    #endregion

    #region PRIVATE_VARS
    #endregion

    #region UNITY_CALLBACKS
    private void FixedUpdate()
    {
        HandleMovememt();
        HandleRotation();
    }
    #endregion

    #region PUBLIC_FUNCTIONS
    #endregion

    #region PRIVATE_FUNCTIONS
    private void HandleMovememt()
    {
        Vector3 targetPos = target.TransformPoint(offset);
        transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.fixedDeltaTime);
    }

    private void HandleRotation()
    {
        Vector3 targetDir = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(targetDir, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.fixedDeltaTime);
    }
    #endregion

    #region CO-ROUTINES
    #endregion

    #region EVENT_HANDLERS
    #endregion

	#region UI_CALLBACKS
	#endregion
}