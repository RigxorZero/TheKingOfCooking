using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class mostrarRecetaMultiplayer : MonoBehaviour
{
    [SerializeField] private List<Canvas> recetaCanvases = new List<Canvas>();
    private Camera playerCamera;
    private bool recetaActiva = false;
    private bool actionPerformed = false; // Para evitar múltiples ejecuciones por frame
    private float cooldownTimer = 0.5f; // Cooldown de 0.5 segundos
    private PlayerController playerController;
    public InputAction interaccion;
    public GameObject player;

    void Start()
    {
        interaccion.Enable();
        // Desactivar todos los canvas al inicio
        foreach (var canvas in recetaCanvases)
        {
            canvas.enabled = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "JugadorUno" || other.tag == "JugadorDos")
        {
            player = other.gameObject;
            int playerIndex = other.GetComponentInParent<PlayerInput>().playerIndex;

            if (interaccion.WasReleasedThisFrame() && !actionPerformed && cooldownTimer <= 0)
            {
                actionPerformed = true; // Marcar la acción como realizada
                cooldownTimer = 0.5f; // Reiniciar cooldown de 0.5 segundos
                playerController = other.GetComponentInParent<PlayerController>();

                if (recetaActiva)
                {
                    // Desactivar todos los canvas
                    foreach (var canvas in recetaCanvases)
                    {
                        canvas.enabled = false;
                        canvas.worldCamera = null;
                    }
                    playerController.sePuedeMover = true;
                }
                else
                {
                    GameObject esJugadorUno = GameObject.FindGameObjectWithTag("JugadorUnoPrefab");
                    GameObject esJugadorDos = GameObject.FindGameObjectWithTag("JugadorDosPrefab");

                    if (esJugadorUno != null && playerIndex == 0)
                    {
                        playerCamera = esJugadorUno.GetComponentInChildren<Camera>();
                    }
                    else if (esJugadorDos != null && playerIndex == 1)
                    {
                        playerCamera = esJugadorDos.GetComponentInChildren<Camera>();
                    }

                    // Activar todos los canvas
                    foreach (var canvas in recetaCanvases)
                    {
                        canvas.enabled = true;
                        canvas.renderMode = RenderMode.ScreenSpaceCamera;
                        canvas.worldCamera = playerCamera;
                        canvas.planeDistance = 1;
                    }
                    playerController.sePuedeMover = false;
                }

                recetaActiva = !recetaActiva;
                other.GetComponentInParent<playerTutorial>().mostroReceta = recetaActiva;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other != null)
        {
            player = null;
        }
    }

    void Update()
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }

        if (!interaccion.WasReleasedThisFrame())
        {
            actionPerformed = false;
        }
    }
}
