using UnityEngine;

public class TitleScript : MonoBehaviour
{
    public float StartLimit = 1.0f;

    private bool PushFlag = false;

    public AudioSource GameBGM;
    private float BGM_Volume = 1.0f;

    public AudioSource StartSE;

    void Update()
    {
        if (StartLimit > 0.0f)
        {
            StartLimit -= Time.deltaTime;
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
            FadeManager.Instance.LoadScene("MainScene", 1.0f);
            StartSE.Play();
            PushFlag = true;
        }
    }
}
