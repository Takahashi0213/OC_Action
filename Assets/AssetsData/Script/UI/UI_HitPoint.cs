using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HitPoint : MonoBehaviour
{
    public GameObject Player;
    private PlayerScript playerScript;
    private Animator animator;

    public int LifeBorder = -1; //この値以下になったらないことにする

    private Image image;

    public Sprite Life_On;
    public Sprite Life_Off;


    // Start is called before the first frame update
    void Start()
    {
        playerScript = Player.GetComponent<PlayerScript>();
        image = this.GetComponent<Image>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerScript.GetPlayerHP() < LifeBorder)
        {
            if (image.sprite == Life_On)
            {
                animator.Play("GetAnim", 0, 0);
            }
            image.sprite = Life_Off;
        }
        else
        {
            if (image.sprite == Life_Off)
            {
                animator.Play("GetAnim", 0, 0);
            }
            image.sprite = Life_On;
        }
    }
}
