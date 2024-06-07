using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StoneCount : MonoBehaviour
{
    public TMP_Text stones;
    public TMP_Text SC;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        stones.text = SC.text;
    }
}
