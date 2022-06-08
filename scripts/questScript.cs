using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class quest : MonoBehaviour
{

    int idx = 0;
    public List<string> list = new List<string>();
    public bool finishedstat;
    public bool pickedpistol;
    talkto talktoasuna = new talkto();
    [SerializeField] talkto talktoscript;

    public TextMeshProUGUI maintext;
    public TextMeshProUGUI shootwpistoltotal;
    public TextMeshProUGUI shootwriffletotal;
    public TextMeshProUGUI killsoldiertotal;
    public TextMeshProUGUI killguardtotal;
    //public TextMeshProUGUI asunaquest;
    //public TextMeshProUGUI pistolquest;
    //public TextMeshProUGUI shootpistolquest;
    //public TextMeshProUGUI rifflequest;
    //public TextMeshProUGUI questisdone;


    public int shootwpistol;
    public int shootwriffle;
    public int soldierkill;
    public int guardkill;
    public int qidx;

    public GameObject pistolmission;
    public GameObject rifflemission;
    public GameObject villagemission;
    public GameObject secretroommission;

    public GameObject asunaparent;
    public GameObject parentpistol;
    public GameObject parerntriffle;
    public GameObject questdoneparent;
    public GameObject enemyparent;
    public GameObject bossparent;

    // Start is called before the first frame update
    void Start()
    {
        pickedpistol = false;
        questgenerator();
        qidx = 0;
        shootwpistol = 0;
        shootwriffle = 0;
        soldierkill = 0;
        guardkill = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("total guard kill: " + guardkill);
        }

        // if (talktoasuna.firstquest == true)
        //{
        //    Debug.Log("selesai satu");
        //    qidx++;
        //}

        maintext.text = list[qidx];

        if(qidx == 2)
        {
            pistolmission.SetActive(true);
            shootwpistoltotal.color = Color.yellow;
            shootwpistoltotal.SetText("(" + shootwpistol.ToString() + " / 10)");

            if(shootwpistol == 10)
            {
                maintext.color = Color.green;
                shootwpistoltotal.color = Color.green;
                talktoscript.doneparent.SetActive(true);
                finishedstat = true;
            }
            else
            {
                finishedstat = false;
            }
        }
        if(qidx == 3)
        {
            rifflemission.SetActive(true);
            shootwriffletotal.color = Color.yellow;
            shootwriffletotal.SetText("(" + shootwriffle.ToString() + " / 50)");

            if(shootwriffle == 50)
            {
                maintext.color = Color.green;
                shootwriffletotal.color = Color.green;
                talktoscript.doneparent.SetActive(true);
                finishedstat = true;
            }
            else
            {
                finishedstat = false;
            }
        }
        if(qidx == 4)
        {
            villagemission.SetActive(true);
            killsoldiertotal.color = Color.yellow;
            killsoldiertotal.SetText("(" + soldierkill.ToString() + " / 1)");
            
            if(soldierkill == 1)
            {
                maintext.color = Color.green;
                killsoldiertotal.color = Color.green;
                talktoscript.doneparent.SetActive(true);
                finishedstat = true;
            }
            else
            {
                finishedstat = false;
            }
        }if(qidx == 5)
        {

        }

       

    }

    public void questgenerator()
    {
        list.Add("Find 'Asuna' and talk to her!");  
        list.Add("Pick up the pistol!");
        list.Add("Shoot 10 rounds at the shooting target!");
        list.Add("Pick up the riffle and shoot 50 bullets with the rifle!");
        list.Add("Eliminate the soldiers that are attacking the village"); 
        list.Add("Head to the secret teleport room and defeat the boss!");
    }
}
