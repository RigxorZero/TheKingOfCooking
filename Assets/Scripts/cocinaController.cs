using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class cocinaController : MonoBehaviour
{
    [SerializeField] private List<Canvas> canvases = new List<Canvas>();
    [SerializeField] private List<Image> perillas = new List<Image>();
    private Camera playerCamera;

    private bool[,] nivelPerilla = new bool[4, 4]; // 4 cocinas, 4 niveles

    private bool[] canvasActivo = new bool[2];
    private bool[] actionPerformed = new bool[2]; // Para evitar múltiples ejecuciones por frame
    private float[] cooldownTimers = new float[2]; // Timers para el cooldown

    private PlayerController playerController;
    public GameObject player;

    public InputAction[] CanvasActiveButton = new InputAction[2]; // Botones de activación para dos jugadores
    public InputAction[] ArribaButtom = new InputAction[2]; // Botones para manipular la perilla
    public InputAction[] AbajoButtom = new InputAction[2];
    public InputAction[] DerechaButtom = new InputAction[2];
    public InputAction[] IzquierdaButtom = new InputAction[2];

    public Canvas canvasTimer;

    private int cocinaIndex = 0;

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "PlayerInteractionZone")
        {
            player = other.gameObject;
            int playerIndex = other.GetComponentInParent<PlayerInput>().playerIndex;
            if (CanvasActiveButton[playerIndex].WasReleasedThisFrame() && !actionPerformed[playerIndex] && cooldownTimers[playerIndex] <= 0)
            {
                Debug.Log("C button was released");
                actionPerformed[playerIndex] = true; // Marcar la acción como realizada
                cooldownTimers[playerIndex] = 0.5f; // Establecer cooldown de 0.5 segundos
                player.GetComponentInParent<PlayerController>().sePuedeMover = false;
                GameObject esJugadorUno = GameObject.FindGameObjectWithTag("JugadorUnoPrefab");
                GameObject esJugadorDos = GameObject.FindGameObjectWithTag("JugadorDosPrefab");

                if (esJugadorUno != null && playerIndex == 0)
                {
                    playerCamera = esJugadorUno.GetComponentInChildren<Camera>();
                    playerController = esJugadorUno.GetComponent<PlayerController>();
                    ActivateCanvas(playerIndex, cocinaIndex);
                }
                else if (esJugadorDos != null && playerIndex == 1)
                {
                    playerCamera = esJugadorDos.GetComponentInChildren<Camera>();
                    playerController = esJugadorDos.GetComponent<PlayerController>();
                    ActivateCanvas(playerIndex, cocinaIndex);
                }
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
            actionPerformed[i] = false; // Inicializar la bandera
            cooldownTimers[i] = 0; // Inicializar el cooldown
        }

        canvasTimer.enabled = false;

        if (gameObject.layer == 8)
        {
            cocinaIndex = 1;
        }
        else if (gameObject.layer == 7)
        {
            cocinaIndex = 0;
        }
        else if (gameObject.layer == 9)
        {
            cocinaIndex = 2;
        }
        else if (gameObject.layer == 10)
        {
            cocinaIndex = 3;
        }

        SetNivelPerilla(0, 0);
        SetNivelPerilla(1, 0);
        SetNivelPerilla(2, 0);
        SetNivelPerilla(3, 0);

        // Desactivar todos los canvases al inicio
        foreach (var canvas in canvases)
        {
            canvas.enabled = false;
        }

        // Imprimir el contenido de nivelPerilla
        ImprimirNivelPerilla();
    }

    private void ImprimirNivelPerilla()
    {
        string resultado = "NivelPerilla: \n";
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                resultado += $"Cocina {i} - Nivel {j}: {(nivelPerilla[i, j] ? "Activado" : "Desactivado")}\n";
            }
        }
        Debug.Log(resultado);
    }

    void Update()
    {
        for (int i = 0; i < 2; i++)
        {
            if (cooldownTimers[i] > 0)
            {
                cooldownTimers[i] -= Time.deltaTime;
            }
        }

        HandlePlayerInput(0); // Jugador 1
        HandlePlayerInput(1); // Jugador 2

        GetNivelPerilla(0);
        GetNivelPerilla(1);
        GetNivelPerilla(2);
        GetNivelPerilla(3);

        // Resetear la marca de acción realizada al final del frame
        for (int i = 0; i < 2; i++)
        {
            if (!CanvasActiveButton[i].WasReleasedThisFrame())
            {
                actionPerformed[i] = false;
            }
        }
    }

    void HandlePlayerInput(int playerIndex)
    {
        if (canvasActivo[playerIndex])
        {
            switch (GetNivelPerilla(cocinaIndex))
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

            if (ArribaButtom[playerIndex].WasReleasedThisFrame() && !actionPerformed[playerIndex]) // Arriba
            {
                actionPerformed[playerIndex] = true; // Marcar la acción como realizada
                ReferenciaPlayer.player1.GetComponent<playerTutorial>().cambiarTemperatura = true;
                if (ReferenciaPlayer.player1.GetComponent<playerTutorial>().aguaEnLaOlla == true)
                {
                    ReferenciaPlayer.player1.GetComponent<playerTutorial>().cambioTemperatura2 = true;
                }
                perillas[playerIndex].rectTransform.rotation = Quaternion.Euler(0, 0, 0);
                SetNivelPerilla(cocinaIndex, 0);
            }
            else if (AbajoButtom[playerIndex].WasReleasedThisFrame() && !actionPerformed[playerIndex]) // Abajo
            {
                actionPerformed[playerIndex] = true; // Marcar la acción como realizada
                ReferenciaPlayer.player1.GetComponent<playerTutorial>().cambiarTemperatura = true;
                if (ReferenciaPlayer.player1.GetComponent<playerTutorial>().aguaEnLaOlla == true)
                {
                    ReferenciaPlayer.player1.GetComponent<playerTutorial>().cambioTemperatura2 = true;
                }
                perillas[playerIndex].rectTransform.rotation = Quaternion.Euler(0, 0, 180);
                SetNivelPerilla(cocinaIndex, 1);
            }
            else if (DerechaButtom[playerIndex].WasReleasedThisFrame() && !actionPerformed[playerIndex]) // Derecha
            {
                actionPerformed[playerIndex] = true; // Marcar la acción como realizada
                ReferenciaPlayer.player1.GetComponent<playerTutorial>().cambiarTemperatura = true;
                if (ReferenciaPlayer.player1.GetComponent<playerTutorial>().aguaEnLaOlla == true)
                {
                    ReferenciaPlayer.player1.GetComponent<playerTutorial>().cambioTemperatura2 = true;
                }
                perillas[playerIndex].rectTransform.rotation = Quaternion.Euler(0, 0, 270);
                SetNivelPerilla(cocinaIndex, 2);
            }
            else if (IzquierdaButtom[playerIndex].WasReleasedThisFrame() && !actionPerformed[playerIndex]) // Izquierda
            {
                actionPerformed[playerIndex] = true; // Marcar la acción como realizada
                ReferenciaPlayer.player1.GetComponent<playerTutorial>().cambiarTemperatura = true;
                if (ReferenciaPlayer.player1.GetComponent<playerTutorial>().aguaEnLaOlla == true)
                {
                    ReferenciaPlayer.player1.GetComponent<playerTutorial>().cambioTemperatura2 = true;
                }
                perillas[playerIndex].rectTransform.rotation = Quaternion.Euler(0, 0, 90);
                SetNivelPerilla(cocinaIndex, 3);
            }

            if (CanvasActiveButton[playerIndex].WasReleasedThisFrame() && canvasActivo[playerIndex] && cooldownTimers[playerIndex] <= 0)
            {
                //tutorial
                ReferenciaPlayer.player1.GetComponent<playerTutorial>().cerrarCocina = true;
                if (!ReferenciaPlayer.player1.GetComponent<playerTutorial>().cambioTimer)
                {
                    ReferenciaPlayer.player1.GetComponent<playerTutorial>().tiempoActual = ReferenciaPlayer.player1.GetComponent<playerTutorial>().tiempoEntreCambio;
                    ReferenciaPlayer.player1.GetComponent<playerTutorial>().cambioTimer = true;
                }

                canvasActivo[playerIndex] = false;
                canvases[playerIndex].enabled = false;
                canvases[playerIndex].worldCamera = null;
                player.GetComponentInParent<PlayerController>().sePuedeMover = true;

                cooldownTimers[playerIndex] = 0.5f; // Establecer cooldown de 0.5 segundos
            }
        }

        playerCamera = null;
    }

    private void SetNivelPerilla(int cocinaIndex, int valor)
    {
        for (int i = 0; i < 4; i++)
        {
            nivelPerilla[cocinaIndex, i] = i == valor;
        }
    }

    public void apagarCocina()
    {
        SetNivelPerilla(cocinaIndex, 0);
    }

    private void ActivateCanvas(int playerIndex, int cocinaIndex)
    {
        // Activar el canvas específico por índice
        actionPerformed[playerIndex] = true; // Marcar la acción como realizada
        ReferenciaPlayer.player1.GetComponent<playerTutorial>().abrirCocina = true;
        canvases[playerIndex].enabled = true;
        canvases[playerIndex].renderMode = RenderMode.ScreenSpaceCamera;
        canvases[playerIndex].worldCamera = playerCamera;
        canvases[playerIndex].planeDistance = 1;
        canvasActivo[playerIndex] = true;
    }

    int GetNivelPerilla(int cocinaIndex)
    {
        // Encuentra el nivel de la perilla activado para la cocina especificada
        for (int i = 0; i < 4; i++)
        {
            if (nivelPerilla[cocinaIndex, i])
            {
                return i; // Retorna el índice del nivel activado
            }
        }
        return -1; // Retorna -1 si no hay ningún nivel activado
    }

    public int GetIntensidadCocina()
    {
        return GetNivelPerilla(cocinaIndex);
    }
}
