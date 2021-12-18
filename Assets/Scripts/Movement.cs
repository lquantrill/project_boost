using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rocketRigidbody;
    AudioSource rocketAudioSource;
    [SerializeField] AudioClip thrusterSound;
    [SerializeField] ParticleSystem thrusterParticles;
    [SerializeField] ParticleSystem sideThrusterRightParticles;
    [SerializeField] ParticleSystem sideThrusterLeftParticles;
    [SerializeField] float thrustMagnitude = 2000f;
    [SerializeField] float rotateMagnitude = 750f;
    
    void Start()
    {
        rocketRigidbody = GetComponent<Rigidbody>();
        rocketAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessInput();
    }

    void ProcessInput()
    {
        if(Input.GetKey(KeyCode.W))
        {
            ThrustRocket();
        }
        else
        {
            rocketAudioSource.Stop();
            thrusterParticles.Stop();
        }

        if(Input.GetKey(KeyCode.A))
        {
            RotateRocket("left");
        }
        else if(Input.GetKey(KeyCode.D))
        {
            RotateRocket("right");
        }
        else
        {
            sideThrusterRightParticles.Stop();
            sideThrusterLeftParticles.Stop();
        }
    }

    void ThrustRocket()
    {
        rocketRigidbody.AddRelativeForce(Vector3.up * thrustMagnitude * Time.deltaTime);
        if(!rocketAudioSource.isPlaying)
        {
            rocketAudioSource.PlayOneShot(thrusterSound);
        }
        if(!thrusterParticles.isPlaying)
        {
            thrusterParticles.Play();
        }
    }

    void RotateRocket(string direction)
    {
        if(direction == "left")
        {
            rocketRigidbody.AddRelativeTorque(Vector3.forward * rotateMagnitude * Time.deltaTime);
            if(sideThrusterLeftParticles.isPlaying)
            {
                sideThrusterLeftParticles.Stop();
            }
            if(!sideThrusterRightParticles.isPlaying)
            {
                sideThrusterRightParticles.Play();
            }
        }
        else
        {
            rocketRigidbody.AddRelativeTorque(-Vector3.forward * rotateMagnitude * Time.deltaTime);
            if(sideThrusterRightParticles.isPlaying)
            {
                sideThrusterRightParticles.Stop();
            }
            if(!sideThrusterLeftParticles.isPlaying)
            {
                sideThrusterLeftParticles.Play();
            }
        } 
    }
}
