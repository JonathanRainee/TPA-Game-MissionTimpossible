using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponpickup : MonoBehaviour
{

    public weaponscript weaponprefab;
     
    private void OnTriggerEnter(Collider other)
    {
        activeweapon currweapon = other.gameObject.GetComponent<activeweapon>();
        if (currweapon)
        {   
        
            weaponscript newweapon = Instantiate(weaponprefab);
            currweapon.equip(newweapon);
        }
    }
}
