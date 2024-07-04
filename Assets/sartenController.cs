using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class sartenController : MonoBehaviour
{
    // Start is called before the first frame update
    public bool estaSostenido;
    public bool tocacarme;
    public bool estaLleno; 
    public GameObject carne3D; 

    [Header("Material")]
    [SerializeField] private Material cocinado;
    [SerializeField] private Material quemado; 
    public InputAction interaccion;
    public bool tieneCarne;

    public int cantidadDeEscencias;
    public int cantidadDeSal;

    [SerializeField] private TextMeshProUGUI txtSal;
    [SerializeField] private TextMeshProUGUI txtEscencia;

    [SerializeField] private cocinaController cocina;
    private bool cocinaEncontrada;
    private int intensidad;

    private float timerCanvas = 60f;
    [SerializeField] private float timerFase;
    [SerializeField] private float timerLlamaBaja;
    [SerializeField] private float timerLlamaMedia;
    [SerializeField] private float timerLlamaAlta;
    public bool faseCompleta; 

    [SerializeField] private Image Circulo;
    [SerializeField] private Canvas canvas;

    private const float TIEMPO_TOTAL_FASE = 60f;
    private const float TIEMPO_CRITICO = 20f;
    private float perfeccion;
    void Start()
    {
        interaccion.Enable();
        carne3D.SetActive(false);
        cocinaEncontrada = false;

        timerFase = 0f;
        timerLlamaBaja = 0f;
        timerLlamaMedia = 0f;
        timerLlamaAlta = 0f;
        timerCanvas = TIEMPO_TOTAL_FASE;

        perfeccion = 0f;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "carne")
        {
            tocacarme = true;
            if (estaSostenido)
            {
                if (!estaLleno)
                {
                    if (interaccion.WasPressedThisFrame())
                    {
                        llenarCarne();
                        tieneCarne = true;
                    }
                }
            }
           

        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "carne")
        {
            tocacarme = false; 
        }
    }
    private void Update()
    {
        txtEscencia.text = cantidadDeEscencias.ToString();
        txtSal.text = cantidadDeSal.ToString();

        if (!cocinaEncontrada)
        {
            // Obtén todos los componentes del GameObject
            Component[] components = GetComponentsInParent<Component>();

            // Itera sobre cada componente y muestra su nombre
            foreach (Component component in components)
            {
                if (component.GetType().Name == "cocinaController")
                {
                    cocina = (cocinaController)component;
                    cocinaEncontrada = true;
                }

                if (component.name == "CocinaCarne")
                {
                    // Obtén todos los componentes del GameObject
                    Component[] componentsCocina = component.GetComponentsInChildren<Component>();

                    // Itera sobre cada componente y muestra su nombre
                    foreach (Component component2 in componentsCocina)
                    {
                        if (component2.name == "canvasTimer")
                        {
                            canvas = component2.GetComponent<Canvas>();
                        }
                        Debug.Log(component2.GetType().Name + " " + component2.name);

                        if (component2.name == "GreenZone")
                        {
                            Circulo = component2.GetComponent<Image>();
                        }
                    }
                }
            }
        }
        else if (!GetComponentInParent<PickableObject>().isPickeable)
        {
            cocina = null;
            cocinaEncontrada = false;
            canvas = null;
            Circulo = null;
        }


        //cuando hay carne
        if (tieneCarne)
        {
            if (!faseCompleta)
            {
                if (cocina != null)
                {
                    intensidad = cocina.GetIntensidadCocina();
                    if (timerFase < TIEMPO_TOTAL_FASE && intensidad != 0)
                    {
                        canvas.enabled = true;

                        if (timerCanvas >= 0)
                        {
                            timerCanvas -= Time.deltaTime;
                            Circulo.fillAmount = (TIEMPO_TOTAL_FASE - timerCanvas) / TIEMPO_TOTAL_FASE;
                        }

                        timerFase += Time.deltaTime;
                        if (intensidad == 2)
                        {
                            timerLlamaBaja += Time.deltaTime;
                        }
                        else if (intensidad == 1)
                        {
                            timerLlamaMedia += Time.deltaTime;
                        }
                        else if (intensidad == 3)
                        {
                            timerLlamaAlta += Time.deltaTime;
                        }
                    }
                    else if (timerFase >= TIEMPO_TOTAL_FASE)
                    {
                        cocina.apagarCocina();
                        perfeccion = CalcularPerfeccion(1);

                        canvas.enabled = false;
                    }
                }
            }
            
        }

    }
    private float CalcularPerfeccion(int intensidadFuego)
    {
        float porcentajePerfecto = 0f;
        float porcentajeQuemado = 0f;
        float porcentajePocoCocido = 0f;
        float tiempoTotalFase = TIEMPO_TOTAL_FASE;

        if (intensidadFuego == 1) // Fuego medio
        {
            porcentajePerfecto = (timerLlamaMedia / tiempoTotalFase) * 100f;
            porcentajeQuemado = (timerLlamaAlta > TIEMPO_CRITICO) ? (timerLlamaAlta / tiempoTotalFase) * 100f : 0f;
            porcentajePocoCocido = (timerLlamaBaja > TIEMPO_CRITICO) ? (timerLlamaBaja / tiempoTotalFase) * 100f : 0f;
        }
        else if (intensidadFuego == 2) // Fuego bajo
        {
            porcentajePerfecto = (timerLlamaBaja / tiempoTotalFase) * 100f;
            porcentajeQuemado = (timerLlamaAlta > TIEMPO_CRITICO) ? (timerLlamaAlta / tiempoTotalFase) * 100f : 0f;
            porcentajePocoCocido = (timerLlamaMedia > TIEMPO_CRITICO) ? (timerLlamaMedia / tiempoTotalFase) * 100f : 0f;
        }

        float descuentoEscencia = calcularDescuento(cantidadDeEscencias, faseCompleta ? 1 : 0);
        porcentajePerfecto *= descuentoEscencia;
        porcentajeQuemado *= descuentoEscencia;
        porcentajePocoCocido *= descuentoEscencia;

        float descuentoSal = calcularDescuento(cantidadDeSal, faseCompleta ? 1 : 0);
        porcentajePerfecto *= descuentoSal;
        porcentajeQuemado *= descuentoSal;
        porcentajePocoCocido *= descuentoSal;


        float perfeccionFinal = porcentajePerfecto - porcentajeQuemado - porcentajePocoCocido;
        perfeccionFinal = Mathf.Clamp(perfeccionFinal, 0f, 100f);

        return perfeccionFinal;
    }
    private float calcularDescuento(int ingrediente, int cantidadMinima)
    {
        float D = 5f; // Descuento base

        if (ingrediente > cantidadMinima)
        {
            float descuentoPorCantidad = 0f;

            // Calcular el descuento por cada unidad adicional de ingrediente
            for (int i = cantidadMinima + 1; i <= ingrediente; i++)
            {
                float descuentoUnitario = D * Mathf.Pow((1 - (D / 100f)), (i - 2));
                descuentoPorCantidad += descuentoUnitario;
            }

            // Convertir descuento a factor multiplicativo
            float factorDescuento = 1f - (descuentoPorCantidad / 100f);
            return factorDescuento;
        }
        else if (ingrediente < cantidadMinima)
        {
            float diferencia = cantidadMinima - ingrediente;
            float penalizacionPorCantidad = D * diferencia;

            // Convertir penalización a factor multiplicativo
            float factorPenalizacion = 1f - (penalizacionPorCantidad / 100f);
            return Mathf.Clamp(factorPenalizacion, 0f, 1f); // Asegurar que no sea menor que 0
        }
        else
        {
            return 1f; // Sin descuento ni penalización
        }
    }
    // Update is called once per frame
    public void llenarCarne()
    {
        carne3D.SetActive(true);
        estaLleno = true;
    }
}
