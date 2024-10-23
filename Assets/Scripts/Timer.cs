using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private TMP_Text timer;
    private static float time = 0;

    public static bool hasMoved = false;
    public static bool hasWon = false;

    public void Start()
    {
        time = 0;
        timer = GetComponent<TMP_Text>();
        
        hasMoved = false;
        hasWon = false;
    }

    public void Update()
    {
        if (!hasMoved || hasWon) {
            return;
        }

        time += Time.deltaTime;
        timer.text = "Time: " + GetTime();
    }

    public static float GetTime()
    {
        return Mathf.Round(time * 100) / 100;
    }
}