using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Cinemachine;
using TMPro;
using UnityEngine.UI;

public class gameManager : MonoBehaviour {
    public static bool dead = false;
    public static int highscore = 0;

    public GameObject boss;
    public GameObject startingTarget;

    public CinemachineImpulseSource damageScreenShake;
    public CinemachineImpulseSource healScreenShake;

    public SpriteRenderer[] numberSpriteRenderers = new SpriteRenderer[3];
    public Sprite[] numberSprites = new Sprite[10];

    public TextMeshProUGUI progressText;
    public TextMeshProUGUI levelText;
    public AudioSource gameOverTune;
    public AudioSource glitchTrack;
    public GameObject continueButton;

    public GameObject startMenu;
    public SpriteRenderer gameOver;
    public GameObject loreButton;
    public GameObject bossIntroSprite;

    public TextMeshProUGUI[] leaderboards = new TextMeshProUGUI[10];
    public GameObject leaderboardMenu;

    public GameObject terminal;
    public TextMeshProUGUI[] paragraphs = new TextMeshProUGUI[9];

    public Image progressChart;
    public Image darkenScreen;

    private bossActions bossScript;

    private float trueMaxHealth = 9f;
    private int maxHealth = 9;
    private int health = 2;

    private int level = 0;

    private int schrodingerLvl = 0;
    private int[] challengeLvls = new int[] { 3, 5, 6, 7, 8, 9, 10, 11, 12 };
    private string[] leaderboardNames = new string[] { "Isaac", "Merlin", "Uriel", "Leonardo", "Alexander", "Tesla", "Icarus", "Oedipus", "Nero"};

    private int attackCount = 0;

    private bool displayingLore = false;

    void Awake() {
        bossScript = boss.GetComponent<bossActions>();
        DontDestroyOnLoad(gameOverTune.gameObject);
        increaseLevel();
    }

    void Start() {
        dead = true;
        darkenScreen.enabled = true;
        startMenu.SetActive(true);
        if (highscore >= challengeLvls[0]) { loreButton.SetActive(true); }
    }

    public void attackEnded() {
        if (dead) { return; }
        if (level <= 4) {
            if (attackCount % 3 == 0) {
                bossScript.StartCoroutine(bossScript.spinAttack(level, 0.1f, level == 1 ? 0.2f : 0.1f));
            } else if (attackCount % 3 == 1) {
                bossScript.StartCoroutine(bossScript.targetedAttack(1 + level, 0.1f, level < 3 ? false : true, level, level < 3 ? 1f : 1f - (0.25f * ((float) level - 2f))));
            } else {
                bossScript.StartCoroutine(bossScript.targetPhase(6 + level, 0.55f - (0.025f * (float) level)));
            }
        } else if (level <= 7) {
            if (attackCount % 2 == 0) {
                bossScript.StartCoroutine(bossScript.spinAttack(3 * (level - 4), 0.15f - 0.025f * ((float) level - 4), 1f, true));
            } else {
                bossScript.StartCoroutine(bossScript.targetPhase(6 + level, 0.55f - (0.02f * (float)level)));
            }
        } else if (level <= 9) {
            if (attackCount % 3 == 0) {
                bossScript.StartCoroutine(bossScript.spinAttack(level - 3, 0.1f, 0.1f));
            } else if (attackCount % 3 == 1) {
                bossScript.StartCoroutine(bossScript.targetedAttack(level - 7, 0.1f, level <= 8 ? false : true, level - 6, 1f - 0.2f * (float) level, true));
            } else {
                bossScript.StartCoroutine(bossScript.targetPhase(6 + level, 0.55f - (0.02f * (float)level)));
            }
        } else if (level == 10) {
            if (attackCount % 3 == 0) {
                bossScript.StartCoroutine(bossScript.spinAttack(level - 3, 0.1f, 0.1f));
            } else if (attackCount % 3 == 1) {
                bossScript.StartCoroutine(bossScript.targetedAttack(level - 3, 0.1f, true, 3, 0.5f, false, true));
            } else {
                bossScript.StartCoroutine(bossScript.targetPhase(6 + level, 0.55f - (0.02f * (float)level)));
            }
        } else if (level == 11) {
            if (attackCount % 3 == 0) {
                bossScript.StartCoroutine(bossScript.spinAttack(level - 3, 0.2f, 0.2f, false, true));
            } else if (attackCount % 3 == 1) {
                bossScript.StartCoroutine(bossScript.targetedAttack(3, 0.1f, true, 3, 0.5f));
            } else {
                bossScript.StartCoroutine(bossScript.targetPhase(6 + level, 0.55f - (0.02f * (float)level)));
            }
        } else {
            if (attackCount % 3 == 0) {
                bossScript.StartCoroutine(bossScript.spinAttack(level - 3, 0.2f, 0.15f, false, true));
            } else if (attackCount % 3 == 1) {
                bossScript.StartCoroutine(bossScript.targetedAttack(level - 9, 0.1f, level % 2 == 0 ? false : true, level - 9, 0.3f, false, true));
            } else {
                bossScript.StartCoroutine(bossScript.targetPhase(6 + level, level == 12 ? 0.3f : 0.4f, 1, level == 12 ? 1 : 3));
            }
        }
        attackCount++;
    }

    public void restartGame() {
        dead = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void displayLore() {
        if (displayingLore) {
            dead = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        } else {
            glitchTrack.Play();
            displayingLore = true;
            startMenu.SetActive(false);
            terminal.SetActive(true);

            schrodingerLvl = highscore;
            for (int paragraphIdx = 0; paragraphIdx < paragraphs.Length; paragraphIdx++) {
                if (schrodingerLvl >= challengeLvls[paragraphIdx]) {
                    paragraphs[paragraphIdx].enabled = true;
                    if (paragraphIdx == 5) { terminal.GetComponent<Image>().color = Color.red; }
                }
            }
        }
    }

    public void displayLeaderboard() {
        schrodingerLvl = highscore;
        startMenu.SetActive(false);
        leaderboardMenu.SetActive(true);

        List<string> orderedNames = new List<string>();
        for (int nameIdx = 0; nameIdx < leaderboardNames.Length; nameIdx++) {
            orderedNames.Add(leaderboardNames[nameIdx] + " - Lvl." + challengeLvls[8-nameIdx]);
        }

        string schrodingerText = "Schrodinger - Lvl." + schrodingerLvl.ToString();

        if (schrodingerLvl > challengeLvls[0]) {
            for (int challenger = 0; challenger < challengeLvls.Length; challenger++) {
                if (schrodingerLvl >= challengeLvls[challenger]) {
                    orderedNames.Insert(challenger, schrodingerText);
                    leaderboards[challenger].color = new Color32(115, 239, 232, 255);
                    break;
                }
            }
        } else {
            orderedNames.Add(schrodingerText);
            leaderboards[9].color = new Color32(115, 239, 232, 255);
        }

        for (int board = 0; board < leaderboards.Length; board++) {
            leaderboards[board].text = (board + 1).ToString() + ". " + orderedNames[board];
        }
    }

    public void startGame() {
        dead = false;
        darkenScreen.enabled = false;
        startMenu.SetActive(false);
        startingTarget.SetActive(true);
    }

    public void startTriggered() {
        boss.SetActive(true);
        bossIntroSprite.SetActive(false);
        attackEnded();
    }

    public void quitGame() {
        Application.Quit();
    }

    public void increaseLevel() {
        if (dead) { return; }
        level++;
        trueMaxHealth *= 1.1f;
        maxHealth = (int) trueMaxHealth;
        health = maxHealth / 3;
        attackCount = 0;
        updateHealth((float)health / (float)maxHealth);
        numberSpriteRenderers[0].sprite = numberSprites[level % 10];
        numberSpriteRenderers[1].sprite = numberSprites[((level - (level % 10)) % 100) / 10];
        numberSpriteRenderers[2].sprite = numberSprites[(int) Mathf.Floor(((float)level) / 100f)];
    }

    public void decreaseHealth() {
        if (dead) { return; }
        health -= 1;
        damageScreenShake.GenerateImpulseWithForce(1f);
        updateHealth((float)health / (float)maxHealth);
        if (health <= 0) {
            if (highscore < level) { highscore = level; }
            darkenScreen.enabled = true;
            gameOver.enabled = true;
            levelText.enabled = true;
            levelText.text = "Lvl. " + level.ToString();
            continueButton.SetActive(true);
            dead = true;
            gameOverTune.Play();
        }
    }

    public void increaseHealth() {
        if (dead) { return; }
        health += 1;
        healScreenShake.GenerateImpulseWithForce(1f);
        if (health >= maxHealth) { increaseLevel(); }
        updateHealth((float)health / (float)maxHealth);
    }

    void updateHealth(float percent) {
        if (dead) { return; }
        progressText.text = ((int)(percent * 100f)).ToString();
        progressChart.fillAmount = percent;
    }
}