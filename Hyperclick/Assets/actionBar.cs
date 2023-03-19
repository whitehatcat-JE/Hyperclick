using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Cinemachine;
using TMPro;
using UnityEngine.UI;

public class actionBar : MonoBehaviour
{
    enum ATTACK
    {
        spin = 0,
        player = 1,
        target = 2
    }

    public static bool dead = false;

    public GameObject self;
    public GameObject boss;
    public GameObject startingTarget;

    public CinemachineImpulseSource damageScreenShake;
    public CinemachineImpulseSource healScreenShake;

    public SpriteRenderer firstDigitSprite;
    public SpriteRenderer secondDigitSprite;
    public SpriteRenderer thirdDigitSprite;
    public Sprite numSprite0;
    public Sprite numSprite1;
    public Sprite numSprite2;
    public Sprite numSprite3;
    public Sprite numSprite4;
    public Sprite numSprite5;
    public Sprite numSprite6;
    public Sprite numSprite7;
    public Sprite numSprite8;
    public Sprite numSprite9;

    public TextMeshProUGUI progressText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI levelText;
    public GameObject continueButton;

    public SpriteRenderer logo;
    public SpriteRenderer gameOver;
    public GameObject startButton;
    public GameObject quitButton;
    public GameObject bossIntroSprite;

    public Image progressChart;
    public Image darkenScreen;

    private boss bossScript;

    private float trueMaxHealth = 9f;
    private int maxHealth = 9;
    private int STARTING_HEALTH = 2;
    private int health;

    private int score = 0;
    private int level = 0;

    private ATTACK lastAttack = ATTACK.target;

    public actionBar()
    {
        health = STARTING_HEALTH;
    }

    void Awake()
    {
        bossScript = boss.GetComponent<boss>();
        increaseLevel();
    }

    void Start()
    {
        dead = true;
        darkenScreen.enabled = true;
        logo.enabled = true;
        startButton.SetActive(true);
        quitButton.SetActive(true);
    }

    public void attackEnded()
    {
        if (dead) { return; }
        if (lastAttack == ATTACK.target)
        {
            lastAttack = ATTACK.spin;
            bossScript.StartCoroutine(bossScript.spinAttack((level < 10) ? (3+level) : Mathf.Clamp(13-level, 1, 4), Mathf.Clamp(0.2f/((float)level), 0.05f, 1f), 0.25f));
        } else if (lastAttack == ATTACK.spin)
        {
            lastAttack = ATTACK.player;
            bossScript.StartCoroutine(bossScript.targetedAttack(1 + level, 0.2f, (level < 5) ? false : true, 1+level, 0.5f / ((float)level)));
        } else
        {
            lastAttack = ATTACK.target;
            bossScript.StartCoroutine(bossScript.targetPhase(6 + level, 0.5f / (1f - ((float) level) / 100f), 1, (int) Mathf.Clamp((float)level / 5f, 1f, 5f)));
        }
    }

    public void restartGame()
    {
        dead = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void startGame()
    {
        dead = false;
        darkenScreen.enabled = false;
        logo.enabled = false;
        startButton.SetActive(false);
        quitButton.SetActive(false);
        startingTarget.SetActive(true);
    }

    public void startTriggered()
    {
        boss.SetActive(true);
        bossIntroSprite.SetActive(false);
        attackEnded();
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void increaseLevel()
    {
        if (dead) { return; }
        level++;
        STARTING_HEALTH++;
        trueMaxHealth *= 1.1f;
        maxHealth = (int) trueMaxHealth;
        health = STARTING_HEALTH;
        score += (level-1) * 100;
        updateHealth((float)health / (float)maxHealth);
        switch (level % 10)
        {
            case 0:
                firstDigitSprite.sprite = numSprite0;
                break;
            case 1:
                firstDigitSprite.sprite = numSprite1;
                break;
            case 2:
                firstDigitSprite.sprite = numSprite2;
                break;
            case 3:
                firstDigitSprite.sprite = numSprite3;
                break;
            case 4:
                firstDigitSprite.sprite = numSprite4;
                break;
            case 5:
                firstDigitSprite.sprite = numSprite5;
                break;
            case 6:
                firstDigitSprite.sprite = numSprite6;
                break;
            case 7:
                firstDigitSprite.sprite = numSprite7;
                break;
            case 8:
                firstDigitSprite.sprite = numSprite8;
                break;
            case 9:
                firstDigitSprite.sprite = numSprite9;
                break;
        }

        switch (((level - (level % 10)) % 100) / 10)
        {
            case 0:
                secondDigitSprite.sprite = numSprite0;
                break;
            case 1:
                secondDigitSprite.sprite = numSprite1;
                break;
            case 2:
                secondDigitSprite.sprite = numSprite2;
                break;
            case 3:
                secondDigitSprite.sprite = numSprite3;
                break;
            case 4:
                secondDigitSprite.sprite = numSprite4;
                break;
            case 5:
                secondDigitSprite.sprite = numSprite5;
                break;
            case 6:
                secondDigitSprite.sprite = numSprite6;
                break;
            case 7:
                secondDigitSprite.sprite = numSprite7;
                break;
            case 8:
                secondDigitSprite.sprite = numSprite8;
                break;
            case 9:
                secondDigitSprite.sprite = numSprite9;
                break;
        }

        switch (Mathf.Floor(((float) level) / 100f))
        {
            case 0:
                thirdDigitSprite.sprite = numSprite0;
                break;
            case 1:
                thirdDigitSprite.sprite = numSprite1;
                break;
            case 2:
                thirdDigitSprite.sprite = numSprite2;
                break;
            case 3:
                thirdDigitSprite.sprite = numSprite3;
                break;
            case 4:
                thirdDigitSprite.sprite = numSprite4;
                break;
            case 5:
                thirdDigitSprite.sprite = numSprite5;
                break;
            case 6:
                thirdDigitSprite.sprite = numSprite6;
                break;
            case 7:
                thirdDigitSprite.sprite = numSprite7;
                break;
            case 8:
                thirdDigitSprite.sprite = numSprite8;
                break;
            case 9:
                thirdDigitSprite.sprite = numSprite9;
                break;
        }
    }

    public void decreaseHealth()
    {
        if (dead) { return; }
        score -= 5 * level;
        if (score < 0) { score = 0; }
        health -= 1;
        damageScreenShake.GenerateImpulseWithForce(1f);
        updateHealth((float)health / (float)maxHealth);
        if (health <= 0)
        {
            darkenScreen.enabled = true;
            gameOver.enabled = true;
            scoreText.enabled = true;
            scoreText.text = "Score: " + score.ToString();
            levelText.enabled = true;
            levelText.text = "Level: " + level.ToString();
            continueButton.SetActive(true);
            dead = true;
        }
    }

    public void increaseHealth()
    {
        if (dead) { return; }
        score += 5 * level;
        health += 1;
        healScreenShake.GenerateImpulseWithForce(1f);
        if (health >= maxHealth)
        {
            increaseLevel();
        }
        updateHealth((float)health / (float)maxHealth);
    }

    void updateHealth(float percent)
    {
        if (dead) { return; }
        progressText.text = ((int)(percent * 100f)).ToString();
        progressChart.fillAmount = percent;
    }
}
