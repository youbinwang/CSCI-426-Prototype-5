using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static int score;
    public TextMeshProUGUI scoreText;

    public AudioClip diedSounds;
    private AudioSource audioSource;
    
    private void Start()
    {
       
        audioSource = GetComponent<AudioSource>();

        score = 0;
    }
    
    
    void Update()
    {
        scoreText.text = score.ToString("00");

        if (Input.GetKeyDown(KeyCode.R))
        {

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void PlayerDiedSound()
    {
        if (diedSounds != null)
        {
            AudioClip clip = diedSounds;
            audioSource.PlayOneShot(clip);
        }
    }
}
