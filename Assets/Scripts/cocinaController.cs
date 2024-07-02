using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class cocinaController : MonoBehaviour
{
    [SerializeField] private List<Canvas> canvases = new List<Canvas>();
    [SerializeField] private Image[] perillas;
    [SerializeField] private Camera playerCamera;

    [SerializeField] private bool[] nivelPerilla = new bool[4];

    private bool[] canvasActivo = new bool[2];

    private Collider player;

    public InputAction[] CanvasActiveButton = new InputAction[2]; // Botones de activación para dos jugadores
    public InputAction[] ArribaButtom = new InputAction[2]; // Botones para manipular la perilla
    public InputAction[] AbajoButtom = new InputAction[2];
    public InputAction[] DerechaButtom = new InputAction[2];
    public InputAction[] IzquierdaButtom = new InputAction[2];
    private int valor = -1;

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "PlayerInteractionZone")
        {
            int playerIndex = other.GetComponentInParent<PlayerInput>().playerIndex;
            if (CanvasActiveButton[playerIndex].WasPressedThisFrame())
            {
                other.GetComponentInParent<PlayerController>().sePuedeMover = false;
                player = other;

                GameObject esJugadorUno = GameObject.FindGameObjectWithTag("JugadorUnoPrefab");
                GameObject esJugadorDos = GameObject.FindGameObjectWithTag("JugadorDosPrefab");

                
                if (esJugadorUno != null && other.GetComponentInParent<PlayerInput>().playerIndex == 0)
                {
                    playerCamera = esJugadorUno.GetComponentInChildren<Camera>();
                    ActivateCanvas(0); // Activar primer canvas en la lista
                }
                else if (esJugadorDos != null && other.GetComponentInParent<PlayerInput>().playerIndex == 1)
                {
                    playerCamera = esJugadorDos.GetComponentInChildren<Camera>();
                    ActivateCanvas(1); // Activar segundo canvas en la lista
                }
            }
        }
    }

    void Start()
    {
        // Inicializar nivelPerilla
        for (int i = 0; i < nivelPerilla.Length; i++)
        {
            nivelPerilla[i] = false;
            
        }

        for (int i = 0; i < 2; i++)
        {
            CanvasActiveButton[i].Enable();
            ArribaButtom[i].Enable();
            AbajoButtom[i].Enable();
            DerechaButtom[i].Enable();
            IzquierdaButtom[i].Enable();
            canvasActivo[i] = false;
        }

        // Desactivar todos los canvases al inicio
        foreach (var canvas in canvases)
        {
            canvas.enabled = false;
        }
    }

    void Update()
    {
            HandlePlayerInput(0); // Jugador 1
            HandlePlayerInput(1); // Jugador 2
  
    }

    void HandlePlayerInput(int playerIndex)
    {
        Debug.Log($"Jugador {playerIndex} apreto W: {ArribaButtom[playerIndex].WasPressedThisFrame()}");

        if (canvasActivo[playerIndex])
        {
            if (ArribaButtom[playerIndex].WasReleasedThisFrame()) // Arriba
            {
                perillas[playerIndex].rectTransform.rotation = Quaternion.Euler(0, 0, 0);
                nivelPerilla[0] = true;
                nivelPerilla[1] = false;
                nivelPerilla[2] = false;
                nivelPerilla[3] = false;
            }
            else if (AbajoButtom[playerIndex].WasPressedThisFrame()) // Abajo
            {
                perillas[playerIndex].rectTransform.rotation = Quaternion.Euler(0, 0, 180);
                nivelPerilla[0] = false;
                nivelPerilla[1] = true;
                nivelPerilla[2] = false;
                nivelPerilla[3] = false;
            }
            else if (DerechaButtom[playerIndex].WasPressedThisFrame()) // Derecha
            {
                perillas[playerIndex].rectTransform.rotation = Quaternion.Euler(0, 0, 270);
                nivelPerilla[0] = false;
                nivelPerilla[1] = false;
                nivelPerilla[2] = true;
                nivelPerilla[3] = false;
            }
            else if (IzquierdaButtom[playerIndex].IsPressed()) // Izquierda
            {
                perillas[playerIndex].rectTransform.rotation = Quaternion.Euler(0, 0, 90);
                nivelPerilla[0] = false;
                nivelPerilla[1] = false;
                nivelPerilla[2] = false;
                nivelPerilla[3] = true;
            }

            if (CanvasActiveButton[playerIndex].WasPressedThisFrame() && canvasActivo[playerIndex])
            {
                player.GetComponentInParent<PlayerController>().sePuedeMover = true;
                canvasActivo[playerIndex] = false;
                canvases[playerIndex].enabled = false;
                canvases[playerIndex].worldCamera = null;
            }
        }

        playerCamera = null;
    }

    private void SetCanvasViewport(RectTransform canvasRect, Rect viewport)
    {
        canvasRect.anchorMin = new Vector2(viewport.x, viewport.y);
        canvasRect.anchorMax = new Vector2(viewport.x + viewport.width, viewport.y + viewport.height);
        canvasRect.offsetMin = Vector2.zero;
        canvasRect.offsetMax = Vector2.zero;
    }

    private void ActivateCanvas(int index)
    {

        // Activar el canvas específico por índice
        canvases[index].enabled = true;
        canvases[index].renderMode = RenderMode.ScreenSpaceCamera;
        canvases[index].worldCamera = playerCamera;
        canvases[index].planeDistance = 1;

        canvasActivo[index] = true;
    }
}