using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool isPaused;

    public void RestartLevel()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
        SwipeManager.instance.enabled = true;
    }

    public void PauseLevel()
    {
        if (isPaused == true)
        {
            Time.timeScale = 1;
            isPaused = false;
            SwipeManager.instance.enabled = true;
            
            return;
        }

        if(isPaused == false)
        {
            Time.timeScale = 0;
            isPaused = true;
            SwipeManager.instance.enabled = false;
            return;
        }           
    }
    
}
