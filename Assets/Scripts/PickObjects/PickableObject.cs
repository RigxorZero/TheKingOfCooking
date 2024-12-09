using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObject : MonoBehaviour
{
    public bool isPickeable = true;
    public bool drop = false;
    public bool sostenido = false;
    public bool eliminado = false;
    [SerializeField] public int type;

    public GameObject spritePrefab; // Primer prefab de sprite
    public GameObject spritePrefab2; // Segundo prefab de sprite

    private GameObject spawnedSprite; // Instancia del primer sprite
    private GameObject spawnedSprite2; // Instancia del segundo sprite

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "basurero")
        {
            eliminado = true;
            if (sostenido)
            {
                ReferenciaPlayer.player1.GetComponent<playerTutorial>().botarBasura = true;
                Destroy(gameObject);
            }
        }

        if (other.tag == "PlayerInteractionZone")
        {
            other.GetComponentInParent<PickUpObject>().ObjectToPickUp = this.gameObject;
            this.transform.rotation = Quaternion.Euler(0, 0, 0);

            // Mostrar los sprites al entrar en rango
            ShowSprites();
        }

        if (other.tag == "MesaInteractiveZone" && isPickeable)
        {
            if (other.GetComponent<mesaInteractiva>().type == type)
            {
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
        if (other.tag == "PlayerInteractionZone")
        {
            other.GetComponentInParent<PickUpObject>().ObjectToPickUp = null;

            // Ocultar los sprites al salir de rango
            HideSprites();
        }
    }

    private void ShowSprites()
    {
        if (spritePrefab != null && spawnedSprite == null && spritePrefab2 != null)
        {
            float offset = 0.5f; // Ajuste de separación entre los sprites

            // Instanciar el primer sprite
            spawnedSprite = Instantiate(
                spritePrefab,
                transform.position + Vector3.up * 2.0f + Vector3.left * offset,
                Quaternion.identity
            );

            // Instanciar el segundo sprite
            spawnedSprite2 = Instantiate(
                spritePrefab2,
                transform.position + Vector3.up * 2.0f + Vector3.right * offset,
                Quaternion.identity
            );

            // Hacer que sigan al objeto padre
            spawnedSprite.transform.SetParent(transform);
            spawnedSprite2.transform.SetParent(transform);
        }
    }

    private void HideSprites()
    {
        // Destruir los sprites si existen
        if (spawnedSprite != null)
        {
            Destroy(spawnedSprite);
            spawnedSprite = null;
        }
        if (spawnedSprite2 != null)
        {
            Destroy(spawnedSprite2);
            spawnedSprite2 = null;
        }
    }
}
