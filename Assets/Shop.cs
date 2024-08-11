using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public KnightSpawner archerSpawner;
    public PlayerData data;
    public TextMeshProUGUI[] texts;
    public TextMeshProUGUI wallPriceText;
    public TextMeshProUGUI basePriceText;
    public Damageable baseDamageable;
    public Damageable playerDamageable;
    public GameObject player;

    enum Wall
    {
        zeroWallDestroyed,
        oneWallDestroyed,
        twoWallDestroyed
    }

    enum Base
    {
        unDamaged,
        slightlyDamaged,
        moderatelyDamaged,
        heavilyDamaged
    }

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindWithTag("Player");
        data = player.GetComponent<PlayerData>();
        playerDamageable = data.GetComponent<Damageable>();
        archerSpawner = GetComponent<KnightSpawner>();
        texts = gameObject.transform.GetComponentsInChildren<TextMeshProUGUI>();
        baseDamageable = GameManager.Instance.Bases[0].GetComponent<Damageable>();
        for (int i = 0; i < texts.Length; i++)
        {
            if (texts[i].name == "Wall Price Text")
            {
                wallPriceText = texts[i];
            }
            else if (texts[i].name == "Base Price Text")
            {
                basePriceText = texts[i];
            }
        }
        data.AddGold(1000);
    }

    private void OnEnable()
    {
        Wall wallStatus;
        Base baseStatus;
        if (GameManager.Instance.Bases[1].activeSelf && GameManager.Instance.Bases[2].activeSelf)
        {
            wallStatus = Wall.zeroWallDestroyed;
        }
        else if (!GameManager.Instance.Bases[1].activeSelf && !GameManager.Instance.Bases[2].activeSelf)
        {
            wallStatus = Wall.twoWallDestroyed;
        }
        else
        {
            wallStatus = Wall.oneWallDestroyed;
        }

        switch (wallStatus)
        {
            case Wall.oneWallDestroyed:
                wallPriceText.text = "100";
                break;
            case Wall.twoWallDestroyed:
                wallPriceText.text = "200";
                break;
            case Wall.zeroWallDestroyed:
                wallPriceText.text = "0";
                break;
            default:
                wallPriceText.text = "0";
                break;
        }

        if (baseDamageable.Health < 100)
        {
            baseStatus = Base.heavilyDamaged;
        }
        else if (baseDamageable.Health < 500)
        {
            baseStatus = Base.moderatelyDamaged;
        }
        else if (baseDamageable.Health < 900)
        {
            baseStatus = Base.slightlyDamaged;
        }
        else
        {
            baseStatus = Base.unDamaged;
        }

        switch (baseStatus)
        {
            case Base.heavilyDamaged:
                basePriceText.text = "250";
                break;
            case Base.moderatelyDamaged:
                basePriceText.text = "100";
                break;
            case Base.slightlyDamaged:
                basePriceText.text = "25";
                break;
            case Base.unDamaged:
                basePriceText.text = "0";
                break;
            default:
                basePriceText.text = "0";
                break;
        }
    }

    public void RenewWalls()
    {
        int price = int.Parse(wallPriceText.text);

        if (data.gold >= price)
        {
            foreach (GameObject bases in GameManager.Instance.Bases)
            {
                if (!bases.activeSelf)
                {
                    bases.SetActive(true);
                    bases.GetComponent<Damageable>().IsAlive = true;
                    if (bases.name == "Base")
                    {
                        bases.GetComponent<Damageable>().Heal(1000);
                        Debug.Log("base RenewWalls");

                    }
                    else
                    {
                        Debug.Log("walls RenewWalls");
                        bases.GetComponent<Damageable>().Heal(500);

                    }
                    SpriteRenderer renderer = bases.GetComponent<SpriteRenderer>();
                    Color alpha = renderer.color;
                    alpha.a = 1;
                    renderer.color = alpha;
                }

            }

            data.AddGold(-price);
            wallPriceText.text = "0";

        }
        else
        {
            Debug.Log("not enough gold");
        }
    }

    public void RepairBase()
    {
        int price = int.Parse(basePriceText.text);
        if (data.gold >= price)
        {
            if (baseDamageable.Health != 1000)
            {
                baseDamageable.Heal(baseDamageable.MaxHealth);
                data.AddGold(-price);

            }
        }
        else
        {
            Debug.Log("not enough gold");
        }
    }

    public void UpgradeArmor(int amount)
    {
        int upgradeCost = amount * 1;

        if (playerDamageable.Armor < 100)
        {
            if (data.gold >= upgradeCost)
            {
                data.AddGold(-upgradeCost);

                if (playerDamageable != null)
                {
                    playerDamageable.UpgradeArmor(amount);
                }
            }
        }
        else
        {
            Debug.Log("Upgrade armor iþlemi baþarýsýz");
        }

        //else
        //{
        //    Debug.Log("Not enough gold to upgrade armor.");
        //}
    }
    public void UpgradeChildAttackDamage(int amount)
    {
        int upgradeCost = amount * 1; // Örnek maliyet hesaplama: Her saldýrý deðeri için 1 altýn


        if (data.gold >= upgradeCost && data.weaponUpgrade != 5)
        {
            data.AddGold(-upgradeCost);
            data.weaponUpgrade++;

            foreach (Transform child in data.transform)
            {
                Attack attackScript = child.GetComponent<Attack>();
                if (attackScript != null)
                {
                    attackScript.UpgradeAttackDamage(amount);
                }
            }
        }

        //else
        //{
        //    Debug.Log("Not enough gold to upgrade.");
        //}
    }

    // Update is called once per frame
    void Update()
    {

    }
}
