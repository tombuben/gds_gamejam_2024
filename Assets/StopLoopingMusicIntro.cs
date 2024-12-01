using System.Diagnostics;
using UnityEngine;

public class StopLoopingMusicIntro : MonoBehaviour
{
    public GameObject musicController;
    public MusicLoopper musicLooper;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        musicLooper = musicController.GetComponent<MusicLoopper>();
    }

    private void OnTriggerEnter(Collider other)
    {
        musicLooper.StopLoopingIntroMusic();
    }
}
