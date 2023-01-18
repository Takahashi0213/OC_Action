using UnityEngine;

public class StarObject : MonoBehaviour
{
    private bool GetFlag = false;

    private Light starlight;
    private StarCount sc;

    void Start()
    {
        starlight = transform.GetChild(0).gameObject.GetComponent<Light>();
        sc = GameObject.Find("StarCountObject").GetComponent<StarCount>();
    }

    void Update()
    {
        if (GetFlag)
        {
            starlight.intensity -= 0.02f;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (GetFlag)
        {
            return;
        }

        //接触したオブジェクトのタグが"Player"のとき
        if (other.CompareTag("Player"))
        {
            sc.StarAdd();
            this.GetComponent<Animator>().SetBool("GetFlag", true);
            if (sc.GetMaxStarCount() != sc.GetNowStarCount())
            {
                //最後の星じゃないなら効果音再生
                this.GetComponent<AudioSource>().Play();
            }
            GetFlag = true;
        }
    }

    public void StarDelete()
    {
        Destroy(this.gameObject);
    }
}
