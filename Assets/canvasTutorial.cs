using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class canvasTutorial : MonoBehaviour
{
    public static int index;
    public bool estutorial = false;
    public Image imagenTutorial;
    public Sprite[] imagenes;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        imagenTutorial.sprite = imagenes[index];   
    }
}
