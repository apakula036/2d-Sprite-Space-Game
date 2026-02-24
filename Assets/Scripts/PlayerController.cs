using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private float score = 0f;
    public float scoreMultiplier = 10f;
    private float elapsedTime = 0f;
    public float thrustForce = 2f;
    public GameObject BoosterFlmae;
    Rigidbody2D spaceship;
    public UIDocument uiDocument;
    private Label scoreText;
    private Button restartButton;
    public GameObject explosionEggfect; 


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //initialize the variables to update these items
        spaceship = GetComponent<Rigidbody2D>();
        scoreText = uiDocument.rootVisualElement.Q<Label>("ScoreLabel");
        restartButton = uiDocument.rootVisualElement.Q<Button>("RestartButton");
        restartButton.style.display = DisplayStyle.None;
        restartButton.clicked += ReloadScene;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScore();
        MovePlayer();
        ActivateFlame();
    }
    void UpdateScore()
    {
        // Add the score number to the UI
        elapsedTime += Time.deltaTime;
        score = Mathf.FloorToInt(elapsedTime * scoreMultiplier);
        scoreText.text = "Score: " + score;
    }
    void OnCollisionEnter2D(Collision2D collision) 
    {
        Destroy(gameObject);   
        Instantiate(explosionEggfect, transform.position, transform.rotation);
        restartButton.style.display = DisplayStyle.Flex;
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
