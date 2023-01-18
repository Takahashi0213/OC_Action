using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    public GameObject PauseCanvas;
    public GameObject CameraObject;

    private bool PauseMode = false;

    private GameOverScript gos;
    private ClearScript cs;

    public AudioClip PauseOpenSE;
    public AudioClip PauseCloseSE;

    // Start is called before the first frame update
    void Start()
    {
        PauseCanvas.SetActive(false);

        gos = gameObject.GetComponent<GameOverScript>();
        cs = gameObject.GetComponent<ClearScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gos.GetGameOverFlag() || cs.GetClearFlag())
        {
            return;
        }

        if (Input.GetKeyDown("joystick button 7") || Input.GetKeyDown(KeyCode.P))
        {
            PauseMode = !PauseMode;

            if (PauseMode)
            {
                PauseCanvas.SetActive(true);
                AudioSource.PlayClipAtPoint(PauseOpenSE, CameraObject.transform.position);
                Time.timeScale = 0.0f;
            }
            else
            {
                Time.timeScale = 1.0f;
                PauseCanvas.SetActive(false);
                AudioSource.PlayClipAtPoint(PauseCloseSE, CameraObject.transform.position);

            }
        }
    }

    public bool IsPause()
    {
        return PauseMode;
    }

}
