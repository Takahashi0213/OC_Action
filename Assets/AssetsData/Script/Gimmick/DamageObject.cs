using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageObject : MonoBehaviour
{
    private PlayerScript playerScript;

    void OnTriggerStay(Collider other)
    {
        //接触したオブジェクトのタグが"Player"のとき
        if (other.CompareTag("Player"))
        {
            if (playerScript == null)
            {
                playerScript = other.GetComponent<PlayerScript>();
            }
            playerScript.Damage();    //ダメージを与える
        }
    }
}
