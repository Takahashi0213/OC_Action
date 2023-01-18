using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonScript : MonoBehaviour
{
    public GameObject CannonBullet;
    [Header("発射間隔")]
    public float CannonLimit = 5.0f;
    private float Timer = 1.0f;

    [Header("弾の速度")]
    public float BulletMoveSpeed = 1.0f;
    [Header("弾の生存時間")]
    public float BulletLifeTime = 3.0f;

    private GameOverScript gos;
    private ClearScript cs;
    private AudioSource SE_as;

    void Start()
    {
        gos = GameObject.Find("GodObject").GetComponent<GameOverScript>();
        cs = GameObject.Find("GodObject").GetComponent<ClearScript>();
        SE_as = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (gos.GetGameOverFlag() || cs.GetClearFlag())
        {
            return;
        }

        Timer -= Time.deltaTime;
        if (Timer <= 0.0f)
        {
            //発射
            Vector3 startPos = transform.position;
            startPos.y += 1.2f * transform.localScale.y;
            GameObject cb = Instantiate(CannonBullet, startPos, Quaternion.identity);
            cb.transform.localScale = new Vector3(cb.transform.localScale.x * transform.localScale.x,
                cb.transform.localScale.y * transform.localScale.y,
                cb.transform.localScale.z * transform.localScale.z);

            CannonBullet cbs = cb.GetComponent<CannonBullet>();

            cbs.SetMove(-transform.right * 0.02f * BulletMoveSpeed);
            cbs.SetLifeTime(BulletLifeTime);
            cbs.SetScript(gos, cs);

            SE_as.Play();

            Timer = CannonLimit;
        }
    }
}
