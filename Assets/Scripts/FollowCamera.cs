using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public float yOffset;  

    private Transform playerTransform;

    void Start()
    { 
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("Player nesnesi bulunamadý!");
        }
    }

    void Update()
    {
        if (playerTransform != null)
        { 
            Vector3 newPosition = new Vector3(playerTransform.position.x , playerTransform.position.y + yOffset, playerTransform.position.z);
            transform.position = newPosition;
             
            transform.rotation = playerTransform.rotation;
            transform.localScale = playerTransform.localScale;
        }
    }
}