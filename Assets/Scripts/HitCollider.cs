using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCollider : MonoBehaviour
{
    // Variable para almacenar la referencia del jugador que ejecuta el golpe
    private GameObject hittingPlayer;
    public float tiempoactual; 
    public float tiempoentrecambio;

    public bool seactivoPlayer;
    public bool seactivoBot;

    PlayerController playerstun;
    botMove botstun; 

    // M�todo para asignar la referencia del jugador que ejecuta el golpe
    public void SetHittingPlayer(GameObject player)
    {
        hittingPlayer = player;
    }
    private void Update()
    {
        if (tiempoactual >= 0) { 
            tiempoactual -= Time.deltaTime;
        }
        else
        {
            if (seactivoPlayer)
            {
                if (playerstun != null) { 
                    playerstun.sePuedeMover = true;
                    seactivoPlayer = false;

                }
            }
            else if (seactivoBot)
            {
                if (botstun != null)
                {
                    botstun.sePuedeMover = true;
                    seactivoBot = false;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Ignorar colisiones con el jugador que ejecuta el golpe
        if (other.gameObject == hittingPlayer)
        {
            return;
        }

        // Procesar colisiones con otros jugadores
        if (other.CompareTag("JugadorUno") || other.CompareTag("JugadorDos"))
        {
            PlayerController otherPlayer = other.GetComponent<PlayerController>();
            if (otherPlayer != null && seactivoPlayer == false)
            {
                otherPlayer.stune();    
            }
        }else if (other.CompareTag("bot"))
        {
            botMove otherBot = other.GetComponent<botMove>();
            if (otherBot != null)
            {
                otherBot.stune();
            }
        }
    }
    private void golpeJugador()
    {
        seactivoPlayer = true;
        //playerstun.sePuedeMover = false;
        tiempoactual = tiempoentrecambio;
        

    }
    private void golpeBot()
    {
        seactivoBot = true;
        //botstun.sePuedeMover = false;
        tiempoactual = tiempoentrecambio;

    }
}
