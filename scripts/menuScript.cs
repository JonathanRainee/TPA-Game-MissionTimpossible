using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
    public void play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //SceneManager.LoadScene("game");
        Time.timeScale = 1f;
    }

    public void quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
