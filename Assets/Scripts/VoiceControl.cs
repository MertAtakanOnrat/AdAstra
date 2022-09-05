using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceControl : MonoBehaviour
{
    [SerializeField]
    AudioClip asteroidExplosion = default;

    [SerializeField]
    AudioClip spaceshipExplosion = default;

    [SerializeField]
    AudioClip fire = default;

    AudioSource audioSource;
        
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void AsteroidExplosion()
    {
        audioSource.PlayOneShot(asteroidExplosion, 0.05f);
    }

    public void SpaceshipExplosion()
    {
        audioSource.PlayOneShot(spaceshipExplosion, 0.1f);
    }

    public void Fire()
    {
        audioSource.PlayOneShot(fire, 0.02f);
    }
}
