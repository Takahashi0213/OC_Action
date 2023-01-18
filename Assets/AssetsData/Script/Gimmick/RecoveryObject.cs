using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoveryObject : MonoBehaviour
{
    private bool GetFlag = false;

    void OnTriggerStay(Collider other)
    {
        if (GetFlag)
        {
            return;
        }

        //接触したオブジェクトのタグが"Player"のとき
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerScript>().PlayerRecovery();
            this.GetComponent<AudioSource>().Play();
            this.GetComponent<Animator>().SetBool("GetFlag", true);
            GetFlag = true;
        }
    }

    public void ItemDelete()
    {
        Destroy(this.gameObject);
    }
}
