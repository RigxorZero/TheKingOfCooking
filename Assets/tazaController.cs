using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tazaController : MonoBehaviour
{
    public bool estaSostenido;
    public bool puedeLlenar = false;
    public GameObject tazallenada;  
    // Start is called before the first frame update

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "hervidor")
        {
            if (estaSostenido)
            {
                if (other.GetComponent<hervidorController>().aguaLista)
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        tazallenada.SetActive(true);
                    }
                    puedeLlenar = true;
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
