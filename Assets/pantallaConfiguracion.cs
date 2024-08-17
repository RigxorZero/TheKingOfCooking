using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pantallaConfiguracion : MonoBehaviour
{
    public Toggle fullscreenToggle;

    void Start()
    {
        // Inicializar el estado del toggle basado en el estado actual de pantalla completa
        fullscreenToggle.isOn = Screen.fullScreen;

        // Añadir el listener para cuando se cambie el estado del toggle
        fullscreenToggle.onValueChanged.AddListener(delegate { ToggleFullScreen(fullscreenToggle.isOn); });
    }

    void ToggleFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
}
