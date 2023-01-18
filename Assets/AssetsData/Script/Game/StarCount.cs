using UnityEngine;

public class StarCount : MonoBehaviour
{
    private int NowStarCount = 0;
    private int MaxStarCount = 0;

    public GameObject ClearObject;
    private ClearScript cs;

    void Start()
    {
        GameObject[] stars = GameObject.FindGameObjectsWithTag("Star");
        MaxStarCount = stars.Length;    //要素数はオブジェクトの数

        cs = ClearObject.GetComponent<ClearScript>();
    }

    void Update()
    {
    }

    //所持している星を増やす
    public void StarAdd()
    {
        NowStarCount++;
    }

    //所持している星を取得
    public int GetNowStarCount()
    {
        return NowStarCount;
    }

    //最大星数を取得
    public int GetMaxStarCount()
    {
        return MaxStarCount;
    }

}
