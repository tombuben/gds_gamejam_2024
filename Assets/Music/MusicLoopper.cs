using UnityEngine;

public class MusicLoopper : MonoBehaviour
{
    public AudioClip music2D;
    public AudioClip music2dLoop;
    public AudioClip music3D;
    public AudioClip music3DLoop;

    [HideInInspector] public AudioSource musicAudioSource;

    bool loopIntroMusic = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        musicAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        LoopIntroMusic();
    }

    private void LoopIntroMusic()
    {
        if (loopIntroMusic)
        {
            if (!musicAudioSource.isPlaying)
            {
                musicAudioSource.resource = music2dLoop;
                musicAudioSource.loop = true;
                musicAudioSource.Play();

                loopIntroMusic = false;
            }
        }
    }

    public void StopLoopingIntroMusic()
    {
        musicAudioSource.loop = false;
    }
}
