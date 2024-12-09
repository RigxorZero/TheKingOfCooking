using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDrawer : MonoBehaviour
{
    public string objectPrefabName;
    [SerializeField] private bool playerInRange = false;

    public GameObject spritePrefab; // Prefab del objeto 2D que contiene el SpriteRenderer
    private GameObject spawnedSprite; // Referencia al objeto 2D instanciado
    public GameObject spritePrefab2; // Prefab del objeto 2D que contiene el SpriteRenderer
    private GameObject spawnedSprite2; // Referencia al objeto 2D instanciado

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerInteractionZone")
        {
            playerInRange = true;
            ShowSprite();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "PlayerInteractionZone")
        {
            playerInRange = false;
            HideSprite();
        }
    }

    public bool IsPlayerInRange()
    {
        return playerInRange;
    }

    void ShowSprite()
    {
        if (spritePrefab != null && spawnedSprite == null && spritePrefab2 != null)
        {
            float offset = 0.5f; // Distancia entre los sprites

            // Instancia el primer sprite un poco a la izquierda
            spawnedSprite = Instantiate(
                spritePrefab,
                transform.position + Vector3.up * 2.0f + Vector3.left * offset,
                Quaternion.identity
            );

            // Instancia el segundo sprite un poco a la derecha
            spawnedSprite2 = Instantiate(
                spritePrefab2,
                transform.position + Vector3.up * 2.0f + Vector3.right * offset,
                Quaternion.identity
            );

            // Opcional: Haz que los sprites sigan al objeto padre
            spawnedSprite.transform.SetParent(transform);
            spawnedSprite2.transform.SetParent(transform);
        }
    }

    void HideSprite()
    {
        if (spawnedSprite != null)
        {
            Destroy(spawnedSprite); // Destruye el objeto del sprite
            Destroy(spawnedSprite2);
            spawnedSprite = null;
        }
    }
}
