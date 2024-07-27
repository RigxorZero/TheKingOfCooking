using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class salController : MonoBehaviour
{
    public bool estaSostenido;
    public InputAction interaccion;
    private bool actionPerformed; // Para evitar m�ltiples ejecuciones por frame
    public bool seecho = false;
    public LayerMask layerToCheck; // Capa espec�fica para verificar

    private void OnTriggerStay(Collider other)
    {
        // Verificar si el objeto est� en la capa espec�fica
        if ((layerToCheck & (1 << other.gameObject.layer)) == 0)
        {
            return; // Ignorar el objeto si no est� en la capa espec�fica
        }

        if (interaccion.WasReleasedThisFrame() && !actionPerformed)
        {
            actionPerformed = true; // Marcar la acci�n como realizada
            Debug.Log("Input action released and action performed");

            Debug.Log("Other tag as : " + other.tag);

            if (other.CompareTag("olla"))
            {
                ReferenciaPlayer.player1.GetComponent<playerTutorial>().echarSal = true;
                other.GetComponentInChildren<ollaController>().cantidadDeSal++;
                seecho = true;
                Debug.Log("Salt added to olla. Current amount: " + other.GetComponentInChildren<ollaController>().cantidadDeSal);
            }
            else if (other.CompareTag("sarten"))
            {
                other.GetComponent<sartenController>().cantidadDeSal++;
                Debug.Log("Salt added to sarten. Current amount: " + other.GetComponentInChildren<sartenController>().cantidadDeSal);
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
