using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBGM : MonoBehaviour
{
    public enum NowGameBGM{
        BGM1,
        BGM2,
        BGM3
        }
    [Header("ÉQÅ[ÉÄBGM")]
    public NowGameBGM Game_BGM = NowGameBGM.BGM1;

    [Header("BGMê›íË")]
    public AudioClip BGM1;
    public AudioClip BGM2;
    public AudioClip BGM3;

    void Start()
    {
        switch (Game_BGM)
        {
            case NowGameBGM.BGM1:
                GetComponent<AudioSource>().clip = BGM1;
                break;
            case NowGameBGM.BGM2:
                GetComponent<AudioSource>().clip = BGM2;
                break;
            case NowGameBGM.BGM3:
                GetComponent<AudioSource>().clip = BGM3;
                break;
        }

        GetComponent<AudioSource>().Play();
    }

    void Update()
    {
        
    }
}
