using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFloor : MonoBehaviour
{
    //スタートと終わりの目印
    private Vector3 startPosition = Vector3.zero;
    [Header("移動先の座標")]
    public Vector3 TargetPosition = Vector3.zero;

    // スピード
    [Header("移動速度")]
    public float MoveSpeed = 1.0F;
    private float nowTime = 0.0f;

    //二点間の距離を入れる
    private float distance_two;

    //移動状態
    private bool moveFlag = false;

    //ウェイト
    private float waitTimer = 0.0f;
    [Header("移動後の停止時間")]
    public float WaitTime = 2.0f;
    private bool waitFlag = false;

    void Start()
    {
        startPosition = this.transform.position;
    }

    void Update()
    {
        if (moveFlag == false)
        {
            if (waitFlag == false)
            {
                // 現在の位置
                nowTime += Time.deltaTime * MoveSpeed;
                if (nowTime > 1.0f)
                {
                    nowTime = 1.0f;
                }

                // オブジェクトの移動
                transform.position = Vector3.Lerp(startPosition, TargetPosition, nowTime);

                //もし目的地にいたら状態遷移
                if (this.transform.position == TargetPosition)
                {
                    waitTimer = WaitTime;
                    waitFlag = true;
                }
            }
            else
            {
                waitTimer -= Time.deltaTime;
                if (waitTimer <= 0.0f)
                {
                    waitFlag = false;
                    moveFlag = true;
                    nowTime = 0.0f;
                }
            }
        }
        else
        {
            if (waitFlag == false)
            {
                // 現在の位置
                nowTime += Time.deltaTime * MoveSpeed;
                if (nowTime > 1.0f)
                {
                    nowTime = 1.0f;
                }
                // オブジェクトの移動
                transform.position = Vector3.Lerp(TargetPosition, startPosition, nowTime);

                //もし目的地にいたら状態遷移
                if (this.transform.position == startPosition)
                {
                    waitTimer = WaitTime;
                    waitFlag = true;
                }
            }
            else
            {
                waitTimer -= Time.deltaTime;
                if (waitTimer <= 0.0f)
                {
                    waitFlag = false;
                    moveFlag = false;
                    nowTime = 0.0f;
                }
            }
        }

    }
}
