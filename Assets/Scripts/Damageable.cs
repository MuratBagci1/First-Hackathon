using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    public UnityEvent<int, Vector2> damageableHit;
    public UnityEvent damageableDeath;
    public UnityEvent<int, int> healthChanged;
    public UnityEvent<int, int> baseHealthChanged;
    public UnityEvent<int, int> armorChanged;
    public UnityEvent<Collider2D> damageableDestroyed;

    public GameOverScreen gameOver;

    Animator animator;

    [SerializeField]
    private int _maxHealth = 100;

    [SerializeField]
    private int _maxArmor = 100;

    public int goldReward = 10; // Düþmanýn öldüðünde verdiði altýn miktarý
    public int MaxHealth
    {
        get
        {
            return _maxHealth;
        }
        set
        {
            _maxHealth = value;
        }
    }

    public int MaxArmor
    {
        get
        {
            return _maxArmor;
        }
        set
        {
            _maxArmor = value;
        }
    }

    [SerializeField]
    private int _health = 100;
    public int Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;
            healthChanged?.Invoke(_health, MaxHealth);
            if (_health <= 0)
            {
                IsAlive = false;
            }
        }
    }

    [SerializeField]
    private int _armor = 0;
    public int Armor
    {
        get
        {
            return _armor;
        }
        set
        {
            _armor = value;
            armorChanged?.Invoke(_armor, MaxArmor);
        }
    }

    private bool _isAlive = true;

    public bool IsAlive
    {
        get
        {
            return _isAlive;
        }
        set
        {
            _isAlive = value;
            if (animator != null)
            {
                animator.SetBool(AnimationStrings.isAlive, value);
            }
            Debug.Log("IsAlive set " + value);
            if (!value)
            {
                damageableDeath.Invoke();
                if (gameObject.CompareTag("Enemy"))
                {
                    GiveGoldToPlayer(goldReward);
                }
            }
        }
    }
    public bool isInvincible = false;


    private float timeSinceHit = 0;
    public float invinciblityTime = 1f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isInvincible)
        {
            if (timeSinceHit > invinciblityTime)
            {
                isInvincible = false;
                timeSinceHit = 0;
            }

            timeSinceHit += Time.deltaTime;

        }
        if (!IsAlive)
        {
            if (gameObject.CompareTag("Base"))
            {
                gameOver.Setup();
            }

        }
    }

    public bool Hit(int damage, Vector2 knockback)
    {
        if (IsAlive && !isInvincible)
        {
            if (Armor <= 0)
            {
                Health -= damage;
                isInvincible = true;
                if (animator != null)
                {
                    animator.SetTrigger(AnimationStrings.hitTrigger);
                }
                damageableHit?.Invoke(damage, knockback);

                CharacterEvents.characterDamaged.Invoke(gameObject, damage);

                return true;

            }
            else
            {
                if (Armor >= damage)
                {
                    Armor -= damage;
                    isInvincible = true;
                    if (animator != null)
                    {
                        animator.SetTrigger(AnimationStrings.hitTrigger);
                    }
                    damageableHit?.Invoke(damage, knockback);

                    CharacterEvents.characterDamaged.Invoke(gameObject, damage);

                    return true;
                }
                else
                {
                    damage -= Armor;
                    Armor = 0;

                    Health -= damage;
                    isInvincible = true;
                    if (animator != null)
                    {
                        animator.SetTrigger(AnimationStrings.hitTrigger);
                    }
                    damageableHit?.Invoke(damage, knockback);

                    CharacterEvents.characterDamaged.Invoke(gameObject, damage);

                    return true;
                }
            }
        }
        else
        {
            return false;
        }
    }

    public bool Heal(int healthRestore)
    {
        if (IsAlive && Health != MaxHealth)
        {
            int maxHeal = Mathf.Max((MaxHealth - Health), 0);
            int actualHeal = Mathf.Min(maxHeal, healthRestore);
            Health += healthRestore;

            CharacterEvents.characterHealed(gameObject, actualHeal);
            return true;
        }
        else
        {
            return false;
        }
    }
    private void GiveGoldToPlayer(int gold)
    {
        PlayerData playerData = FindObjectOfType<PlayerData>();
        if (playerData != null)
        {
            playerData.AddGold(gold);
        }
    }
    public void UpgradeArmor(int amount)
    {
        Armor += amount;
    }

    public void OnDeath()
    {
        damageableDestroyed?.Invoke(GetComponent<Collider2D>()); // Notify listeners before destruction


        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        // Ensure any necessary cleanup happens here if needed
        damageableDestroyed?.Invoke(GetComponent<Collider2D>());
    }
}
