using TMPro;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public TextMeshProUGUI goldText;
    public Damageable damageable;
    public int weaponUpgrade = 0;
    public int gold = 0;

    private void Awake()
    {
        SaveManager.Instance.playerData = gameObject.GetComponent<PlayerData>();
        damageable = transform.GetComponent<Damageable>();
    }

    public void GameLoaded()
    {
        goldText.text = gold.ToString();

        foreach (Transform child in transform)
        {
            Attack attackScript = child.GetComponent<Attack>();
            if (attackScript != null)
            {
                attackScript.UpgradeAttackDamage(weaponUpgrade * 10);
            }
        }
    }

    public void AddGold(int amount)
    {
        gold += amount;
        goldText.text = gold.ToString();
    }
}
