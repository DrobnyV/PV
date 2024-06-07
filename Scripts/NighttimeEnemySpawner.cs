using System.Collections;
using UnityEngine;

/// <summary>
/// Třída pro spawnování nepřátel během noci v herním světě.
/// </summary>
public class NighttimeEnemySpawner : MonoBehaviour
{
    /// <summary>
    /// Prefab nepřítele.
    /// </summary>
    public GameObject enemyPrefab;

    /// <summary>
    /// Body spawnutí nepřátel.
    /// </summary>
    public Transform[] spawnPoints;

    /// <summary>
    /// Čas mezi spawnováním nepřátel v sekundách.
    /// </summary>
    public float spawnTime = 40.0f;

    /// <summary>
    /// Reference na skript cyklu dne a noci.
    /// </summary>
    public DayNight dayNightCycle;

    /// <summary>
    /// Inicializace třídy při startu hry.
    /// </summary>
    void Start()
    {
        StartCoroutine(SpawnEnemyCoroutine());
    }

    /// <summary>
    /// Coroutine pro spawnování nepřátel.
    /// </summary>
    IEnumerator SpawnEnemyCoroutine()
    {
        while (true)
        {
            Light sunLight = GameObject.Find("SunLight").GetComponent<Light>();

            // Kontrola, zda je noc
            if (sunLight.intensity < 0.58)
            {
                // Počkat zadaný čas

                // Vytvoření nepřátel na všech spawnovacích bodech
                foreach (Transform spawnPoint in spawnPoints)
                {
                    Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
                }

                yield return new WaitForSeconds(spawnTime);

            }
            else
            {
                // Pokud není noc, počkat kratší dobu
                yield return new WaitForSeconds(1.0f);
            }
        }
    }
}
