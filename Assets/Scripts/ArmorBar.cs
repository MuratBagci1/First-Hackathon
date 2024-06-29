using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ArmorBar : MonoBehaviour
{
    public Slider armorSlider;
    public TMP_Text armorBarText;

    private Damageable playerDamageable;

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            Debug.Log("No player found in the scene. Make sure it has tag 'player'");
        }

        playerDamageable = player.GetComponent<Damageable>();
    }

    // Start is called before the first frame update
    void Start()
    {
        armorSlider.value = CalculateSliderPercentage(playerDamageable.Armor, playerDamageable.MaxArmor);
        armorBarText.text = "Armor " + playerDamageable.Armor + " / " + playerDamageable.MaxArmor;
    }

    private void OnEnable()
    {
        playerDamageable.armorChanged.AddListener(OnPlayerArmorChanged);
    }

    private void OnDisable()
    {
        playerDamageable.armorChanged.RemoveListener(OnPlayerArmorChanged);
    }

    private float CalculateSliderPercentage(float currentArmor, float maxArmor)
    {
        return currentArmor / maxArmor;
    }

    public void OnPlayerArmorChanged(int currentArmor, int maxArmor)
    {
        armorSlider.value = CalculateSliderPercentage(currentArmor, maxArmor);
        armorBarText.text = "Armor " + playerDamageable.Armor + " / " + playerDamageable.MaxArmor;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
