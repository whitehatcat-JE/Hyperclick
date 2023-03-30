// Displays randomly selected boss taunt when game starts
// Libraries
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class introText : MonoBehaviour {
    // Game Objects
    public TextMeshProUGUI tauntText;
    // Animation Variables
    public float speed = 0.2f;
    private float alphaVisiblity = 0f;

    private bool increasing = true;
    private bool animActive = false;
    // Activates taunt animation
    public void taunt() {
        string[] taunts = new string[] {
            "Shall we begin?",
            "Let us begin.",
            "Your death shall be swift.",
            "I will make your death painless.",
            "It seems we are destined to fight.",
            "Prove yourself worthy!",
            "How far will you get this time?"
        };

        tauntText.text = taunts[Random.Range(0, taunts.Length)];
        animActive = true;
    }
    // Called once per frame
    void Update() {
        if (animActive) { // Play taunt animation
            if (increasing) { // Fade in taunt text
                alphaVisiblity += Time.deltaTime * speed;
                if (alphaVisiblity >= 1f) { // Switch to fading out taunt text once fade in complete
                    increasing = false;
                    alphaVisiblity = 1f;
                }
            } else { // Fade out taunt text
                alphaVisiblity -= Time.deltaTime * speed;
                if (alphaVisiblity <= 0f) { // Stop animation once fade out complete
                    increasing = false;
                    alphaVisiblity = 0f;
                    animActive = false;
                }
            }
            // Update taunt text transparency
            Color newColor = tauntText.color;
            newColor.a = alphaVisiblity;
            tauntText.color = newColor;
        }
    }
}
