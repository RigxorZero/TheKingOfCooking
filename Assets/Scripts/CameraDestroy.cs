using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraDestroy : MonoBehaviour
{
    [SerializeField] private PlayerInputManager playerInputManager;
    [SerializeField] private Camera cameraDefault;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private GameObject player;
    [SerializeField] private bool isTutorial = false; // Variable para determinar si es tutorial
    private bool playerOneIsCreated = false;

    public GameObject spawnUno;
    public GameObject spawnDos;

    void Start()
    {
        if (isTutorial)
        {
            // Crear y configurar al jugador 1 directamente si es tutorial
            CreatePlayerOne();
        }
        else
        {
            // Registra el evento de unión de jugadores
            playerInputManager.onPlayerJoined += OnPlayerJoined;
            // Modifica el prefab instanciado para el primer jugador
            playerInputManager.playerPrefab.name = "JugadorUnoPrefab";
            playerInputManager.playerPrefab.tag = "JugadorUnoPrefab";
        }
    }

    void CreatePlayerOne()
    {
        // Simular la creación del primer jugador
        GameObject playerInstance = Instantiate(playerInputManager.playerPrefab, spawnUno.transform.position, spawnUno.transform.rotation);
        playerInstance.name = "JugadorUno";
        playerInstance.tag = "JugadorUno";
        ReferenciaPlayer.player1 = playerInstance;
        playerOneIsCreated = true;

        // Opcional: Configurar el mapa de acciones para el jugador
        PlayerInput playerInput = playerInstance.GetComponent<PlayerInput>();
        if (playerInput != null)
        {
            playerInput.SwitchCurrentActionMap("Player");
        }

        // Desactivar la cámara predeterminada si está configurada
        if (cameraDefault != null)
        {
            cameraDefault.enabled = false;
        }
    }

    void OnPlayerJoined(PlayerInput playerInput)
    {
        if (playerInputManager.playerCount <= 1 && playerInput.playerIndex == 0 && playerOneIsCreated == false)
        {
            // Cambia el mapa de acciones del primer jugador a "Player"
            playerInput.SwitchCurrentActionMap("Player");
            player = playerInput.gameObject;

            // Modifica el prefab instanciado para el primer jugador
            playerInputManager.playerPrefab.name = "JugadorUnoPrefab";
            playerInputManager.playerPrefab.tag = "JugadorUnoPrefab";
            // Establece la posición y rotación del jugador en el punto de spawnUno
            player.transform.position = spawnUno.transform.position;
            player.transform.rotation = spawnUno.transform.rotation;

            // Modifica el jugador instanciado
            player.name = "JugadorUno";
            player.tag = "JugadorUno";
            ReferenciaPlayer.player1 = player;
            playerOneIsCreated = true;
        }
        else if (playerInputManager.playerCount >= 2 && playerInput.playerIndex == 1 && playerOneIsCreated)
        {
            Timer.isRunning = true;
            // Cambia el mapa de acciones del segundo jugador a "Player2"
            playerInput.SwitchCurrentActionMap("Player2");
            player = playerInput.gameObject;

            // Establece la posición y rotación del jugador en el punto de spawnDos
            player.transform.position = spawnDos.transform.position;
            player.transform.rotation = spawnDos.transform.rotation;

            // Modifica el jugador instanciado
            player.name = "JugadorDos";
            player.tag = "JugadorDos";
            ReferenciaPlayer.player2 = player;

            // Desactiva la cámara si está configurada
            if (cameraDefault != null)
            {
                cameraDefault.enabled = false;
            }
        }

        // Modifica el prefab instanciado para el segundo jugador
        playerInputManager.playerPrefab.name = "JugadorDosPrefab";
        playerInputManager.playerPrefab.tag = "JugadorDosPrefab";
    }
}
