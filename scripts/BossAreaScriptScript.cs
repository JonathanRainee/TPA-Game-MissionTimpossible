using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAreaScript : MonoBehaviour
{

    public AudioSource battlesound;
    public GameObject questbox;
    public GameObject dialogbox;
    GameObject player;
    float StartBossRange;
    bool bossStarted;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("char");
        StartBossRange = 5;
        bossStarted = false;
    }

    // Update is called once per frame
    void Update()
    {
        StartBoss();
    }

    void StartBoss()
    {
        float dist = Vector3.Distance(player.transform.position, transform.position);
        if(!bossStarted && dist < StartBossRange)
        {
            //GameStateScript.StartBoss();
            battlesound.Play();
            questbox.SetActive(false);
            dialogbox.SetActive(false);
            BossScript.BossStart = true;
            bossStarted = true;
        }
    }
}
