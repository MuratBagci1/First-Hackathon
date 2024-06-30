using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DetectionZone : MonoBehaviour
{
    public UnityEvent noCollidersRemain;

    public List<Collider2D> detectedColliders = new List<Collider2D>();
    Collider2D col;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    detectedColliders.Add(collision);
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();
        if (damageable != null)
        {
            damageable.damageableDestroyed.AddListener(OnColliderDestroyed);
        }
        detectedColliders.Add(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        RemoveCollider(collision);
    }

    private void OnColliderDestroyed(Collider2D collider)
    {
        RemoveCollider(collider);
    }

    private void RemoveCollider(Collider2D collider)
    {
        detectedColliders.Remove(collider);

        if (detectedColliders.Count <= 0)
        {
            noCollidersRemain.Invoke();
        }
    }

}
