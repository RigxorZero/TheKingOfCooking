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
    public Image Receta;
    public TextMeshProUGUI textPlayer1;
    public TextMeshProUGUI textPlayer2;
    public bool estutorial = false;

    private void OnDisable()
    {
        cantidadDePlayer = 0;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        string currentSceneName = SceneManager.GetActiveScene().name;

        // Compara el nombre de la escena actual
        if (currentSceneName == "Tutorial 1 jugador")
        {
            if (cantidadDePlayer == 1)
            {
                sepuedemover = true;
            }
        }
        else
        {
            if (cantidadDePlayer == 1)
            {
                player1.enabled = false;
                textPlayer1.enabled = false;
            }
            else if (cantidadDePlayer == 2)
            {
                player2.enabled = false;
                textPlayer2.enabled = false;
                Receta.enabled = false;
                sepuedemover = true;
            }
        }
        
    }
}