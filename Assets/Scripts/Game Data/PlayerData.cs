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
        // Alt�n miktar�n� g�ncellemek i�in UI gibi ba�ka i�lemler de yapabilirsiniz.
    }

}
