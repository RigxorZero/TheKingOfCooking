using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlarVolumen : MonoBehaviour
{
    public static controlarVolumen Instance;
    [SerializeField] private float cantidadVolumen; 
    
    void Start()
    {
        cantidadVolumen = 0.6f;   
    }
    private void Awake()
    {
        if (controlarVolumen.Instance == null) { 
            controlarVolumen.Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void cambiarVolumen(float volumen){
        cantidadVolumen = volumen;   
    }
    public void inicializarVolumen()
    {
        cantidadVolumen = 0.6f;
    }

    public float obtenerVolumen()
    {
        return cantidadVolumen;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
