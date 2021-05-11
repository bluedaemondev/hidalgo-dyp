﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseInKeyInput : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            this.gameObject.SetActive(false);
        }
    }
}
