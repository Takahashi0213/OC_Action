using UnityEngine;
using UnityEngine.UI;

public class ResultTime : MonoBehaviour
{
    private Text TimeText;
    private string TimeString;

    // Start is called before the first frame update
    void Start()
    {
        TimeText = this.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        TimeString = ClearScore.GetClearMinute().ToString("D2");
        TimeString += ":";
        TimeString += ClearScore.GetClearSeconds().ToString("D2");

        TimeText.text = TimeString;
    }
}
