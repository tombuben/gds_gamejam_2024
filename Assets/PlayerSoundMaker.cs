using UnityEngine;

public class PlayerSoundMaker : MonoBehaviour
{
    public AudioClip[] audioClips;
    public AudioClip killedSound;
    public AudioClip scoreSound;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GlobalManager.Instance.Jumped += Jumped;
        GlobalManager.Instance.PlayerKilled += PlayerKilled;
        GlobalManager.Instance.TrpaslikKilled += TrpaslikKilled;
        GlobalManager.Instance.TrpaslikApologized += TrpaslikKilled;
        // Update is called 
    }

    private void TrpaslikKilled()
    {
        AudioSource.PlayClipAtPoint(scoreSound, transform.position, volume: 3f);
    }

    private void PlayerKilled()
    {
        AudioSource.PlayClipAtPoint(killedSound, transform.position, volume: 0.7f);
    }

    private void Jumped()
    {
        var i = Random.Range(0, audioClips.Length);
        AudioSource.PlayClipAtPoint(audioClips[i], transform.position, volume: 0.7f);
    }
}