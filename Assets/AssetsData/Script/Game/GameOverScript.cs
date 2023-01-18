using UnityEngine;

public class GameOverScript : MonoBehaviour
{
    public GameObject GameOverImage;
    public GameObject CameraObject;
    public AudioClip GameOverSE;

    private bool GameOverFlag = false;
    private bool PushFlag = false;

    void Update()
    {
        if (PushFlag || GameOverFlag == false)
        {
            return;
        }

        if (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Space))
        {
            //遷移
            FadeManager.Instance.LoadScene("Title", 1.0f);
            //連続で実行させない
            PushFlag = true;
        }
    }

    public void SetGameOver()
    {
        GameOverImage.SetActive(true);
        GameOverFlag = true;
        GetComponent<AudioSource>().Stop(); //BGMを止める
        AudioSource.PlayClipAtPoint(GameOverSE, CameraObject.transform.position);
    }

    public bool GetGameOverFlag()
    {
        return GameOverFlag;
    }

}
