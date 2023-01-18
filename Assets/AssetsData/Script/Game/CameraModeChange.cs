using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraModeChange : MonoBehaviour
{
    public GameCamera gameCamera;
    public Text ModeText;

    private void Start()
    {
        if (gameCamera.CameraMode)
        {
            ModeText.text = "カメラモード：通常";
        }
        else
        {
            ModeText.text = "カメラモード：リバース";
        }
    }

    void Update()
    {
        if((Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Space)))
        {
            gameCamera.CameraMode = !gameCamera.CameraMode;

            if (gameCamera.CameraMode)
            {
                ModeText.text = "カメラモード：通常";
            }
            else
            {
                ModeText.text = "カメラモード：リバース";
            }
        }
    }
}
