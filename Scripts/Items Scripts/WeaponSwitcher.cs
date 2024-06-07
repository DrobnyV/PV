using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Třída WeaponSwitcher řídí přepínání mezi zbraněmi hráče.
/// </summary>
public class WeaponSwitcher : MonoBehaviour
{
    /// <summary>
    /// Reference na objekt výběru krumpáče.
    /// </summary>
    public GameObject pickaxe;

    /// <summary>
    /// Reference na objekt výběru sekery.
    /// </summary>
    public GameObject axe;

    /// <summary>
    /// Seznam modelů zbraní.
    /// </summary>
    private List<GameObject> models;

    /// <summary>
    /// Index aktuálně vybrané zbraně.
    /// </summary>
    private int selectionIndex = 0;

    /// <summary>
    /// Reference na textovou komponentu zobrazující pomoc.
    /// </summary>
    public TMP_Text help;

    /// <summary>
    /// Aktivní zbraň.
    /// </summary>
    private string active;

    /// <summary>
    /// Inicializační metoda. Skryje všechny modely zbraní kromě vybrané.
    /// </summary>
    void Start()
    {
        models = new List<GameObject>();
        foreach (Transform t in transform)
        {
            models.Add(t.gameObject);
            t.gameObject.SetActive(false);
        }
        active = "mainWeapon";
        pickaxe.SetActive(false);
        axe.SetActive(false);
    }

    /// <summary>
    /// Metoda pro výběr zbraně podle indexu.
    /// </summary>
    public void Select(int index)
    {
        help.text = index.ToString();

        if (index < 0 || index >= models.Count)
        {
            return;
        }
        models[selectionIndex].SetActive(false);
        selectionIndex = index;
        models[selectionIndex].SetActive(true);
    }

    /// <summary>
    /// Metoda pro přepínání mezi zbraněmi.
    /// </summary>
    void SwitchWeapons()
    {
        if (active != "mainWeapon" && Input.GetKeyDown(KeyCode.Alpha1))
        {
            pickaxe.SetActive(false);
            axe.SetActive(false);
            models[selectionIndex].SetActive(true);
            active = "mainWeapon";
        }

        if (active != "pickaxe" && Input.GetKeyDown(KeyCode.Alpha2))
        {
            models[selectionIndex].SetActive(false);
            axe.SetActive(false);
            pickaxe.SetActive(true);
            active = "pickaxe";
        }
        if (active != "axe" && Input.GetKeyDown(KeyCode.Alpha3))
        {
            models[selectionIndex].SetActive(false);
            pickaxe.SetActive(false);
            axe.SetActive(true);
            active = "axe";
        }
    }

    /// <summary>
    /// Metoda pro aktualizaci, která se volá jednou za frame. Zajišťuje přepínání zbraní.
    /// </summary>
    void Update()
    {
        SwitchWeapons();
    }
}
