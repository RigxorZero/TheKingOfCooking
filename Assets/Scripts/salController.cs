using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class salController : MonoBehaviour
{
    public bool estaSostenido;
    public InputAction interaccion;
    [SerializeField] private bool actionPerformed; // Para evitar m�ltiples ejecuciones por frame
    public bool seecho = false; 

    private void OnTriggerStay(Collider other)
    {
        if (interaccion.WasReleasedThisFrame() && !actionPerformed)
        {
            actionPerformed = true; // Marcar la acci�n como realizada

            if (other.tag == "olla")
            {
                ReferenciaPlayer.player1.GetComponent<playerTutorial>().echarSal = true;
                other.GetComponent<ollaController>().cantidadDeSal++;
                seecho = true;
            }
            else if (other.tag == "sarten")
            {
                other.GetComponent<sartenController>().cantidadDeSal++;
            }
        }
    }

    private void Update()
    {
        // Resetear la marca de acci�n realizada al final del frame
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
