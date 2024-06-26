using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class hervidorController : MonoBehaviour
{
    [SerializeField] private float tiempoHervido;
    [SerializeField] private float tiempoActual;
    [SerializeField] private Image Circulo;
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject TazaHervida; 
    public bool presiono = false;
    public bool aguaLista = false;

    void Start()
    {
        canvas.enabled = false;
    }  
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "PlayerInteractionZone")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (tiempoActual <= 0)
                {
                    tiempoActual = tiempoHervido;
                    canvas.enabled = true;
                    presiono = true; 
                }
            }
        }
    }
    void Update()
    {
        if(tiempoActual >= 0)
        {
            tiempoActual -= Time.deltaTime;
            Circulo.fillAmount = (tiempoHervido - tiempoActual)/tiempoHervido;
        }
        else
        {
            if (presiono)
            {
                aguaLista = true; 
                presiono = false;
            }
        }
    }
}
