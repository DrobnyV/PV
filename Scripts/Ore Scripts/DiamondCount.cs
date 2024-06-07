using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DiamondCount : MonoBehaviour
{
    public TMP_Text diamonds;
    public TMP_Text DC;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        diamonds.text = DC.text;
    }
}
