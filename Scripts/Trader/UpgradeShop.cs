using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeShop : MonoBehaviour
{
    public float mainWeaponUP = 1; // Úroveň vylepšení hlavní zbraně
    public float axeWeaponUP = 1; // Úroveň vylepšení sekery
    public float pickWeaponUP = 1; // Úroveň vylepšení krumpáče
    private TMP_Text wood; // Reference na textový objekt pro dřevo
    private TMP_Text stone; // Reference na textový objekt pro kámen
    private TMP_Text iron; // Reference na textový objekt pro železo
    private TMP_Text diamond; // Reference na textový objekt pro diamanty
    public TMP_Text mainWeapon; // Reference na textový objekt pro hlavní zbraň
    public TMP_Text axe; // Reference na textový objekt pro sekeru
    public TMP_Text pickaxe; // Reference na textový objekt pro krumpáč

    private void Start()
    {
        // Najde a přiřadí textové objekty pro suroviny
        wood = GameObject.Find("WC").GetComponent<TMP_Text>();
        stone = GameObject.Find("SC").GetComponent<TMP_Text>();
        iron = GameObject.Find("IC").GetComponent<TMP_Text>();
        diamond = GameObject.Find("DC").GetComponent<TMP_Text>();
    }

    /// <summary>
    /// Metoda pro vylepšení hlavní zbraně.
    /// </summary>
    public void MainWeaponUpgrade()
    {
        // Podmínka pro vylepšení na další úroveň
        if (mainWeaponUP == 1)
        {
            if (int.Parse(wood.text) >= 100)
            {
                mainWeaponUP += 1f;
                wood.text = (int.Parse(wood.text) - 100).ToString();
                mainWeapon.text = "Vylepšit \n 100 Kamení";
            }
        }
        else if (mainWeaponUP == 2)
        {
            if (int.Parse(stone.text) >= 100)
            {
                mainWeaponUP += 1f;
                stone.text = (int.Parse(stone.text) - 100).ToString();
                mainWeapon.text = "Vylepšit \n 100 Železa";
            }
        }
        else if (mainWeaponUP == 3)
        {
            if (int.Parse(iron.text) >= 100)
            {
                mainWeaponUP += 1f;
                iron.text = (int.Parse(iron.text) - 100).ToString();
                mainWeapon.text = "Vylepšit \n 100 Diamantů";
            }
        }
        else if (mainWeaponUP == 4)
        {
            if (int.Parse(diamond.text) >= 100)
            {
                mainWeaponUP += 1f;
                diamond.text = (int.Parse(diamond.text) - 100).ToString();
                mainWeapon.text = "Hotovo";
            }
        }
    }

    /// <summary>
    /// Metoda pro vylepšení sekery.
    /// </summary>
    public void AxeUpgrade()
    {
        // Podmínka pro vylepšení na další úroveň
        if (axeWeaponUP == 1)
        {
            if (int.Parse(wood.text) >= 100)
            {
                axeWeaponUP += 1f;
                wood.text = (int.Parse(wood.text) - 100).ToString();
                axe.text = "Vylepšit sekeru \n 100 Kamení";
            }
        }
        else if (axeWeaponUP == 2)
        {
            if (int.Parse(stone.text) >= 100)
            {
                axeWeaponUP += 1f;
                stone.text = (int.Parse(stone.text) - 100).ToString();
                axe.text = "Vylepšit sekeru \n 100 Železa";
            }
        }
        else if (axeWeaponUP == 3)
        {
            if (int.Parse(iron.text) >= 100)
            {
                axeWeaponUP += 1f;
                iron.text = (int.Parse(iron.text) - 100).ToString();
                axe.text = "Vylepšit sekeru \n 100 Diamantů";
            }
        }
        else if (axeWeaponUP == 4)
        {
            if (int.Parse(diamond.text) >= 100)
            {
                axeWeaponUP += 1f;
                diamond.text = (int.Parse(diamond.text) - 100).ToString();
                axe.text = "Hotovo";
            }
        }
    }

    /// <summary>
    /// Metoda pro vylepšení krumpáče.
    /// </summary>
    public void PickAxeUpgrade()
    {
        // Podmínka pro vylepšení na další úroveň
        if (pickWeaponUP == 1)
        {
            if (int.Parse(wood.text) >= 100)
            {
                pickWeaponUP += 1f;
                wood.text = (int.Parse(wood.text) - 100).ToString();
                pickaxe.text = "Vylepšit krumpáč \n 100 Kamení";
            }
        }
        else if (pickWeaponUP == 2)
        {
            if (int.Parse(stone.text) >= 100)
            {
                pickWeaponUP += 1f;
                stone.text = (int.Parse(stone.text) - 100).ToString();
                pickaxe.text = "Vylepšit krumpáč \n 100 Železa";
            }
        }
        else if (pickWeaponUP == 3)
        {
            if (int.Parse(iron.text) >= 100)
            {
                pickWeaponUP += 1f;
                iron.text = (int.Parse(iron.text) - 100).ToString();
                pickaxe.text = "Vylepšit krumpáč \n 100 Diamantů";
            }
        }
        else if (pickWeaponUP == 4)
        {
            if (int.Parse(diamond.text) >= 100)
            {
                pickWeaponUP += 1f;
                diamond.text = (int.Parse(diamond.text) - 100).ToString();
                pickaxe.text = "Hotovo";
            }
        }
    }
}
