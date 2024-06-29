using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ollaController : MonoBehaviour
{
    public GameObject[] aguaAnimation;
    public GameObject[] arrozAnimation;
    public int cantidadDeAgua; 
    public int cantidadDeArroz; 
    void Start()
    {
        cantidadDeAgua = 0;
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
