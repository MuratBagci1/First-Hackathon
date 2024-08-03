using UnityEngine;
using TMPro;

public class PlayerData : MonoBehaviour
{
    public TextMeshProUGUI goldText;
    public Damageable damageable;
    public int weaponUpgrade = 0;
    public int gold = 0;

    private void Awake()
    {
        damageable = transform.GetComponent<Damageable>();
    }

    public void AddGold(int amount)
    {
        gold += amount;
        goldText.text = gold.ToString();
    }

    public void UpgradeChildAttackDamage(int amount)
    {
        int upgradeCost = amount * 1; // Örnek maliyet hesaplama: Her saldýrý deðeri için 1 altýn


        if (gold >= upgradeCost && weaponUpgrade != 5)
        {
            AddGold(-upgradeCost);
            foreach (Transform child in transform)
            {
                Attack attackScript = child.GetComponent<Attack>();
                if (attackScript != null)
                {
                    attackScript.UpgradeAttackDamage(amount);
                    weaponUpgrade++;
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
        if (damageable.Armor < 90)
        {
            if (gold >= upgradeCost)
            {
                AddGold(-upgradeCost);

                if (damageable != null)
                {
                    damageable.UpgradeArmor(amount);
                }
            }
        }
        else
        {
            Debug.Log("Upgrade armor iþlemi baþarýsýz");
        }
            
        //else
        //{
        //    Debug.Log("Not enough gold to upgrade armor.");
        //}
    }
}
