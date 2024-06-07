using System;
using System.Collections;
using TMPro;
using UnityEngine;

/// <summary>
/// Třída MecDamager řídí chování meče hráče, včetně provádění útoků a přepínání zbraní.
/// </summary>
public class MecDamager : MonoBehaviour
{
    /// <summary>
    /// Flag určující, zda je možné provádět útok.
    /// </summary>
    private bool canAttack = true;

    /// <summary>
    /// Reference na objekt zajišťující přepínání zbraní.
    /// </summary>
    private GameObject switcher;

    /// <summary>
    /// Flag určující, zda je možné provádět přepínání zbraní.
    /// </summary>
    private bool canSwitch = true;

    /// <summary>
    /// Reference na textovou komponentu zobrazující pomocné poškození.
    /// </summary>
    public GameObject helpDMG;

    /// <summary>
    /// Inicializační metoda. Získá reference na přepínač zbraní.
    /// </summary>
    private void Start()
    {
        switcher = GameObject.Find("MainWeapons");
    }

    /// <summary>
    /// Metoda aktualizace, která se volá jednou za frame. Kontroluje možnost útoku a přepínání zbraní.
    /// </summary>
    void Update()
    {
        if (canAttack && Input.GetMouseButtonDown(0))
        {
            // Get the direction the camera is facing
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

            if (Physics.Raycast(ray, out RaycastHit hit, 2f))
            {
                StartCoroutine(AttackCoroutine(hit));
            }

            if (canSwitch)
            {
                switcher.GetComponent<WeaponSwitcher>().enabled = false;
                StartCoroutine(WaitS());
            }
        }
    }

    /// <summary>
    /// Coroutine pro čekání před povolením přepínání zbraní.
    /// </summary>
    private IEnumerator WaitS()
    {
        canSwitch = false;
        yield return new WaitForSeconds(2f);
        canSwitch = true;
        switcher.GetComponent<WeaponSwitcher>().enabled = true;
    }

    /// <summary>
    /// Coroutine pro provedení útoku s čekáním na další možný útok.
    /// </summary>
    private IEnumerator AttackCoroutine(RaycastHit hit)
    {
        canAttack = false; // Prevent further attacks until the coroutine completes

        yield return new WaitForSeconds(1.3f); // Wait for 1.3 seconds
        try
        {
            if (hit.collider.GetComponent<EnemyHP>() != null)
            {
                hit.collider.GetComponent<EnemyHP>().takeDamage(80 * int.Parse(helpDMG.GetComponent<TMP_Text>().text));
                Debug.Log("Hit Enemy" + 80 * int.Parse(helpDMG.GetComponent<TMP_Text>().text));
            }
            if (hit.collider.GetComponent<EnemyHPAIR>() != null)
            {
                hit.collider.GetComponent<EnemyHPAIR>().takeDamage(80 * int.Parse(helpDMG.GetComponent<TMP_Text>().text));
                Debug.Log("Hit Enemy" + 80 * int.Parse(helpDMG.GetComponent<TMP_Text>().text));
            }
            if (hit.collider.GetComponent<EnemyBossHP>() != null)
            {
                hit.collider.GetComponent<EnemyBossHP>().takeDamage(80 * int.Parse(helpDMG.GetComponent<TMP_Text>().text));
                Debug.Log("Hit Boss Enemy" + 80 * int.Parse(helpDMG.GetComponent<TMP_Text>().text));
            }
            if (hit.collider.GetComponent<RockScript>() != null)
            {
                hit.collider.GetComponent<RockScript>().takeDamage(3);
                Debug.Log("Hit Rock");
            }
            if (hit.collider.GetComponent<IronScript>() != null)
            {
                hit.collider.GetComponent<IronScript>().takeDamage(3);
                Debug.Log("Hit Iron");
            }
            if (hit.collider.GetComponent<DiamondScript>() != null)
            {
                hit.collider.GetComponent<DiamondScript>().takeDamage(3);
                Debug.Log("Hit Diamond");
            }
            if (hit.collider.GetComponent<TreeScript>() != null)
            {
                hit.collider.GetComponent<TreeScript>().takeDamage(3);
                Debug.Log("Hit Tree");
            }
        }
        catch (Exception e)
        {
            // Handle exception if necessary
        }

        canAttack = true; // Allow the next attack
    }
}
