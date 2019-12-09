using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Move : MonoBehaviour
{
    public bool turnLeft;
    public bool turnRight;

    // Start is called before the first frame update
    void Start()
    {
        turnLeft = false;
        turnRight = false;
    }

    void FixedUpdate () {

        var c = SceneManager.GetActiveScene().buildIndex;
        Debug.Log(c);
        Debug.Log(SceneManager.sceneCountInBuildSettings);
        // LoadSceneAsync
        if (turnLeft) {
            var next = (c-1+SceneManager.sceneCountInBuildSettings)%SceneManager.sceneCountInBuildSettings;
            SceneManager.LoadScene(next);
            //transform.Rotate(new Vector3(10, 0, 0) * Time.deltaTime);
        }
        if (turnRight) {
            var next = (c+1+SceneManager.sceneCountInBuildSettings)%SceneManager.sceneCountInBuildSettings;
            SceneManager.LoadScene(next);
            //transform.Rotate(new Vector3(-10, 0, 0) * Time.deltaTime);
        }
    }
}
