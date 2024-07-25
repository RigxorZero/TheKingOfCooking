using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ollaController : MonoBehaviour
{
    public GameObject[] aguaAnimation;
    public GameObject[] arrozAnimation;
    public Material arrozCocinado;

    public int cantidadDeAgua; 
    public int cantidadDeArroz;
    public int cantidadDeEscencias;
    public int cantidadDeSal; 

    [SerializeField] private Canvas relleno;
    [SerializeField] private TextMeshProUGUI txtArroz;
    [SerializeField] private TextMeshProUGUI txtAgua;
    [SerializeField] private TextMeshProUGUI txtSal;
    [SerializeField] private TextMeshProUGUI txtEscencia;

    [SerializeField] private cocinaController cocina;
    private bool cocinaEncontrada;
    private int intensidad;
    public bool faseUnoCompleta;
    public bool faseDosCompleta;
    private float timerCanvas = 60f;

    [SerializeField] private Image Circulo;
    [SerializeField] private Canvas canvas;

    [SerializeField] private float timerFaseUno;
    [SerializeField] private float timerFaseDos;
    [SerializeField] private float timerLlamaBaja;
    [SerializeField] private float timerLlamaMedia;
    [SerializeField] private float timerLlamaAlta;
    private static float TIEMPO_TOTAL_FASE1 = 60f;
    private static float TIEMPO_TOTAL_FASE2 = 60f;
    private static float TIEMPO_CRITICO = 20f;

    private float perfeccionFaseUno;
    private float perfeccionFaseDos;


    void Start()
    {
        cantidadDeAgua = 0;
        cocinaEncontrada = false;

        timerFaseUno = 0f;
        timerFaseDos = 0f;
        timerLlamaBaja = 0f;
        timerLlamaMedia = 0f;
        timerLlamaAlta = 0f;
        timerCanvas = TIEMPO_TOTAL_FASE1;

        faseUnoCompleta = false;
        faseDosCompleta = false;

        perfeccionFaseUno = 0f;
        perfeccionFaseDos = 0f;


        for (int i = 0; i < aguaAnimation.Length; i++)
        {
            aguaAnimation[i].SetActive(false);
        }
        for (int i = 0; i < arrozAnimation.Length; i++)
        {
            arrozAnimation[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (faseUnoCompleta)
        {
            if (faseDosCompleta) {
                int cantidad;
                if(cantidadDeArroz * 2 > arrozAnimation.Length)
                {
                    cantidad = arrozAnimation.Length;
                }
                else
                {
                    cantidad = cantidadDeArroz * 2;
                }
                for (int i = 0; i < (cantidad); i++)
                {
                    arrozAnimation[i].SetActive(true);
                    arrozAnimation[i].GetComponent<MeshRenderer>().material = arrozCocinado;
                }
            }
            else
            {
                for (int i = 0; i < cantidadDeArroz; i++)
                {
                    arrozAnimation[i].GetComponent<MeshRenderer>().material = arrozCocinado;
                }
            }
            
        }
        txtArroz.text = cantidadDeArroz.ToString();
        txtAgua.text = cantidadDeAgua.ToString();
        txtEscencia.text = cantidadDeEscencias.ToString();
        txtSal.text= cantidadDeSal.ToString();

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

                if(component.name == "CocinaArroz" || component.name == "CocinaCarne")
                {
                    // Obtén todos los componentes del GameObject
                    Component[] componentsCocina = component.GetComponentsInChildren<Component>();

                    // Itera sobre cada componente y muestra su nombre
                    foreach (Component component2 in componentsCocina)
                    {
                        if(component2.name == "canvasTimer")
                        {
                            canvas = component2.GetComponent<Canvas>();
                        }
                        //Debug.Log(component2.GetType().Name + " " + component2.name);

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

        if (cantidadDeArroz > 0) // Solo si la cantidad de arroz es mayor que 0
        {
            if (!faseUnoCompleta)
            {
                if (cocina != null)
                {
                    intensidad = cocina.GetIntensidadCocina();
                    if (timerFaseUno < TIEMPO_TOTAL_FASE1 && intensidad != 0)
                    {
                        canvas.enabled = true;

                        if (timerCanvas >= 0)
                        {
                            timerCanvas -= Time.deltaTime;
                            Circulo.fillAmount = (TIEMPO_TOTAL_FASE1 - timerCanvas) / TIEMPO_TOTAL_FASE1;
                        }

                        timerFaseUno += Time.deltaTime;
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
                    else if(timerFaseUno >= TIEMPO_TOTAL_FASE1)
                    {
                        cocina.apagarCocina();
                        perfeccionFaseUno = CalcularPerfeccion(1);
                        faseUnoCompleta = true;
                        timerLlamaAlta = 0;
                        timerLlamaBaja = 0;
                        timerLlamaMedia = 0;
                        canvas.enabled = false;
                        timerCanvas = TIEMPO_TOTAL_FASE2;
                    }
                }
            }
            else if (!faseDosCompleta)
            {
                if (cocina != null)
                {
                    intensidad = cocina.GetIntensidadCocina();
                    if (timerFaseDos < TIEMPO_TOTAL_FASE2 && intensidad != 0)
                    {
                        canvas.enabled = true;

                        if (timerCanvas >= 0)
                        {
                            timerCanvas -= Time.deltaTime;
                            Circulo.fillAmount = (TIEMPO_TOTAL_FASE2 - timerCanvas) / TIEMPO_TOTAL_FASE2;
                        }


                        timerFaseDos += Time.deltaTime;
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
                    else if (timerFaseDos >= TIEMPO_TOTAL_FASE2)
                    {
                        ReferenciaPlayer.player1.GetComponent<playerTutorial>().golpe = true;
                        cocina.apagarCocina();
                        perfeccionFaseDos = CalcularPerfeccion(2);
                        faseDosCompleta = true;
                        canvas.enabled = false;
                        Debug.Log($"Perfección Fase 1: {perfeccionFaseUno}");
                        Debug.Log($"Perfección Fase 2: {perfeccionFaseDos}");

                        // Imprimir perfección total
                        float perfeccionTotal = (perfeccionFaseUno + perfeccionFaseDos) / 2;
                        Debug.Log($"Perfección Total: {perfeccionTotal}");
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
        float tiempoTotalFase = faseUnoCompleta ? TIEMPO_TOTAL_FASE2 : TIEMPO_TOTAL_FASE1;

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

        float descuentoArroz = calcularDescuento(cantidadDeArroz, 1);
        porcentajePerfecto *= descuentoArroz;
        porcentajeQuemado *= descuentoArroz;
        porcentajePocoCocido *= descuentoArroz;

        float descuentoAgua = calcularDescuento(cantidadDeAgua, faseUnoCompleta ? 2 : 0);
        porcentajePerfecto *= descuentoAgua;
        porcentajeQuemado *= descuentoAgua;
        porcentajePocoCocido *= descuentoAgua;

        float descuentoSal = calcularDescuento(cantidadDeSal, faseUnoCompleta ? 1 : 0);
        porcentajePerfecto *= descuentoSal;
        porcentajeQuemado *= descuentoSal;
        porcentajePocoCocido *= descuentoSal;

        float descuentoEscencias = calcularDescuento(cantidadDeEscencias, faseUnoCompleta ? 1 : 0);
        porcentajePerfecto *= descuentoEscencias;
        porcentajeQuemado *= descuentoEscencias;
        porcentajePocoCocido *= descuentoEscencias;

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



    public void cambioAnimationAgua()
    {
        for (int i = 0; i < cantidadDeAgua; i++) { 
            aguaAnimation[i].SetActive(true);
        }
    }
    public void cambioAnimationArroz()
    {
        for (int i = 0; i < cantidadDeArroz; i++)
        {
            arrozAnimation[i].SetActive(true);
        }
    }
}
