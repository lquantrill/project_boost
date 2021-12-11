using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingHandler : MonoBehaviour
{
    AudioSource landingPadAudioSource;
    
    void Start()
    {
        landingPadAudioSource = GetComponent<AudioSource>();
    }
    
    void OnCollisionEnter(Collision other)
    {
        if(!landingPadAudioSource.isPlaying)
        {
            landingPadAudioSource.Play();
        } 
    }
}
