using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBullet : MonoBehaviour
{
    public GameObject ExplosionEffect;

    private Vector3 Move;
    private float LifeTime = 99.0f;
    private GameOverScript gos;
    private ClearScript cs;

    public void SetMove(Vector3 move)
    {
        Move = move;
    }
    public void SetLifeTime(float lifetime)
    {
        LifeTime = lifetime;
    }

    public void SetScript(GameOverScript s_gos, ClearScript s_cs)
    {
        gos = s_gos;
        cs = s_cs;
    }

    void FixedUpdate()
    {
        LifeTime -= Time.deltaTime;
        if (LifeTime <= 0.0f)
        {
            BulletDeath();
        }

        //移動
        transform.Translate(Move * Time.deltaTime * 100.0f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground") || other.CompareTag("MoveGround"))
        {
            BulletDeath();
        }

        //これ以降はゲームオーバーorクリア時にはやらない
        if (gos.GetGameOverFlag() || cs.GetClearFlag())
        {
            return;
        }

        //接触したオブジェクトのタグが"Player"のとき
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerScript>().Damage();    //ダメージを与える
            BulletDeath();
        }
    }

    private void BulletDeath()
    {
        Instantiate(ExplosionEffect, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
