using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hidecursor : MonoBehaviour
{

    pausescript pause = new pausescript();
    bool ispaused = false;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(ispaused == true)
            {
                ispaused = false;
                Cursor.visible = false;

            }
        }else if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (ispaused == false)
            {
                ispaused = true;
                Cursor.visible = true;
            }
        }
    }
}
