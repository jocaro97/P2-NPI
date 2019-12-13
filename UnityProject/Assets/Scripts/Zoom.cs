using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Zoom : MonoBehaviour
{
    public bool zoomIn;
    public bool zoomOut;
    public float zoomMultiplier = 1.0F;

    // Start is called before the first frame update
    void Start()
    {
        zoomIn = false;
        zoomOut = false;
    }

    void FixedUpdate () {

        var zoomAmount = Time.deltaTime * zoomMultiplier;

        if (zoomIn) {
            transform.Translate(zoomAmount, zoomAmount, zoomAmount, Space.World);
        }
        if (zoomOut) {
            transform.Translate(-zoomAmount, -zoomAmount, -zoomAmount, Space.World);
        }
    }
}
