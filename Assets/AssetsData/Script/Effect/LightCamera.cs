using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCamera : MonoBehaviour
{
    public GameObject MainCamera;

    void Update()
    {
        this.transform.position = MainCamera.transform.position;
        this.transform.rotation = MainCamera.transform.rotation;
    }
}
