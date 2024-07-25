using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class salController : MonoBehaviour
{
    public bool estaSostenido;
    public InputAction interaccion;

    private void OnTriggerStay(Collider other)
    {
        if (interaccion.WasPressedThisFrame())
        {
            if (other.tag == "olla")
            {
                ReferenciaPlayer.player1.GetComponent<playerTutorial>().echarSal = true;
                other.GetComponent<ollaController>().cantidadDeSal++;
            }
            if (other.tag == "sarten")
            {
                other.GetComponent<sartenController>().cantidadDeSal++;
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
