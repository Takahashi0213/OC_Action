using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSwitch : MonoBehaviour
{
    public GameObject DoorSetObject;

    private bool PushFlag = false;

    void OnCollisionEnter(Collision collision)
    {
        if (PushFlag)
        {
            return;
        }

        //接触したオブジェクトのタグが"Player"のとき
        if (collision.gameObject.CompareTag("Player"))
        {
            this.GetComponent<Animator>().SetBool("PushFlag", true);
            this.GetComponent<AudioSource>().Play();
            DoorSetObject.GetComponent<DoorSetScript>().DoorOpen();
            PushFlag = true;
        }
    }
}
