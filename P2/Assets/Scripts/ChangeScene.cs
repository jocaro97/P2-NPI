using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            var next = (c - 1 + SceneManager.sceneCountInBuildSettings) % SceneManager.sceneCountInBuildSettings;
            SceneManager.LoadScene(next);
        }
        if (next)
        {
            var next = (c + 1 + SceneManager.sceneCountInBuildSettings) % SceneManager.sceneCountInBuildSettings;
            SceneManager.LoadScene(next);
        }
    }
}
