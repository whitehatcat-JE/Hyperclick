// Controls tutorial target
// Libraries
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class startingTarget : MonoBehaviour {
    // Game Objects
    public TextMeshProUGUI tutorialText;
    public UnityEngine.Rendering.Universal.Light2D ambientLight;
    // Sprites
    public SpriteRenderer displayedSprite;
    public Sprite[] explosionSprites = new Sprite[4];

    public GameObject bossIntroSprite;
    // Animation Variables
    public float explodeSpeed = 0.05f;
    private bool dead = false;
    // Called when object activated
    void Start() {
        tutorialText.enabled = true;
    }
    // Trigger target explosion animation on click
    void OnMouseDown() {
        if (dead) { return; }
        dead = true;
        tutorialText.enabled = false;
        StartCoroutine(explode());
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
        bossIntroSprite.SetActive(true);
        Destroy(gameObject);
    }
}
