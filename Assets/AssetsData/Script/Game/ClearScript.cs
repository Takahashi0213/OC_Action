using UnityEngine;
public class ClearScript : MonoBehaviour
{
    private bool GameClearFlag = false;
    private bool GameOverFlag = false;

    public GameObject PlayerObject;
    public GameObject CameraObject;
    public GameObject TimeObject;

    public GameObject ClearImage;
    public GameObject GameOverImage;

    public GameObject StarCountObject;
    private StarCount StarCount;

    public AudioClip ClearSE;

    private bool PushFlag = false;

    private float ClearWait = 2.5f; //シーン遷移できるようになるまでの時間

    void Update()
    {
        if (PushFlag)
        {
            return;
        }

        if (GameClearFlag)
        {
            //押せるようになるまで待つ
            if (ClearWait > 0.0f)
            {
                ClearWait -= Time.deltaTime;
                return;
            }

            //クリア中にAボタンが押されたらシーン遷移
            if (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Space))
            {
                //スコアを移動
                TimeCount tc = TimeObject.GetComponent<TimeCount>();
                ClearScore.SetClearMinute(tc.GetMinute());
                ClearScore.SetClearSeconds(tc.GetSeconds());
                //遷移
                FadeManager.Instance.LoadScene("Result", 1.0f);
                //連続で実行させない
                PushFlag = true;
            }
        }
    }

    public void GameClear()
    {
        if (GameClearFlag || GameOverFlag)
        {
            return;
        }

        if (StarCount == null)
        {
            StarCount = StarCountObject.GetComponent<StarCount>();
        }
        if (StarCount.GetMaxStarCount() == 0)
        {
            //スターがそもそもないときは進まない
            return;
        }

        GameClearFlag = true;

        //カメラとプレイヤーのクリア演出
        PlayerObject.GetComponent<PlayerScript>().PlayerClear();
        CameraObject.GetComponent<GameCamera>().Sp_CameraMove();

        //タイマー停止
        TimeObject.GetComponent<TimeCount>().SetStopFlag(true);

        //音楽
        GetComponent<AudioSource>().Stop(); //BGMを止める
        AudioSource.PlayClipAtPoint(ClearSE, CameraObject.transform.position);

        //クリア画像表示
        ClearImage.SetActive(true);
    }

    public void GameOver()
    {
        if (GameClearFlag || GameOverFlag)
        {
            return;
        }
        GameOverFlag = true;
    }

    public bool GetClearFlag()
    {
        return GameClearFlag;
    }
}
