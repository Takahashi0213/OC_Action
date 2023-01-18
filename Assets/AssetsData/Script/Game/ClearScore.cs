using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearScore : MonoBehaviour
{
    public static int ClearMinute = 0;
    public static int ClearSeconds = 0;

    public static int GetClearMinute()
    {
        return ClearMinute;
    }

    public static int GetClearSeconds()
    {
        return ClearSeconds;
    }

    public static void SetClearMinute(int minute)
    {
        ClearMinute = minute;
    }
    
    public static void SetClearSeconds(int seconds)
    {
        ClearSeconds = seconds;
    }


}
