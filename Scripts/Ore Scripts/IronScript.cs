using TMPro;
using UnityEngine;

/// <summary>
/// Spravuje chování železných objektů ve hře.
/// </summary>
public class IronScript : MonoBehaviour
{
    private float health = 80f; // Zdraví železného objektu
    private TMP_Text irons; // Odkaz na komponentu UI Text zobrazující počet železa
    private GameObject player; // Odkaz na herní objekt hráče

    /// <summary>
    /// Inicializuje skript nalezením a přiřazením potřebných odkazů.
    /// </summary>
    private void Start()
    {
        // Najde a přiřadí komponentu UI Text zobrazující počet železa
        irons = GameObject.Find("IC").GetComponent<TMP_Text>();

        // Najde a přiřadí odkaz na herní objekt hráče
        player = GameObject.Find("Player");
    }

    /// <summary>
    /// Aplikuje poškození na železný objekt a zkontroluje, zda by měl být zničen.
    /// </summary>
    /// <param name="damage">Množství poškození k aplikaci.</param>
    public void TakeDamage(float damage)
    {
        // Sníží zdraví železného objektu o dané množství poškození
        health -= damage;

        // Zkontroluje, zda by měl být železný objekt zničen
        Die();
    }

    /// <summary>
    /// Zničí železný objekt, pokud jeho zdraví dosáhne nuly nebo méně a aktualizuje počet železa.
    /// </summary>
    private void Die()
    {
        if (health <= 0)
        {
            // Zničí železný objekt
            Destroy(gameObject);

            // Spočítá nový počet železa na základě množství surovin hráče
            int ironCount = int.Parse(irons.text) + 10 * player.GetComponent<PlayerController>().resourseAmount;

            // Aktualizuje komponentu UI Text zobrazující počet železa
            irons.text = ironCount.ToString();
        }
    }

    /// <summary>
    /// Zkontroluje, zda by měl být železný objekt zničen v každém aktualizování snímku.
    /// </summary>
    private void Update()
    {
        Die();
    }
}
