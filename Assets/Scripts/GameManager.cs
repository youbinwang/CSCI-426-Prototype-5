using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static int score;
    public TextMeshProUGUI scoreText;

    public AudioClip diedSounds;
    private AudioSource audioSource;
    
    private void Start()
    {
       
        audioSource = GetComponent<AudioSource>();
    }
    
    
    void Update()
    {
        scoreText.text = score.ToString("00");
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
