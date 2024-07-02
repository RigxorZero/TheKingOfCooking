using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class Bottones : MonoBehaviour
{
    // Start is called before the first frame update

    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnIniciar()
    {
        SceneManager.LoadScene(0);
    }

    public void OnControles()
    {

    }

    public void OnMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void OnCerrar()
    {

    }

}
