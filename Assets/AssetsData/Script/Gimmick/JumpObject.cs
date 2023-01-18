using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpObject : MonoBehaviour
{
    private PlayerScript playerScript;
    [Header("ジャンプ力")]
    public float JumpPower = 6.0f;

    void OnTriggerEnter(Collider other)
    {
        //接触したオブジェクトのタグが"Player"のとき
        if (other.CompareTag("Player"))
        {
            if (playerScript == null)
            {
                playerScript = other.GetComponent<PlayerScript>();
            }
            playerScript.Jump(JumpPower);
            playerScript.PlayJumpAnim();    //ジャンプアニメーションを最初からにする
            this.GetComponent<AudioSource>().Play();    //効果音
        }
    }
}
