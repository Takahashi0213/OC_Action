using UnityEngine;

public class ResultEnd : MonoBehaviour
{
    private bool PushFlag = false;

    private float Wait = 1.5f;

    public AudioSource GameBGM;
    private float BGM_Volume = 1.0f;

    public AudioSource StartSE;

    // Update is called once per frame
    void Update()
    {
        if (Wait > 0.0f)
        {
            Wait -= Time.deltaTime;
            return;
        }

        if (PushFlag)
        {
            //BGM音量補正
            BGM_Volume -= 0.005f;
            GameBGM.volume = BGM_Volume;
            return;
        }

        if (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Space))
        {
            //遷移
            FadeManager.Instance.LoadScene("Title", 1.0f);
            StartSE.Play();
            //連続で実行させない
            PushFlag = true;
        }
    }
}
