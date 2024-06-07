using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoldCount : MonoBehaviour
{
    public TMP_Text golds;
    public TMP_Text GC;

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        golds.text = GC.text;
    }
}
