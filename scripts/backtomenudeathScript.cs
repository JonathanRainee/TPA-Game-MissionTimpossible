using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class backtomenudeath : MonoBehaviour
{
    public void MoveToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
