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


    void Start()
    {
        AS = GetComponent<AudioSource>();
        
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
