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
    private Camera playerCamera;

    [SerializeField] private static bool[,] nivelPerilla = new bool[2, 4]; // 2 jugadores, 4 niveles

    private bool[] canvasActivo = new bool[2];

    private Collider player;

    public InputAction[] CanvasActiveButton = new InputAction[2]; // Botones de activación para dos jugadores
    public InputAction[] ArribaButtom = new InputAction[2]; // Botones para manipular la perilla
    public InputAction[] AbajoButtom = new InputAction[2];
    public InputAction[] DerechaButtom = new InputAction[2];
    public InputAction[] IzquierdaButtom = new InputAction[2];

    public Canvas canvasTimer;

    private int valor = 0;

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

                if (esJugadorUno != null && playerIndex == 0)
                {
                    playerCamera = esJugadorUno.GetComponentInChildren<Camera>();
                    ActivateCanvas(0); // Activar primer canvas en la lista
                }
                else if (esJugadorDos != null && playerIndex == 1)
                {
                    playerCamera = esJugadorDos.GetComponentInChildren<Camera>();
                    ActivateCanvas(1); // Activar segundo canvas en la lista
                }
            }
        }
    }

    void Start()
    {
        // Inicializar nivelPerilla para ambos jugadores
        for (int i = 0; i < 2; i++)
        {
            CanvasActiveButton[i].Enable();
            ArribaButtom[i].Enable();
            AbajoButtom[i].Enable();
            DerechaButtom[i].Enable();
            IzquierdaButtom[i].Enable();
            canvasActivo[i] = false;
        }

        canvasTimer.enabled = false;

        SetNivelPerilla(0, 0);
        SetNivelPerilla(1, 0);

        if (gameObject.layer == 8)
        {
            valor = 1;
        }
        else if (gameObject.layer == 7)
        {
            valor = 0;
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

        GetNivelPerilla(0);
        GetNivelPerilla(1);
    }

    void HandlePlayerInput(int playerIndex)
    {
        if (canvasActivo[playerIndex])
        {

            switch (GetNivelPerilla(valor))
            {
                case 0:
                    perillas[playerIndex].rectTransform.rotation = Quaternion.Euler(0, 0, 0);
                    break;
                case 1:
                    perillas[playerIndex].rectTransform.rotation = Quaternion.Euler(0, 0, 180);
                    break;
                case 2:
                    perillas[playerIndex].rectTransform.rotation = Quaternion.Euler(0, 0, 270);
                    break;
                case 3:
                    perillas[playerIndex].rectTransform.rotation = Quaternion.Euler(0, 0, 90);
                    break;
                default:
                    break;
            }

            if (ArribaButtom[playerIndex].WasReleasedThisFrame()) // Arriba
            {
                perillas[playerIndex].rectTransform.rotation = Quaternion.Euler(0, 0, 0);
                SetNivelPerilla(valor, 0);
            }
            else if (AbajoButtom[playerIndex].WasPressedThisFrame()) // Abajo
            {
                perillas[playerIndex].rectTransform.rotation = Quaternion.Euler(0, 0, 180);
                SetNivelPerilla(valor, 1);
            }
            else if (DerechaButtom[playerIndex].WasPressedThisFrame()) // Derecha
            {
                perillas[playerIndex].rectTransform.rotation = Quaternion.Euler(0, 0, 270);
                SetNivelPerilla(valor, 2);
            }
            else if (IzquierdaButtom[playerIndex].IsPressed()) // Izquierda
            {
                perillas[playerIndex].rectTransform.rotation = Quaternion.Euler(0, 0, 90);
                SetNivelPerilla(valor, 3);
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

    private void SetNivelPerilla(int playerIndex, int valor)
    {
        for (int i = 0; i < 4; i++)
        {
            nivelPerilla[playerIndex, i] = i == valor;
        }
    }

    public void apagarCocina()
    {
        SetNivelPerilla(valor, 0);
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

    int GetNivelPerilla(int playerIndex)
    {
        // Encuentra el nivel de la perilla activado para el jugador especificado
        for (int i = 0; i < 4; i++)
        {
            if (nivelPerilla[playerIndex, i])
            {
                return i; // Retorna el índice del nivel activado
            }
        }
        return -1; // Retorna -1 si no hay ningún nivel activado
    }

    public int GetIntensidadCocina()
    {
        return GetNivelPerilla(valor);
    }
}
