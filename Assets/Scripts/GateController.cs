using UnityEngine;

public class GateContoller : MonoBehaviour
{
    public int requiredCoins;

    public void Start()
    {
        PlayerController.OnCoinsChanged += OpenGate;
    }

    public void OnDestroy()
    {
        PlayerController.OnCoinsChanged -= OpenGate;
    }

    private void OpenGate(int currentCoins)
    {
        if (currentCoins < requiredCoins)
            return;

        gameObject.SetActive(false);
        PlayerController.OnCoinsChanged -= OpenGate;
    }
}
