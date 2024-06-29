using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData: MonoBehaviour
{
    
 
    public int gold = 0;

    public void AddGold(int amount)
    {
        gold += amount;
        Debug.Log("Gold added: " + amount + ". Total Gold: " + gold);
        // Altýn miktarýný güncellemek için UI gibi baþka iþlemler de yapabilirsiniz.
    }

}
