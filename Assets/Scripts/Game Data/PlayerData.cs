using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerData: MonoBehaviour
{


    public TextMeshProUGUI goldText;
    public int gold = 100;

    private void Awake()
    {
        AddGold(1);
    }


    public void AddGold(int amount)
    {
        gold += amount;
        Debug.Log("Gold added: " + amount + ". Total Gold: " + gold); 
        goldText.text = gold.ToString();
    }
    public void UpgradeChildAttackDamage(int amount)

    {
        Debug.Log("Total Gold: " + gold); 
        int upgradeCost = amount * 1; // Örnek maliyet hesaplama: Her saldýrý deðeri için 10 altýn

        if (gold >= upgradeCost)
        {
            AddGold(-upgradeCost);
            foreach (Transform child in transform)
            {
                Attack attackScript = child.GetComponent<Attack>();
                if (attackScript != null)
                {
                    attackScript.UpgradeAttackDamage(amount);
                }
            }

            Debug.Log("Upgrade successful. Remaining gold: " + gold);
        }
        else
        {
            Debug.Log("Not enough gold to upgrade.");
        }
    }
    public void UpgradeArmor(int amount)
    {
        int upgradeCost = amount * 1; 

        if (gold >= upgradeCost)
        {
            
            AddGold(-upgradeCost);

            Damageable damageable = transform.GetComponent<Damageable>();

            if (damageable != null)
            {
                damageable.UpgradeArmor(amount);
            }

            Debug.Log("Armor upgrade successful. Remaining gold: " + gold);
        }
        else
        {
            Debug.Log("Not enough gold to upgrade armor.");
        }
    }

}
