using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 15;
    public Vector2 moveSpeed = new Vector2(20f, 0);
    public Vector2 knockback = new Vector2(4f, 0);

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //If you want the projectile to be effected by gravity by default, make it dynamic mode rigidbody!
        rb.velocity = new Vector2(moveSpeed.x * transform.localScale.x, moveSpeed.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();

        if (damageable != null)
        {
            Vector2 deliveredKnockback = transform.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);
            //hit the target
            bool gotHit = damageable.Hit(damage, deliveredKnockback);
            if (gotHit)
            {
                Destroy(gameObject);
                Debug.Log(collision.name + " hit for " + damage);
            }
        }
    }
}
