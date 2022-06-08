using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ammowidget : MonoBehaviour
{
    public TMPro.TMP_Text ammotext;
    //[SerializeField] TextMeshProUGUI currtext;

    public void refresh(int currammo, int sumammo)
    {
        string curramonow = currammo.ToString();
        string sumammonow = sumammo.ToString();
        ammotext.text = curramonow + " / " + sumammonow;
    }
}
