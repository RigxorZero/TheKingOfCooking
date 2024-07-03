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
    [SerializeField] private float tiempoActual;

    [SerializeField] private cocinaController cocina;
    private bool cocinaEncontrada;
    private int intensidad;
    void Start()
    {
        cantidadDeAgua = 0;
        cocinaEncontrada = false;
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

        if(cocina != null)
        {
            intensidad = cocina.GetIntensidadCocina();
            Debug.Log("Tiempo actual: " + tiempoActual + "; Intensidad Actual: " + intensidad);
            if (intensidad > 0)
            {
                tiempoActual += Time.deltaTime;
            }
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
