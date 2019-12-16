﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public bool next;
    public bool prev;
    void Start()
    {
        next = false;
        prev = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var c = SceneManager.GetActiveScene().buildIndex;
        // LoadSceneAsync
        if (prev)
        {
            prev = false;
            var next_scene = (c - 1 + SceneManager.sceneCountInBuildSettings) % SceneManager.sceneCountInBuildSettings;
            SceneManager.LoadScene(next_scene);
        }
        if (next)
        {
            next = false;
            var next_scene = (c + 1 + SceneManager.sceneCountInBuildSettings) % SceneManager.sceneCountInBuildSettings;
            SceneManager.LoadScene(next_scene);
        }
    }
}
