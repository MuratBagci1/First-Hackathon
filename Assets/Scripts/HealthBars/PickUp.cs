using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public int healthRestore = 20;
    public Vector3 spinRotationSpeed = new Vector3(0, 180, 0);

    public string objectType = "Coin";
    public int goldReward;

    AudioSource pickupSource;
    Damageable damageable;

    private void Awake()
    {
        pickupSource = GetComponent<AudioSource>();
        gameObject.name = objectType;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        damageable = collision.GetComponent<Damageable>();

        if (damageable)
        {
            if (gameObject.name == "HealthPotion")
            {
                damageable.Heal(healthRestore);
                if (pickupSource)
                {
                    AudioSource.PlayClipAtPoint(pickupSource.clip, gameObject.transform.position, pickupSource.volume);
                }
                Destroy(gameObject);
            }
            else if (gameObject.name == "Coin")
            {
                goldReward *= 5;
                damageable.GiveGoldToPlayer(goldReward);
                if (pickupSource)
                {
                    AudioSource.PlayClipAtPoint(pickupSource.clip, gameObject.transform.position, pickupSource.volume);
                }
                Destroy(gameObject);
            }
        }
    }

    private void Update()
    {
        transform.eulerAngles += spinRotationSpeed * Time.deltaTime;
    }
}