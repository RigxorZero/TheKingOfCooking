using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platoFinalController : MonoBehaviour
{
    // Start is called before the first frame update
    public int type;
    public bool toco;
    public  float perfeccionArrozP1;
    public  float perfeccionCarneP1;
    public  float perfeccionArrozP2;
    public  float perfeccionCarneP2;
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "plato")
        {
            if (type == 1)
            {   
                perfeccionArrozP1 = other.GetComponentInParent<platoController>().perfeccionArroz;
                perfeccionCarneP1 = other.GetComponentInParent<platoController>().perfeccionCarne;
                socreManagement.scores[0] = (perfeccionArrozP1 + perfeccionArrozP1) / 2;
            }
            else if (type == 2) {
                perfeccionArrozP2 = other.GetComponentInParent<platoController>().perfeccionArroz;
                perfeccionCarneP2 = other.GetComponentInParent<platoController>().perfeccionCarne;
                socreManagement.scores[1] = (perfeccionArrozP2 + perfeccionArrozP2) / 2;
            }
            toco = true;
        }
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
