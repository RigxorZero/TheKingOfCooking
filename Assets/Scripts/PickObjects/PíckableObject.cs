using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PíckableObject : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isPickeable = true;
    public bool drop = false;   

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "PlayerInteractionZone")
        {
            other.GetComponentInParent<PickUpObject>().ObjectToPickUp = this.gameObject;
        }
        if(other.tag == "MesaInteractiveZone" && isPickeable)
        {
            drop = true; 
            Vector3 position = other.transform.position;
            position += new Vector3(0, 0.3f, 0);
            this.transform.position = position;
            this.transform.rotation = Quaternion.Euler(0, 0, 0);
            this.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "PlayerInteractionZone")
        {
            other.GetComponentInParent<PickUpObject>().ObjectToPickUp = null;
        }
    }

  
}
