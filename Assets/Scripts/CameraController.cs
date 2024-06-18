using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // El objetivo que la cámara seguirá
    public Vector3 offset; // La distancia entre la cámara y el objetivo

    // Ajusta la posición y rotación de la cámara en Start
    void Start()
    {
        // Posición inicial de la cámara
        transform.position = target.position + offset;

        // Configura la cámara para que mire al objetivo
        transform.LookAt(target);
    }

    void LateUpdate()
    {
        // Mantén la cámara en la posición deseada relativa al objetivo
        transform.position = target.position + offset;
    }
}

