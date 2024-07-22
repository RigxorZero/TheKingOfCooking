using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class scoreFinal : MonoBehaviour
{
    // Start is called before the first frame updateç

    public TextMeshProUGUI score1;

    public TextMeshProUGUI score2;

    void Start()
    {
        score1.text = socreManagement.scores[0].ToString();
        score2.text = socreManagement.scores[1].ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
