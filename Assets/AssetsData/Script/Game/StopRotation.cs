using UnityEngine;

public class StopRotation : MonoBehaviour
{
    void Update()
    {
        this.transform.rotation = Quaternion.identity;
    }
}
