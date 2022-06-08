using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class animationevent : UnityEvent<string>
{

}

public class weaponanimationevent : MonoBehaviour
{

    public animationevent weaponanimationevents = new animationevent();

    public void onanimationevent(string eventname)
    {
        weaponanimationevents.Invoke(eventname);
    }
}
