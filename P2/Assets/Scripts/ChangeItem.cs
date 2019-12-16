using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public bool next;
    public bool prev;
    public GameObject[] objetos;
    public int _currentObject = 0;

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
            objetos[_currentObject].SetActive(false);
            _currentObject = (_currentObject+1) % objetos.Length;
            objetos[_currentObject].SetActive(true);
        }
        if (next)
        {
            next = false;
            objetos[_currentObject].SetActive(false);
            _currentObject = (objetos.Length + _currentObject-1) % objetos.Length;
            objetos[_currentObject].SetActive(true);
        }
    }
}
