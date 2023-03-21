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
    public static int highscore = 0;

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
    public GameObject loreButton;
    public GameObject bossIntroSprite;
    public GameObject leaderboardButton;

    public TextMeshProUGUI leaderboard1;
    public TextMeshProUGUI leaderboard2;
    public TextMeshProUGUI leaderboard3;
    public TextMeshProUGUI leaderboard4;
    public TextMeshProUGUI leaderboard5;
    public TextMeshProUGUI leaderboard6;
    public TextMeshProUGUI leaderboard7;
    public TextMeshProUGUI leaderboard8;
    public TextMeshProUGUI leaderboard9;
    public TextMeshProUGUI leaderboard10;
    public GameObject backButton;
    public SpriteRenderer leaderboard;

    public GameObject terminal;
    public TextMeshProUGUI paragraph1;
    public TextMeshProUGUI paragraph2;
    public TextMeshProUGUI paragraph3;
    public TextMeshProUGUI paragraph4;
    public TextMeshProUGUI paragraph5;
    public TextMeshProUGUI paragraph6;
    public TextMeshProUGUI paragraph7;
    public TextMeshProUGUI paragraph8;
    public TextMeshProUGUI paragraph9;

    public Image progressChart;
    public Image darkenScreen;

    private boss bossScript;

    private float trueMaxHealth = 9f;
    private int maxHealth = 9;
    private int STARTING_HEALTH = 2;
    private int health;

    private int score = 0;
    private int level = 0;

    private int schrodingerLvl = 0;
    private int personALvl = 9;
    private int personBLvl = 8;
    private int personCLvl = 7;
    private int personDLvl = 6;
    private int personELvl = 5;
    private int personFLvl = 4;
    private int personGLvl = 3;
    private int personHLvl = 2;
    private int personILvl = 1;

    private ATTACK lastAttack = ATTACK.target;

    private bool displayingLore = false;

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
        leaderboardButton.SetActive(true);
        if (highscore >= personILvl) { loreButton.SetActive(true); }
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

    public void displayLore()
    {
        if (displayingLore)
        {
            dead = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        } else
        {
            displayingLore = true;
            logo.enabled = false;
            loreButton.SetActive(false);
            startButton.SetActive(false);
            quitButton.SetActive(false);
            leaderboardButton.SetActive(false);
            terminal.SetActive(true);

            schrodingerLvl = highscore;
            if (schrodingerLvl >= personALvl) { paragraph9.enabled = true; }
            if (schrodingerLvl >= personBLvl) { paragraph8.enabled = true; }
            if (schrodingerLvl >= personCLvl) { paragraph7.enabled = true; }
            if (schrodingerLvl >= personDLvl)
            {
                paragraph6.enabled = true;
                terminal.GetComponent<Image>().color = Color.red;
            }
            if (schrodingerLvl >= personELvl) { paragraph5.enabled = true; }
            if (schrodingerLvl >= personFLvl) { paragraph4.enabled = true; }
            if (schrodingerLvl >= personGLvl) { paragraph3.enabled = true; }
            if (schrodingerLvl >= personHLvl) { paragraph2.enabled = true; }
            if (schrodingerLvl >= personILvl) { paragraph1.enabled = true; }
        }
    }

    public void displayLeaderboard()
    {
        schrodingerLvl = highscore;
        // Forgive me
        logo.enabled = false;
        startButton.SetActive(false);
        quitButton.SetActive(false);
        leaderboardButton.SetActive(false);
        loreButton.SetActive(false);
        leaderboard1.enabled = true;
        leaderboard2.enabled = true;
        leaderboard3.enabled = true;
        leaderboard4.enabled = true;
        leaderboard5.enabled = true;
        leaderboard6.enabled = true;
        leaderboard7.enabled = true;
        leaderboard8.enabled = true;
        leaderboard9.enabled = true;
        leaderboard10.enabled = true;
        leaderboard.enabled = true;
        backButton.SetActive(true);

        List<string> leaderboardNames = new List<string>();
        leaderboardNames.Add("Isaac - Lvl." + personALvl.ToString());
        leaderboardNames.Add("Merlin - Lvl." + personBLvl.ToString());
        leaderboardNames.Add("Uriel - Lvl." + personCLvl.ToString());
        leaderboardNames.Add("Leonardo - Lvl." + personDLvl.ToString());
        leaderboardNames.Add("Alexander - Lvl." + personELvl.ToString());
        leaderboardNames.Add("Tesla - Lvl." + personFLvl.ToString());
        leaderboardNames.Add("Icarus - Lvl." + personGLvl.ToString());
        leaderboardNames.Add("Oedipus - Lvl." + personHLvl.ToString());
        leaderboardNames.Add("Nero - Lvl." + personILvl.ToString());

        string schrodingerText = "Schrodinger - Lvl." + schrodingerLvl.ToString();

        if (schrodingerLvl >= personALvl){leaderboardNames.Insert(0, schrodingerText);
            leaderboard1.color = new Color32(115, 239, 232, 255);
        }
        else if (schrodingerLvl >= personBLvl) { leaderboardNames.Insert(1, schrodingerText);
            leaderboard2.color = new Color32(115, 239, 232, 255);
        }
        else if (schrodingerLvl >= personCLvl) { leaderboardNames.Insert(2, schrodingerText);
            leaderboard3.color = new Color32(115, 239, 232, 255);
        }
        else if (schrodingerLvl >= personDLvl) { leaderboardNames.Insert(3, schrodingerText);
            leaderboard4.color = new Color32(115, 239, 232, 255);
        }
        else if (schrodingerLvl >= personELvl) { leaderboardNames.Insert(4, schrodingerText);
            leaderboard5.color = new Color32(115, 239, 232, 255);
        }
        else if (schrodingerLvl >= personFLvl) { leaderboardNames.Insert(5, schrodingerText);
            leaderboard6.color = new Color32(115, 239, 232, 255);
        }
        else if (schrodingerLvl >= personGLvl) { leaderboardNames.Insert(6, schrodingerText);
            leaderboard7.color = new Color32(115, 239, 232, 255);
        }
        else if (schrodingerLvl >= personHLvl) { leaderboardNames.Insert(7, schrodingerText);
            leaderboard8.color = new Color32(115, 239, 232, 255);
        }
        else if (schrodingerLvl >= personILvl) { leaderboardNames.Insert(8, schrodingerText);
            leaderboard9.color = new Color32(115, 239, 232, 255);
        }
        else { leaderboardNames.Add(schrodingerText);
            leaderboard10.color = new Color32(115, 239, 232, 255);
        }

        leaderboard1.text = "1. " + leaderboardNames[0];
        leaderboard2.text = "2. " + leaderboardNames[1];
        leaderboard3.text = "3. " + leaderboardNames[2];
        leaderboard4.text = "4. " + leaderboardNames[3];
        leaderboard5.text = "5. " + leaderboardNames[4];
        leaderboard6.text = "6. " + leaderboardNames[5];
        leaderboard7.text = "7. " + leaderboardNames[6];
        leaderboard8.text = "8. " + leaderboardNames[7];
        leaderboard9.text = "9. " + leaderboardNames[8];
        leaderboard10.text = "10. " + leaderboardNames[9];
    }

    public void startGame()
    {
        dead = false;
        darkenScreen.enabled = false;
        logo.enabled = false;
        startButton.SetActive(false);
        quitButton.SetActive(false);
        startingTarget.SetActive(true);
        leaderboardButton.SetActive(false);
        loreButton.SetActive(false);
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
            if (highscore < level) { highscore = level; }
            darkenScreen.enabled = true;
            gameOver.enabled = true;
            //scoreText.enabled = true;
            //scoreText.text = "Score: " + score.ToString();
            levelText.enabled = true;
            levelText.text = "Lvl. " + level.ToString();
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
