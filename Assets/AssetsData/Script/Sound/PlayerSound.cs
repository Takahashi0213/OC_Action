using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    public AudioClip JumpSE;
    public AudioClip DamageSE;

    public enum PlaySE
    {
        Jump,
        Damage,
    }

    public void SE_Play_Jump(PlaySE playse , Vector3 position)
    {
        switch (playse)
        {
            case PlaySE.Jump:
                AudioSource.PlayClipAtPoint(JumpSE, position);
                break;
            case PlaySE.Damage:
                AudioSource.PlayClipAtPoint(DamageSE, position);
                break;
        }
    }
}
