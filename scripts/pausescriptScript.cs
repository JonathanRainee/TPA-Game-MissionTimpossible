using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pausescript : MonoBehaviour
{
    //public static bool paused = false;
    public bool paused = false;
    public GameObject pauseMenu;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }    
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        paused = false;

    }

    void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        paused = true;
    }

    public void quit()
    {
        //Debug.Log("quit");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);

        //SceneManager.LoadScene("mainmenu");
        Time.timeScale = 1f;
    }
}
