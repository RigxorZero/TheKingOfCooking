using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class escenciaController : MonoBehaviour
{
    public bool estaSostenido;
    public InputAction interaccion;
    private bool actionPerformed; // Para evitar múltiples ejecuciones por frame
    public LayerMask layerMaskToIgnore;

    private void OnTriggerStay(Collider other)
    {

        // Verificar si el objeto está en una capa que debe ser ignorada
        if ((layerMaskToIgnore & (1 << other.gameObject.layer)) != 0)
        {
            return; // Ignorar el objeto
        }

        if (interaccion.WasReleasedThisFrame() && !actionPerformed)
        {
            actionPerformed = true; // Marcar la acción como realizada
            Debug.Log("Input action released and action performed");

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

                var olla = other.GetComponentInChildren<ollaController>();
                if (olla != null)
                {
                    olla.cantidadDeEscencias++;
                    Debug.Log("Escencia added to olla. Current amount: " + olla.cantidadDeEscencias);
                }
                else
                {
                    Debug.LogWarning("ollaController component not found on the collided object");
                }
            }
            else if (other.CompareTag("sarten"))
            {
                var sarten = other.GetComponentInChildren<sartenController>();
                if (sarten != null)
                {
                    sarten.cantidadDeEscencias++;
                    Debug.Log("Escencia added to sarten. Current amount: " + sarten.cantidadDeEscencias);
                }
                else
                {
                    Debug.LogWarning("sartenController component not found on the collided object");
                }
            }
        }
    }

    private void Update()
    {
        // Resetear la marca de acción realizada al final del frame
        if (!interaccion.WasReleasedThisFrame())
        {
            actionPerformed = false;
        }
    }

    void Start()
    {
        interaccion.Enable();
    }
}
