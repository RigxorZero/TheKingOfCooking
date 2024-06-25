using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDrawer : MonoBehaviour
{
    public string objectPrefabName;
    [SerializeField] private bool playerInRange = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerInteractionZone")
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "PlayerInteractionZone")
        {
            playerInRange = false;
        }
    }

    public bool IsPlayerInRange()
    {
        return playerInRange;
    }
}
