using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCollider : MonoBehaviour
{
    // Variable para almacenar la referencia del jugador que ejecuta el golpe
    private GameObject hittingPlayer;

    // Método para asignar la referencia del jugador que ejecuta el golpe
    public void SetHittingPlayer(GameObject player)
    {
        hittingPlayer = player;
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
            if (otherPlayer != null)
            {
                StartCoroutine(StunPlayer(otherPlayer));
            }
        }else if (other.CompareTag("bot"))
        {
            botMove otherBot = other.GetComponent<botMove>();
            if (otherBot != null)
            {
                StartCoroutine(StunBot(otherBot));
            }
        }
    }

    private IEnumerator StunPlayer(PlayerController player)
    {
        player.sePuedeMover = false;
        yield return new WaitForSeconds(1.0f);
        player.sePuedeMover = true;
    }

    private IEnumerator StunBot(botMove player)
    {
        player.sePuedeMover = false;
        ReferenciaPlayer.player1.GetComponent<playerTutorial>().golpe = true;
        yield return new WaitForSeconds(1.0f);
        player.sePuedeMover = true;
    }
}
