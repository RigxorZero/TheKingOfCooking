using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basureroController : MonoBehaviour
{
   
    
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "taza" || other.tag == "escencia" || other.tag == "sal")
        {
            if (other.GetComponentInParent<PickableObject>().sostenido)
            {
                ReferenciaPlayer.player1.GetComponent<playerTutorial>().botarBasura = true; 
                Destroy(other.gameObject);
            }
        }
       
    }
   
}
