using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CantidadDePlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public static int cantidadDePlayer;
    public static bool sepuedemover;
    public Image player1;
    public Image player2;
    public GameObject Receta;
    public TextMeshProUGUI textPlayer1;
    public TextMeshProUGUI textPlayer2;
    public bool estutorial = false;
    public static bool ready = false;

    private void OnDisable()
    {
        cantidadDePlayer = 0;
        ready = false;
        sepuedemover = false;
    }

    void Start()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        // Compara el nombre de la escena actual
        if (currentSceneName == "Tutorial 1 jugador")
        {
            estutorial = true;
        }

        if(currentSceneName == "SampleScene 1")
        {
            sepuedemover = false;
        }
    }

    // Update is called once per frame
    void Update()
    {


        string currentSceneName = SceneManager.GetActiveScene().name;

        // Compara el nombre de la escena actual
        if (currentSceneName == "Tutorial 1 jugador" && estutorial)
        {
            if (cantidadDePlayer == 1 && !ready)
            {
                sepuedemover = true;
                Receta.SetActive(false);
                player1.enabled = false;
                textPlayer1.enabled = false;
                ready = true;
            }
        }
        else
        {
            if (cantidadDePlayer == 1)
            {
                player1.enabled = false;
                textPlayer1.enabled = false;
            }
            else if (cantidadDePlayer == 2 && !ready)
            {
                player2.enabled = false;
                textPlayer2.enabled = false;
                Receta.SetActive(false);
                sepuedemover = true;
                ready = true;
            }
        }
        
    }
}
