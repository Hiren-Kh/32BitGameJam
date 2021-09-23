using UnityEngine;

public class LookAtObject : MonoBehaviour
 {
    #region PUBLIC_VARS
    public Transform target;
    #endregion

    #region PRIVATE_VARS
    #endregion

    #region UNITY_CALLBACKS
    private void Awake()
    {
        if (target == null)
            target = Camera.main.transform;
    }
    private void LateUpdate()
    {
        transform.LookAt(transform.position + target.forward);
    }
    #endregion

    #region PUBLIC_FUNCTIONS
    #endregion

    #region PRIVATE_FUNCTIONS
    #endregion

    #region CO-ROUTINES
    #endregion

    #region EVENT_HANDLERS
    #endregion

    #region UI_CALLBACKS
    #endregion
}