using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class opcionesController : MonoBehaviour
{
    public GameObject bottonPantalla;
    public GameObject bottonControles;
    public GameObject bottonMusica;

    public GameObject puntoImagen;
    public Image titulo;

    public GameObject[] puntosPosicion;
    public Sprite[] imagenes;

    public GameObject pantallaOpciones;
    public GameObject controlesOpciones;
    public GameObject musicaOpciones;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(EventSystem.current.currentSelectedGameObject == bottonPantalla)
        {
            titulo.sprite = imagenes[0]; 
            puntoImagen.transform.position = puntosPosicion[0].transform.position;
            
            pantallaOpciones.SetActive(true);
            controlesOpciones.SetActive(false);
            musicaOpciones.SetActive(false);
        }
        if(EventSystem.current.currentSelectedGameObject == bottonControles)
        {
            titulo.sprite = imagenes[1];
            puntoImagen.transform.position = puntosPosicion[1].transform.position;

            pantallaOpciones.SetActive(false);
            controlesOpciones.SetActive(true);
            musicaOpciones.SetActive(false);
        }
        if(EventSystem.current.currentSelectedGameObject == bottonMusica)
        {
            titulo.sprite = imagenes[2];
            puntoImagen.transform.position = puntosPosicion[2].transform.position;

            pantallaOpciones.SetActive(false);
            controlesOpciones.SetActive(false);
            musicaOpciones.SetActive(true);
        }
    }
}
