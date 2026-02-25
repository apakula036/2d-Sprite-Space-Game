using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public int score = 0;
    public int HighScore = 0;
    public int NewHighScore = 0;
    public int currentScore = 0;
    public float scoreMultiplier = 10f;
    private float elapsedTime = 0f;
    public float thrustForce = 2f;
    public GameObject BoosterFlmae;
    Rigidbody2D spaceship;
    public UIDocument uiDocument;
    private Label scoreText;
    private Label highScoreText;
    private Button restartButton;
    public GameObject explosionEggfect; 


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //initialize the variables to update these items
        spaceship = GetComponent<Rigidbody2D>();
        scoreText = uiDocument.rootVisualElement.Q<Label>("ScoreLabel");
        highScoreText = uiDocument.rootVisualElement.Q<Label>("HighScoreLabel");
        restartButton = uiDocument.rootVisualElement.Q<Button>("RestartButton");
        restartButton.style.display = DisplayStyle.None;
        highScoreText.style.display = DisplayStyle.None;
        restartButton.clicked += ReloadScene;
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScore();
        MovePlayer();
        ActivateFlame();
        UpdateHighScore();
    }

void UpdateScore()
    {
        elapsedTime += Time.deltaTime;
        score = Mathf.FloorToInt(elapsedTime * scoreMultiplier);
        scoreText.text = "Score: " + score;
    }
void UpdateHighScore()
    {
        elapsedTime += Time.deltaTime;
        score = Mathf.FloorToInt(elapsedTime * scoreMultiplier);
        Debug.Log("score:" + score);
        Debug.Log("HighScore:" + NewHighScore);
        if (score > NewHighScore)
        {
            NewHighScore = score;
            highScoreText.text = "New Highscore: " + score;
        }
        else
        {
            highScoreText.text = "Current Highscore: " + NewHighScore;
        }
        //highScoreText.text = "Score: " + HighScore;
    }





void OnCollisionEnter2D(Collision2D collision) 
    {
        Destroy(gameObject);   
        Instantiate(explosionEggfect, transform.position, transform.rotation);
        restartButton.style.display = DisplayStyle.Flex;
        highScoreText.style.display = DisplayStyle.Flex;
    }
void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
void MovePlayer()
    {
        //Check if mouse1 is pressed 
        if (Mouse.current.leftButton.isPressed) {

            // Calculate mouse direction
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.value);
            Vector2 direction = (mousePos - transform.position).normalized;

            // Move player in direction of mouse
            transform.up = direction;
            spaceship.AddForce(direction * thrustForce);
        }
    }
void ActivateFlame()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame){
            BoosterFlmae.SetActive(true);
        }
        else if (Mouse.current.leftButton.wasReleasedThisFrame){
            BoosterFlmae.SetActive(false);
        }
    }
}
