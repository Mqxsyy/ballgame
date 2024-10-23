using UnityEngine;
using TMPro;

public class PersonalBest : MonoBehaviour
{
    private static float time = -1;
    private static TMP_Text pbText;

    void Start()
    {
        pbText = GetComponent<TMP_Text>();

        if (time != -1)
        {
            pbText.text = "PB: " + time;
        }
    }

    public static void UpdateTime(float newTime)
    {
        if (time == -1 || newTime < time)
        {
            time = newTime;
            pbText.text = "PB: " + time;
        }
    }
}