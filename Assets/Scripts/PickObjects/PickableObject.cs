using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObject : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isPickeable = true;
    public bool drop = false;
    public bool sostenido = false;
    public bool eliminado = false;
    [SerializeField] public int type; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "basurero")
        {
            eliminado = true;
            if (sostenido)
            {
                Destroy(gameObject);

            }
        }
        if (other.tag == "PlayerInteractionZone")
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
                this.transform.SetParent(other.transform);
                this.GetComponent<Rigidbody>().isKinematic = true;
                if (type == 2)
                {
                    ReferenciaPlayer.player1.GetComponent<playerTutorial>().dejoOlla = true;
                }       
            }
        }
        if (other.tag == "ObjectInteractionZone" && isPickeable)
        {
            if (other.GetComponent<mesaInteractiva>().type == type)
            {
                drop = true;
                isPickeable = false;
                Vector3 position = other.transform.position;
                position += new Vector3(0, -0.1f, 0);
                this.transform.position = position;
                this.transform.rotation = Quaternion.Euler(0, 0, 0);
                this.GetComponent<Rigidbody>().isKinematic = true;

                // Make this object a child of the object it interacted with
                this.transform.SetParent(other.transform);

                // Disable the collider to prevent further interactions
                this.GetComponent<Collider>().enabled = false;
            }
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
