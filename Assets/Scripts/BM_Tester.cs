using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;
using MoreMountains.Tools;

public class BM_Tester : MonoBehaviour
{
    [Header("Beat Matching")]
    [SerializeField] public GameObject BM;
    private BeatMatching beatMatching;

    [Header("Test Feedbacks")]
    [SerializeField] public MMFeedbacks fb;
    
    
    
    void Start()
    {
        beatMatching = BM.GetComponent<BeatMatching>();
    }

    
    

    void Update()
    {
        if (beatMatching.isOntheBeat)
        {
            //在Beat上的Feedback效果
            fb.PlayFeedbacks();
        }
        else
        {
            
        }
    }
}
