using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_NowStarCount : MonoBehaviour
{
    private StarCount sc;
    private Text StarText;
    private Animator animator;

    private int OldStarCount = 0;

    void Start()
    {
        sc = GameObject.Find("StarCountObject").GetComponent<StarCount>();
        StarText = GetComponent<Text>();
        animator = GetComponent<Animator>();

        StarTextUpdate();
    }

    void Update()
    {
        StarTextUpdate();
    }

    private void StarTextUpdate()
    {
        StarText.text = "" + sc.GetNowStarCount();

        if(OldStarCount!= sc.GetNowStarCount())
        {
            animator.Play("GetAnim", 0, 0);
            OldStarCount = sc.GetNowStarCount();
        }
    }
}
