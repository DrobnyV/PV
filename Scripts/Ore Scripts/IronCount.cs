using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IronCount : MonoBehaviour
{
    public TMP_Text irons;
    public TMP_Text IC;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        irons.text = IC.text;
    }
}
