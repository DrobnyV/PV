using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Spravuje chování diamantových objektů ve hře.
/// </summary>
public class DiamondScript : MonoBehaviour
{
    private float health = 100f; // Zdraví diamantového objektu
    public TMP_Text diamonds; // Odkaz na komponentu UI Text zobrazující počet diamantů
    private float diamond; // Aktuální počet diamantů
    public GameObject Player; // Odkaz na herní objekt hráče

    /// <summary>
    /// Přiděluje poškození diamantovému objektu.
    /// </summary>
    /// <param name="damage">Množství poškození k způsobení.</param>
    public void takeDamage(float damage)
    {
        health -= damage;
    }

    /// <summary>
    /// Zničí diamantový objekt, pokud jeho zdraví dosáhne nuly nebo méně, a aktualizuje počet diamantů.
    /// </summary>
    public void die()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
            diamond = int.Parse(diamonds.text) + 10 * Player.GetComponent<PlayerController>().resourseAmount;
            diamonds.text = diamond.ToString();
        }
    }

    private void Start()
    {
        // Najde a přiřadí komponentu UI Text zobrazující počet diamantů
        diamonds = GameObject.Find("DC").GetComponent<TMP_Text>();
        
        // Přiřadí odkaz na herní objekt hráče
        Player = GameObject.Find("Player");

        // Převede počáteční počet diamantů z komponenty UI Text
        diamond = int.Parse(diamonds.text);
    }

    private void Update()
    {
        // Zkontroluje, zda by měl být diamant zničen
        die();
    }
}
