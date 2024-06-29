using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tazaController : MonoBehaviour
{
    public bool estaSostenido;
    public bool puedeLlenar = false;
    public bool estaLlena = false; 
    public GameObject tazallenada;
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
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        tazallenada.SetActive(true);
                        estaLlena = true;
                    }
                    puedeLlenar = true;
                }
            }
        }
        if(other.tag == "olla")
        {
            chocaConOlla = true; 
            if( estaSostenido && estaLlena)
            {
                if (Input.GetKeyDown(KeyCode.E)) {
                    tazallenada.SetActive(false);
                    estaLlena = false;
                    other.GetComponent<ollaController>().cantidadDeAgua++;
                    other.GetComponent<ollaController>().cambioAnimationAgua();
                }
            }
        }
    }
    void Start()
    {
        tazallenada.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
