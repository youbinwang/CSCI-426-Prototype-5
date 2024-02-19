using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class BeatMatching : MonoBehaviour
{
    [Header("Beat Matching")]
    [SerializeField] public AudioSource AS;
    [SerializeField] public float MusicBPM;
    [SerializeField] public int NoteValue = 4; //1/4音符的时值
    [SerializeField] public bool isOntheBeat;

    public float musicTimer { get; private set; }
    public float secondPerBeat { get; private set; }
    private float thisBeatTime;
    private float nextBeatTime;

    private int beatCount = 0;
    public event Action<int> OnBeat;

    //Music Start
    [SerializeField] public bool isStartPlay;
    private bool isMusicTimerStart;

    void Start()
    {
        AS = GetComponent<AudioSource>();
        isStartPlay = false;

        //secondPerBeat = (60 / MusicBPM) / (NoteValue / 4);
        //musicTimer = 0.0f;
        //thisBeatTime = 0;
        //nextBeatTime = secondPerBeat;
        //InvokeRepeating("BeatTick", 0.0f, secondPerBeat);
    }
    
    void Update()
    {
        if (Input.GetKey("space") && !isStartPlay)
        {
            isStartPlay = true;

            AS.loop = true;
            AS.Play();
            BeatMatchingStart();
        }

        if (isMusicTimerStart)
        {
            musicTimer += Time.deltaTime;
            //Debug.Log("Music Time: " + musicTimer);
        }

        if (isStartPlay)
        {
            //Debug.Log("Music Time: " + musicTimer);
            //Debug.Log("Music Time: " + musicTimer + " Beat tick: " + thisBeatTime + " Beat tick Next: " + nextBeatTime);
            IsOntheBeat();
        }
    }

    void BeatMatchingStart()
    {
        Debug.Log("BeatMatchingStar!");

        secondPerBeat = (60 / MusicBPM) / (NoteValue / 4);
        //secondPerBeat = 0.25f;

        Debug.Log("SPB: " + secondPerBeat);

        musicTimer = 0.0f;
        isMusicTimerStart = true;

        thisBeatTime = 0;
        nextBeatTime = secondPerBeat;

        //StartCoroutine(BeatChecker());

        InvokeRepeating("BeatTick", 0.0f, secondPerBeat);
    }

    void BeatTick()
    {
        thisBeatTime += secondPerBeat;
        nextBeatTime += secondPerBeat;
        beatCount++;
        
        OnBeat?.Invoke(beatCount);

        if (beatCount == 4)
        {
            //第四拍的动作
            Debug.Log("Fourth Beat!");
            
            beatCount = 0;
        }
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
