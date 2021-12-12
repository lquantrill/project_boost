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
            StartCoroutine(LoadNextLevel());
        }
        else
        {
            if(!doneFlag)
            {
                ReloadLevel();
            }  
        }
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
            Debug.Log("Finished");
            Application.Quit();
        }
        else
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }
}
