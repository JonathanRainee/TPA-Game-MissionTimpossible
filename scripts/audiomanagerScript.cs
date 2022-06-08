using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class audiomanager : MonoBehaviour
{
    public static audiomanager instance;
    public bool ispaused = false;

    void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }

        if (SceneManager.GetActiveScene().name == "game")
        {
            if (Input.GetKeyDown(KeyCode.Escape) && ispaused == false)
            {
                audiomanager.instance.GetComponent<AudioSource>().Pause();
                ispaused = true;
            }

            else if (Input.GetKeyDown(KeyCode.Escape) && ispaused == true)
            {
                audiomanager.instance.GetComponent<AudioSource>().Play();
                ispaused = false;
            }
        }

    }
}
