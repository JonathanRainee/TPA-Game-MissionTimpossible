using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reloadweapon : MonoBehaviour
{

    public Animator rigidcontroller;
    public weaponanimationevent animationevent;
    public activeweapon activeweaponsccript;
    public Transform lefthand;
    public ammowidget widget;
    GameObject magzhand;
     

    // Start is called before the first frame update
    void Start()
    {
        animationevent.weaponanimationevents.AddListener(onanimationevent);
    }

    // Update is called once per frame
    void Update()
    {
        weaponscript weapon = activeweaponsccript.getactiveweapon();

        if (weapon)
        {
            //if (Input.GetKeyDown(KeyCode.R) || weapon.currammo <= 0)
            //{
            //    Debug.Log("reloadinf weapon");
            //    rigidcontroller.SetTrigger("reload_weapon");
            //    if (weapon.ammocount >= weapon.magzsize)
            //    {

            //        int ammotoreload = weapon.magzsize - weapon.currammo;
            //        weapon.ammocount -= ammotoreload;
            //        weapon.currammo += ammotoreload;
            //        Debug.Log(weapon.ammocount);
            //    }
            //    else
            //    {
            //        if (weapon.currammo + weapon.ammocount > weapon.magzsize)
            //        {
            //            int ammoleft = weapon.ammocount + weapon.currammo - weapon.magzsize;
            //            weapon.ammocount = weapon.magzsize;
            //            weapon.currammo = weapon.magzsize;


            //        }
            //        else
            //        {
            //            weapon.currammo += weapon.ammocount;
            //            weapon.ammocount = 0;

            //        }
            //    }
                //weapon.ammocount -= (weapon.magzsize - weapon.currammo);


                rigidcontroller.ResetTrigger("reload_weapon");
                //widget.refresh(weapon.currammo, weapon.ammocount);
            //}
            if (weapon.firing)
            {
                //widget.refresh(weapon.currammo, weapon.ammocount);
            }
        }

        
    }

    void onanimationevent(string eventname)
    {
        Debug.Log(eventname);
        switch (eventname)
        {
            case "detachmag":
                //detachmagz();
                break;
            case "dropmagz":
                dropmagz();
                break;
            case "refillmagz":
                refillmagz();
                break;
            case "attachmagz":
                attachmagz();
                break;

        }
    }

    void detachmagz()
    {
        weaponscript weapon = activeweaponsccript.getactiveweapon();
        magzhand = Instantiate(weapon.magazine, lefthand, true);
        weapon.magazine.SetActive(false);
    }

    void dropmagz()
    {

    }

    void refillmagz()
    {

    }

    void attachmagz()
    {
        //weaponscript weapon = activeweaponsccript.getactiveweapon();
        //weapon.ammocount = weapon.magzsize;
    }
}
