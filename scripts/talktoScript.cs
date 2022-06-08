using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Playables;

public class talkto : MonoBehaviour
{
    public bool firstquest;
    public PlayableDirector directorcamera;
    public PlayableDirector directorshootingrange;
    public PlayableDirector directorriffle;
    public PlayableDirector directortunnel;
    public PlayableDirector directorgate;
    public List<string> list = new List<string>();
    public PlayableDirector pistoldirector;
    public bool questfinishied = false;
    [SerializeField] public GameObject doneparent;
    [SerializeField] TextMeshProUGUI donetext;
    [SerializeField] TextMeshProUGUI spacetext;
    [SerializeField] GameObject spaceparent;
    string texttest = "";
    [SerializeField] GameObject dialogbox;
    [SerializeField] GameObject talktoasuna;
    [SerializeField] TextMeshProUGUI currtext;
    [SerializeField] public LayerMask player;
    [SerializeField] quest questscript;
    public float sphereRadius;
    int counter = 0;
    public Animator anim;

    public bool activedialog;
    public bool resume;
    public bool isplaying;
    public bool pressftotalk;

    // Start is called before the first frame update
    void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        dialoggen();
        firstquest = false;
        pressftotalk = true;
        questscript.maintext.color = Color.yellow;
        //pistoldirector.played += Director_Played;
        //pistoldirector.stopped += Director.Stopped;
    }

    // Update is called once per frame
    void Update()
    {

        //currtext.text = list[counter];

        //if (questfinishied == true) {
        //    counter++;
        //}



        bool checknear = Physics.CheckSphere(transform.position, sphereRadius, player);
        //Debug.Log(texttest);
        if (checknear)
        {
            //talktoasuna.SetActive(true);

            if (pressftotalk)
            {
                talktoasuna.SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                //questscript.qidx++;
                //questscript.finishedstat = false;
                Debug.Log("selesai satu");
                firstquest = true;
                resume = false;
                talktoasuna.SetActive(false);
                currtext.text = list[counter];
                dialogbox.SetActive(true);
                spacetext.color = Color.green;
                spaceparent.SetActive(true);
                Time.timeScale = 0f;
                pressftotalk = false;
                activedialog = false;

               


                if (questscript.qidx >= 1 && questscript.finishedstat == false)
                {
                    currtext.SetText("You Haven't finished your task yet, come back when you've finished it");
                    dialogbox.SetActive(true);
                    spaceparent.SetActive(true);
                    Time.timeScale = 0f;
                    activedialog = true;
                    pressftotalk = false;
                }

                

                if (questscript.finishedstat)
                {
                    Debug.Log("quest selesai");
                }

                if (questscript.qidx == 0)
                {
                    counter++;
                    questscript.maintext.color = Color.yellow;
                    directorcamera.Play();
                    questscript.qidx++;
                }
                else if (questscript.qidx == 1 && questscript.pickedpistol == true)
                {
                    counter++;
                    questscript.maintext.color = Color.yellow;
                    questscript.qidx++;
                    doneparent.SetActive(false);
                    directorshootingrange.Play();
                }
                else if (questscript.qidx == 2 && questscript.shootwpistol == 10)
                {
                    counter++;
                    questscript.maintext.color = Color.yellow;
                    questscript.qidx++;
                    questscript.pistolmission.SetActive(false);
                    doneparent.SetActive(false);
                    directorriffle.Play();
                }else if (questscript.qidx == 3 && questscript.shootwriffle == 50)
                {
                    counter++;
                    questscript.maintext.color = Color.yellow;
                    questscript.qidx++;
                    questscript.rifflemission.SetActive(false);
                    doneparent.SetActive(false);
                    directortunnel.Play();
                    anim.Play("tunneldoor");
                }
                else if (questscript.qidx == 4 && questscript.soldierkill == 1)
                {
                    counter++;
                    questscript.maintext.color = Color.yellow;
                    questscript.qidx++;
                    questscript.villagemission.SetActive(false);
                    doneparent.SetActive(false);
                    directorgate.Play();
                }
            }
            


            if (Input.GetKeyDown(KeyCode.Space) && questscript.qidx >= 1 && questscript.finishedstat == false && activedialog == false)
                {
                    Time.timeScale = 1f;
                    Debug.Log("udah bisa jalan");
                    resume = true;
                    isplaying = true;
                    pressftotalk = true;
                }

            if (Input.GetKeyDown(KeyCode.Space) && questscript.qidx >= 1 && questscript.finishedstat == true && activedialog == false)
            {
                Time.timeScale = 1f;
                Debug.Log("udah bisa jalan");
                resume = true;
                isplaying = true;
                pressftotalk = true;
            }

            if (Input.GetKeyDown(KeyCode.Space) && activedialog == true)
            {
                    dialogbox.SetActive(false);
                    activedialog = false;
                    Time.timeScale = 1f;
                    pressftotalk = true;
            }

            if (isplaying)
            {
                    talktoasuna.SetActive(false);
            }
            else
            {
                    talktoasuna.SetActive(true);
            }

            if (!pressftotalk)
            {
                talktoasuna.SetActive(false);
            }
           



            bool isfinished()
            {
                if (firstquest)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            
        }
        else
        {
            talktoasuna.SetActive(false);
        }

        
    }

    void dialoggen()
    {
        list.Add("Go pick up a pistol and start shooting");
        list.Add("Shoot 10 rounds at the shooting range!");
        list.Add("Pick up the riffle and shoot 50 bullets with the rifle!");
        list.Add("Eliminate the soldiers that are attacking the village");
        list.Add("Head to the secret teleport room and defeat the boss!");
    }

    void OnEnable()
    {
        directorcamera.stopped += OnPlayableDirectorStopped;
        directorshootingrange.stopped += OnPlayableDirectorStopped;
        directorriffle.stopped += OnPlayableDirectorStopped;
        directortunnel.stopped += OnPlayableDirectorStopped;
        directorgate.stopped += OnPlayableDirectorStopped;
    }

    void OnPlayableDirectorStopped(PlayableDirector adirector)
    {
        if (directorcamera == adirector)
        {
            isplaying = false;
            dialogbox.SetActive(false);
        }

        if (directorshootingrange == adirector)
        {
            isplaying = false;
            dialogbox.SetActive(false);
        }

        if (directorriffle == adirector)
        {
            isplaying = false;
            dialogbox.SetActive(false);
        }

        if(directortunnel == adirector)
        {
            isplaying = false;
            dialogbox.SetActive(false);
        }
        if (directorgate == adirector)
        {
            isplaying = false;
            dialogbox.SetActive(false);
        }
    }

    void OnDisable()
    {
        directorcamera.stopped -= OnPlayableDirectorStopped;
        directorshootingrange.stopped -= OnPlayableDirectorStopped;
        directorriffle.stopped -= OnPlayableDirectorStopped;
        directortunnel.stopped -= OnPlayableDirectorStopped;
        directorgate.stopped -= OnPlayableDirectorStopped;
    }
}