using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Damageable))]
public class Knight : Dummy
{
    public GameObject coin;
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
        coinParent = GameObject.FindGameObjectWithTag("Coins").transform;
        Instantiate(coin, transform.position, Quaternion.identity, coinParent);
    }
}
