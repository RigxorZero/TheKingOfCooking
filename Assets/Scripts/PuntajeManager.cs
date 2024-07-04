using System.Collections.Generic;
using UnityEngine;

public class PuntajeManager : MonoBehaviour
{
    // Lista est�tica de puntajes para dos jugadores
    public static List<float> puntajes = new List<float>();

    // Inicializaci�n de los puntajes al inicio del juego
    void Start()
    {
        // Inicializar puntajes para dos jugadores
        puntajes.Add(0f); // Jugador 1
        puntajes.Add(0f); // Jugador 2
    }

    // Funci�n para aumentar el puntaje de un jugador
    public static void AumentarPuntaje(int jugadorIndex, float cantidad)
    {
        if (jugadorIndex >= 0 && jugadorIndex < puntajes.Count)
        {
            puntajes[jugadorIndex] += cantidad;
            Debug.Log($"Puntaje del Jugador {jugadorIndex + 1}: {puntajes[jugadorIndex]}");
        }
        else
        {
            Debug.LogError($"�ndice de jugador inv�lido: {jugadorIndex}");
        }
    }

    // Funci�n para obtener el puntaje de un jugador
    public static float ObtenerPuntaje(int jugadorIndex)
    {
        if (jugadorIndex >= 0 && jugadorIndex < puntajes.Count)
        {
            return puntajes[jugadorIndex];
        }
        else
        {
            Debug.LogError($"�ndice de jugador inv�lido: {jugadorIndex}");
            return 0f;
        }
    }


    // Funci�n para reiniciar los puntajes
    public static void ReiniciarPuntajes()
    {
        for (int i = 0; i < puntajes.Count; i++)
        {
            puntajes[i] = 0f;
        }
    }
}
