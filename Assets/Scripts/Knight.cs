using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Damageable))]
public class Knight : Dummy
{
    public GameObject coin;
    public GameObject potion;
    public Transform coinParent;

    private void Start()
    {
        if(transform.localPosition.x > 0)
        {
            FlipDirection();
        }
    }
    public void GiveReward()
    {
        int reward = UnityEngine.Random.Range(1, 6);
        if(reward == 5)
        {
            coinParent = GameObject.FindGameObjectWithTag("Coins").transform;
            potion = Instantiate(potion, transform.position, Quaternion.identity, coinParent);
            potion.GetComponent<PickUp>().objectType = "HealthPotion";
        }
        else if (reward > 1)
        {
            coinParent = GameObject.FindGameObjectWithTag("Coins").transform;            
            coin = Instantiate(coin, transform.position, Quaternion.identity, coinParent);
            coin.GetComponent<PickUp>().objectType = "Coin";
            coin.GetComponent<PickUp>().goldReward = reward;
        }
    }
}
