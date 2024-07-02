using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class sartenController : MonoBehaviour
{
    // Start is called before the first frame update
    public bool estaSostenido;
    public bool tocacarme;
    public bool estaLleno; 
    public GameObject carne3D; 

    [Header("Material")]
    [SerializeField] private Material cocinado;
    [SerializeField] private Material quemado; 
    public InputAction interaccion;
    void Start()
    {
        interaccion.Enable();
        carne3D.SetActive(false);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "carne")
        {
            tocacarme = true;
            if (estaSostenido)
            {
                if (!estaLleno)
                {
                    if (interaccion.WasPressedThisFrame())
                    {
                        llenarCarne();
                    }
                }
            }
           

        }
    }

    // Update is called once per frame
    public void llenarCarne()
    {
        carne3D.SetActive(true);
        estaLleno = true;
    }
}
