using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSetScript : MonoBehaviour
{
    public GameObject DoorObject;
    public GameObject OpenDoorObject;
    public GameObject SwitchObject;

    private bool OpenFlag = false;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void DoorOpen()
    {
        if (OpenFlag)
        {
            return;
        }

        DoorObject.GetComponent<BoxCollider>().enabled = false;
        OpenDoorObject.GetComponent<Animator>().SetBool("OpenFlag", true);

        OpenFlag = true;
    }
}
