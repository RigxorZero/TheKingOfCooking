using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cambioTutorial : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "JugadorUno")
        {
            other.GetComponentInParent<playerTutorial>().esTutorial = true;
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
