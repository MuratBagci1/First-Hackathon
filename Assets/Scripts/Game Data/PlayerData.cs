using UnityEngine;
using TMPro;

public class PlayerData : MonoBehaviour
{
    public TextMeshProUGUI goldText;
    public int gold = 0;

    private void Awake()
    {
        AddGold(50);
    }

    public void AddGold(int amount)
    {
        gold += amount;
        goldText.text = gold.ToString();
    }

    public void UpgradeChildAttackDamage(int amount)
    {
        int upgradeCost = amount * 1; // �rnek maliyet hesaplama: Her sald�r� de�eri i�in 1 alt�n

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
        }
        //else
        //{
        //    Debug.Log("Not enough gold to upgrade.");
        //}
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
        }
        //else
        //{
        //    Debug.Log("Not enough gold to upgrade armor.");
        //}
    }
}
