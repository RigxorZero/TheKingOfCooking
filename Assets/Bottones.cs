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
        SceneManager.LoadScene(1);
    }

    public void OnControles()
    {
        SceneManager.LoadScene(6);
    }

    public void OnMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void onFinal()
    {
        //escena 3 y 4
        if(socreManagement.scores[0] > socreManagement.scores[1]){

            SceneManager.LoadScene(3);

        }
        else
        {
            SceneManager.LoadScene(4);


            socreManagement.scores[0] = 0;
            socreManagement.scores[0] = 0;


        }
    }


    public void onCreditos()
    {
        SceneManager.LoadScene(5);
    }


    public void OnCerrar()
    {

    }

}
