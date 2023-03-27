using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class playerMovement : MonoBehaviour
{
    public UnityEvent damage;

    public SpriteRenderer displayedSprite;
    public Sprite playerFrameA;
    public Sprite playerFrameB;
    public Sprite playerFrameC;
    public Sprite playerFrameD;
    public Sprite playerFrameE;
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
        if (gameManager.dead) { return; }
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
            switch (currentFrame)
            {
                case 0:
                    displayedSprite.sprite = playerFrameA;
                    break;
                case 1:
                    displayedSprite.sprite = playerFrameB;
                    break;
                case 2:
                    displayedSprite.sprite = playerFrameC;
                    break;
                case 3:
                    displayedSprite.sprite = playerFrameD;
                    break;
                case 4:
                    displayedSprite.sprite = playerFrameE;
                    break;
            }

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
