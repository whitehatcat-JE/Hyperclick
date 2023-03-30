// Controls bullet movement and collisions
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletMotion : MonoBehaviour {
    // Game Objects
    public Rigidbody2D rdby;
    public UnityEngine.Rendering.Universal.Light2D ambientLight;
    public Collider2D collision;
    // Sprites
    public SpriteRenderer displayedSprite;

    public Sprite[] spritePhases = new Sprite[2];
    public Sprite[] explosionSprites = new Sprite[4];
    // Movement Variables
    public float moveSpeed = 1f;
    public float explodeSpeed = 0.05f;

    public bool isHoming = false;

    float MAX_TIME = 25f;
    bool dead = false;

    int collisionCount = 0;
    // Animation Variables
    float expireTime = 0f;
    // Called when object created
    void Start() {
        gameObject.tag = "Bullet";
        if (!isHoming) { rdby.AddForce(gameObject.transform.up * moveSpeed); }
    }
    // Called once per frame
    void Update() {
        expireTime += Time.deltaTime;
        // Destroy bullet if player dies or bullet existance time reaches threshold
        if ((gameManager.dead || expireTime > MAX_TIME) && !dead) { StartCoroutine(explode()); }
    }
    // Explosion animation
    public IEnumerator explode() {
        dead = true;
        collision.enabled = false;
        Destroy(rdby);
        displayedSprite.sprite = explosionSprites[0];
        ambientLight.intensity = 1.0f;
        yield return new WaitForSeconds(explodeSpeed);
        displayedSprite.sprite = explosionSprites[1];
        ambientLight.intensity = 0.4f;
        yield return new WaitForSeconds(explodeSpeed);
        displayedSprite.sprite = explosionSprites[2];
        ambientLight.intensity = 0.2f;
        yield return new WaitForSeconds(explodeSpeed);
        displayedSprite.sprite = explosionSprites[3];
        ambientLight.intensity = 0.1f;
        yield return new WaitForSeconds(explodeSpeed);
        Destroy(gameObject);
    }
    // Called when bullet rigidbody collides with wall
    void OnCollisionEnter2D(Collision2D collision) {
        if (gameManager.dead) { return; }
        if (collision.gameObject.tag == "Wall") {
            collisionCount++;
            if (collisionCount <= spritePhases.Length && !isHoming) { // Changes bullet sprite to indicate how many times bullet has collided with wall
                displayedSprite.sprite = spritePhases[collisionCount-1];
            } else {
                StartCoroutine(explode());
            }
        }
    }
}
