using UnityEngine;


// TODO: oklarýn hasarlarý artacak

public class Projectile : MonoBehaviour
{
    public int damage = 15;
    public Vector2 moveSpeed = new Vector2(30f, 0);

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        rb.velocity = new Vector2(moveSpeed.x * transform.localScale.x, moveSpeed.y);
        PlayerData playerData = FindAnyObjectByType<PlayerData>();
        damage = damage + (playerData.weaponUpgrade * 10);
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y - 5f * Time.deltaTime);
        Debug.Log("oky " + gameObject.transform.localPosition .y);
        if (gameObject.transform.position.y < -3)
        {
            Destroy(gameObject);
        }
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
