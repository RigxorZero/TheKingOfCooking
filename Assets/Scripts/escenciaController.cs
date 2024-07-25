using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class escenciaController : MonoBehaviour
{
    public bool estaSostenido;
    public InputAction interaccion;
    private bool actionPerformed; // Para evitar múltiples ejecuciones por frame

    private void OnTriggerStay(Collider other)
    {
        if (interaccion.WasReleasedThisFrame() && !actionPerformed)
        {
            actionPerformed = true; // Marcar la acción como realizada

            if (other.CompareTag("olla"))
            {
                var playerTutorial = ReferenciaPlayer.player1.GetComponent<playerTutorial>();
                playerTutorial.echarEscencia = true;
                playerTutorial.golpe = false;

                if (!playerTutorial.cambioTimer)
                {
                    playerTutorial.tiempoActual = playerTutorial.tiempoEntreCambio;
                    playerTutorial.cambioTimer = true;
                }

                var olla = other.GetComponent<ollaController>();
                if (olla != null)
                {
                    olla.cantidadDeEscencias++;
                }
            }
            else if (other.CompareTag("sarten"))
            {
                var sarten = other.GetComponent<sartenController>();
                if (sarten != null)
                {
                    sarten.cantidadDeEscencias++;
                }
            }
        }
    }

    private void Update()
    {
        // Resetear la marca de acción realizada al final del frame
        if (interaccion.WasReleasedThisFrame())
        {
            actionPerformed = false;
        }
    }

    void Start()
    {
        interaccion.Enable();
    }
}
