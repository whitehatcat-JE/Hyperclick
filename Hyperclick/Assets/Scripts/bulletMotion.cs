using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletMotion : MonoBehaviour
{
    public GameObject self;
    public Rigidbody2D rdby;
    public UnityEngine.Rendering.Universal.Light2D light;

    public Sprite spriteB;
    public Sprite spriteC;

    public Sprite explodeA;
    public Sprite explodeB;
    public Sprite explodeC;
    public Sprite explodeD;

    public Collider2D collision;

    public SpriteRenderer displayedSprite;

    public float moveSpeed = 1f;

    public int MAX_COLLISIONS = 3;

    float MAX_TIME = 25f;

    float expireTime = 0f;
    public float explodeSpeed = 0.05f;

    bool dead = false;
    public bool applyForce = true;

    int collisionCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        self.tag = "Bullet";
        if (applyForce) { rdby.AddForce(self.transform.up * moveSpeed); }
    }

    void Update()
    {
        if (gameManager.dead && !dead)
        {
            StartCoroutine(explode());
            return;
        }
        expireTime += Time.deltaTime;
        if (expireTime > MAX_TIME && !dead)
        {
            StartCoroutine(explode());
        }
    }

    public IEnumerator explode()
    {
        dead = true;
        collision.enabled = false;
        Destroy(rdby);
        displayedSprite.sprite = explodeA;
        light.intensity = 1.0f;
        yield return new WaitForSeconds(explodeSpeed);
        displayedSprite.sprite = explodeB;
        light.intensity = 0.4f;
        yield return new WaitForSeconds(explodeSpeed);
        displayedSprite.sprite = explodeC;
        light.intensity = 0.2f;
        yield return new WaitForSeconds(explodeSpeed);
        displayedSprite.sprite = explodeD;
        light.intensity = 0.1f;
        yield return new WaitForSeconds(explodeSpeed);
        Destroy(self);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameManager.dead) { return; }
        if (collision.gameObject.tag == "Wall")
        {
            collisionCount++;
            switch (collisionCount)
            {
                case 1:
                    displayedSprite.sprite = spriteB;
                    break;
                case 2:
                    displayedSprite.sprite = spriteC;
                    break;
            }
            if (collisionCount >= MAX_COLLISIONS)
            {
                StartCoroutine(explode());
            }
        }
    }
}