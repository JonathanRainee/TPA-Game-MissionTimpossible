using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthmanagement : MonoBehaviour
{

    public int maxhealth = 100;
    public int currhealth;
    public healthbar bar;

    public AudioSource deathSound;

    public GameObject deathcanvasparent;
    public GameObject healthbossparent;
    public GameObject questparent;
    public GameObject armoryparent;
    public GameObject dialogparent;
    public GameObject healthparent;

    CanvasGroup cg;
    public Canvas deathScreen;

    public bsptele bspscript;
    public GameObject dsp;
    public BossAreaScript bas;

    // Start is called before the first frame update
    void Start()
    {
        currhealth = maxhealth;
        bar.setmaxhealth(maxhealth);
        cg = deathScreen.GetComponent<CanvasGroup>();
        cg.alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(bspscript.teleported == true)
        {
            currhealth = maxhealth;
            bar.sethealth(currhealth);
            bspscript.teleported = false;
        }
        //test
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    takedmg(10);
        //}
        if(currhealth <= 0)
        {
            dsp.SetActive(true);
            bas.battlesound.Stop();
            deathcanvasparent.SetActive(true);
            deathSound.Play();
            cg = deathScreen.GetComponent<CanvasGroup>();
            cg.alpha += Time.deltaTime;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;


            healthbossparent.SetActive(false);
            questparent.SetActive(false);
            armoryparent.SetActive(false);
            dialogparent.SetActive(false);
            healthparent.SetActive(false);
            //deathSound.Play();

        }
    }

    public void takedmg(int dmg)
    {
        currhealth -= dmg;
        bar.sethealth(currhealth); 
    }
}
