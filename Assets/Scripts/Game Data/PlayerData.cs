using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerData: MonoBehaviour
{


    public TextMeshProUGUI goldText;
    private int gold = 0;
 

    public void AddGold(int amount)
    {
        gold += amount;
        Debug.Log("Gold added: " + amount + ". Total Gold: " + gold); 
        goldText.text = gold.ToString();
    }

    
}
