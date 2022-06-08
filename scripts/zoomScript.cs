using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class zoom : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera vcam;
   

    // Start is called before the first frame update
    bool zoomed;

    

    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        zoomed = Input.GetMouseButton(1);

        if (zoomed)
        {
            vcam.gameObject.SetActive(true);
        }
        else
        {
            vcam.gameObject.SetActive(false);
        }

    }
}