using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class playabledirector : MonoBehaviour
{

    public PlayableDirector director;
    [SerializeField] GameObject dialogbox;
    [SerializeField] GameObject ammowidget;

    // Update is called once per frame
    //void Update()
    //{
    //    if (director.stopped)
    //    {
    //        Debug.Log("selesai");
    //        dialogbox.SetActive(false);
    //    }

    //}

    void OnEnable()
    {
        director.stopped += OnPlayableDirectorStopped;
    }

    void OnPlayableDirectorStopped(PlayableDirector aDirector)
    {
        if (director == aDirector)
        {
            Debug.Log("PlayableDirector named " + aDirector.name + " is now stopped.");
            dialogbox.SetActive(false);
            ammowidget.SetActive(true);
        }
            
    }

    void OnDisable()
    {
        director.stopped -= OnPlayableDirectorStopped;
    }
}
