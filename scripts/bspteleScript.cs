using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bsptele : MonoBehaviour
{
    public healthmanagement hm;
    public bool teleported = false;
    public quest qscript;
    public timertrigger trig;
    public bool pressed = false;
    public bsp bspscript;
    [SerializeField] public LayerMask player;
    public float sphereRadius;
    [SerializeField] public GameObject pressftele;

    // Start is called before the first frame update
    void Start()
    {
        sphereRadius = 10.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(qscript.guardkill == 1)
        {
            trig.stopTimer();
        }

        bool checknear = Physics.CheckSphere(transform.position, sphereRadius, player);
        if (checknear)
        {
            Debug.Log("teleeeeeeeeeeeeee");
            pressftele.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F) && qscript.guardkill == 1)
            {
                teleported = true;
                pressed = true;
                bspscript.MovePlayerToStartMaze();
            }
        }
        else
        {
            pressftele.SetActive(false);
        }
    }
}
