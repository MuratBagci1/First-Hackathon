using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public int healthRestore = 20;
    public Vector3 spinRotationSpeed = new Vector3(0, 180, 0);

    AudioSource pickupSource;
    Damageable damageable;

    private void Awake()
    {
        pickupSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        damageable = collision.GetComponent<Damageable>();

        if (damageable)
        {
            if (gameObject.name == "HealthPickup")
            {

                if (damageable.Heal(healthRestore))
                {
                    if (pickupSource)
                    {
                        AudioSource.PlayClipAtPoint(pickupSource.clip, gameObject.transform.position, pickupSource.volume);
                    }
                    Destroy(gameObject);
                }
            }
            else if (gameObject.name == "Coin(Clone)")
            {
                int goldReward = Random.Range(0, 5) * 5;
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
