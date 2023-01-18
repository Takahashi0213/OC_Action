using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClear : MonoBehaviour
{
    public GameObject StarCountObject;
    private StarCount StarCount;

    public GameObject ClearObject;
    private ClearScript GameClearScript;

    void Start()
    {
        StarCount = StarCountObject.GetComponent<StarCount>();
        GameClearScript = ClearObject.GetComponent<ClearScript>();
    }

    void Update()
    {
        //�����������Ă�X�^�[�̐����ő�X�^�[���Ɠ�����������N���A�ɂ���
        if (StarCount.GetNowStarCount() == StarCount.GetMaxStarCount())
        {
            GameClearScript.GameClear();
        }
    }
}
