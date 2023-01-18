using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_NowTime : MonoBehaviour
{
    private Text TimeText;
    public GameObject TimeObject;
    private TimeCount timecount;

    private string TimeString;

    // Start is called before the first frame update
    void Start()
    {
        timecount = TimeObject.GetComponent<TimeCount>();
        TimeText = this.GetComponent<Text>();
    }

    void Update()
    {
        TimeString = timecount.GetMinute().ToString("D2");
        TimeString += ":";
        TimeString += timecount.GetSeconds().ToString("D2");

        TimeText.text = TimeString;

    }

}

