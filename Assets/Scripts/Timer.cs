using UnityEngine;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class Timer : MonoBehaviour
{
    public float duration = 10f; // Duraci�n del cron�metro en segundos
    private float timer;         // Variable para contar el tiempo
    public AudioSource audioSource;
    [SerializeField] public static bool isRunning = false; // Variable para saber si el cron�metro est� en marcha

    public TextMeshProUGUI timerText; // Referencia al TextMeshPro

    void Start()
    {
        // Inicializar el cron�metro
        timer = duration;
        UpdateTimerDisplay();
    }

    void Update()
    {
        if (isRunning)
        {
            // Reducir el cron�metro con el tiempo transcurrido
            timer -= Time.deltaTime;

            // Actualizar el texto del cron�metro
            UpdateTimerDisplay();
            if (timer <= 50f){

                audioSource.pitch = 1.3f;
            }

            // Comprobar si el cron�metro ha llegado a cero
            if (timer <= 0f)
            {
                // Acciones cuando el cron�metro llegue a cero
                timer = 0f;
                isRunning = false;
                Debug.Log("El cron�metro ha terminado para " + gameObject.name);
                SceneManager.LoadScene(2);
            }
        }
    }

    // M�todo para iniciar el cron�metro
    public void StartTimer()
    {
        isRunning = true;
        timer = duration;
    }

    // M�todo para detener el cron�metro
    public void StopTimer()
    {
        isRunning = false;
    }

    // M�todo para actualizar el texto del cron�metro
    void UpdateTimerDisplay()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(timer / 60);
            int seconds = Mathf.FloorToInt(timer % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}


