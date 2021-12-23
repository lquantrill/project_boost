using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    AudioSource rocketAudioSource;
    GameObject landingPad;
    AudioSource landingPadAudioSource;
    Movement rocketMovement;

    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem explosionParticles;
    [SerializeField] AudioClip crashSound;

    bool isTransitioning = false;
    bool collisionEnabled = true;
    bool useDebug = false;
    string obstacleHit;

    void Start()
    {
        rocketAudioSource = GetComponent<AudioSource>();
        rocketMovement = GetComponent<Movement>();

        landingPad = GameObject.FindWithTag("Finish");
        landingPadAudioSource = landingPad.GetComponent<AudioSource>();
    }

    void Update()
    {
        RespondToDebugKeys();
    }

    void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            collisionEnabled = !collisionEnabled;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            useDebug = true;
            StartCoroutine(ReloadLevel(useDebug));
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            useDebug = true;
            StartCoroutine(LoadNextLevel(useDebug));
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if(!isTransitioning)
        {
            if(other.gameObject.tag == "Start"){}
            else if(other.gameObject.tag == "Finish")
            {
                obstacleHit = "Landing Pad";
                CrashHandler(obstacleHit);
            }
            else
            {
                if(collisionEnabled)
                {
                    obstacleHit = "Obstacle";
                    CrashHandler(obstacleHit);
                }
            }
        }
    }

    void CrashHandler(string obstacleHit)
    {
        isTransitioning = true;
        rocketMovement.enabled = false;
        rocketAudioSource.Stop();
        
        if(obstacleHit == "Landing Pad")
        {
            landingPadAudioSource.Play();
            successParticles.Play();
            StartCoroutine(LoadNextLevel(useDebug));
        }
        else if(obstacleHit == "Obstacle")
        {
            rocketAudioSource.PlayOneShot(crashSound);
            explosionParticles.Play();
            StartCoroutine(ReloadLevel(useDebug));
        }
    }

    IEnumerator LoadNextLevel(bool useDebug)
    {
        if(!useDebug)
        {
            yield return new WaitForSeconds(2);
        }
        
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            Application.Quit();
        }
        else
        {
            GameObject StartAnnouncement = GameObject.FindWithTag("Start Announcement");
            Destroy(StartAnnouncement);
            SceneManager.LoadScene(nextSceneIndex);
        }
    }

    IEnumerator ReloadLevel(bool useDebug)
    {
        if(!useDebug)
        {
            yield return new WaitForSeconds(2);
        }
        
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;      

        SceneManager.LoadScene(currentSceneIndex);
    }
}
