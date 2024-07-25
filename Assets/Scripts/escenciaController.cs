using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class escenciaController : MonoBehaviour
{
    public bool estaSostenido;
    public InputAction interaccion;
    private void OnTriggerStay(Collider other)
    {
        if (interaccion.WasPressedThisFrame())
        {
            if (other.tag == "olla")
            {
                ReferenciaPlayer.player1.GetComponent<playerTutorial>().echarEscencia = true;

                ReferenciaPlayer.player1.GetComponent<playerTutorial>().golpe = false;

                if (!ReferenciaPlayer.player1.GetComponent<playerTutorial>().cambioTimer)
                {
                    ReferenciaPlayer.player1.GetComponent<playerTutorial>().tiempoActual = ReferenciaPlayer.player1.GetComponent<playerTutorial>().tiempoEntreCambio;
                    ReferenciaPlayer.player1.GetComponent<playerTutorial>().cambioTimer = true;
                }
                if(other.GetComponent<ollaController>() != null)
                {
                    other.GetComponent<ollaController>().cantidadDeEscencias++;

                }
            }
            if (other.tag == "sarten")
            {
                other.GetComponent<sartenController>().cantidadDeEscencias++;
            }
        }
    }
    
    void Start()
    {
        interaccion.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
