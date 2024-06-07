using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WoodCount : MonoBehaviour
{
    public TMP_Text woods;
    public TMP_Text WC;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        woods.text = WC.text;
    }
}
