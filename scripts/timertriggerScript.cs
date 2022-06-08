using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class timertrigger : MonoBehaviour
{
    public quest qscript;
    [SerializeField] public GameObject startcanvas;
    [SerializeField] public GameObject telecanvas;
    [SerializeField] public bsptele bsptelescript;
    [SerializeField] private GameObject timerUI;
    [SerializeField] private TextMeshProUGUI timerText;
    float currTime = 0f;
    float startTime = 60f;
    private bool flagTimer;
    public healthmanagement hm;

    private float cd = 0f;
    private float cd2 = 0f;
    private float offset = 1f;

    // Start is called before the first frame update
    void Start()
    {
        currTime = startTime;
        flagTimer = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (flagTimer)
        {
            startTimer();
        }
        
        if(bsptelescript.pressed == true)
        {
            stopTimer();
        }

        if(qscript.guardkill == 10)
        {
            showcanvas();
            stopTimer();
        }
    }

    void showcanvas()
    {
        telecanvas.SetActive(true);
        cd2 += Time.deltaTime;

        if (cd2 >= offset)
        {
            telecanvas.SetActive(false);
        }

    }

    void OnTriggerEnter(Collider trigger)
    {
  /*      Debug.Log("pler!");*/
        if (trigger.gameObject.tag == "Player")
        {
            /* Debug.Log("crot2 aw aw masukk!!!");*/
            flagTimer = true;


        }
    }

    private void startTimer()
    {
        timerUI.SetActive(true);
        startcanvas.SetActive(true);

        cd += Time.deltaTime;

        if(cd >= offset)
        {
            startcanvas.SetActive(false);
        }

        currTime -= 1 * Time.deltaTime;

        timerText.text = currTime.ToString();
        if(currTime <= 0)
        {
            //mati
            hm.takedmg(hm.currhealth);
            Debug.Log("mateee!");
            timerUI.SetActive(false);
            currTime = 0;
        }
    }

    public void stopTimer()
    {
        timerUI.SetActive(false);
        currTime = 10;
    }
}
