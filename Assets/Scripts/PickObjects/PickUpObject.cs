using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickUpObject : MonoBehaviour
{
    public GameObject ObjectToPickUp; // Objeto actual que puede ser recogido
    [SerializeField] private GameObject PickedObject; // Objeto actualmente recogido
    [SerializeField] private Transform interactionZone; // Zona de interacción para sostener objetos

    public InputAction recoger; // Acción de entrada para recoger/soltar
    public bool sarten; // Bandera específica para sartenes

    private void Start()
    {
        // Inicializar el InputAction "Recoger"
        recoger = GetComponent<PlayerInput>().actions.FindAction("Recoger");

        if (recoger == null)
        {
            Debug.LogError("No se encontró el InputAction 'Recoger'. Asegúrate de asignarlo en el Inspector.");
        }

        recoger.Enable(); // Activar la acción de entrada
    }

    void Update()
    {
        // Buscar todos los cajones en la escena
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
            else
            {
                // Caso: Instanciar y recoger un objeto desde un cajón
                foreach (ObjectDrawer drawer in drawers)
                {
                    if (drawer.IsPlayerInRange() && recoger.WasPressedThisFrame())
                    {
                        // Cargar el prefab desde Resources
                        GameObject prefab = Resources.Load<GameObject>(drawer.objectPrefabName);

                        if (prefab != null)
                        {
                            // Instanciar el prefab y recogerlo
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
        // Configuración inicial del objeto recogido
        PickedObject = objectToPick;
        PickedObject.GetComponent<PickableObject>().isPickeable = false;
        PickedObject.transform.SetParent(interactionZone);
        PickedObject.GetComponent<PickableObject>().sostenido = false;

        // Asegurar la posición, rotación y escala
        PickedObject.transform.localPosition = Vector3.zero;
        PickedObject.transform.localRotation = Quaternion.identity;
        PickedObject.transform.localScale = Vector3.one;

        // Desactivar física del objeto
        var rb = PickedObject.GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = true;

        // Comportamiento basado en etiquetas específicas
        HandleTagSpecificBehaviors(objectToPick, true);
    }

    void Drop()
    {
        // Restaurar física y comportamientos del objeto al soltarlo
        var rb = PickedObject.GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.isKinematic = false;

        PickedObject.GetComponent<PickableObject>().isPickeable = true;
        PickedObject.GetComponent<PickableObject>().sostenido = true;
        PickedObject.transform.SetParent(null);

        // Comportamiento basado en etiquetas específicas
        HandleTagSpecificBehaviors(PickedObject, false);

        PickedObject = null;
    }

    void HandleTagSpecificBehaviors(GameObject obj, bool isPickedUp)
    {
        switch (obj.tag)
        {
            case "olla":
                ReferenciaPlayer.player1.GetComponent<playerTutorial>().tomoOlla = true;
                break;

            case "sarten":
                obj.GetComponent<sartenController>().estaSostenido = isPickedUp;
                break;

            case "taza":
                obj.GetComponent<tazaController>().estaSostenido = true;
                ReferenciaPlayer.player1.GetComponent<playerTutorial>().tomarTaza = isPickedUp;
                break;

            case "escencia":
                obj.GetComponent<escenciaController>().estaSostenido = isPickedUp;
                break;

            case "sal":
                obj.GetComponent<salController>().estaSostenido = isPickedUp;
                ReferenciaPlayer.player1.GetComponent<playerTutorial>().tomarSal = true;
                break;

            case "plato":
                ReferenciaPlayer.player1.GetComponent<playerTutorial>().tomarPlato = true;
                break;

            default:
                Debug.LogWarning("Etiqueta no manejada: " + obj.tag);
                break;
        }
    }

    public void Stunned()
    {
        // Soltar el objeto cuando el jugador queda aturdido
        if (PickedObject != null)
        {
            Drop();
        }
    }
}
