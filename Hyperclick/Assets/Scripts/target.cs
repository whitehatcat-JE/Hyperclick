using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class target : MonoBehaviour
{
    public Animator targetAnims;

    public SpriteRenderer displayedSprite;
    public Sprite explosionA;
    public Sprite explosionB;
    public Sprite explosionC;
    public Sprite explosionD;
    public UnityEngine.Rendering.Universal.Light2D light;

    public GameObject ring;

    public float explodeSpeed = 0.05f;
    
    private bool dead = false;

    void Start()
    {
        targetAnims.Play("target");
    }

    void Update()
    {
        if (gameManager.dead && !dead)
        {
            OnMouseDown();
        }
    }

    void OnMouseDown()
    {
        if (dead)
        {
            return;
        }
        dead = true;
        Destroy(ring);
        targetAnims.enabled = false;
        GameObject scoreManager = GameObject.FindGameObjectWithTag("ScoreManager");
        scoreManager.GetComponent<gameManager>().increaseHealth();
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
        Destroy(gameObject);
    }

    public void decayed()
    {
        GameObject scoreManager = GameObject.FindGameObjectWithTag("ScoreManager");
        scoreManager.GetComponent<gameManager>().decreaseHealth();
        Destroy(gameObject);
    }
}
