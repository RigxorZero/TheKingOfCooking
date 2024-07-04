using UnityEngine;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class Timer : MonoBehaviour
{
    public float duration = 10f; // Duración del cronómetro en segundos
    private float timer;         // Variable para contar el tiempo
    public AudioSource audioSource;
    [SerializeField] public static bool isRunning = false; // Variable para saber si el cronómetro está en marcha

    public TextMeshProUGUI timerText; // Referencia al TextMeshPro

    void Start()
    {
        // Inicializar el cronómetro
        timer = duration;
        UpdateTimerDisplay();
    }

    void Update()
    {
        if (isRunning)
        {
            // Reducir el cronómetro con el tiempo transcurrido
            timer -= Time.deltaTime;

            // Actualizar el texto del cronómetro
            UpdateTimerDisplay();
            if (timer <= 50f){

                audioSource.pitch = 1.3f;
            }

            // Comprobar si el cronómetro ha llegado a cero
            if (timer <= 0f)
            {
                // Acciones cuando el cronómetro llegue a cero
                timer = 0f;
                isRunning = false;
                Debug.Log("El cronómetro ha terminado para " + gameObject.name);
                SceneManager.LoadScene(2);
            }
        }
    }

    // Método para iniciar el cronómetro
    public void StartTimer()
    {
        isRunning = true;
        timer = duration;
    }

    // Método para detener el cronómetro
    public void StopTimer()
    {
        isRunning = false;
    }

    // Método para actualizar el texto del cronómetro
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


