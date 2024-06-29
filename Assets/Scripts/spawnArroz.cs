using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class spawnArroz : MonoBehaviour
{
    // Start is called before the first frame update
    public bool playerColision;

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "taza")
        {
            playerColision = true;
            if(other.GetComponent<tazaController>() != null)
            {
                Debug.Log("no es nulo");
                if (!other.GetComponent<tazaController>().estaLlena)
                {
                    Debug.Log("no esta llena");
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        other.GetComponent<tazaController>().llenarArroz();
                    }
                }
            }
            else
            {
                Debug.Log("esta llena");
            }
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
