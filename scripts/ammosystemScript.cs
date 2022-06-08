using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ammosystem : MonoBehaviour
{

    public int clipsize;
    public int extraammo;
    public int currammo;
    // Start is called before the first frame update
    void Start() 
    {
        currammo = clipsize;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) reload(); 
    }

    public void reload()
    {
        if(extraammo >= clipsize)
        {
            int ammotoreload = clipsize - currammo;
            extraammo -= ammotoreload;
            currammo += ammotoreload;
        }
        else if(extraammo > 0)
        {
            if(extraammo + currammo > clipsize)
            {
                int leftover = extraammo + currammo - clipsize;
                extraammo = leftover;
                currammo = clipsize;
            }
            else
            {
                currammo += extraammo;
                extraammo = 0;
            }
        }
    }

}
