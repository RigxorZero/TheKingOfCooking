using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObject : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isPickeable = true;
    public bool drop = false;
    [SerializeField] public int type; 

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("1: " + tag);
        Debug.Log("2: " + other.tag);
        if(other.tag == "PlayerInteractionZone")
        {
            other.GetComponentInParent<PickUpObject>().ObjectToPickUp = this.gameObject;
            this.transform.rotation = Quaternion.Euler(0, 0, 0);

        }
        if (other.tag == "MesaInteractiveZone" && isPickeable)
        {
            if (other.GetComponent<mesaInteractiva>().type == type) {
                drop = true;
                Vector3 position = other.transform.position;
                position += new Vector3(0, -0.1f, 0);
                this.transform.position = position;
                this.transform.rotation = Quaternion.Euler(0, 0, 0);
                this.GetComponent<Rigidbody>().isKinematic = true;
            }
        }
        if (other.tag == "ObjectInteractionZone" && isPickeable)
        {
            if (other.GetComponent<mesaInteractiva>().type == type)
            {
                other.GetComponent<sartenController>().llenarCarne();
                Destroy(this.gameObject);
            }
        }
        if(tag == "carne" && other.tag == "sarten")
        {

            other.GetComponent<sartenController>().llenarCarne();
            Destroy(this.gameObject);
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
