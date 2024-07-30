using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class pauseOpcion : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject canvasOpciones;
    public InputAction interaccion;
    void Start()
    {
        canvasOpciones.SetActive(false);
        interaccion.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if (interaccion.WasReleasedThisFrame())
        {
            canvasOpciones.SetActive(true);
            Timer.pause = true;
            
        }

        
    }
}
