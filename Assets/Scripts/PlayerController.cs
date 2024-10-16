using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public delegate void OnCoinsChangedEvent(int newAmount);
    public static event OnCoinsChangedEvent OnCoinsChanged; 

    public float movementSpeed;
    public TMP_Text winText;
    public GameObject wall;

    private int score;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        score = 0;
        SetScoreText();
        winText.text = "";
    }

    void Update()
    {
        float deltaX = Input.GetAxis("Horizontal");
        float deltaY = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(deltaX, 0, deltaY).normalized;
        rb.AddForce(direction * movementSpeed);

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Main");
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Coin"))
        {
            collider.gameObject.SetActive(false);
            score++;
            SetScoreText();

            if (score >= 5)
            {
                wall.SetActive(false);
            }
        }
        else if (collider.gameObject.CompareTag("Danger"))
        {
            SceneManager.LoadScene("Main");
        }
    }

    private void SetScoreText()
    {
        OnCoinsChanged(score);

        if (score == 3)
        {
            winText.text = "You finished in " + Timer.GetTime() + " !\n Press \'R\' to restart or \'ESC\' to quit.";
        }
    }
}
