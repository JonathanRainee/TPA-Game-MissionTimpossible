using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bosstrigger : MonoBehaviour
{

    public GameObject healthbossparent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider trigger)
    {
        if (trigger.gameObject.tag == "Player")
        {
            healthbossparent.SetActive(true);
        }
    }
}
