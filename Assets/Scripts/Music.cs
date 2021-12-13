using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    AudioSource backgroundAudioSource;
    
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Music");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
            backgroundAudioSource = GetComponent<AudioSource>();

            if(!backgroundAudioSource.isPlaying)
            {
                backgroundAudioSource.Play();
            }
        }
    }
}
