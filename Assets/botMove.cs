using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class botMove : MonoBehaviour
{
    public float speed = 3.0f; // Velocidad del bot
    public float changeDirectionTime = 2.0f; // Tiempo en segundos antes de cambiar de direcci�n
    public bool sePuedeMover = true; // Determina si el bot puede moverse

    private Vector3 randomDirection; // Direcci�n aleatoria del bot
    private float timer; // Temporizador para cambiar de direcci�n

    public bool stuneactivo;
    public float tiempoActual;
    public float tiempoEntrecambio;
    void Start()
    {
        // Inicializa el temporizador y la direcci�n aleatoria
        timer = changeDirectionTime;
        randomDirection = GetRandomDirection();
    }

    void Update()
    {
        if (tiempoActual >= 0)
        {
            tiempoActual -= Time.deltaTime;
        }
        else
        {
            if (stuneactivo)
            {
                stuneactivo = false;
                sePuedeMover = true;
            }
        }

        if (!sePuedeMover) return; // Si no se puede mover, no hace nada

        // Decrementa el temporizador
        timer -= Time.deltaTime;

        // Si el temporizador llega a cero, cambia la direcci�n aleatoria
        if (timer <= 0)
        {
            randomDirection = GetRandomDirection();
            timer = changeDirectionTime; // Reinicia el temporizador
        }

        
        // Mueve el bot en la direcci�n aleatoria
        MoveBot();
    }

    Vector3 GetRandomDirection()
    {
        // Obtiene una direcci�n aleatoria en el plano XZ
        float randomAngle = Random.Range(0f, 360f);
        return new Vector3(Mathf.Cos(randomAngle), 0, Mathf.Sin(randomAngle));
    }

    void MoveBot()
    {
        // Mueve el bot en la direcci�n actual
        transform.Translate(randomDirection * speed * Time.deltaTime, Space.World);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Si el bot choca con un objeto con el tag "pared", cambia de direcci�n
        if (collision.gameObject.tag == "pared")
        {
            randomDirection = GetRandomDirection();
        }
    }
    public void stune()
    {
        stuneactivo = true;
        sePuedeMover = false;
        tiempoActual = tiempoEntrecambio;
    }
}
