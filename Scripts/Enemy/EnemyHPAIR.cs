using System.Numerics;
using TMPro;
using UnityEngine;

/// <summary>
/// Třída EnemyHPAIR zajišťuje správu zdraví nepřátelského letícího nepřítele a jeho interakce s hráčem.
/// </summary>
public class EnemyHPAIR : MonoBehaviour
{
    /// <summary>
    /// Aktuální zdraví letícího nepřítele.
    /// </summary>
    public float health;

    /// <summary>
    /// Aktuální počet zlaťáků hráče.
    /// </summary>
    private int gold;

    /// <summary>
    /// Textová komponenta zobrazující počet zlaťáků.
    /// </summary>
    public TMP_Text golds;

    /// <summary>
    /// Textová komponenta zobrazující číslo vlny.
    /// </summary>
    private TMP_Text wave;

    /// <summary>
    /// Reference na objekt hráče.
    /// </summary>
    private GameObject player;

    /// <summary>
    /// Inicializační metoda. Získá reference na hráče, textové komponenty a nastaví počáteční hodnoty zdraví a zlaťáků.
    /// </summary>
    void Start()
    {
        player = GameObject.Find("Player");
        wave = GameObject.Find("CisloNoci").GetComponent<TMP_Text>();
        golds = GameObject.Find("GC").GetComponent<TMP_Text>();
        health = 50 + int.Parse(wave.text) * 10;
        gold = int.Parse(golds.text);
    }

    /// <summary>
    /// Metoda, která je volána při smrti letícího nepřítele. Při smrti se zvýší počet zlaťáků hráče a zlepší se jeho atributy.
    /// </summary>
    public void die()
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
            gold += 5;
            golds.text = gold.ToString();
            player.GetComponent<PlayerController>().health += player.GetComponent<PlayerController>().lifeSteal;
            player.GetComponent<PlayerController>().maxHealth += player.GetComponent<PlayerController>().HPOnKill;
        }
    }

    /// <summary>
    /// Metoda pro přijímání poškození. Snižuje zdraví letícího nepřítele o danou hodnotu poškození.
    /// </summary>
    /// <param name="damage">Hodnota poškození, kterou letící nepřítel obdrží.</param>
    public void takeDamage(float damage)
    {
        health -= damage;
    }

    /// <summary>
    /// Metoda aktualizace, která se volá jednou za frame. Kontroluje stav zdraví letícího nepřítele a aktualizuje počet zlaťáků.
    /// </summary>
    void Update()
    {
        die();
        gold = int.Parse(golds.text);
    }
}
