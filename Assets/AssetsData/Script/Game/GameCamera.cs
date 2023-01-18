using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    [Header("カメラの回転速度")]
    public float RotSpeed = 1.0f;
    [Header("カメラの回転モード")]
    public bool CameraMode = false;

    [Header("カメラ設定")]
    public GameObject Player;
    public float CameraRange = 2.0f;
    public float CameraY_Up = 1.5f;

    private PlayerScript ps;
    private PauseScript pauseScript;

    private float NowX_Rot = 0.0f;

    private bool Sp_CameraMoveFlag = false;
    private bool StopFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        ps = Player.GetComponent<PlayerScript>();
        pauseScript = GameObject.Find("GodObject").GetComponent<PauseScript>();

        NowX_Rot = transform.localEulerAngles.x;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        if (pauseScript.IsPause())
        {
            return;
        }

        if (Sp_CameraMoveFlag)
        {
            Sp_Camera();
        }

        if (ps.GetDeathFlag() || StopFlag)
        {
            return;
        }

        //右スティックでカメラ回転

        //上下
        float rot = 200.0f * Time.deltaTime * RotSpeed;
        if (Input.GetAxisRaw("Vertical2") != 0.0f)
        {
            rot *= -Input.GetAxisRaw("Vertical2");

            if (CameraMode)
            {
                rot *= -1.0f;
            }

            //角度制限
            NowX_Rot += rot;
            if (NowX_Rot > 40.0f)
            {
                NowX_Rot = 40.0f;
                rot = 0.0f;
            }
            if (NowX_Rot < -20.0f)
            {
                NowX_Rot = -20.0f;
                rot = 0.0f;
            }
        }
        else
        {
            rot = 0.0f;
        }
        transform.RotateAround(Player.transform.position, this.transform.right, rot);

        //左右
        rot = 200.0f * Time.deltaTime * RotSpeed;

        if (CameraMode)
        {
            rot *= -1.0f;
        }

        if (Input.GetAxisRaw("Horizontal2") != 0.0f)
        {
            rot *= -Input.GetAxisRaw("Horizontal2");
        }
        else
        {
            rot = 0.0f;
        }
        transform.RotateAround(Player.transform.position, Vector3.up, rot);

        //座標
        Vector3 cameraMove = transform.forward * -CameraRange;
        cameraMove.y += CameraY_Up;

        transform.position = Player.transform.position + cameraMove;
    }

    public void SetStopFlag(bool flag)
    {
        StopFlag = flag;
    }

    public void Sp_CameraMove()
    {
        //カメラ停止
        SetStopFlag(true);

        Sp_Camera();

        //特殊カメラオン
        Sp_CameraMoveFlag = true;
    }

    private void Sp_Camera()
    {
        //カメラ移動
        Vector3 move = Player.transform.forward * 1.5f;
        move.y += 1.0f;
        transform.position = Player.transform.position + move;

        //プレイヤーを見る
        move = Vector3.zero;
        move.y += 1.0f;
        transform.LookAt(Player.transform.position + move);
    }
}
