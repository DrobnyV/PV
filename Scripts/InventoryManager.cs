using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Třída spravující inventář v herním světě.
/// </summary>
public class InventoryManager : MonoBehaviour
{
    /// <summary>
    /// Obrázek pozadí inventáře.
    /// </summary>
    public GameObject imageBackground;

    /// <summary>
    /// Textové pole pro zobrazení počtu dřeva.
    /// </summary>
    public GameObject textWood;

    /// <summary>
    /// Obrázek dřeva.
    /// </summary>
    public GameObject imageWood;

    /// <summary>
    /// Textové pole pro zobrazení počtu kamene.
    /// </summary>
    public GameObject textRock;

    /// <summary>
    /// Obrázek kamene.
    /// </summary>
    public GameObject imageRock;

    /// <summary>
    /// Textové pole pro zobrazení počtu zlata.
    /// </summary>
    public GameObject textGold;

    /// <summary>
    /// Obrázek zlata.
    /// </summary>
    public GameObject imageGold;

    /// <summary>
    /// Textové pole pro zobrazení počtu železa.
    /// </summary>
    public GameObject textIron;

    /// <summary>
    /// Obrázek železa.
    /// </summary>
    public GameObject imageIron;

    /// <summary>
    /// Textové pole pro zobrazení počtu diamantů.
    /// </summary>
    public GameObject textDiamond;

    /// <summary>
    /// Obrázek diamantů.
    /// </summary>
    public GameObject imageDiamond;

    /// <summary>
    /// Příznak skrytí inventáře.
    /// </summary>
    private bool isHidden = false;

    /// <summary>
    /// Inicializace třídy při startu hry.
    /// </summary>
    void Start()
    {
        // Skrytí všech prvků inventáře
        imageBackground.SetActive(false);
        textWood.SetActive(false);
        imageWood.SetActive(false);
        textRock.SetActive(false);
        imageRock.SetActive(false);
        textGold.SetActive(false);
        imageGold.SetActive(false);
        textIron.SetActive(false);
        imageIron.SetActive(false);
        textDiamond.SetActive(false);
        imageDiamond.SetActive(false);
    }

    /// <summary>
    /// Aktualizace třídy každý snímek.
    /// </summary>
    void Update()
    {
        // Zobrazení/skrytí inventáře při stisknutí klávesy Tab
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isHidden = !isHidden;
            imageBackground.SetActive(!isHidden);
            textWood.SetActive(!isHidden);
            imageWood.SetActive(!isHidden);
            textRock.SetActive(!isHidden);
            imageRock.SetActive(!isHidden);
            textGold.SetActive(!isHidden);
            imageGold.SetActive(!isHidden);
            textIron.SetActive(!isHidden);
            imageIron.SetActive(!isHidden);
            textDiamond.SetActive(!isHidden);
            imageDiamond.SetActive(!isHidden);
        }
    }
}
