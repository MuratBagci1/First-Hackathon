using System.Collections;
using System.Collections.Generic;
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
        data = GameObject.FindWithTag("Player").GetComponent<PlayerData>();
        archerSpawner = GetComponent<KnightSpawner>();
        texts = gameObject.transform.GetComponentsInChildren<TextMeshProUGUI>();
        baseDamageable = GameManager.Instance.Bases[0].GetComponent<Damageable>();
        for (int i = 0; i < texts.Length; i++)
        {
            if(texts[i].name == "Wall Price Text")
            {
                wallPriceText = texts[i];
            }
            else if(texts[i].name == "Base Price Text")
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
        if(GameManager.Instance.Bases[1].activeSelf && GameManager.Instance.Bases[1].activeSelf)
        {
            wallStatus = Wall.zeroWallDestroyed;
        }
        else if(!GameManager.Instance.Bases[1].activeSelf && !GameManager.Instance.Bases[1].activeSelf)
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

        if(baseDamageable.Health < 100)
        {
            baseStatus = Base.heavilyDamaged;
        }
        else if(baseDamageable.Health < 500)
        {
            baseStatus = Base.moderatelyDamaged;
        }
        else if(baseDamageable.Health < 900)
        {
            baseStatus = Base.slightlyDamaged;
        }
        else
        {
            baseStatus = Base.unDamaged;
        }

        switch(baseStatus)
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
        if(data.gold > price)
        {
            foreach (GameObject bases in GameManager.Instance.Bases)
            {
                if(!bases.activeSelf)
                {
                    bases.SetActive(true);
                    bases.GetComponent<Damageable>().IsAlive = true;
                    bases.GetComponent<Damageable>().Heal(1000);
                    SpriteRenderer renderer = bases.GetComponent<SpriteRenderer>();
                    Color alpha = renderer.color;
                    alpha.a = 1;
                    renderer.color = alpha;
                }                
            }
            data.AddGold(-price);
        }
        else
        {
            Debug.Log("not enough gold");
        }
    }

    public void RepairBase()
    {
        int price = int.Parse(basePriceText.text);
        if(data.gold > price)
        {
            baseDamageable.Heal(baseDamageable.MaxHealth);
            data.AddGold(-price);
        }
        else
        {
            Debug.Log("not enough gold");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
