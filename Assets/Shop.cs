using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public KnightSpawner archerSpawner;
    public GameObject archersParent;
    public PlayerData playerData;
    public TextMeshProUGUI wallPriceText;
    public TextMeshProUGUI basePriceText;
    public TextMeshProUGUI archerBuyPriceText;
    public TextMeshProUGUI archerUpgradePriceText;
    public TextMeshProUGUI weaponpgradePriceText;
    public Damageable baseDamageable;
    public Damageable playerDamageable;
    public GameObject player;
    public GameManager gameManager;
    public Transform archerBow;
    public Transform playerBow;

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
        gameManager = GameManager.Instance;
        player = GameObject.FindWithTag("Player");
        playerData = player.GetComponent<PlayerData>();
        playerDamageable = playerData.GetComponent<Damageable>();
        baseDamageable = gameManager.Bases[0].GetComponent<Damageable>();
    }

    private void OnEnable()
    {
        Wall wallStatus;
        Base baseStatus;
        if (gameManager.Bases[1].activeSelf && gameManager.Bases[2].activeSelf)
        {
            wallStatus = Wall.zeroWallDestroyed;
        }
        else if (!gameManager.Bases[1].activeSelf && !gameManager.Bases[2].activeSelf)
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
        else if (baseDamageable.Health < 800)
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
                basePriceText.text = "500";
                break;
            case Base.moderatelyDamaged:
                basePriceText.text = "250";
                break;
            case Base.slightlyDamaged:
                basePriceText.text = "100";
                break;
            case Base.unDamaged:
                basePriceText.text = "0";
                break;
            default:
                basePriceText.text = "0";
                break;
        }

        RenewPrice();
    }

    public void RenewWalls()
    {
        int price = int.Parse(wallPriceText.text);

        if (playerData.gold >= price)
        {
            foreach (GameObject bases in gameManager.Bases)
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

            playerData.AddGold(-price);
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
        if (playerData.gold >= price)
        {
            if (baseDamageable.Health != 1000)
            {
                baseDamageable.Heal(baseDamageable.MaxHealth);
                playerData.AddGold(-price);

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
            if (playerData.gold >= upgradeCost)
            {
                playerData.AddGold(-upgradeCost);

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
        int upgradeCost = amount * 5; // Örnek maliyet hesaplama: Her saldýrý deðeri için 1 altýn


        if (playerData.gold >= upgradeCost && playerData.weaponUpgrade < 5)
        {
            playerData.AddGold(-upgradeCost);
            playerData.weaponUpgrade++;

            foreach (Transform child in playerData.transform)
            {
                Attack attackScript = child.GetComponent<Attack>();
                if (attackScript != null)
                {
                    attackScript.UpgradeAttackDamage(amount);
                }
            }
            RenewPrice();
        }

        //else
        //{
        //    Debug.Log("Not enough gold to upgrade.");
        //}
    }
    public void UpgradeArcher(int amount)
    {
        int upgradeCost = amount * 5;


        if (playerData.gold >= upgradeCost && gameManager.archerUpgrade < 5)
        {
            playerData.AddGold(-upgradeCost);
            gameManager.archerUpgrade++;

            foreach (Transform child in archersParent.transform)
            {
                Damageable archerDamageable = child.GetComponent<Damageable>();
                if (archerDamageable != null)
                {
                    archerDamageable.Health += gameManager.archerUpgrade * 10;
                    Debug.Log("arcer ýn caný arttý");
                }
            }
            RenewPrice();
        }

        //else
        //{
        //    Debug.Log("Not enough gold to upgrade.");
        //}
    }

    public void BuyArcher()
    {
        int archerPrice = 50 + gameManager.archerCount * 5;
        if (playerData.gold >= archerPrice && gameManager.archerCount < 10)
        {
            archerSpawner.SpawnSingleEnemy();
            playerData.AddGold(-archerPrice);

            gameManager.archerCount++;
            RenewPrice();
        }
    }

    public void RenewPrice()
    {
        archerBuyPriceText.text = (50 + gameManager.archerCount * 5).ToString();
        archerUpgradePriceText.text = (50 * (gameManager.archerUpgrade + 1)).ToString();
        weaponpgradePriceText.text = (50 + playerData.weaponUpgrade * 50).ToString();
    }
}
