using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    bool isTransitioning = false;
    string obstacleHit;
    AudioSource rocketAudioSource;
    GameObject landingPad;
    AudioSource landingPadAudioSource;
    Movement rocketMovement;
    [SerializeField] AudioClip crashSound;

    void Start() {
        rocketAudioSource = GetComponent<AudioSource>();
        landingPad = GameObject.FindWithTag("Finish");
        landingPadAudioSource = landingPad.GetComponent<AudioSource>();
        rocketMovement = GetComponent<Movement>();
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
                obstacleHit = "Obstacle";
                CrashHandler(obstacleHit);  
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
            StartCoroutine(LoadNextLevel());
        }
        else if(obstacleHit == "Obstacle")
        {
            rocketAudioSource.PlayOneShot(crashSound);
            StartCoroutine(ReloadLevel());
        }
    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(2);
        
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

    IEnumerator ReloadLevel()
    {
        yield return new WaitForSeconds(2);
        
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;      

        SceneManager.LoadScene(currentSceneIndex);
    }
}
