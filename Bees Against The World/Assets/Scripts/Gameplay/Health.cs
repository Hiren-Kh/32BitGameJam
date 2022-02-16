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
    public void takeDamage(float damage)
    {
        currentHealthPoint -= damage;

        healthBar.SetHealthBar(currentHealthPoint);

        if (currentHealthPoint <= 0f)
        {
            Die();
        }
    }

    #endregion

    #region PRIVATE_FUNCTIONS
    private void Die()
    {
        Debug.Log("Die");
        if(gameObject.CompareTag(Constants.PLAYER))
        {
            var obj = Instantiate(transform, new Vector3(0,1,0), Quaternion.identity);
            Camera.main.GetComponent<SmoothFollow>().target = obj;
            DestroyImmediate(gameObject);
            return;
        }
        Spawner.Instance.Spawn(transform);
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