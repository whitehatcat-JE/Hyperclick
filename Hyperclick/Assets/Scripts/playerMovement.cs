// Controls player movement
// Libraries
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class playerMovement : MonoBehaviour {
    // Events
    public UnityEvent damage;
    // Sprite Objects
    public SpriteRenderer displayedSprite;
    public Sprite[] frames = new Sprite[6];
    // Physics Objects
    public Rigidbody2D rb;
    // Movement Variables
    public float moveSpeed = 5f;
    private Vector2 movement;
    // Animation Variables
    private int currentFrame = 0;
    public float frameTime = 0.2f;
    public float frameTimeSlow = 0.2f;
    private float timeSinceLastFrame = 0f;
    // Called once per frame
    void Update() {
        if (gameManager.dead) { // Stop player from moving after death
            displayedSprite.sprite = frames[5];
            return;
        }
        // Calculate movement direction
        float xMov = Input.GetAxisRaw("Horizontal");
        float yMov = Input.GetAxisRaw("Vertical");

        Vector2 movHorizontal = transform.right * xMov;
        Vector2 movVertical = transform.up * yMov;
        movement = (movHorizontal + movVertical).normalized;
        // Update sprite animations
        timeSinceLastFrame += Time.deltaTime;
        if ((timeSinceLastFrame > frameTime && (xMov != 0f || yMov != 0f || currentFrame > 1)) || timeSinceLastFrame > frameTimeSlow) {
            timeSinceLastFrame = 0f;
            currentFrame += 1;
            if (xMov != 0f || yMov != 0f) { // Walking animation
                currentFrame %= 5;
            } else { // Idle animation
                currentFrame %= 2;
            }
            displayedSprite.sprite = frames[currentFrame];
        }
    }
    // Called once per physics frame
    void FixedUpdate() {
        if (gameManager.dead) { return; }
        // Moves player in direction calculated in Update() function
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
    // Detect bullet collisions
    void OnTriggerEnter2D(Collider2D other) {
        if (gameManager.dead) { return; }
        if (other.CompareTag("Bullet")) {
            // Hurts player
            damage.Invoke();
            // Destroys bullet
            bulletMotion bulletScript = other.gameObject.transform.parent.GetComponent<bulletMotion>();
            bulletScript.StartCoroutine(bulletScript.explode());
        }
    }
}
