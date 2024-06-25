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
    private bool presiono = false;

    void Start()
    {
        canvas.enabled = false;
    }  
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "PlayerInteractionZone")
        {
            if (Input.GetKeyDown(KeyCode.F))
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
                Vector3 position = transform.position;
                position += new Vector3(0, 0.3f, 0);
                GameObject taza = Instantiate(TazaHervida,position, Quaternion.Euler(0, 0, 0));
                presiono = false;
            }
        }
    }
}
