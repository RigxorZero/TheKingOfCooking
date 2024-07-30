using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class PersistentAudio : MonoBehaviour
{
    // Start is called before the first frame update
    static float volumen;


    public AudioSource myMusic;
    void Start()
    {   
     
   



    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<AudioSource>().volume = volumen; 
    }
}
