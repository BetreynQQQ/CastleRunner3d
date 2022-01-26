using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    private bool isPaused;
      
    public void RestartLevel()
    {

        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void PauseLevel()
    {
        if (isPaused == true)
        {
            Time.timeScale = 1;
            isPaused = false;
            return;
        }

        if(isPaused == false)
        {
            Time.timeScale = 0;
            isPaused = true;
            return;
        }           
    }
    
}
