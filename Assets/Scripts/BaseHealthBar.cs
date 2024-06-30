using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BaseHealthBar : MonoBehaviour
{
     public Slider healthSlider;
    public TMP_Text healthBarText;

    private Damageable playerDamageable;

    private void Awake()
    {
      
            GameObject player = GameObject.FindGameObjectWithTag("Base");
            if (player == null)
            {
                Debug.Log("No player found in the scene. Make sure it has tag 'player'");
            }

            playerDamageable = player.GetComponent<Damageable>(); 

    } 
    void Start()
    {        
        healthSlider.value = CalculateSliderPercentage(playerDamageable.Health, playerDamageable.MaxHealth);
        healthBarText.text = "Base HP " + playerDamageable.Health + " / " + playerDamageable.MaxHealth;
    }

    private void OnEnable()
    {
        playerDamageable.healthChanged.AddListener(OnPlayerHealthChanged);
    }

    private void OnDisable()
    {
        playerDamageable.healthChanged.RemoveListener(OnPlayerHealthChanged);
    }

    private float CalculateSliderPercentage(float currentHealth, float maxHealth)
    {
        return currentHealth / maxHealth;
    }

    public void OnPlayerHealthChanged(int currentHealth, int maxHealth)
    {
        healthSlider.value = CalculateSliderPercentage(currentHealth, maxHealth);
        healthBarText.text = "Base HP " + playerDamageable.Health + " / " + playerDamageable.MaxHealth;
    }
     
}
