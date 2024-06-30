using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Velocidad de movimiento del jugador
    public float speed;
    // Dirección del movimiento
    private Vector2 move;

    public bool sePuedeMover; 

    // Método que se llama cuando se detecta un movimiento en el Input System
    public void OnMove(InputAction.CallbackContext context)
    {
        // Lee el valor del contexto de la acción y lo almacena en la variable move
        move = context.ReadValue<Vector2>();
    }

    // Método llamado antes de la primera actualización del frame
    void Start()
    {
        sePuedeMover = true;   
    }

    // Método llamado una vez por frame
    void Update()
    {
        if (sePuedeMover) {
            movePlayer();
        }
        
    }

    // Método que maneja el movimiento del jugador
    public void movePlayer()
    {
        // Crea un vector de movimiento en el espacio 3D utilizando la entrada del usuario
        Vector3 movement = new Vector3(move.x, 0f, move.y);

        // Si el vector de movimiento no es cero (es decir, hay entrada del usuario)
        if (movement != Vector3.zero)
        {
            // Rotar suavemente al jugador en la dirección del movimiento
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15f);
            // Mueve al jugador en la dirección del movimiento multiplicado por la velocidad y el tiempo entre frames
            transform.Translate(movement * speed * Time.deltaTime, Space.World);
        }
    }
}

