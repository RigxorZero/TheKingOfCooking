using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerTutorial : MonoBehaviour
{
    public bool esTutorial = false;

    public bool mostroReceta = false;
    public bool dejoOlla = false;


    public bool fase1 = false;
    
    void Start()
    {
        canvasTutorial.index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (esTutorial)
        {
            if (this.GetComponent<PlayerController>().seMovio)
            {
                canvasTutorial.index = 1;
                fase1 = true;
            }
            if(mostroReceta && fase1)
            {
                canvasTutorial.index = 2;
            }
            if(dejoOlla && mostroReceta && fase1)
            {
                canvasTutorial.index = 3;
            }
        }
    }
}
