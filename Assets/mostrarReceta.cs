using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class mostrarReceta : MonoBehaviour
{
    // Start is called before the first frame update}}
    public InputAction interaccion;
    public GameObject receta;
    public bool estaActiva = false;
    void Start()
    {
        interaccion.Enable();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "JugadorUno" || other.tag == "JugadorDos")
        {
            if (interaccion.WasPressedThisFrame())
            {
                if (estaActiva)
                {
                    GameObject esJugadorUno = GameObject.FindGameObjectWithTag("JugadorUnoPrefab");
                    receta.SetActive(false);
                    other.GetComponentInParent<PlayerController>().sePuedeMover = true;
                    estaActiva = false;
                }
                else
                {
                    receta.SetActive(true);
                    other.GetComponentInParent<PlayerController>().sePuedeMover = false;
                    estaActiva = true;
                    other.GetComponentInParent<playerTutorial>().mostroReceta = true;
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
