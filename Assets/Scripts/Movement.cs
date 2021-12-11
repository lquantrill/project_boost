using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rocketRigidbody;
    AudioSource rocketAudioSource;
    [SerializeField] float thrustMagnitude = 2000f;
    [SerializeField] float rotateMagnitude = 750f;
    
    // Start is called before the first frame update
    void Start()
    {
        rocketRigidbody = GetComponent<Rigidbody>();
        rocketAudioSource = GetComponent<AudioSource>();    
    }

    // Update is called once per frame
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
        }

        if(Input.GetKey(KeyCode.A))
        {
            RotateRocket("left");
        }
        else if(Input.GetKey(KeyCode.D))
        {
            RotateRocket("right");
        }
    }

    void ThrustRocket()
    {
        rocketRigidbody.AddRelativeForce(Vector3.up * thrustMagnitude * Time.deltaTime);
        if (!rocketAudioSource.isPlaying)
        {
            rocketAudioSource.Play();
        }
    }

    void RotateRocket(string direction)
    {
        if(direction == "left")
        {
            rocketRigidbody.AddRelativeTorque(Vector3.forward * rotateMagnitude * Time.deltaTime);
        }
        else
        {
            rocketRigidbody.AddRelativeTorque(-Vector3.forward * rotateMagnitude * Time.deltaTime);
        }
        
    }
}
