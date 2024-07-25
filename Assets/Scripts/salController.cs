using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class salController : MonoBehaviour
{
    public bool estaSostenido;
    public InputAction interaccion;
    private bool actionPerformed; // Para evitar múltiples ejecuciones por frame
    public bool seecho = false;

    private void OnTriggerStay(Collider other)
    {
        if (interaccion.WasReleasedThisFrame() && !actionPerformed)
        {
            actionPerformed = true; // Marcar la acción como realizada
            Debug.Log("Input action released and action performed");

            if (other.CompareTag("olla"))
            {
                ReferenciaPlayer.player1.GetComponent<playerTutorial>().echarSal = true;
                other.GetComponent<ollaController>().cantidadDeSal++;
                seecho = true;
                Debug.Log("Salt added to olla. Current amount: " + other.GetComponent<ollaController>().cantidadDeSal);
            }
            else if (other.CompareTag("sarten"))
            {
                other.GetComponent<sartenController>().cantidadDeSal++;
                Debug.Log("Salt added to sarten. Current amount: " + other.GetComponent<sartenController>().cantidadDeSal);
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
