using UnityEngine;

public class Health : MonoBehaviour
 {
    #region PUBLIC_VARS
    public HealthBar healthBar;
    public float maxHealthPoints = 100.0f;
    #endregion

    #region PRIVATE_VARS
    private float currentHealthPoint = 100.0f;
    #endregion

    #region UNITY_CALLBACKS
    private void OnEnable()
    {
        currentHealthPoint = maxHealthPoints;
        healthBar.SetMaxHealth(maxHealthPoints);
    }
    #endregion

    #region PUBLIC_FUNCTIONS
    public bool takeDamage(float damage)
    {
        currentHealthPoint -= damage;

        healthBar.SetHealthBar(currentHealthPoint);

        if (currentHealthPoint <= 0f)
        {
            Die();
            return true;
        }
        return false;
    }

    #endregion

    #region PRIVATE_FUNCTIONS
    private void Die()
    {
        Debug.Log("Die");
        if(gameObject.CompareTag(Constants.PLAYER))
        {
            gameObject.SetActive(false);
            return;
        }

        Destroy(gameObject);
    }
    #endregion

    #region CO-ROUTINES
    #endregion

    #region EVENT_HANDLERS
    #endregion

    #region UI_CALLBACKS
    #endregion
}