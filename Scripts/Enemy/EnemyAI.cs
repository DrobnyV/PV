using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using TMPro;

/// <summary>
/// Třída EnemyAI zajišťuje chování nepřítele včetně navigace a interakce s hráčem.
/// </summary>
public class EnemyAI : MonoBehaviour
{
    /// <summary>
    /// Reference na hráče.
    /// </summary>
    public Transform player;

    /// <summary>
    /// Reference na NavMeshAgent komponentu pro pohyb nepřítele.
    /// </summary>
    private NavMeshAgent agent;

    /// <summary>
    /// Textová komponenta zobrazení čísla noci.
    /// </summary>
    public TMP_Text textComponent;

    /// <summary>
    /// Flag pro kontrolu, zda je nepřítel zastaven.
    /// </summary>
    private bool isStopped = false;

    /// <summary>
    /// Flag pro kontrolu, zda může nepřítel způsobit poškození.
    /// </summary>
    private bool canDamage = true;

    /// <summary>
    /// Inicializační metoda. Získá reference na hráče, textovou komponentu a NavMeshAgent.
    /// </summary>
    void Start()
    {
        textComponent = GameObject.Find("CisloNoci").GetComponent<TMP_Text>();
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            Debug.LogError("NavMeshAgent komponenta chybí na nepříteli.");
        }
    }

    /// <summary>
    /// Metoda aktualizace, která se volá jednou za frame. Aktualizuje cíl agenta, pokud je hráč definován a nepřítel není zastaven.
    /// </summary>
    void Update()
    {
        if (player != null && !isStopped)
        {
            agent.SetDestination(player.position);
        }
    }

    /// <summary>
    /// Metoda detekce kolize. Pokud nepřítel narazí na hráče a může způsobit poškození, způsobí hráči poškození a zastaví se na 1 sekundu.
    /// </summary>
    /// <param name="other">Kolizní objekt.</param>
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && canDamage)
        {
            player.GetComponent<PlayerController>().takeDamage(10 + int.Parse(textComponent.text)*5 - player.GetComponent<PlayerController>().defense);
            StartCoroutine(StopForSeconds(1f));
        }
    }

    /// <summary>
    /// Coroutine, která zastaví nepřítele na určený počet sekund.
    /// </summary>
    /// <param name="seconds">Počet sekund, po které bude nepřítel zastaven.</param>
    /// <returns>Vrací enumerátor.</returns>
    private IEnumerator StopForSeconds(float seconds)
    {
        isStopped = true;
        canDamage = false;
        agent.isStopped = true;
        yield return new WaitForSeconds(seconds);
        agent.isStopped = false;
        isStopped = false;
        canDamage = true;
    }
}
