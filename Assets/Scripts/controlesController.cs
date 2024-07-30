using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class controlesController : MonoBehaviour
{
    private Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();
    public TextMeshProUGUI arriba, abajo, derecha, izquierda, tomar, abrirCocina, interactuar, golpear; 

    private GameObject currentKey; 
    void Start()
    {
        keys.Add("Arriba", KeyCode.UpArrow);
        keys.Add("Abajo", KeyCode.DownArrow);
        keys.Add("Derecha", KeyCode.RightArrow);
        keys.Add("Izquierda", KeyCode.LeftArrow);
        keys.Add("Tomar", KeyCode.Z);
        keys.Add("AbrirCocina", KeyCode.C);
        keys.Add("Interactuar", KeyCode.X);
        keys.Add("Golpear", KeyCode.V);

        arriba.text = keys["Arriba"].ToString();
        abajo.text = keys["Abajo"].ToString();
        derecha.text = keys["Derecha"].ToString();
        izquierda.text = keys["Izquierda"].ToString();
        tomar.text = keys["Tomar"].ToString();
        abrirCocina.text = keys["AbrirCocina"].ToString();
        interactuar.text = keys["Interactuar"].ToString();
        golpear.text = keys["Golpear"].ToString();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnGUI()
    {
        if(currentKey != null)
        {
            Event e = Event.current;
            if (e.isKey)
            {
                keys[currentKey.name] = e.keyCode;
                currentKey.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = e.keyCode.ToString();
                
                currentKey = null;
            }
        }
    }

    public void changeKey(GameObject clicked)
    {
        currentKey = clicked;
    }
}
