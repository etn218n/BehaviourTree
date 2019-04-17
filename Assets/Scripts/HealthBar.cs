using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Image  healthBar;
    private Health health;

    private void Start()
    {
        IHealthGauge healthGauge = GetComponentInParent<IHealthGauge>();

        if (healthGauge != null)
            health = healthGauge.GetHealth();

        healthBar = GetComponent<Image>();

        if (health != null && healthBar != null)
        {
            health.OnChanged += UpdateHealthBar;
        }
    }

    private void UpdateHealthBar(System.Object sender, System.EventArgs eventArgs)
    {
        healthBar.fillAmount = health.CurrentHP / health.MaxHP;
    }
}
