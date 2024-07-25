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
    public List<LayerMask> layerMasksToIgnore;

    private void OnTriggerStay(Collider other)
    {
        // Verificar si el objeto está en alguna de las capas que deben ser ignoradas
        bool ignore = false;
        foreach (var layerMask in layerMasksToIgnore)
        {
            if ((layerMask & (1 << other.gameObject.layer)) != 0)
            {
                ignore = true;
                break;
            }
        }

        if (ignore)
        {
            return; // Ignorar el objeto
        }

        if (interaccion.WasReleasedThisFrame() && !actionPerformed)
        {
            actionPerformed = true; // Marcar la acción como realizada
            Debug.Log("Input action released and action performed");

            Debug.Log("Other tag as : " + other.tag);

            if (other.CompareTag("olla"))
            {
                ReferenciaPlayer.player1.GetComponent<playerTutorial>().echarSal = true;
                other.GetComponent<ollaController>().cantidadDeSal++;
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
