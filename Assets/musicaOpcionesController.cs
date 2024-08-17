using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class musicaOpcionesController : MonoBehaviour
{
    public Slider volumen; 
    void Start()
    {
        volumen.value = controlarVolumen.Instance.obtenerVolumen();
    }

    // Update is called once per frame
    void Update()
    {
        if(volumen != null)
        {
            controlarVolumen.Instance.cambiarVolumen(volumen.value);

        }
    }
}
