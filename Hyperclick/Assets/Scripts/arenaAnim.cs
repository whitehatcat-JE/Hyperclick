// Controls Arena Start Sparkle Animation
// Libraries
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arenaAnim : MonoBehaviour
{
    // Sprites
    public Sprite[] frames = new Sprite[2];
    public SpriteRenderer displayedSprite;

    // Called when arena sprite loaded
    void Start() {
        StartCoroutine(twinkleAnim()); // Start animation
    }

    // Loop animation indefinitely
    private IEnumerator twinkleAnim() {
        while (true) {
            displayedSprite.sprite = frames[0]; // Changes sprite
            yield return new WaitForSeconds(1f); // Pauses for 1 second
            displayedSprite.sprite = frames[1];
            yield return new WaitForSeconds(1f);
        }
    }
}
