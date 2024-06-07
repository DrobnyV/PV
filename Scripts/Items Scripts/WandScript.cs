using System;
using System.Collections;
using TMPro;
using UnityEngine;

/// <summary>
/// Třída WandScript řídí chování kouzelného proutku hráče, včetně vystřelování projektilů a přepínání zbraní.
/// </summary>
public class WandScript : MonoBehaviour
{
    /// <summary>
    /// Prefab projektilu.
    /// </summary>
    public GameObject projectilePrefab;

    /// <summary>
    /// Pozice, ze které se vystřeluje projektil.
    /// </summary>
    public Transform firePoint;

    /// <summary>
    /// Doba cooldownu mezi střelbami.
    /// </summary>
    public float cooldown = 1f;

    /// <summary>
    /// Příznak určující, zda je možné střílet.
    /// </summary>
    private bool canShoot = true;

    /// <summary>
    /// Reference na objekt zajišťující přepínání zbraní.
    /// </summary>
    private GameObject switcher;

    /// <summary>
    /// Příznak určující, zda je možné provádět přepínání zbraní.
    /// </summary>
    private bool canSwitch = true;

    /// <summary>
    /// Reference na textovou komponentu zobrazující pomocné poškození.
    /// </summary>
    public TMP_Text helpDMG;

    /// <summary>
    /// Inicializační metoda. Získá referenci na objekt zajišťující přepínání zbraní.
    /// </summary>
    private void Start()
    {
        switcher = GameObject.Find("MainWeapons");
    }

    /// <summary>
    /// Coroutine pro čekání před povolením přepínání zbraní.
    /// </summary>
    private IEnumerator WaitS()
    {
        canSwitch = false;
        yield return new WaitForSeconds(1.3f);
        canSwitch = true;
        switcher.GetComponent<WeaponSwitcher>().enabled = true;
    }

    /// <summary>
    /// Metoda pro aktualizaci, která se volá jednou za frame. Kontroluje možnost střílení a přepínání zbraní.
    /// </summary>
    void Update()
    {
        if (canShoot && Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
        if (Input.GetMouseButtonDown(0) && canSwitch)
        {
            switcher.GetComponent<WeaponSwitcher>().enabled = false;
            StartCoroutine(WaitS());
        }
    }

    /// <summary>
    /// Metoda pro vystřelení projektilu.
    /// </summary>
    void Shoot()
    {
        canShoot = false;
        StartCoroutine(ShootWithDelay());
    }

    /// <summary>
    /// Coroutine pro čekání mezi jednotlivými střelbami.
    /// </summary>
    private IEnumerator ShootWithDelay()
    {
        yield return new WaitForSeconds(cooldown);

        // Výpočet směru střelby na základě směru kamery
        Vector3 shootingDirection = Camera.main.transform.forward;

        // Vytvoření instance projektilu na pozici firePoint a s rotací kamery
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.LookRotation(shootingDirection));

        // Předání poškození projektilu
        if (int.TryParse(helpDMG.text, out int helpDamageMultiplier))
        {
            projectile.GetComponent<ProjectileScript>().damage = 10 * helpDamageMultiplier;
        }
        else
        {
            Debug.LogError("Failed to parse helpDMG text to int.");
        }

        // Získání Rigidbody komponenty projektilu
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Nastavení rychlosti projektilu ve směru kamery
            rb.velocity = shootingDirection * projectile.GetComponent<ProjectileScript>().speed;
        }
        else
        {
            Debug.LogError("Projectile does not have a Rigidbody component.");
        }

        canShoot = true;
    }
}
