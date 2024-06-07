using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Spravuje chování kamenných objektů ve hře.
/// </summary>
public class RockScript : MonoBehaviour
{
    private float health = 50f; // Zdraví kamenného objektu
    public TMP_Text rocks; // Odkaz na komponentu UI Text zobrazující počet kamenů
    private float rock; // Množství kamenných surovin získaných zničením tohoto objektu
    public GameObject Player; // Odkaz na herní objekt hráče

    /// <summary>
    /// Aplikuje poškození kamennému objektu.
    /// </summary>
    /// <param name="damage">Množství poškození k aplikaci.</param>
    public void TakeDamage(float damage)
    {
        health -= damage;
        Die();
    }

    /// <summary>
    /// Zničí kamenný objekt, pokud jeho zdraví dosáhne nuly nebo méně a aktualizuje počet kamenů.
    /// </summary>
    private void Die()
    {
        if (health <= 0)
        {
            // Zničí kamenný objekt
            Destroy(gameObject);

            // Spočítá nový počet kamenů na základě množství surovin hráče
            rock = int.Parse(rocks.text) + 10 * Player.GetComponent<PlayerController>().resourseAmount;

            // Aktualizuje komponentu UI Text zobrazující počet kamenů
            rocks.text = rock.ToString();
        }
    }

    /// <summary>
    /// Zkontroluje, zda by měl být kamenný objekt zničen v každém aktualizování snímku.
    /// </summary>
    void Update()
    {
        Die();
    }

    /// <summary>
    /// Inicializuje skript nalezením a přiřazením potřebných odkazů.
    /// </summary>
    void Start()
    {
        // Najde a přiřadí komponentu UI Text zobrazující počet kamenů
        rocks = GameObject.Find("SC").GetComponent<TMP_Text>();

        // Najde a přiřadí odkaz na herní objekt hráče
        Player = GameObject.Find("Player");

        // Inicializuje počet kamenů
        rock = int.Parse(rocks.text);
    }
}
