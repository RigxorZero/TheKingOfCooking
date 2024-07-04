using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class tazaController : MonoBehaviour
{
    public bool estaSostenido;
    public bool puedeLlenar = false;
    public bool estaLlena;
    public GameObject tazallenada;

    [SerializeField] private int queEstaLlenando; //1 para agua y 2 para arroz

    [Header("Material")]
    [SerializeField] private Material aguaMaterial;
    [SerializeField] private Material arrozMaterial;

    public InputAction interaccion;
    // Start is called before the first frame update

    public bool chocaConOlla = false; 

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "hervidor")
        {
            if (estaSostenido)
            {
                if (other.GetComponentInChildren<hervidorController>().aguaLista)
                {
                    if (interaccion.WasPressedThisFrame())
                    {
                        llenarAgua();
                    }
                    puedeLlenar = true;
                }
            }
        }
        if(other.tag == "olla")
        {
            if(other.GetComponent<ollaController>() != null)
            {
                chocaConOlla = true;
                if (estaSostenido && estaLlena)
                {
                    if (interaccion.WasPressedThisFrame())
                    {
                        tazallenada.SetActive(false);
                        estaLlena = false;
                        if (queEstaLlenando == 1)
                        {
                            other.GetComponent<ollaController>().cantidadDeAgua++;
                            other.GetComponent<ollaController>().cambioAnimationAgua();
                        }
                        else if (queEstaLlenando == 2)
                        {
                            other.GetComponent<ollaController>().cantidadDeArroz++;
                            other.GetComponent<ollaController>().cambioAnimationArroz();
                        }

                    }
                }
            }
            
        }
    }
    public void llenarAgua()
    {
        tazallenada.GetComponent<MeshRenderer>().material = aguaMaterial;
        queEstaLlenando = 1;
        tazallenada.SetActive(true);
        estaLlena = true;
    }
    public void llenarArroz()
    {
        tazallenada.GetComponent<MeshRenderer>().material = arrozMaterial;
        queEstaLlenando = 2; 
        tazallenada.SetActive(true);
        estaLlena = true;
    }
    void Start()
    {
        interaccion.Enable();
        tazallenada.SetActive(false);
        estaLlena = false;
    }
   
    // Update is called once per frame
    void Update()
    {
        
    }
}
