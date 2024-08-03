using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 25;
    public Vector2 moveSpeed = new Vector2(30f, 0);

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        rb.velocity = new Vector2(moveSpeed.x * transform.localScale.x, moveSpeed.y);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();

        if (damageable != null)
        {
            bool gotHit = damageable.Hit(damage);
            if (gotHit)
            {
                Destroy(gameObject);
                Debug.Log($"{collision.name} hit for {damage}");
            }
        }
    }

    //private Vector2 GetKnockback()
    //{
    //    return transform.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);
    //}
}
