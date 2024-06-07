using System;
using System.Collections;
using TMPro;
using UnityEngine;

/// <summary>
/// Třída Axe řídí chování sekery v ruce hráče, včetně útoků a přepínání zbraní.
/// </summary>
public class Axe : MonoBehaviour
{
    /// <summary>
    /// Flag určující, zda je možné provádět útok.
    /// </summary>
    private bool canAttack = true;

    /// <summary>
    /// Flag určující, zda je možné provádět přepínání zbraní.
    /// </summary>
    private bool canSwitch = true;

    /// <summary>
    /// Reference na objekt zajišťující přepínání zbraní.
    /// </summary>
    private GameObject switcher;

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
        if (canAttack)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 4f) && Input.GetMouseButtonDown(0) && this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                StartCoroutine(AttackCoroutine(hit));
            }
        }
        if (Input.GetMouseButtonDown(0) && canSwitch)
        {
            switcher.GetComponent<WeaponSwitcher>().enabled = false;
            StartCoroutine(WaitS());
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
        canAttack = false;

        yield return new WaitForSeconds(1.3f);

        try
        {
            if (hit.collider.GetComponent<TreeScript>() != null)
            {
                hit.collider.GetComponent<TreeScript>().takeDamage(10 * int.Parse(helpDMG.GetComponent<TMP_Text>().text));
                Debug.Log("Hit Tree" + 10 * int.Parse(helpDMG.GetComponent<TMP_Text>().text));
            }

            if (hit.collider.GetComponent<EnemyHPAIR>() != null)
            {
                hit.collider.GetComponent<EnemyHPAIR>().takeDamage(3 * int.Parse(helpDMG.GetComponent<TMP_Text>().text));
                Debug.Log("Hit Enemy");
            }

            if (hit.collider.GetComponent<EnemyHP>() != null)
            {
                hit.collider.GetComponent<EnemyHP>().takeDamage(4);
                Debug.Log("Hit Enemy");
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
        }
        catch (Exception e)
        {
            // Obsluha výjimky (pokud je potřeba)
        }

        canAttack = true;
    }
}
