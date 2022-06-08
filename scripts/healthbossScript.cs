using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthboss : MonoBehaviour
{

    public int maxhealth = 2000;
    public int currhealth;
    public healthbar bar;
    bool flag;
    private Transform _transform;

    CanvasGroup cg;
    public Canvas victoryscreen;
    public AudioSource victorysound;

    public GameObject victcanvasparent;
    public GameObject healthbossparent;
    public GameObject questparent;
    public GameObject armoryparent;
    public GameObject dialogparent;
    public GameObject healthparent;
    public GameObject dsp;
    public BossAreaScript bas;
    public AudioSource deathSound;


    // Start is called before the first frame update
    void Start()
    {
        currhealth = maxhealth;
        //currhealth = 0;
        bar.setmaxhealth(maxhealth);
        flag = false;
        cg = victoryscreen.GetComponent<CanvasGroup>();
        cg.alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("space"))
        {
            currhealth -= 100;
            bar.sethealth(currhealth);
        }

        if (currhealth <= 0)
        {
            if (flag == false)
            {
                if (this.transform.position.y <= -3)
                {
                    Destroy(this.gameObject);
                }


                flag = true;
            }

            victcanvasparent.SetActive(true);
            deathSound.Play();
            cg = victoryscreen.GetComponent<CanvasGroup>();
            cg.alpha += Time.deltaTime;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            victorysound.Play();
            healthbossparent.SetActive(false);
            questparent.SetActive(false);
            armoryparent.SetActive(false);
            dialogparent.SetActive(false);
            healthparent.SetActive(false);

            transform.position += Vector3.down * Time.deltaTime;
        }
    }

    public void takedmg(int dmg)
    {
        currhealth -= dmg;
        bar.sethealth(currhealth);
    }
}
