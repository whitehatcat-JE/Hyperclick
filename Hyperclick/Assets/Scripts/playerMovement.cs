using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class playerMovement : MonoBehaviour
{
    public UnityEvent damage;

    public SpriteRenderer displayedSprite;
    public Sprite[] frames = new Sprite[6];
    public float moveSpeed = 5f;

    public Rigidbody2D rb;

    private int currentFrame = 0;
    public float frameTime = 0.2f;
    public float frameTimeSlow = 0.2f;
    private float timeSinceLastFrame = 0f;

    Vector2 movement;

    // Update is called once per frame
    void Update()
    {
        if (gameManager.dead) {
            displayedSprite.sprite = frames[5];
            return;
        }
        float xMov = Input.GetAxisRaw("Horizontal");
        float yMov = Input.GetAxisRaw("Vertical");

        Vector2 movHorizontal = transform.right * xMov;
        Vector2 movVertical = transform.up * yMov;

        movement = (movHorizontal + movVertical).normalized;
        timeSinceLastFrame += Time.deltaTime;
        if ((timeSinceLastFrame > frameTime && (xMov != 0f || yMov != 0f || currentFrame > 1)) || timeSinceLastFrame > frameTimeSlow)
        {
            timeSinceLastFrame = 0f;
            currentFrame += 1;
            if (xMov != 0f || yMov != 0f)
            {
                currentFrame %= 5;
            } else
            {
                currentFrame %= 2;
            }
            displayedSprite.sprite = frames[currentFrame];
        }
    }

    void FixedUpdate()
    {
        if (gameManager.dead) { return; }
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (gameManager.dead) { return; }
        if (other.CompareTag("Bullet"))
        {
            damage.Invoke();
            bulletMotion bulletScript = other.gameObject.transform.parent.GetComponent<bulletMotion>();
            bulletScript.StartCoroutine(bulletScript.explode());
        }
    }
}
