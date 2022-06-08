using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class weapon : MonoBehaviour
{

    
    [SerializeField] GameObject weaponparent;
    weaponscript playerweapon;
    //public float aimduration = 0.3f;

    //public Rig aimlayer;
    //ammosystem ammo;


    // Start is called before the first frame update
    void Start()
    {
        playerweapon = weaponparent.GetComponentInChildren<weaponscript>();
        //ammo = GetComponent<ammosystem>();
    }

    // Update is called once per frame
    //private void Update()
    //{

    //    if (Input.GetKeyDown("1"))
    //    {
    //        Debug.Log("pressed");
    //        aimlayer.weight += Time.deltaTime / aimduration;
    //    }
    //    else
    //    {
    //        aimlayer.weight -= Time.deltaTime / aimduration;
    //    }
    //    //Debug.Log(ammo.currammo);
    //}

    private void LateUpdate()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    playerweapon.startfire();
        //    //ammo.currammo--;
        //}
        //else if (Input.GetMouseButtonUp(0))
        //{
        //    playerweapon.stopfire();
        //}

        //if (playerweapon.firing)
        //{
        //    playerweapon.UpdateFire(Time.deltaTime);
        //}
        ////if (ammo.currammo <= 0)
        ////{
        ////    playerweapon.stopfire();
        ////}

        //playerweapon.updatebullet(Time.deltaTime);
    }
}
