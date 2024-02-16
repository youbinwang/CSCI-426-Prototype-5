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

    // Start is called before the first frame update
    void Start()
    {
        beatMatching = BM.GetComponent<BeatMatching>();
    }

    // Update is called once per frame
    void Update()
    {
        if (beatMatching.isOntheBeat)
        {
            fb.PlayFeedbacks();
        }
        else
        {
            
        }
    }
}
