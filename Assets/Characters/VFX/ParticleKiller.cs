using UnityEngine;

public class ParticleKiller : MonoBehaviour
{
    [SerializeField] private ParticleSystem particleSystem;

    private float deathTime;

    private void OnEnable()
    {
        // Get the total duration of the particle system
        deathTime = particleSystem.main.duration + particleSystem.main.startLifetime.constant;
    }

    private void Update()
    {
        // Count down and destroy the particle system when its duration ends
        if (deathTime > 0)
        {
            deathTime -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}