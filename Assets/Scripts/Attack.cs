using UnityEngine;

public class Attack : MonoBehaviour
{
    Collider2D attackCollider;
    public int attackDamage = 10;

    private void Awake()
    {
        attackCollider = GetComponent<Collider2D>();
    }
    private void Start()
    {
        

        CalculateDamageBasedOnParent( );

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Hasar alabilecek bir hedef olup olmad���n� kontrol et
        Damageable damageable = collision.GetComponent<Damageable>();
        if (damageable != null)
        {
        
            // Hedefe vuru� yap
            bool gotHit = damageable.Hit(attackDamage);
            if (gotHit)
            {
                Debug.Log(collision.name + " hit for " + attackDamage);
            }
        }
    }

    // Parent ad�na g�re hasar hesaplama metodu
    private void CalculateDamageBasedOnParent()
    {
        //int damage = attackDamage;
        string  parentTag = transform.parent.tag; 
        if (parentTag == "Player")
        {
            //damage += 50;  
        }
        else
        {
            attackDamage = attackDamage + (2 * GameManager.Instance.enemyDamageMultiplier);
        }

        //return damage;
    }

    public void UpgradeAttackDamage(int amount)
    {
        attackDamage += amount;
        Debug.Log("Attack damage upgraded by: " + amount + ". New attack damage: " + attackDamage);
    }
}
