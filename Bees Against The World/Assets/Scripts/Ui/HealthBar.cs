using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
 {
    #region PUBLIC_VARS
    public Slider slider;
    public Gradient gradient;
    public Image barImage;
    #endregion

    #region PRIVATE_VARS
    #endregion

    #region UNITY_CALLBACKS
    #endregion

    #region PUBLIC_FUNCTIONS
    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;

        barImage.color = gradient.Evaluate(1f);
    }

    public void SetHealthBar(float health)
    {
        slider.value = health;

        barImage.color = gradient.Evaluate(slider.normalizedValue);
    }
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