using System.Collections;
using TMPro;
using UnityEngine;

/// <summary>
/// Třída řídící spawnování bosse v herním světě.
/// </summary>
public class BossSpawn : MonoBehaviour
{
    /// <summary>
    /// Prefab bosse.
    /// </summary>
    public GameObject enemyPrefab;

    /// <summary>
    /// Bod spawnutí bosse.
    /// </summary>
    public Transform spawnPoint;

    /// <summary>
    /// Textové pole pro zobrazení počtu nocí.
    /// </summary>
    private TMP_Text nightCountText;

    /// <summary>
    /// Čas mezi spawnováním bosse v sekundách.
    /// </summary>
    public float spawnTime = 10000.0f;

    /// <summary>
    /// Reference na skript řídící cyklus dne a noci.
    /// </summary>
    public DayNight dayNightCycle;

    /// <summary>
    /// Inicializace třídy při startu hry.
    /// </summary>
    void Start()
    {
        nightCountText = GameObject.Find("CisloNoci").GetComponent<TMP_Text>();
        StartCoroutine(SpawnEnemyCoroutine());
    }

    /// <summary>
    /// Coroutine pro spawnování bosse.
    /// </summary>
    IEnumerator SpawnEnemyCoroutine()
    {
        while (true)
        {
            Light sunLight = GameObject.Find("SunLight").GetComponent<Light>();

            // Kontrola, zda je noc
            if (int.Parse(nightCountText.text) == 10)
            {
                // Pokud je noc a intenzita slunečního světla je menší než 0.58
                if (sunLight.intensity < 0.58)
                {
                    // Počkej zadaný čas
                    yield return new WaitForSeconds(spawnTime);

                    // Vytvoření bosse
                    GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
                }
                else
                {
                    // Pokud není noc, počkej kratší dobu
                    yield return new WaitForSeconds(1.0f);
                }
            }
            else
            {
                // Pokud není noc, počkej kratší dobu
                yield return new WaitForSeconds(1.0f);
            }
        }
    }
}
