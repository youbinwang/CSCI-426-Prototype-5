using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameEnd : MonoBehaviour
{
    public TextMeshProUGUI scoreTextEnd;
    
    void Start()
    {
        scoreTextEnd.text = GameManager.score.ToString("00");
    }
    
    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.R))
        {
          
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
