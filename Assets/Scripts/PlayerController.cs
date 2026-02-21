using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private float score = 0f;
    public float scoreMultiplier = 10f;
    private float elapsedTime = 0f;
    public float thrustForce = 2f;
    public GameObject BoosterFlmae;
    Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        score = Mathf.FloorToInt(elapsedTime * scoreMultiplier);
        Debug.Log("Score: " + score);
        if (Mouse.current.leftButton.isPressed) {
            // Calculate mouse direction
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.value);
            
            Vector2 direction = (mousePos - transform.position).normalized;

            // Move player in direction of mouse
            transform.up = direction;
            rb.AddForce(direction * thrustForce);
        }

        if (Mouse.current.leftButton.wasPressedThisFrame){
            BoosterFlmae.SetActive(true);
            Debug.Log("FLAME ON ");
        }
        else if (Mouse.current.leftButton.wasReleasedThisFrame){
            BoosterFlmae.SetActive(false);
        }
    }
    void OnCollisionEnter2D(Collision2D collision) 
    {
    Destroy(gameObject);       
    }
}
