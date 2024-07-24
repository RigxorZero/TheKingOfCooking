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

    public static List<GameObject> instancias;

    private Animator animator;

    public bool seMovio = false; 

    // Método que se llama cuando se detecta un movimiento en el Input System
    public void OnMove(InputAction.CallbackContext context)
    {
        // Lee el valor del contexto de la acción y lo almacena en la variable move
        if (sePuedeMover)
        {
            move = context.ReadValue<Vector2>();
        }
        else
        {
            // Si no se puede mover, asegura que el movimiento sea cero
            move = Vector2.zero;
        }
    }

    // Método que se llama cuando se detecta la acción de golpear en el Input System
    public void OnHit(InputAction.CallbackContext context)
    {
        // Solo realizar acción si el contexto es de tipo Started
        if (context.started)
        {
            // Cambia el valor del booleano hitting
            animator.SetBool("hitting", !animator.GetBool("hitting"));
        }
    }

    // Método llamado por el AnimationEvent al final de la animación
    public void ResetHitting()
    {
        animator.SetBool("hitting", false);
    }

    // Método llamado antes de la primera actualización del frame
    void Start()
    {
        sePuedeMover = true;
        CantidadDePlayer.cantidadDePlayer++;
        animator = GetComponent<Animator>();
    }

    // Método llamado una vez por frame
    void Update()
    {
        if (sePuedeMover)
        {
            movePlayer();
        }
        //sePuedeMover = CantidadDePlayer.sepuedemover;
    }

    // Método que maneja el movimiento del jugador
    public void movePlayer()
    {
        // Crea un vector de movimiento en el espacio 3D utilizando la entrada del usuario
        Vector3 movement = new Vector3(move.x, 0f, move.y);

        // Si el vector de movimiento no es cero (es decir, hay entrada del usuario)
        if (movement != Vector3.zero)
        {
            seMovio = true;
            // Rotar suavemente al jugador en la dirección del movimiento
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15f);
            // Mueve al jugador en la dirección del movimiento multiplicado por la velocidad y el tiempo entre frames
            transform.Translate(movement * speed * Time.deltaTime, Space.World);
        }
    }
}
