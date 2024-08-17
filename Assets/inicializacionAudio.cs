using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inicializacionAudio : MonoBehaviour
{
    
    void Start()
    {
        controlarVolumen.Instance.inicializarVolumen();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
