using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCount : MonoBehaviour
{
    public GameObject PlayerObject;
    private PlayerScript playerScript;

    [SerializeField]
    private int Minute = 0;
    [SerializeField]
    private float Seconds = 0.0f;

    private bool StopFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = PlayerObject.GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerScript.GetDeathFlag() || StopFlag)
        {
            return;
        }

        //プレイタイム加算
        Seconds += Time.deltaTime;
        if (Seconds >= 60.0f)
        {
            Minute++;
            Minute = Mathf.Min(Minute, 99);    //最大値を99に調整
            Seconds = Seconds - 60;
        }
    }

    public void SetStopFlag(bool flag)
    {
        StopFlag = flag;
    }

    //取得系
    public int GetMinute()
    {
        return Minute;
    }
    public int GetSeconds()
    {
        return (int)Seconds;
    }
}
