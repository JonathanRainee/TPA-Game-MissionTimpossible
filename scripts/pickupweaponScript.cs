using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using TMPro;


public class pickupweapon : MonoBehaviour
{

    //public quest questscript;
    public weaponscript rifflefab;
    public weaponscript pistolfab;
    //public activeweapon currweapon;
    [SerializeField] public LayerMask layerpick;
    [SerializeField] public LayerMask layerpick2;
    [SerializeField] public LayerMask player;
    public Transform equipPos;
    float radius = 1.5f;
    public float distance = 2.0f;
    [SerializeField] Camera weaponcam;
    [SerializeField] GameObject weaponaim;
    [SerializeField] GameObject pistolcanvas;
    [SerializeField] GameObject rifflecanvas;
    [SerializeField] quest questlist;
    [SerializeField] talkto talktoasuna;
    [SerializeField] GameObject doneparent;
    [SerializeField] TextMeshProUGUI donetext;
    bool pickpistol;
    bool pickriffle;
    bool checknear;
    public activeweapon activeweaponscript;

    public bool grab;


    // Start is called before the first frame update
    void Start()
    {
        pickpistol = false;
        pickriffle = false;



    }
    


    // Update is called once per frame
    void Update()
    {

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    Debug.Log("quest no "+questlist.qidx);
        //}

        RaycastHit hit;
        //Ray ray = new Ray(weaponcam.transform.position, weaponcam.transform.forward);
        Ray ray = weaponcam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        //checknear = Physics.CheckSphere(transform.position, radius, player);

        //if (checknear)
        //{
        //    pistolcanvas.SetActive(true);
        //}

        // KALO KENA SESUATU
        if (Physics.Raycast(ray, out hit, distance, layerpick) && questlist.qidx == 1)
        {
            
            
            //Debug.Log(hit);
            pistolcanvas.SetActive(true);


            if (Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("pencet f");
                if (hit.transform.tag == "riffle" || checknear == true)
                {
                    activeweaponscript.rifflecounterparent.SetActive(true);
                    activeweaponscript.pistolcounterparent.SetActive(false);
                    activeweaponscript.refreshriffle(activeweaponscript.rifflecurrammo, activeweaponscript.riffleammocount);
                    questlist.finishedstat = true;
                    questlist.maintext.color = Color.green;
                    pickriffle = true;
                    activeweapon currweapon = GetComponent<activeweapon>();
                    pistolcanvas.SetActive(false);
                    if (currweapon)
                    {
                        Debug.Log("1");
                        Destroy(hit.transform.gameObject);
                        Debug.Log("2");

                        weaponscript newweapon = GetComponentInChildren<weaponscript>();
                        newweapon = Instantiate(rifflefab);


                        if (newweapon)
                        {
                            
                            currweapon.equip(newweapon);
                        }
                        
                        Debug.Log("4");
                    }

                    
                    //Debug.Log("3");
                    //newweapon = GetComponentInChildren<weaponscript>();
                    ////if (rifflefab)
                    ////{
                    ////currweapon.equip(rifflefab);
                    ////}
                    //currweapon.equip(newweapon);
                    //Debug.Log("4");
                }

                else if (hit.transform.tag == "pistol")
                {
                    activeweaponscript.rifflecounterparent.SetActive(false);
                    activeweaponscript.pistolcounterparent.SetActive(true);
                    activeweaponscript.refreshpistol(activeweaponscript.pistolcurrammo, activeweaponscript.pistolammocount);
                    pickpistol = true;
                    questlist.finishedstat = true;
                    talktoasuna.questfinishied = true;
                    questlist.maintext.color = Color.green;
                    questlist.pickedpistol = true;
                    donetext.color = Color.green;
                    doneparent.SetActive(true);
                    activeweapon currweapon = GetComponent<activeweapon>();
                    pistolcanvas.SetActive(false);
                    if (currweapon)
                    {
                        Debug.Log("1");
                        Destroy(hit.transform.gameObject);
                        Debug.Log("2");

                        weaponscript newweapon = GetComponentInChildren<weaponscript>();
                        newweapon = Instantiate(pistolfab);

                        if (newweapon)
                        {

                            currweapon.equip(newweapon);
                        }
                        Debug.Log("4");
                    }
                    //Debug.Log("1");
                    //Destroy(hit.transform.gameObject);
                    //Debug.Log("2");
                    //weaponscript newweapon = Instantiate(pistolfab);
                    //Debug.Log("3");
                    //currweapon.equip(newweapon);
                    //Debug.Log("4");
                }
                else
                {
                    Debug.Log("ga kena");
                }
            }

        }else if (Physics.Raycast(ray, out hit, distance, layerpick2) && questlist.qidx == 3)
        {
            Debug.Log(hit);
            rifflecanvas.SetActive(true);


            if (Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("pencet f");
                if (hit.transform.tag == "riffle")
                {
                    activeweaponscript.rifflecounterparent.SetActive(true);
                    activeweaponscript.pistolcounterparent.SetActive(false);
                    activeweaponscript.refreshriffle(activeweaponscript.rifflecurrammo, activeweaponscript.riffleammocount);
                    questlist.finishedstat = true;
                    pickriffle = true;
                    activeweapon currweapon = GetComponent<activeweapon>();
                    rifflecanvas.SetActive(false);
                    if (currweapon)
                    {
                        Debug.Log("1");
                        Destroy(hit.transform.gameObject);
                        Debug.Log("2");

                        weaponscript newweapon = GetComponentInChildren<weaponscript>();
                        newweapon = Instantiate(rifflefab);

                        if (newweapon)
                        {

                            currweapon.equip(newweapon);
                        }

                        Debug.Log("4");
                    }


                    //Debug.Log("3");
                    //newweapon = GetComponentInChildren<weaponscript>();
                    ////if (rifflefab)
                    ////{
                    ////currweapon.equip(rifflefab);
                    ////}
                    //currweapon.equip(newweapon);
                    //Debug.Log("4");
                }

                if (hit.transform.tag == "pistol")
                {
                    activeweaponscript.rifflecounterparent.SetActive(false);
                    activeweaponscript.pistolcounterparent.SetActive(true);
                    activeweaponscript.refreshpistol(activeweaponscript.pistolcurrammo, activeweaponscript.pistolammocount);
                    questlist.finishedstat = true;
                    pickpistol = true;
                    activeweapon currweapon = GetComponent<activeweapon>();
                    pistolcanvas.SetActive(false);
                    if (currweapon)
                    {
                        Debug.Log("1");
                        Destroy(hit.transform.gameObject);
                        Debug.Log("2");

                        weaponscript newweapon = GetComponentInChildren<weaponscript>();
                        newweapon = Instantiate(pistolfab);

                        if (newweapon)
                        {
                            currweapon.equip(newweapon);
                        }
                        Debug.Log("4");
                    }
                    //Debug.Log("1");
                    //Destroy(hit.transform.gameObject);
                    //Debug.Log("2");
                    //weaponscript newweapon = Instantiate(pistolfab);
                    //Debug.Log("3");
                    //currweapon.equip(newweapon);
                    //Debug.Log("4");
                }
            }
        }
        else
        {
            pistolcanvas.SetActive(false);
            rifflecanvas.SetActive(false);
        }

        if(pickpistol == true && pickriffle == true)
        {
            pistolcanvas.SetActive(false);
            rifflecanvas.SetActive(false);
        }
    }

    //void pickup()
    //{

    //}

    //private void checkweapon()
    //{
    //    RaycastHit hit;

    //    if (Physics.Raycast(Camera.main.transform.position, Camera.main, transform.forward, out hit, distance))
    //    {
    //        if(hit.transform.tag == "pistol")
    //        {
    //            Debug.Log("picked a gun");
    //            grab = true;
    //            weapon = hit.transform.gameObject;
    //        }
    //    }
    //    else
    //        grab = false;
    //}

    //private void pickup()
    //{
    //    currweapon = weapon;
    //    currweapon.transform.position = equipPos.position;
    //    currweapon.transform.parent = equipPos;
    //    currweapon.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
    //    currweapon.Getcomponent<Rigidbody>().isKinematic = true;
    //}

    //private void drop()
    //{
    //    currweapon.transform.parent = null;
    //    currweapon.Getcomponent<Rigidbody>().isKinematic = false;
    //    currweapon = null;
    //}

}
