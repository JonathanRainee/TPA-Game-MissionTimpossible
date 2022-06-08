using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickammo : MonoBehaviour
{

    [SerializeField] public LayerMask pistolammolayer;
    [SerializeField] public LayerMask riffleammolayer;
    public float distance = 2.0f;
    [SerializeField] Camera weaponcam;
    [SerializeField] GameObject ammocanvas;
    public activeweapon activeweaponscript;
    public ammowidget widget;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        weaponscript weapon = activeweaponscript.getactiveweapon();
        RaycastHit hit;
        Ray ray = weaponcam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));

        //if (Physics.Raycast(ray, out hit, distance, pistolammolayer))
        //{
        //    ammocanvas.SetActive(true);
        //    if (Input.GetKeyDown(KeyCode.F))
        //    {
        //        if (hit.transform.tag == "ammopistol")
        //        {
        //            Destroy(hit.transform.gameObject);
        //            //weapon.ammocount += weapon.magzsize;
        //            if(activeweaponscript.pistolcurrammo == 0)
        //            {
        //                activeweaponscript.pistolcurrammo = activeweaponscript.pistolmagzsize;
        //            }
        //            activeweaponscript.pistolammocount += activeweaponscript.pistolmagzsize;
        //            //widget.refresh(weapon.currammo, weapon.ammocount);
        //            activeweaponscript.refreshpistol(activeweaponscript.pistolcurrammo, activeweaponscript.pistolammocount);
        //        }
        //    }

        //}
        //else if (Physics.Raycast(ray, out hit, distance, riffleammolayer))
        //{
        //    ammocanvas.SetActive(true);
        //    if (Input.GetKeyDown(KeyCode.F))
        //    {
        //        if (hit.transform.tag == "ammoriffle")
        //        {
        //            Destroy(hit.transform.gameObject);
        //            //weapon.ammocount += weapon.magzsize;
        //            if (activeweaponscript.rifflecurrammo == 0)
        //            {
        //                activeweaponscript.rifflecurrammo = activeweaponscript.rifflemagzsize;
        //            }
        //            activeweaponscript.riffleammocount += activeweaponscript.rifflemagzsize;
        //            //widget.refresh(weapon.currammo, weapon.ammocount);
        //            activeweaponscript.refreshriffle(activeweaponscript.rifflecurrammo, activeweaponscript.riffleammocount);
        //        }
        //    }
        //}
        //else
        //{
        //    ammocanvas.SetActive(false);
        //}
    }
}








//        if (Physics.Raycast(ray, out hit, distance, pistolammolayer))
//{
//    ammocanvas.SetActive(true);

//    if (Input.GetKeyDown(KeyCode.F))
//    {
//        if (hit.transform.tag == "ammopistol")
//        {
//            weapon.ammocount += weapon.magzsize;
//            activeweaponscript.pistolammocount += activeweaponscript.pistolmagzsize;

//        }
//    }
//}


//if (Physics.Raycast(ray, out hit, distance, riffleammolayer))
//{
//    ammocanvas.SetActive(true);
//    if (Input.GetKeyDown(KeyCode.F))
//    {
//        if (hit.transform.tag == "ammoriffle")
//        {
//            weapon.ammocount += weapon.magzsize;
//            activeweaponscript.riffleammocount += activeweaponscript.rifflemagzsize;

//        }
//    }
//}
//else
//{
//    ammocanvas.SetActive(false);
//}

