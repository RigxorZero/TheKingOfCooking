using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerTutorial : MonoBehaviour
{
    public bool esTutorial = false;

    //variables para comprobar que las cosas se hacen (se van comprovando en otros script)
    public bool fase1 = false;
    public bool mostroReceta = false;//se comprueba en mostrarReceta
    public bool tomoOlla = false;//se comprueba en PickUpObject
    public bool botarBasura = false; //se comprueba en basureroController y PickableObject
    public bool dejoOlla = false; //se comprueba en PickableObject
    public bool tomarTaza = false; //se comprueba en PickUpObject
    public bool llenarTazaArroz = false; //se comprueba en TazaController 
    public bool dejarArrozEnOlla = false; //se comprueba en TazaController 
    public bool abrirCocina = false; //se comprueba en cocinaController
    public bool cambiarTemperatura = false; //se comprueba en cocinaController
    public bool cerrarCocina = false; //se comprueba en cocinaController
    public bool arrozListo = false; //un timer que se comprueba aca
    public bool agualista = false; //se comprueba en hervidoController
    public bool aguaEnLaOlla = false; //se comprueba en TazaController 
    public bool cambioTemperatura2 = false;
    public bool tomarSal = false; //se comprueba en PickUpObject
    public bool echarSal = false; //se comprueba en salController
    public bool echarEscencia = false; //se comprueba en escenciaController
    public bool arrozlisto2 = false; // un timer que se comprueba aca
    public bool golpe = false;  // se comprueba en hit colllider
    public bool tomarPlato = false; //se comprueba en PickUpObject
    public bool servirArroz = false; // se comprueba en platoController
    public bool servirCarne = false; //se comprueba en platoController
    public bool platoListo = false; //se comprueba en platoFinalController

    public bool timerActivo = false;
    public bool cambioTimer = false;
    public float tiempoActual;
    public float tiempoEntreCambio; 
    
    
    void Start()
    {
        canvasTutorial.index = 0;
        tiempoEntreCambio = 8f;
    }

    // Update is called once per frame
    void Update()
    {
        if(tiempoActual >= 0)
        {
            tiempoActual -= Time.deltaTime;
        }
        else
        {
            if (timerActivo)
            {
                if (arrozListo)
                {
                    arrozlisto2 = true;
                }
                else
                {
                    arrozListo = true;
                    timerActivo = false;
                    cambioTimer = false;

                }
            }
        }
        
        if (esTutorial)
        {
            if (this.GetComponent<PlayerController>().seMovio)
            {
                canvasTutorial.index = 1;
                fase1 = true;

}
            if(mostroReceta && fase1 && tomoOlla == false && botarBasura == false && dejoOlla == false && tomarTaza == false && llenarTazaArroz == false && dejarArrozEnOlla == false && abrirCocina == false && cambiarTemperatura == false && cerrarCocina == false && arrozListo == false && agualista == false && aguaEnLaOlla == false && tomarSal == false && echarSal == false && echarEscencia == false && tomarPlato == false && servirArroz == false && servirCarne == false && platoListo == false)
            {
                canvasTutorial.index = 2;
            }
            if(tomoOlla && mostroReceta && fase1)
            {
                canvasTutorial.index = 3;
            }
            if(botarBasura && tomoOlla && mostroReceta && fase1)
            {
                canvasTutorial.index = 4;

                
            }
            if(dejoOlla && botarBasura && tomoOlla && mostroReceta && fase1)
            {
                canvasTutorial.index = 5;

            }
            if(tomarTaza && dejoOlla && botarBasura && tomoOlla && mostroReceta && fase1)
            {
                canvasTutorial.index = 6;

            }
            if (llenarTazaArroz && tomarTaza && dejoOlla && botarBasura && tomoOlla && mostroReceta && fase1)
            {
                canvasTutorial.index = 7;

            }
            if(dejarArrozEnOlla && llenarTazaArroz && tomarTaza && dejoOlla && botarBasura && tomoOlla && mostroReceta && fase1 )
            {
                canvasTutorial.index = 8;

            }
            if(abrirCocina && dejarArrozEnOlla && llenarTazaArroz && tomarTaza && dejoOlla && botarBasura && tomoOlla && mostroReceta && fase1 )
            {
                canvasTutorial.index = 9;

            }
            if(cambiarTemperatura && abrirCocina && dejarArrozEnOlla && llenarTazaArroz && tomarTaza && dejoOlla && botarBasura && tomoOlla && mostroReceta && fase1 )
            {
                canvasTutorial.index = 10;
            }
            if(cerrarCocina && cambiarTemperatura && abrirCocina && dejarArrozEnOlla && llenarTazaArroz && tomarTaza && dejoOlla && botarBasura && tomoOlla && mostroReceta && fase1 )
            {
                canvasTutorial.index = 11;
                timerActivo = true;
            }
            if(arrozListo && cerrarCocina && cambiarTemperatura && abrirCocina && dejarArrozEnOlla && llenarTazaArroz && tomarTaza && dejoOlla && botarBasura && tomoOlla && mostroReceta && fase1)
            {
                canvasTutorial.index = 12;

            }
            if(agualista && arrozListo && cerrarCocina && cambiarTemperatura && abrirCocina && dejarArrozEnOlla && llenarTazaArroz && tomarTaza && dejoOlla && botarBasura && tomoOlla && mostroReceta && fase1)
            {
                canvasTutorial.index = 13;

            }         
          
            if (aguaEnLaOlla && agualista && arrozListo && cerrarCocina && cambiarTemperatura && abrirCocina && dejarArrozEnOlla && llenarTazaArroz && tomarTaza && dejoOlla && botarBasura && tomoOlla && mostroReceta && fase1)
            {
                canvasTutorial.index = 15;

            }

            if (tomarSal && aguaEnLaOlla && agualista && arrozListo && cerrarCocina && cambiarTemperatura && abrirCocina && dejarArrozEnOlla && llenarTazaArroz && tomarTaza && dejoOlla && botarBasura && tomoOlla && mostroReceta && fase1)
            {
                canvasTutorial.index = 16;

            }
            if (echarSal && tomarSal && aguaEnLaOlla && agualista && arrozListo && cerrarCocina && cambiarTemperatura && abrirCocina && dejarArrozEnOlla && llenarTazaArroz && tomarTaza && dejoOlla && botarBasura && tomoOlla && mostroReceta && fase1)
            {
                canvasTutorial.index = 17;

            }
            if (echarEscencia && echarSal && tomarSal && aguaEnLaOlla && agualista && arrozListo && cerrarCocina && cambiarTemperatura && abrirCocina && dejarArrozEnOlla && llenarTazaArroz && tomarTaza && dejoOlla && botarBasura && tomoOlla && mostroReceta && fase1)
            {
                canvasTutorial.index = 14;
                timerActivo = true;
            }


            if (cambioTemperatura2 && aguaEnLaOlla && agualista && arrozListo && cerrarCocina && cambiarTemperatura && abrirCocina && dejarArrozEnOlla && llenarTazaArroz && tomarTaza && dejoOlla && botarBasura && tomoOlla && mostroReceta && fase1 && tomarSal && echarSal && echarEscencia)
            {
                canvasTutorial.index = 18;

            }
            
            if(arrozlisto2 && echarEscencia && echarSal && tomarSal && cambioTemperatura2 && aguaEnLaOlla && agualista && arrozListo && cerrarCocina && cambiarTemperatura && abrirCocina && dejarArrozEnOlla && llenarTazaArroz && tomarTaza && dejoOlla && botarBasura && tomoOlla && mostroReceta && fase1)
            {
                canvasTutorial.index = 19;
            }
            if(golpe && arrozlisto2 && echarEscencia && echarSal && tomarSal && cambioTemperatura2 && aguaEnLaOlla && agualista && arrozListo && cerrarCocina && cambiarTemperatura && abrirCocina && dejarArrozEnOlla && llenarTazaArroz && tomarTaza && dejoOlla && botarBasura && tomoOlla && mostroReceta && fase1)
            {
                canvasTutorial.index = 20;
            }
            if(tomarPlato && golpe && arrozlisto2 && echarEscencia && echarSal && tomarSal && cambioTemperatura2 && aguaEnLaOlla && agualista && arrozListo && cerrarCocina && cambiarTemperatura && abrirCocina && dejarArrozEnOlla && llenarTazaArroz && tomarTaza && dejoOlla && botarBasura && tomoOlla && mostroReceta && fase1)
            {
                canvasTutorial.index = 21;

            }
            if(servirArroz && tomarPlato && golpe && arrozlisto2 && echarEscencia && echarSal && tomarSal && cambioTemperatura2 && aguaEnLaOlla && agualista && arrozListo && cerrarCocina && cambiarTemperatura && abrirCocina && dejarArrozEnOlla && llenarTazaArroz && tomarTaza && dejoOlla && botarBasura && tomoOlla && mostroReceta && fase1)
            {
                canvasTutorial.index = 22;

            }
            if(servirCarne && servirArroz && tomarPlato && golpe && arrozlisto2 && echarEscencia && echarSal && tomarSal && cambioTemperatura2 && aguaEnLaOlla && agualista && arrozListo && cerrarCocina && cambiarTemperatura && abrirCocina && dejarArrozEnOlla && llenarTazaArroz && tomarTaza && dejoOlla && botarBasura && tomoOlla && mostroReceta && fase1)
            {
                canvasTutorial.index = 23;
            }
            if(platoListo && servirCarne && servirArroz && tomarPlato && golpe && arrozlisto2 && echarEscencia && echarSal && tomarSal && cambioTemperatura2 && aguaEnLaOlla && agualista && arrozListo && cerrarCocina && cambiarTemperatura && abrirCocina && dejarArrozEnOlla && llenarTazaArroz && tomarTaza && dejoOlla && botarBasura && tomoOlla && mostroReceta && fase1)
            {
                Timer.platolisto = true;
                canvasTutorial.index = 24;
            }
            
        }
    }
}
