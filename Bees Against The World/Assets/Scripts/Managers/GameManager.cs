using UnityEngine;

public class GameManager : Manager<GameManager>
 {
    #region PUBLIC_VARS
    public PhaseType phaseType;
    public Transform queenBee;
    #endregion

    #region PRIVATE_VARS
    #endregion

    #region UNITY_CALLBACKS
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

public enum PhaseType
{
    GATHERING,
    DEFENCE
}