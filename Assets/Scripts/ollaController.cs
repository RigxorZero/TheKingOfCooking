using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ollaController : MonoBehaviour
{
    public GameObject[] aguaAnimation;
    public GameObject[] arrozAnimation;
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

    [SerializeField] private float timerGeneral;
    [SerializeField] private float timerLlamaBaja;
    [SerializeField] private float timerLlamaMedia;
    [SerializeField] private float timerLlamaAlta;
    private const float TIEMPO_TOTAL = 60f;
    private const float TIEMPO_CRITICO = 20f;

    void Start()
    {
        cantidadDeAgua = 0;
        cocinaEncontrada = false;

        timerGeneral = 0f;
        timerLlamaBaja = 0f;
        timerLlamaMedia = 0f;
        timerLlamaAlta = 0f;

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
                    Debug.Log("Cocina controller encontrado");
                    continue;
                }
            }
        }
        else if (!GetComponentInParent<PickableObject>().isPickeable)
        {
            cocina = null;
            cocinaEncontrada = false;
        }

        if (cantidadDeArroz > 0) // Solo si la cantidad de arroz es mayor que 0
        {
            if (cocina != null)
            {
                intensidad = cocina.GetIntensidadCocina();
                if (timerGeneral < TIEMPO_TOTAL && intensidad != 0)
                {
                    timerGeneral += Time.deltaTime;
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
                else
                {
                    CalcularPerfeccion();
                }
            }
        }
    }

    private void CalcularPerfeccion()
    {
        float porcentajePerfecto = (timerLlamaMedia / TIEMPO_TOTAL) * 100f;
        float porcentajeQuemado = (timerLlamaAlta > TIEMPO_CRITICO) ? (timerLlamaAlta / TIEMPO_TOTAL) * 100f : 0f;
        float porcentajePocoCocido = (timerLlamaBaja > TIEMPO_CRITICO) ? (timerLlamaBaja / TIEMPO_TOTAL) * 100f : 0f;

        // Ajustar porcentaje de perfección según cantidad de arroz
        if(cantidadDeArroz > 1)
        {
            float descuentoPorCantidadArroz = 0f;
            float D = 5f;
            // Calcular el descuento por cada unidad adicional de arroz
            for (int i = 2; i <= cantidadDeArroz; i++)
            {
                float descuentoUnitario = D * Mathf.Pow((1 - (D / 100f)), (i - 2));
                descuentoPorCantidadArroz += descuentoUnitario;
            }

            // Aplicar el descuento por cantidad de arroz al porcentaje de perfección final
            float factorCantidadArroz = 1f - (descuentoPorCantidadArroz / 100f); // Convertir descuento a factor multiplicativo

            porcentajePerfecto *= factorCantidadArroz;
            porcentajeQuemado *= factorCantidadArroz;
            porcentajePocoCocido *= factorCantidadArroz;
        }
        
        float perfeccionFinal = porcentajePerfecto - porcentajeQuemado - porcentajePocoCocido;
        perfeccionFinal = Mathf.Clamp(perfeccionFinal, 0f, 100f);

        Debug.Log($"Porcentaje de perfección: {perfeccionFinal}%");
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
