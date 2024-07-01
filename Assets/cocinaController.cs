using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class cocinaController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Canvas canva;
    [SerializeField] private Image perilla;

    public bool[] nivelPerilla = new bool[4]; 
    public bool canvasActivo;

    private Collider player;

    public InputAction CanvasActiveButton;
    public InputAction ArribaButtom;
    public InputAction AbajoButtom;
    public InputAction DerechaButtom;
    public InputAction IzquierdaButtom;
    private void OnTriggerStay(Collider other)
    {
        if( other.tag == "PlayerInteractionZone")
        {
            if(CanvasActiveButton.WasPressedThisFrame())
            {
                other.GetComponentInParent<PlayerController>().sePuedeMover = false;
                player = other; 
                canva.enabled = true;
                canvasActivo = true;
            }
        }
    }
    void Start()
    {
        CanvasActiveButton.Enable();
        canva.enabled = false;
        canvasActivo = false;

        //Inicializar perilla
        for (int i = 0; i < nivelPerilla.Length; i++)
        {
            nivelPerilla[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (CanvasActiveButton.WasPressedThisFrame() && canvasActivo)
        {
            player.GetComponentInParent<PlayerController>().sePuedeMover = true;
            canvasActivo = false;
            canva.enabled = false; 
        }

        /*
            0 -> apagado
            1 -> bajo
            2 -> medio
            3 -> alto 
         */
        int valor;
        if (canvasActivo)
        {
            if (ArribaButtom.WasPressedThisFrame())//Arriba
            {
                perilla.rectTransform.rotation = Quaternion.Euler(0, 0, 0);

                valor = 0;

                for (int i = 0; i <= nivelPerilla.Length; i++)
                {
                    if (i == valor)
                    {
                        nivelPerilla[i] = true;
                    }
                    else
                    {
                        nivelPerilla[i] = false;
                    }
                }
            }
            if (AbajoButtom.WasPressedThisFrame())//Abajo
            {
                perilla.rectTransform.rotation = Quaternion.Euler(0, 0, 180);

                valor = 1;

                for (int i = 0; i <= nivelPerilla.Length; i++)
                {
                    if (i == valor)
                    {
                        nivelPerilla[i] = true;
                    }
                    else
                    {
                        nivelPerilla[i] = false;
                    }
                }
            }
            if (DerechaButtom.WasPressedThisFrame())//Derecha
            {
                perilla.rectTransform.rotation = Quaternion.Euler(0, 0, 270);

                valor = 2;

                for (int i = 0; i <= nivelPerilla.Length; i++)
                {
                    if (i == valor)
                    {
                        nivelPerilla[i] = true;
                    }
                    else
                    {
                        nivelPerilla[i] = false;
                    }
                }
            }
            if (IzquierdaButtom.WasPressedThisFrame())//Izquierda
            {
                perilla.rectTransform.rotation = Quaternion.Euler(0, 0, 90);

                valor = 3;

                for (int i = 0; i <= nivelPerilla.Length; i++)
                {
                    if (i == valor)
                    {
                        nivelPerilla[i] = true;
                    }
                    else
                    {
                        nivelPerilla[i] = false;
                    }
                }
            }
        }
    }
}
