using UnityEngine;

public class DestroyAfterEffect : MonoBehaviour
{
    private ParticleSystem splashFX;

    void Start()
    {
        splashFX = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        // If the particle system has stopped playing, destroy the GameObject
        if (splashFX != null && !splashFX.IsAlive())
        {
            Destroy(gameObject);
        }
    }
}
