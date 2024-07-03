using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickUpObject : MonoBehaviour
{
    public GameObject ObjectToPickUp;
    [SerializeField] private GameObject PickedObject;
    [SerializeField] private Transform interactionZone;

    public InputAction recoger;
    public bool sarten; 
    private void Start()
    {
        // Obtener la referencia al InputAction desde el jugador (asignar en el Inspector)
        recoger = GetComponent<PlayerInput>().actions.FindAction("Recoger");

        if (recoger == null)
        {
            Debug.LogError("No se encontró el InputAction 'Recoger'. Asegúrate de asignarlo en el Inspector.");
        }

        // Habilitar el InputAction para esta instancia
        recoger.Enable();
    }
    void Update()
    {
        ObjectDrawer[] drawers = FindObjectsOfType<ObjectDrawer>();

        if (PickedObject == null)
        {
            // Caso: Recoger un objeto existente
            if (ObjectToPickUp != null && ObjectToPickUp.GetComponent<PickableObject>().isPickeable)
            {
                if (recoger.WasPressedThisFrame())
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
                        if (recoger.WasPressedThisFrame())
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
            if (recoger.WasPressedThisFrame())
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
        if (objectToPick.tag == "sarten")
        {
            PickedObject.GetComponent<sartenController>().estaSostenido = true;
        }
        if (objectToPick.tag == "taza") { 
            objectToPick.GetComponent<tazaController>().estaSostenido = true;
        }
        if(objectToPick.tag == "escencia")
        {
            objectToPick.GetComponent<escenciaController>().estaSostenido = true;
        }
        if (objectToPick.tag == "sal")
        {
            objectToPick.GetComponent<salController>().estaSostenido = true;
        }

    }

    void Drop()
    {
        if (PickedObject.tag == "sarten")
        {
            PickedObject.GetComponent<sartenController>().estaSostenido = false;
        }
        
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

