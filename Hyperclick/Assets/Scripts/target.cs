// Controls targets
// Libraries
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class target : MonoBehaviour {
    // Audio Tracks
    public AudioSource popSFX;
    // Game Objects
    public Animator targetAnims;
    public UnityEngine.Rendering.Universal.Light2D ambientLight;
    public GameObject ring;
    // Sprites
    public SpriteRenderer displayedSprite;
    public Sprite[] explosionSprites = new Sprite[4];
    // Animation Variables
    public float explodeSpeed = 0.05f;
    
    private bool dead = false;
    // Called when object spawned in
    void Start() {
        targetAnims.Play("target");
    }
    // Called once per frame
    void Update() {
        // Destroy target if player dies
        if (gameManager.dead && !dead) {
            dead = true;
            Destroy(ring);
            targetAnims.enabled = false;
            StartCoroutine(explode());
        }
    }
    // Called when target clicked on
    void OnMouseDown() {
        if (dead) { return; }
        // Destroy target
        dead = true;
        Destroy(ring);
        popSFX.Play();
        targetAnims.enabled = false;
        StartCoroutine(explode());
        // Increase player health
        GameObject scoreManager = GameObject.FindGameObjectWithTag("ScoreManager");
        scoreManager.GetComponent<gameManager>().increaseHealth();
    }
    // Explosion animation
    IEnumerator explode() {
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
    // Destroy target after set period of time
    public void decayed() {
        Destroy(gameObject);
        // Decrease player health
        GameObject scoreManager = GameObject.FindGameObjectWithTag("ScoreManager");
        scoreManager.GetComponent<gameManager>().decreaseHealth();
    }
}
