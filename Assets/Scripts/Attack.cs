using UnityEngine;

public class Attack : MonoBehaviour
{
    Collider2D attackCollider;
    public int attackDamage = 10;
    public Vector2 knockback = Vector2.zero;

    private void Awake()
    {
        attackCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Hasar alabilecek bir hedef olup olmadýðýný kontrol et
        Damageable damageable = collision.GetComponent<Damageable>();
        if (damageable != null)
        {
 
            string parentName = transform.parent != null ? transform.parent.name : "No Parent";
            Debug.Log("parentName " + parentName);

 
            int updatedDamage = CalculateDamageBasedOnParent(parentName);

            Vector2 deliveredKnockback = transform.parent.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);
            // Hedefe vuruþ yap
            bool gotHit = damageable.Hit(updatedDamage, deliveredKnockback);
            if (gotHit)
            {
                Debug.Log(collision.name + " hit for " + updatedDamage);
            }
        }
    }

    // Parent adýna göre hasar hesaplama metodu
    private int CalculateDamageBasedOnParent(string parentName)
    {
        int damage = attackDamage;

        if (parentName == "Player")
        {
            //damage += 50;  
        }
        else  
        {
            damage = attackDamage * GameManager.Instance.enemyDamageMultiplier;  
        }

        return damage;
    }

    public void UpgradeAttackDamage(int amount)
    {
        attackDamage += amount;
        Debug.Log("Attack damage upgraded by: " + amount + ". New attack damage: " + attackDamage);
    }
}
