using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PickAxeW : MonoBehaviour
{
    public GameObject shop;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<TMP_Text>().text = shop.GetComponent<UpgradeShop>().pickWeaponUP.ToString();
    }
}
