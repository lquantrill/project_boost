using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    AudioSource backgroundAudioSource;
    
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        backgroundAudioSource = GetComponent<AudioSource>();

        if(!backgroundAudioSource.isPlaying)
        {
            backgroundAudioSource.Play();
        }
    }
}
