using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatMatching : MonoBehaviour
{
    [Header("Beat Matching")]
    [SerializeField] public AudioSource AS;
    [SerializeField] public float MusicBPM;
    [SerializeField] public int NoteValue = 4; //1/4音符的时值
    [SerializeField] public bool isOntheBeat;

    private float musicTimer;
    private float secondPerBeat;
    private float thisBeatTime;
    private float nextBeatTime;

    // Start is called before the first frame update
    void Start()
    {
        AS = GetComponent<AudioSource>();

        //Set UP
        secondPerBeat = (60 / MusicBPM) / (NoteValue / 4);
        musicTimer = 0.0f;
        thisBeatTime = 0;
        nextBeatTime = secondPerBeat;
        InvokeRepeating("BeatTick", 0.0f, secondPerBeat);
    }
    
    void Update()
    {
        musicTimer += Time.deltaTime;

        IsOntheBeat();
    }

    void BeatTick()
    {
        thisBeatTime += secondPerBeat;
        nextBeatTime += secondPerBeat;
    }

    bool IsOntheBeat()
    {
        if (Mathf.Abs(thisBeatTime - musicTimer) <= 0.03f)
        {
            isOntheBeat = true;
            //在节拍上的判定
            return true;
        }
        else
        {
            isOntheBeat = false;
            return false;
        }
    }
}
