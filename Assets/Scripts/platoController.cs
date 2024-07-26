using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class platoController : MonoBehaviour
{
    public bool estaSostenido;
    public bool tieneArroz;
    public bool tieneCarne;
    public InputAction interaccion;

    public float perfeccionArroz;
    public float perfeccionCarne;

    public GameObject arroz;
    public GameObject carne;

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "olla")
        {
            if (other.GetComponent<ollaController>().faseDosCompleta)
            {
                if (interaccion.WasPressedThisFrame())
                {
                    ReferenciaPlayer.player1.GetComponent<playerTutorial>().servirArroz = true;
                    perfeccionArroz = (other.GetComponent<ollaController>().PerfeccionFaseUno + other.GetComponent<ollaController>().PerfeccionFaseDos) / 2; 
                    arroz.SetActive(true);
                    tieneArroz = true;
                }
            }
        }
        if (other.tag == "sarten")
        {
            //Debug.Log(other.GetComponent<sartenController>());
           

            if (other.GetComponent<sartenController>().faseCompleta)
            {
                if (interaccion.WasPressedThisFrame())
                {
                    ReferenciaPlayer.player1.GetComponent<playerTutorial>().servirCarne = true;
                    perfeccionCarne = other.gameObject.GetComponent<sartenController>().Perfeccion;
                    carne.SetActive(true);
                    tieneCarne = true;
                }
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        arroz.SetActive(false);
        carne.SetActive(false);
        interaccion.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
