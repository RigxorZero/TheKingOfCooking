using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class Bottones : MonoBehaviour
{
    // Start is called before the first frame update

    public AudioSource myMusic;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnIniciar()
    {
        Destroy(myMusic);
        SceneManager.LoadScene(1);
       
       
    }

    public void OnControles()
    {
        SceneManager.LoadScene(6);
    }

    public void OnMenu()
    {
        Destroy(myMusic);
        SceneManager.LoadScene(0); 
    }

    public void onTutorial()
    {
        SceneManager.LoadScene(8);
    }

    public void onFinal()
    {
        //escena 3 y 4
        if(socreManagement.scores[0] > socreManagement.scores[1]){

            SceneManager.LoadScene(3);

        }
        else if(socreManagement.scores[0] < socreManagement.scores[1])
        {
            SceneManager.LoadScene(4);



        }
        else{

            SceneManager.LoadScene(7);
        }


        socreManagement.scores[0] = 0;
        socreManagement.scores[0] = 0;
    }


    public void onCreditos()
    {
        SceneManager.LoadScene(5);
    }


    public void OnCerrar()
    {
        Application.Quit();
    }

}
