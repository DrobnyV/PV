using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopBuyScipt : MonoBehaviour
{
    public GameObject player; // Odkaz na herní objekt hráče
    public TMP_Text golds; // Odkaz na textovou komponentu zobrazující počet zlatých mincí
    private int gold; // Aktuální počet zlatých mincí hráče
    public GameObject US; // Odkaz na herní objekt obchodu
    public WandScript w1; // Odkaz na skript pro první hůl
    public WandScript w2; // Odkaz na skript pro druhou hůl

    /// <summary>
    /// Metoda pro zahájení.
    /// </summary>
    void Start()
    {
        
    }

    /// <summary>
    /// Zvyšuje maximální zdraví hráče.
    /// </summary>
    public void MaxHealth()
    {
        if (int.Parse(golds.text) >= 30)
        {
            player.GetComponent<PlayerController>().maxHealth += 5;
            golds.text = (int.Parse(golds.text) - 30).ToString();
        }
    }

    /// <summary>
    /// Zvyšuje maximální vytrvalost hráče.
    /// </summary>
    public void MaxStamina()
    {
        if (int.Parse(golds.text) >= 30)
        {
            player.GetComponent<PlayerController>().maxStamina += 5;
            golds.text = (int.Parse(golds.text) - 30).ToString();
        }
    }

    /// <summary>
    /// Zvyšuje rychlost regenerace zdraví hráče.
    /// </summary>
    public void HealthRegen()
    {
        if (int.Parse(golds.text) >= 30)
        {
            player.GetComponent<PlayerController>().regenRate += 1;
            golds.text = (int.Parse(golds.text) - 30).ToString();
        }
    }

    /// <summary>
    /// Zvyšuje základní a rychlou rychlost pohybu hráče.
    /// </summary>
    public void Speed()
    {
        if (int.Parse(golds.text) >= 30)
        {
            player.GetComponent<PlayerController>().basicMoveSpeed += 1;
            player.GetComponent<PlayerController>().fastMoveSpeed += 1;
            golds.text = (int.Parse(golds.text) - 30).ToString();
        }
    }

    /// <summary>
    /// Zvyšuje lifesteal hráče.
    /// </summary>
    public void LifeSteal()
    {
        if (int.Parse(golds.text) >= 50)
        {
            player.GetComponent<PlayerController>().lifeSteal += 1;
            golds.text = (int.Parse(golds.text) - 50).ToString();
        }
    }

    /// <summary>
    /// Zvyšuje zdraví získané po zabití nepřítele.
    /// </summary>
    public void HPOnKill()
    {
        if (int.Parse(golds.text) >= 50)
        {
            player.GetComponent<PlayerController>().HPOnKill += 1;
            golds.text = (int.Parse(golds.text) - 50).ToString();
        }
    }

    /// <summary>
    /// Zvyšuje obranu hráče.
    /// </summary>
    public void Defense()
    {
        if (int.Parse(golds.text) >= 50)
        {
            player.GetComponent<PlayerController>().defense += 2;
            golds.text = (int.Parse(golds.text) - 50).ToString();
        }
    }

    /// <summary>
    /// Zvyšuje množství surovin získaných hráčem.
    /// </summary>
    public void Resourse()
    {
        if (int.Parse(golds.text) >= 100)
        {
            player.GetComponent<PlayerController>().resourseAmount += 1f;
            golds.text = (int.Parse(golds.text) - 100).ToString();
        }
    }

    /// <summary>
    /// Zvyšuje bonus při nízkém zdraví hráče.
    /// </summary>
    public void LowHPBonus()
    {
        if (int.Parse(golds.text) >= 100)
        {
            player.GetComponent<PlayerController>().lowHPBonus += 1f;
            golds.text = (int.Parse(golds.text) - 100).ToString();
        }
    }

    /// <summary>
    /// Aktivuje obchod.
    /// </summary>
    public void UpgradeShop()
    {
        US.SetActive(true);
        w1.enabled = false;
        w2.enabled = false;
    }

    /// <summary>
    /// Aktualizace se volá jednou za snímek.
    /// </summary>
    void Update()
    {
        
    }
}
