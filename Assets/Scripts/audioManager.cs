using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class audioManager : MonoBehaviour
{
    // Start is called before the first frame update

    public AudioSource audioSource;
    
    //public 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        audioSource.volume = controlarVolumen.Instance.obtenerVolumen();
    }
}
