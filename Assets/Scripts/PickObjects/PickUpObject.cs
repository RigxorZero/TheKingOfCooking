using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    public GameObject ObjectToPickUp;
    public GameObject PickedObject;
    public Transform interactionZone;


    void Update()
    {
        ObjectDrawer[] drawers = FindObjectsOfType<ObjectDrawer>();

        if (PickedObject == null)
        {
            // Caso: Recoger un objeto existente
            if (ObjectToPickUp != null && ObjectToPickUp.GetComponent<PickableObject>().isPickeable)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    PickUp(ObjectToPickUp);
                }
            }
            // Caso: Instanciar y recoger un objeto desde cualquier cajón
            else
            {
                foreach (ObjectDrawer drawer in drawers)
                {
                    if (drawer.IsPlayerInRange())
                    {
                        if (Input.GetKeyDown(KeyCode.F))
                        {

                            GameObject prefab = Resources.Load<GameObject>(drawer.objectPrefabName);
                            if (prefab != null)
                            {
                                GameObject instance = Instantiate(prefab, interactionZone.position, interactionZone.rotation);
                                PickUp(instance);
                            }
                            else
                            {
                                Debug.LogError("Prefab no encontrado en Resources: " + drawer.objectPrefabName);
                            }
                        }
                    }
                }
            }
        }
        else
        {
            // Caso: Soltar el objeto recogido
            if (Input.GetKeyDown(KeyCode.F))
            {
                Drop();
            }
        }
    }

    void PickUp(GameObject objectToPick)
    {
        PickedObject = objectToPick;
        PickedObject.GetComponent<PickableObject>().isPickeable = false;
        PickedObject.transform.SetParent(interactionZone);

        // Asegura que esté en el centro del interactionZone
        PickedObject.transform.localPosition = Vector3.zero;
        PickedObject.transform.localRotation = Quaternion.identity;
        PickedObject.transform.localScale = Vector3.one;

        PickedObject.GetComponent<Rigidbody>().useGravity = false;
        PickedObject.GetComponent<Rigidbody>().isKinematic = true;
        if (objectToPick.tag == "taza") { 
            objectToPick.GetComponent<tazaController>().estaSostenido = true;
        }
    }

    void Drop()
    {
        PickedObject.GetComponent<PickableObject>().isPickeable = true;
        PickedObject.transform.SetParent(null);
        PickedObject.GetComponent<Rigidbody>().useGravity = true;
        PickedObject.GetComponent<Rigidbody>().isKinematic = false;
        if (PickedObject.tag == "taza")
        {
            PickedObject.GetComponent<tazaController>().estaSostenido = false;
        }
        PickedObject = null;
    }
}

