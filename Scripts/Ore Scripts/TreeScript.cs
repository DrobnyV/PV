using TMPro;
using UnityEngine;

/// <summary>
/// Spravuje chování stromových objektů ve hře.
/// </summary>
public class TreeScript : MonoBehaviour
{
    private float health = 50f; // Zdraví stromového objektu
    private TMP_Text woods; // Odkaz na komponentu UI Text zobrazující počet dřeva
    private GameObject player; // Odkaz na herní objekt hráče

    /// <summary>
    /// Inicializuje skript nalezením a přiřazením potřebných odkazů.
    /// </summary>
    private void Start()
    {
        // Najde a přiřadí komponentu UI Text zobrazující počet dřeva
        woods = GameObject.Find("WC").GetComponent<TMP_Text>();

        // Najde a přiřadí odkaz na herní objekt hráče
        player = GameObject.Find("Player");
    }

    /// <summary>
    /// Aplikuje poškození stromovému objektu.
    /// </summary>
    /// <param name="damage">Množství poškození k aplikaci.</param>
    public void TakeDamage(float damage)
    {
        health -= damage;
        Die();
    }

    /// <summary>
    /// Zničí stromový objekt, pokud jeho zdraví dosáhne nuly nebo méně a aktualizuje počet dřeva.
    /// </summary>
    private void Die()
    {
        if (health <= 0)
        {
            // Zničí stromový objekt
            Destroy(gameObject);

            // Spočítá nový počet dřeva na základě množství surovin hráče
            int woodCount = int.Parse(woods.text) + 10 * player.GetComponent<PlayerController>().resourseAmount;

            // Aktualizuje komponentu UI Text zobrazující počet dřeva
            woods.text = woodCount.ToString();
        }
    }

    /// <summary>
    /// Zkontroluje, zda by měl být stromový objekt zničen v každém aktualizování snímku.
    /// </summary>
    private void Update()
    {
        Die();
    }
}
