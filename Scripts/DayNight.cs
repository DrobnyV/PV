using System.Collections;
using TMPro;
using UnityEngine;

/// <summary>
/// Třída řídící cyklus dne a noci v herním světě.
/// </summary>
public class DayNight : MonoBehaviour
{
    /// <summary>
    /// Textové pole pro zobrazení aktuálního času.
    /// </summary>
    public TMP_Text textComponent;

    /// <summary>
    /// Délka dne v sekundách.
    /// </summary>
    public float dayLength = 120.0f;

    /// <summary>
    /// Délka noci v sekundách.
    /// </summary>
    public float nightLength = 60.0f;

    /// <summary>
    /// Aktuální fáze cyklu.
    /// </summary>
    private int wave;

    /// <summary>
    /// Reference na transformaci slunce.
    /// </summary>
    private Transform sunTransform;

    /// <summary>
    /// Aktuální čas.
    /// </summary>
    private float currentTime = 0.0f;

    /// <summary>
    /// Inicializace třídy při startu hry.
    /// </summary>
    void Start()
    {
        // Vyhledání transformace slunce
        sunTransform = GameObject.Find("SunLight").transform;
    }

    /// <summary>
    /// Aktualizace třídy každý snímek.
    /// </summary>
    void Update()
    {
        // Aktualizace času
        currentTime += Time.deltaTime;

        // Výpočet aktuální fáze cyklu (den/noc)
        float cycleTime = dayLength + nightLength;
        float normalizedTime = currentTime % cycleTime / cycleTime;

        // Nastavení úhlu rotace slunce
        sunTransform.rotation = Quaternion.Euler(normalizedTime * 360.0f, 0.0f, 0.0f);

        // Nastavení intenzity světla
        Light sunLight = GameObject.Find("SunLight").GetComponent<Light>();
        sunLight.intensity = Mathf.Lerp(1.0f, 0.2f, normalizedTime);

        // Získání čísla ze textového pole
        if (float.TryParse(textComponent.text, out float number))
        {
            // Kontrola intenzity slunce
            if (sunLight.intensity < 0.21f)
            {
                number += 1; // Přidání 1 k číslu
                StartCoroutine(UpdateTextAfterDelay(number, 5f)); // Spuštění aktualizace textu s časovým zpožděním
            }
        }
    }

    /// <summary>
    /// Coroutine pro aktualizaci textu s časovým zpožděním.
    /// </summary>
    /// <param name="newNumber">Nové číslo pro zobrazení v textu.</param>
    /// <param name="delay">Zpoždění před aktualizací textu.</param>
    private IEnumerator UpdateTextAfterDelay(float newNumber, float delay)
    {
        yield return new WaitForSeconds(delay);

        // Aktualizace textu v textovém poli
        textComponent.text = newNumber.ToString();
    }
}
