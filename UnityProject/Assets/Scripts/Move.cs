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

        // LoadSceneAsync
        if (turnLeft) {
            transform.Rotate(new Vector3(10, 0, 0) * Time.deltaTime);
        }
        if (turnRight) {
            transform.Rotate(new Vector3(-10, 0, 0) * Time.deltaTime);
        }
    }
}
