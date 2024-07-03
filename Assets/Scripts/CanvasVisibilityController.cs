using UnityEngine;

public class CanvasVisibilityController : MonoBehaviour
{
    [SerializeField] private Canvas canvas; // El Canvas que se mostrar� o esconder�

    void Start()
    {
        // Aseg�rate de que el canvas est� desactivado al inicio
        canvas.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que entra al trigger tiene la etiqueta "Player"
        if (other.CompareTag("PlayerInteractionZone"))
        {
            // Activa el canvas
            canvas.enabled = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Verifica si el objeto que sale del trigger tiene la etiqueta "Player"
        if (other.CompareTag("PlayerInteractionZone"))
        {
            // Desactiva el canvas
            canvas.enabled = false;
        }
    }
}

