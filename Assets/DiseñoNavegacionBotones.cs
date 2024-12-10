using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DiseñoNavegacionBotones : MonoBehaviour
{
    [System.Serializable]
    public class ButtonImagePair
    {
        public Button button;       // Botón asociado
        public GameObject image;    // Imagen asociada
    }

    public ButtonImagePair[] buttonImagePairs; // Lista de botones con sus imágenes asociadas

    private Vector3 defaultScale = new Vector3(1.2f, 1.5f, 1.2f); // Escala inicial
    private Vector3 enlargedScale = new Vector3(1.5f, 1.8f, 1.5f); // Escala agrandada

    private bool[] isHovered;

    private GameObject lastSelectedButton = null; // Último botón seleccionado para restaurar escala e imagen
    void Start()
    {
        // Inicializar imágenes como ocultas (excepto la primera)
        for (int i = 0; i < buttonImagePairs.Length; i++)
        {
            if (buttonImagePairs[i].image != null)
            {
                buttonImagePairs[i].image.SetActive(i == 0); // Mostrar solo la imagen 1
            }
        }
    }
    void Update()
    {
        // Obtener el botón actualmente seleccionado por el EventSystem
        GameObject currentSelected = EventSystem.current.currentSelectedGameObject;

        // Verificar si el botón seleccionado cambió
        if (currentSelected != lastSelectedButton)
        {
            // Restaurar el estado del botón previamente seleccionado
            if (lastSelectedButton != null)
            {
                ResetButtonState(lastSelectedButton);
            }

            // Aplicar el efecto al nuevo botón seleccionado
            if (currentSelected != null)
            {
                ApplyButtonState(currentSelected);
            }

            // Actualizar el último botón seleccionado
            lastSelectedButton = currentSelected;
        }
    }

    private void ResetButtonState(GameObject buttonObject)
    {
        // Buscar el par asociado al botón
        foreach (var pair in buttonImagePairs)
        {
            if (pair.button != null && pair.button.gameObject == buttonObject)
            {
                // Restaurar escala y ocultar imagen
                pair.button.transform.localScale = defaultScale;
                if (pair.image != null)
                {
                    pair.image.SetActive(false);
                }
                break;
            }
        }
    }

    private void ApplyButtonState(GameObject buttonObject)
    {
        // Buscar el par asociado al botón
        foreach (var pair in buttonImagePairs)
        {
            if (pair.button != null && pair.button.gameObject == buttonObject)
            {
                // Cambiar escala y mostrar imagen
                pair.button.transform.localScale = enlargedScale;
                if (pair.image != null)
                {
                    pair.image.SetActive(true);
                }
                break;
            }
        }
    }
}
