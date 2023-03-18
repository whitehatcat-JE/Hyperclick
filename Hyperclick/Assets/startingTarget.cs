using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class startingTarget : MonoBehaviour
{
    public UnityEvent gameStartTriggered;
    public TextMeshProUGUI tutorialText;

    public SpriteRenderer displayedSprite;
    public Sprite explosionA;
    public Sprite explosionB;
    public Sprite explosionC;
    public Sprite explosionD;
    public UnityEngine.Rendering.Universal.Light2D light;

    public GameObject bossIntroSprite;

    public float explodeSpeed = 0.05f;

    private bool dead = false;

    void Start()
    {
        tutorialText.enabled = true;
    }

    void OnMouseDown()
    {
        if (dead)
        {
            return;
        }
        dead = true;
        tutorialText.enabled = false;
        StartCoroutine(explode());
    }

    IEnumerator explode()
    {
        displayedSprite.sprite = explosionA;
        light.intensity = 1.0f;
        yield return new WaitForSeconds(explodeSpeed);
        displayedSprite.sprite = explosionB;
        light.intensity = 0.4f;
        yield return new WaitForSeconds(explodeSpeed);
        displayedSprite.sprite = explosionC;
        light.intensity = 0.2f;
        yield return new WaitForSeconds(explodeSpeed);
        displayedSprite.sprite = explosionD;
        light.intensity = 0.1f;
        yield return new WaitForSeconds(explodeSpeed);
        bossIntroSprite.SetActive(true);
        Destroy(gameObject);
    }
}
