using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    public void goExit()
    {
        Application.Quit();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }
}
