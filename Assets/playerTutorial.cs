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
    public bool arrozlisto2 = false;
    public bool golpe = false; 
    public bool tomarPlato = false;
    public bool servirArroz = false;
    public bool servirCarne = false;
    public bool platoListo = false; 

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
                arrozListo = true;
            }
        }
        
        if (esTutorial)
        {
            if (this.GetComponent<PlayerController>().seMovio && mostroReceta == false && tomoOlla == false && botarBasura == false && dejoOlla == false && tomarTaza == false && llenarTazaArroz == false && dejarArrozEnOlla == false && abrirCocina == false && cambiarTemperatura == false && cerrarCocina == false && arrozListo == false && agualista == false && aguaEnLaOlla == false && tomarSal == false && echarSal == false && echarEscencia == false && tomarPlato == false && servirArroz == false && servirCarne == false && platoListo == false)
            {
                canvasTutorial.index = 1;
                fase1 = true;

                mostroReceta = false;
                tomoOlla = false;
                botarBasura = false;
                dejoOlla = false;
                tomarTaza = false;
                llenarTazaArroz = false;
                dejarArrozEnOlla = false;
                abrirCocina = false;
                cambiarTemperatura = false;
                cerrarCocina = false;
                arrozListo = false;
                agualista = false;
                aguaEnLaOlla = false;
                tomarSal = false;
                echarSal = false;
                echarEscencia = false;
                tomarPlato = false;
                servirArroz = false;
                servirCarne = false;
                platoListo = false;
}
            if(mostroReceta && fase1 && tomoOlla == false && botarBasura == false && dejoOlla == false && tomarTaza == false && llenarTazaArroz == false && dejarArrozEnOlla == false && abrirCocina == false && cambiarTemperatura == false && cerrarCocina == false && arrozListo == false && agualista == false && aguaEnLaOlla == false && tomarSal == false && echarSal == false && echarEscencia == false && tomarPlato == false && servirArroz == false && servirCarne == false && platoListo == false)
            {
                canvasTutorial.index = 2;

                tomoOlla = false;
                botarBasura = false;
                dejoOlla = false;
                tomarTaza = false;
                llenarTazaArroz = false;
                dejarArrozEnOlla = false;
                abrirCocina = false;
                cambiarTemperatura = false;
                cerrarCocina = false;
                arrozListo = false;
                agualista = false;
                aguaEnLaOlla = false;
                tomarSal = false;
                echarSal = false;
                echarEscencia = false;
                tomarPlato = false;
                servirArroz = false;
                servirCarne = false;
                platoListo = false;
            }
            if(tomoOlla && mostroReceta && fase1 && botarBasura == false && dejoOlla == false && tomarTaza == false && llenarTazaArroz == false && dejarArrozEnOlla == false && abrirCocina == false && cambiarTemperatura == false && cerrarCocina == false && arrozListo == false && agualista == false && aguaEnLaOlla == false && tomarSal == false && echarSal == false && echarEscencia == false && tomarPlato == false && servirArroz == false && servirCarne == false && platoListo == false)
            {
                canvasTutorial.index = 3;

                botarBasura = false;
                dejoOlla = false;
                tomarTaza = false;
                llenarTazaArroz = false;
                dejarArrozEnOlla = false;
                abrirCocina = false;
                cambiarTemperatura = false;
                cerrarCocina = false;
                arrozListo = false;
                agualista = false;
                aguaEnLaOlla = false;
                tomarSal = false;
                echarSal = false;
                echarEscencia = false;
                tomarPlato = false;
                servirArroz = false;
                servirCarne = false;
                platoListo = false;
            }
            if(botarBasura && tomoOlla && mostroReceta && fase1 && dejoOlla == false && tomarTaza == false && llenarTazaArroz == false && dejarArrozEnOlla == false && abrirCocina == false && cambiarTemperatura == false && cerrarCocina == false && arrozListo == false && agualista == false && aguaEnLaOlla == false && tomarSal == false && echarSal == false && echarEscencia == false && tomarPlato == false && servirArroz == false && servirCarne == false && platoListo == false)
            {
                canvasTutorial.index = 4;

                dejoOlla = false;
                tomarTaza = false;
                llenarTazaArroz = false;
                dejarArrozEnOlla = false;
                abrirCocina = false;
                cambiarTemperatura = false;
                cerrarCocina = false;
                arrozListo = false;
                agualista = false;
                aguaEnLaOlla = false;
                tomarSal = false;
                echarSal = false;
                echarEscencia = false;
                tomarPlato = false;
                servirArroz = false;
                servirCarne = false;
                platoListo = false;
            }
            if(dejoOlla && botarBasura && tomoOlla && mostroReceta && fase1 && tomarTaza == false && llenarTazaArroz == false && dejarArrozEnOlla == false && abrirCocina == false && cambiarTemperatura == false && cerrarCocina == false && arrozListo == false && agualista == false && aguaEnLaOlla == false && tomarSal == false && echarSal == false && echarEscencia == false && tomarPlato == false && servirArroz == false && servirCarne == false && platoListo == false)
            {
                canvasTutorial.index = 5;

                tomarTaza = false;
                llenarTazaArroz = false;
                dejarArrozEnOlla = false;
                abrirCocina = false;
                cambiarTemperatura = false;
                cerrarCocina = false;
                arrozListo = false;
                agualista = false;
                aguaEnLaOlla = false;
                tomarSal = false;
                echarSal = false;
                echarEscencia = false;
                tomarPlato = false;
                servirArroz = false;
                servirCarne = false;
                platoListo = false;
            }
            if(tomarTaza && dejoOlla && botarBasura && tomoOlla && mostroReceta && fase1 && llenarTazaArroz == false && dejarArrozEnOlla == false && abrirCocina == false && cambiarTemperatura == false && cerrarCocina == false && arrozListo == false && agualista == false && aguaEnLaOlla == false && tomarSal == false && echarSal == false && echarEscencia == false && tomarPlato == false && servirArroz == false && servirCarne == false && platoListo == false)
            {
                canvasTutorial.index = 6;

                llenarTazaArroz = false;
                dejarArrozEnOlla = false;
                abrirCocina = false;
                cambiarTemperatura = false;
                cerrarCocina = false;
                arrozListo = false;
                agualista = false;
                aguaEnLaOlla = false;
                tomarSal = false;
                echarSal = false;
                echarEscencia = false;
                tomarPlato = false;
                servirArroz = false;
                servirCarne = false;
                platoListo = false;
            }
            if (llenarTazaArroz && tomarTaza && dejoOlla && botarBasura && tomoOlla && mostroReceta && fase1 && dejarArrozEnOlla == false && abrirCocina == false && cambiarTemperatura == false && cerrarCocina == false && arrozListo == false && agualista == false && aguaEnLaOlla == false && tomarSal == false && echarSal == false && echarEscencia == false && tomarPlato == false && servirArroz == false && servirCarne == false && platoListo == false)
            {
                canvasTutorial.index = 7;

                dejarArrozEnOlla = false;
                abrirCocina = false;
                cambiarTemperatura = false;
                cerrarCocina = false;
                arrozListo = false;
                agualista = false;
                aguaEnLaOlla = false;
                tomarSal = false;
                echarSal = false;
                echarEscencia = false;
                tomarPlato = false;
                servirArroz = false;
                servirCarne = false;
                platoListo = false;
            }
            if(dejarArrozEnOlla && llenarTazaArroz && tomarTaza && dejoOlla && botarBasura && tomoOlla && mostroReceta && fase1 && abrirCocina == false && cambiarTemperatura == false && cerrarCocina == false && arrozListo == false && agualista == false && aguaEnLaOlla == false && tomarSal == false && echarSal == false && echarEscencia == false && tomarPlato == false && servirArroz == false && servirCarne == false && platoListo == false)
            {
                canvasTutorial.index = 8;

                abrirCocina = false;
                cambiarTemperatura = false;
                cerrarCocina = false;
                arrozListo = false;
                agualista = false;
                aguaEnLaOlla = false;
                tomarSal = false;
                echarSal = false;
                echarEscencia = false;
                tomarPlato = false;
                servirArroz = false;
                servirCarne = false;
                platoListo = false;
            }
            if(abrirCocina && dejarArrozEnOlla && llenarTazaArroz && tomarTaza && dejoOlla && botarBasura && tomoOlla && mostroReceta && fase1 && cambiarTemperatura == false && cerrarCocina == false && arrozListo == false && agualista == false && aguaEnLaOlla == false && tomarSal == false && echarSal == false && echarEscencia == false && tomarPlato == false && servirArroz == false && servirCarne == false && platoListo == false)
            {
                canvasTutorial.index = 9;

                cambiarTemperatura = false;
                cerrarCocina = false;
                arrozListo = false;
                agualista = false;
                aguaEnLaOlla = false;
                tomarSal = false;
                echarSal = false;
                echarEscencia = false;
                tomarPlato = false;
                servirArroz = false;
                servirCarne = false;
                platoListo = false;
            }
            if(cambiarTemperatura && abrirCocina && dejarArrozEnOlla && llenarTazaArroz && tomarTaza && dejoOlla && botarBasura && tomoOlla && mostroReceta && fase1 && cerrarCocina == false && arrozListo == false && agualista == false && aguaEnLaOlla == false && tomarSal == false && echarSal == false && echarEscencia == false && tomarPlato == false && servirArroz == false && servirCarne == false && platoListo == false)
            {
                canvasTutorial.index = 10;

                cerrarCocina = false;
                arrozListo = false;
                agualista = false;
                aguaEnLaOlla = false;
                tomarSal = false;
                echarSal = false;
                echarEscencia = false;
                tomarPlato = false;
                servirArroz = false;
                servirCarne = false;
                platoListo = false;
            }
            if(cerrarCocina && cambiarTemperatura && abrirCocina && dejarArrozEnOlla && llenarTazaArroz && tomarTaza && dejoOlla && botarBasura && tomoOlla && mostroReceta && fase1 && arrozListo == false && agualista == false && aguaEnLaOlla == false && tomarSal == false && echarSal == false && echarEscencia == false && tomarPlato == false && servirArroz == false && servirCarne == false && platoListo == false)
            {
                canvasTutorial.index = 11;
                timerActivo = true;

                arrozListo = false;
                agualista = false;
                aguaEnLaOlla = false;
                tomarSal = false;
                echarSal = false;
                echarEscencia = false;
                tomarPlato = false;
                servirArroz = false;
                servirCarne = false;
                platoListo = false;
            }
            if(arrozListo && cerrarCocina && cambiarTemperatura && abrirCocina && dejarArrozEnOlla && llenarTazaArroz && tomarTaza && dejoOlla && botarBasura && tomoOlla && mostroReceta && fase1 && agualista == false && aguaEnLaOlla == false && tomarSal == false && echarSal == false && echarEscencia == false && tomarPlato == false && servirArroz == false && servirCarne == false && platoListo == false)
            {
                canvasTutorial.index = 12;

                agualista = false;
                aguaEnLaOlla = false;
                tomarSal = false;
                echarSal = false;
                echarEscencia = false;
                tomarPlato = false;
                servirArroz = false;
                servirCarne = false;
                platoListo = false;
            }
            if(agualista && arrozListo && cerrarCocina && cambiarTemperatura && abrirCocina && dejarArrozEnOlla && llenarTazaArroz && tomarTaza && dejoOlla && botarBasura && tomoOlla && mostroReceta && fase1 && aguaEnLaOlla == false && tomarSal == false && echarSal == false && echarEscencia == false && tomarPlato == false && servirArroz == false && servirCarne == false && platoListo == false)
            {
                canvasTutorial.index = 13;

                aguaEnLaOlla = false;
                tomarSal = false;
                echarSal = false;
                echarEscencia = false;
                tomarPlato = false;
                servirArroz = false;
                servirCarne = false;
                platoListo = false;
            }
            if(aguaEnLaOlla && agualista && arrozListo && cerrarCocina && cambiarTemperatura && abrirCocina && dejarArrozEnOlla && llenarTazaArroz && tomarTaza && dejoOlla && botarBasura && tomoOlla && mostroReceta && fase1 && tomarSal == false && echarSal == false && echarEscencia == false && tomarPlato == false && servirArroz == false && servirCarne == false && platoListo == false)
            {
                canvasTutorial.index = 14;

                tomarSal = false;
                echarSal = false;
                echarEscencia = false;
                tomarPlato = false;
                servirArroz = false;
                servirCarne = false;
                platoListo = false;
            }
            if (cambioTemperatura2 && agualista && arrozListo && cerrarCocina && cambiarTemperatura && abrirCocina && dejarArrozEnOlla && llenarTazaArroz && tomarTaza && dejoOlla && botarBasura && tomoOlla && mostroReceta && fase1 && aguaEnLaOlla == false && tomarSal == false && echarSal == false && echarEscencia == false && tomarPlato == false && servirArroz == false && servirCarne == false && platoListo == false)
            {
                canvasTutorial.index = 15;

                echarSal = false;
                echarEscencia = false;
                tomarPlato = false;
                servirArroz = false;
                servirCarne = false;
                platoListo = false;
            }
            if (tomarSal && cambioTemperatura2 && aguaEnLaOlla && agualista && arrozListo && cerrarCocina && cambiarTemperatura && abrirCocina && dejarArrozEnOlla && llenarTazaArroz && tomarTaza && dejoOlla && botarBasura && tomoOlla && mostroReceta && fase1 && echarSal == false && echarEscencia == false && tomarPlato == false && servirArroz == false && servirCarne == false && platoListo == false)
            {
                canvasTutorial.index = 16;
                
                echarSal = false;
                echarEscencia = false;
                tomarPlato = false;
                servirArroz = false;
                servirCarne = false;
                platoListo = false;
            }
            if(echarSal && tomarSal && cambioTemperatura2 && aguaEnLaOlla && agualista && arrozListo && cerrarCocina && cambiarTemperatura && abrirCocina && dejarArrozEnOlla && llenarTazaArroz && tomarTaza && dejoOlla && botarBasura && tomoOlla && mostroReceta && fase1 && echarEscencia == false && tomarPlato == false && servirArroz == false && servirCarne == false && platoListo == false)
            {
                canvasTutorial.index = 17;

                echarEscencia = false;
                tomarPlato = false;
                servirArroz = false;
                servirCarne = false;
                platoListo = false;
            }
            if(echarEscencia && echarSal && tomarSal && cambioTemperatura2  && aguaEnLaOlla && agualista && arrozListo && cerrarCocina && cambiarTemperatura && abrirCocina && dejarArrozEnOlla && llenarTazaArroz && tomarTaza && dejoOlla && botarBasura && tomoOlla && mostroReceta && fase1 && tomarPlato == false && servirArroz == false && servirCarne == false && platoListo == false)
            {
                canvasTutorial.index = 18;

                tomarPlato = false;
                servirArroz = false;
                servirCarne = false;
                platoListo = false;
            }
        }
    }
}
