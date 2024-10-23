using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public delegate void OnCoinsChangedEvent(int newAmount);
    public static event OnCoinsChangedEvent OnCoinsChanged;

    private static bool hasFinishedTutorial = false;

    public float movementSpeed;
    public TMP_Text winText;
    public TMP_Text tutorialText;
    public ParticleSystem particles;

    private int score;
    private Rigidbody rb;

    void Start()
    {
        if (hasFinishedTutorial)
        {
            if (tutorialText.IsActive())
            {
                tutorialText.gameObject.SetActive(false);
            }
        }

        OnCoinsChanged += (newAmount) => { };

        rb = GetComponent<Rigidbody>();
        score = 0;
        SetScoreText();
        winText.text = "";
    }

    void Update()
    {
        float deltaX = Input.GetAxis("Horizontal");
        float deltaY = Input.GetAxis("Vertical");

        if (deltaX != 0 || deltaY != 0)
        {
            Timer.hasMoved = true;

            if (!hasFinishedTutorial)
            {
                tutorialText.gameObject.SetActive(false);
                hasFinishedTutorial = true;
            }
        }

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

            particles.gameObject.transform.position = collider.gameObject.transform.position;
            particles.Play();
        }
        else if (collider.gameObject.CompareTag("Danger"))
        {
            SceneManager.LoadScene("Main");
        }
    }

    private void SetScoreText()
    {
        OnCoinsChanged(score);

        if (score == 4)
        {
            PersonalBest.UpdateTime(Timer.GetTime());
            winText.text = "You finished in " + Timer.GetTime() + " !\n Press \'R\' to restart or \'ESC\' to quit.";
            Timer.hasWon = true;
        }
    }
}
