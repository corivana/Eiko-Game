﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMusicSingleton : MonoBehaviour
{
    public static BGMusicSingleton instance; //singleton
    private void Awake()
    {
        // Has the singleton not been created yet
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}