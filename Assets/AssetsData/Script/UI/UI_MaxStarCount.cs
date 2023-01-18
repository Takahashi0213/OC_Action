using UnityEngine;
using UnityEngine.UI;

public class UI_MaxStarCount : MonoBehaviour
{
    private StarCount sc;
    private Text StarText;

    void Start()
    {
        sc = GameObject.Find("StarCountObject").GetComponent<StarCount>();
        StarText = this.GetComponent<Text>();

        StarTextUpdate();
    }

    void Update()
    {
        StarTextUpdate();
    }

    private void StarTextUpdate()
    {
        StarText.text = "/" + sc.GetMaxStarCount();
    }
}
