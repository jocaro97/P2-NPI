using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public bool next;
    public bool prev;

    // Start is called before the first frame update
    void Start()
    {
        next = false;
        prev = false;
    }

    void FixedUpdate () {

        var c = SceneManager.GetActiveScene().buildIndex;
        // LoadSceneAsync
        if (prev) {
            var next = (c-1+SceneManager.sceneCountInBuildSettings)%SceneManager.sceneCountInBuildSettings;
            SceneManager.LoadScene(next);
        }
        if (next) {
            var next = (c+1+SceneManager.sceneCountInBuildSettings)%SceneManager.sceneCountInBuildSettings;
            SceneManager.LoadScene(next);
        }
    }
}
