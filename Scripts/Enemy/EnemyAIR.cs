using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Třída EnemyAIR zajišťuje chování nepřítele včetně útoků projektily na hráče.
/// </summary>
public class EnemyAIR : MonoBehaviour
{
    /// <summary>
    /// Reference na objekt hráče.
    /// </summary>
    public GameObject player;

    /// <summary>
    /// Prefab projektilu, který nepřítel vystřeluje.
    /// </summary>
    public GameObject projectilePrefab;

    /// <summary>
    /// Pozice, odkud je projektil vystřelen.
    /// </summary>
    public Transform firePoint;

    /// <summary>
    /// Rychlost projektilu.
    /// </summary>
    public float projectileSpeed = 10f;

    /// <summary>
    /// Vzdálenost, na kterou nepřítel začne útočit.
    /// </summary>
    public float attackDistance = 10f;

    /// <summary>
    /// Časový interval mezi útoky.
    /// </summary>
    public float attackCooldown = 2f;

    /// <summary>
    /// Flag pro kontrolu, zda může nepřítel útočit.
    /// </summary>
    private bool canAttack = true;

    /// <summary>
    /// Inicializační metoda. Získá reference na hráče.
    /// </summary>
    private void Start()
    {
        player = GameObject.Find("Player");
    }

    /// <summary>
    /// Metoda aktualizace, která se volá jednou za frame. Kontroluje vzdálenost k hráči a zahajuje útok, pokud je hráč v dosahu.
    /// </summary>
    void Update()
    {
        // Vypočítá vzdálenost k hráči
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        // Pokud je hráč v dosahu útoku a nepřítel může útočit, zahájí útok
        if (distanceToPlayer <= attackDistance && canAttack)
        {
            // Otočí se směrem k hráči
            transform.LookAt(player.transform);

            // Zahájí útok
            StartCoroutine(Attack());
        }
    }

    /// <summary>
    /// Coroutine, která zajišťuje útok projektily na hráče.
    /// </summary>
    /// <returns>Vrací enumerátor.</returns>
    IEnumerator Attack()
    {
        canAttack = false;

        // Čeká krátký čas před útokem (volitelné)
        yield return new WaitForSeconds(0.5f);

        // Vytvoří projektil na pozici firePoint
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        // Vypočítá směr k hráči
        Vector3 directionToPlayer = (player.transform.position - firePoint.position).normalized;

        // Získá komponentu Rigidbody projektilu
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Nastaví rychlost projektilu směrem k hráči
            rb.velocity = directionToPlayer * projectileSpeed;
        }

        // Čeká po dobu attackCooldown, než dovolí další útok
        yield return new WaitForSeconds(attackCooldown);

        canAttack = true;
    }
}
