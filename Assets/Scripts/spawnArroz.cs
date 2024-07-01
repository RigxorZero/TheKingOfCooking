using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;

public class spawnArroz : MonoBehaviour
{
    // Start is called before the first frame update
    public bool playerColision;
    public InputAction interaccion;
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "taza")
        {
            playerColision = true;
            if(other.GetComponent<tazaController>() != null)
            {
                if (!other.GetComponent<tazaController>().estaLlena)
                {
                    if (interaccion.WasPressedThisFrame())
                    {
                        other.GetComponent<tazaController>().llenarArroz();
                    }
                }
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
