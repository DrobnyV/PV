using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Třída řídící skrytí modelů a obnovení ovládání hráče.
/// </summary>
public class ClassButtonDisapear : MonoBehaviour
{
    /// <summary>
    /// Seznam modelů, které mají být skryty.
    /// </summary>
    private List<GameObject> models;

    /// <summary>
    /// Reference na hráčovu postavu.
    /// </summary>
    public GameObject player;

    /// <summary>
    /// Reference na objekt zbraní.
    /// </summary>
    public GameObject weapons;

    /// <summary>
    /// Inicializace třídy při startu hry.
    /// </summary>
    void Start()
    {
        // Inicializace seznamu modelů
        models = new List<GameObject>();

        // Zakázání ovládání hráče a přepínání zbraní
        player.GetComponent<PlayerController>().enabled = false;
        weapons.GetComponent<WeaponSwitcher>().enabled = false;
    }

    /// <summary>
    /// Funkce pro zavření (skrytí) modelů.
    /// </summary>
    public void Close()
    {
        // Pro každý transform ve třídě
        foreach (Transform t in transform)
        {
            // Přidání modelu do seznamu a deaktivace
            models.Add(t.gameObject);
            t.gameObject.SetActive(false);
        }

        // Obnovení ovládání hráče a přepínání zbraní
        player.GetComponent<PlayerController>().enabled = true;
        weapons.GetComponent<WeaponSwitcher>().enabled = true;

        // Zamknutí kurzoru
        Cursor.lockState = CursorLockMode.Locked;
    }
}
