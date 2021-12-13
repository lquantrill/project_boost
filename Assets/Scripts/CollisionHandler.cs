using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    bool doneFlag = false;
    
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Start"){}
        else if(other.gameObject.tag == "Finish")
        {
            doneFlag = true;
            GetComponent<Movement>().enabled = false;
            GameObject rocket = GameObject.Find("Rocket");
            rocket.GetComponent<AudioSource>().Stop();
            StartCoroutine(LoadNextLevel());
        }
        else
        {
            if(!doneFlag)
            {
                CrashHandler();
            }  
        }
    }

    void CrashHandler()
    {
        GetComponent<Movement>().enabled = false;
        GameObject rocket = GameObject.Find("Rocket");
        rocket.GetComponent<AudioSource>().Stop();
        Invoke("ReloadLevel", 1);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;      

        SceneManager.LoadScene(currentSceneIndex);
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
}
