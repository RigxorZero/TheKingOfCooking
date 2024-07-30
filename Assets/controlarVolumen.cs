using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlarVolumen : MonoBehaviour
{
    public static controlarVolumen Instance;
    [SerializeField] private float cantidadVolumen; 
    
    void Start()
    {
        
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
    public void sumarVolumen()
    {
        cantidadVolumen += 2;
    }
    public void quitarVolumen()
    {
        cantidadVolumen -= 2;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
