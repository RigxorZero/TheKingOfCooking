using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private Vector2 move;

    public bool sePuedeMover;

    public static List<GameObject> instancias;

    private Animator animator;

    public bool seMovio = false;

    public GameObject hitObject; // El objeto que representa el golpe

    public void OnMove(InputAction.CallbackContext context)
    {
        if (sePuedeMover)
        {
            move = context.ReadValue<Vector2>();
        }
        else
        {
            move = Vector2.zero;
        }
    }

    public void OnHit(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetBool("hitting", !animator.GetBool("hitting"));

            // Asignar la referencia del jugador al objeto de golpe
            HitCollider hitCollider = hitObject.GetComponent<HitCollider>();
            if (hitCollider != null)
            {
                hitCollider.SetHittingPlayer(gameObject);
            }
        }
    }

    public void ResetHitting()
    {
        animator.SetBool("hitting", false);
    }

    void Start()
    {
        sePuedeMover = true;
        CantidadDePlayer.cantidadDePlayer++;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (sePuedeMover)
        {
            movePlayer();
        }
    }

    public void movePlayer()
    {
        Vector3 movement = new Vector3(move.x, 0f, move.y);

        if (movement != Vector3.zero)
        {
            seMovio = true;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15f);
            transform.Translate(movement * speed * Time.deltaTime, Space.World);
        }
    }
}
