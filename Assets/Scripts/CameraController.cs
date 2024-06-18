using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // El objetivo que la c�mara seguir�
    public Vector3 offset; // La distancia entre la c�mara y el objetivo

    // Ajusta la posici�n y rotaci�n de la c�mara en Start
    void Start()
    {
        // Posici�n inicial de la c�mara
        transform.position = target.position + offset;

        // Configura la c�mara para que mire al objetivo
        transform.LookAt(target);
    }

    void LateUpdate()
    {
        // Mant�n la c�mara en la posici�n deseada relativa al objetivo
        transform.position = target.position + offset;
    }
}

