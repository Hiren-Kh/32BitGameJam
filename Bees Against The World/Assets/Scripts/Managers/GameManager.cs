using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : Manager<GameManager>
 {
    #region PUBLIC_VARS
    public PhaseType phaseType;
    public Transform queenBee;
    public float gatheringPhaseTimeInSec = 300f;
    public float defensePhaseTimeInSec = 180f;
    public int totalWaves = 3;
    public int currentWave = 1;
    public Text timerText;
    #endregion

    #region PRIVATE_VARS
    private float currentTimer = 0;
    private bool isAlreadyGameOver = false;
    #endregion

    #region UNITY_CALLBACKS
    private void Update()
    {
        if(queenBee == null && !isAlreadyGameOver)
        {
            isAlreadyGameOver = true;
            GameOver();
        }
    }
    #endregion

    #region PUBLIC_FUNCTIONS
    public void StartWave()
    {
        StartCoroutine(Timer());
        Spawner.Instance.SpawnEnemy();
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        OnExitButton();
    }
    #endregion

    #region PRIVATE_FUNCTIONS
    #endregion

    #region CO-ROUTINES
    private IEnumerator Timer()
    {
        if(phaseType == PhaseType.GATHERING)
        {
            queenBee.gameObject.SetActive(false);

            while (currentTimer <= gatheringPhaseTimeInSec)
            {
                timerText.text = "Defence Phase Starts In : " + (gatheringPhaseTimeInSec - currentTimer).ToString() + " sec";
                yield return new WaitForSeconds(1f);
                currentTimer += 1;

                if(currentTimer == gatheringPhaseTimeInSec/2)
                {
                    timerText.color = Color.red;
                    timerText.text = "Defense Phase Starting Soon.. Go Back To Hive";
                }
            }
            currentTimer = 0;
            phaseType = PhaseType.DEFENCE;
        }
        else
        {
            queenBee.gameObject.SetActive(true);

            Spawner.Instance.SpawnBees();
            Spawner.Instance.SpawnBees();
            SpawnEnemies();


            while (currentTimer <= defensePhaseTimeInSec)
            {
                timerText.text = "Gathring Phase Starts In : " + (defensePhaseTimeInSec - currentTimer).ToString() + " sec";

                yield return new WaitForSeconds(1f);
                currentTimer += 1;

                if (currentTimer == defensePhaseTimeInSec / 2)
                {
                    timerText.color = Color.white;
                    timerText.text = "Defense Phase Ending Soon.. Fight It Up";
                }
            }
            currentTimer = 0;
            phaseType = PhaseType.GATHERING;
            currentWave++;
            Spawner.Instance.ResetForGatheringPhase();
        }
        StartWave();
    }

    private void SpawnEnemies()
    {
        StartCoroutine(EnemySpawn());
    }

    private IEnumerator EnemySpawn()
    {
        while(phaseType == PhaseType.DEFENCE)
        {
            Spawner.Instance.SpawnEnemy();
            yield return new WaitForSeconds(20f);
        }
    }
    #endregion

    #region EVENT_HANDLERS
    #endregion

    #region UI_CALLBACKS
    public void OnPlayButton()
    {
        StartWave();
    }

    public void OnExitButton()
    {
        Application.Quit();
    }
    #endregion
}

public enum PhaseType
{
    GATHERING,
    DEFENCE
}