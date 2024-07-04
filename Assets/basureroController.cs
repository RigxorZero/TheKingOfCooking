using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basureroController : MonoBehaviour
{
   
    
    private void OnTriggerStay(Collider other)
    {
        Debug.Log(other.tag);
        if (other.tag == "taza" || other.tag == "escencia" || other.tag == "sal")
        {
            if (other.GetComponent<PickableObject>().sostenido)
            {
                Destroy(other.gameObject);
            }
        }
       
    }
   
}
