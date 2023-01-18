using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostEffect : MonoBehaviour
{
    public GameObject PlayerObject;
    private PlayerScript playerScript;

    private Vignette vignette;
    private PostProcessProfile postProcessProfile;

    private float speed = 0.0005f;

    // Start is called before the first frame update
    void Start()
    {
        PostProcessVolume postProcessVolume = this.GetComponent<PostProcessVolume>();
        postProcessProfile = postProcessVolume.profile;
        postProcessProfile.TryGetSettings(out vignette);

        playerScript = PlayerObject.GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        //HPが1の時演出する
        if (playerScript.GetPlayerHP() <= 1)
        {
            //演出
            vignette.active = true;

            vignette.intensity.value += speed;

            if (vignette.intensity.value > 0.4f)
            {
                vignette.intensity.value = 0.4f;
                speed *= -1.0f;
            }
            if (vignette.intensity.value < 0.3f)
            {
                vignette.intensity.value = 0.3f;
                speed *= -1.0f;
            }
        }
        else
        {
            vignette.active = false;
        }
    }
}
