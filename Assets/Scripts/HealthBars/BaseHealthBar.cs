using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BaseHealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public TMP_Text healthBarText;

    private Damageable baseDamageable;
    public GameObject _base ;

    private void Awake()
    {

        
        if (_base == null)
        {
            Debug.Log("No player found in the scene. Make sure it has tag 'player'");
        }

        baseDamageable = _base.GetComponent<Damageable>();

    }
    void Start()
    {
        healthSlider.value = CalculateSliderPercentage(baseDamageable.Health, baseDamageable.MaxHealth);
        healthBarText.text = "Base HP " + baseDamageable.Health + " / " + baseDamageable.MaxHealth;
    }

    private void OnEnable()
    {
        baseDamageable.healthChanged.AddListener(OnBaseHealthChanged);
    }

    private void OnDisable()
    {
        baseDamageable.healthChanged.RemoveListener(OnBaseHealthChanged);
    }

    private float CalculateSliderPercentage(float currentHealth, float maxHealth)
    {
        return currentHealth / maxHealth;
    }

    public void OnBaseHealthChanged(int currentHealth, int maxHealth)
    {
        healthSlider.value = CalculateSliderPercentage(currentHealth, maxHealth);
        healthBarText.text = "Base HP " + baseDamageable.Health + " / " + baseDamageable.MaxHealth;
    }

}
