using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carneController : MonoBehaviour
{
    // Start is called before the first frame update
    public bool essarten; 
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        // Utiliza CompareTag en lugar de other.tag == "sarten"
        if (other.CompareTag("sarten"))
        {
            essarten = true;

            // Intenta obtener el componente sartenController
            sartenController sartenCtrl = other.GetComponentInParent<sartenController>();
            if (sartenCtrl != null)
            {
                // Llama al método llenarCarne del componente sartenController
                sartenCtrl.llenarCarne();
            }
            else
            {
                Debug.LogWarning("sartenController no encontrado en el objeto con tag 'sarten'");
            }

            // Destruye este gameObject
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
