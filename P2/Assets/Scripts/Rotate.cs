using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rotate : MonoBehaviour
{
    public bool right;
    public bool left;
    public int rotateSpeed = 25;

    // Start is called before the first frame update
    void Start()
    {
        right = false;
        left = false;
    }

    void FixedUpdate () {

        if (right) {
          transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime, Space.World);
        }
        else if (left) {
          transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime, Space.World);
        }
    }
}