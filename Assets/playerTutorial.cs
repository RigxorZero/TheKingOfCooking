using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerTutorial : MonoBehaviour
{
    public bool esTutorial = false; 
   
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
            }
        }
    }
}
