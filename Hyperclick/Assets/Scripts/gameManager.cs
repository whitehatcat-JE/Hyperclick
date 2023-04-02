// This is the core script that controls the game's logic and events
// Libraries
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Cinemachine;
using TMPro;
using UnityEngine.UI;

public class gameManager : MonoBehaviour {
    // Global Variables (Other scripts access these)
    public static bool dead = false;
    public static bool justLeveled = false; // Used to cancel attacks in bossActions script
    public static int highscore = 0;

    // Boss & Combat Objects
    public GameObject boss;
    public GameObject bossIntroSprite;
    private bossActions bossScript;
 
    public GameObject startingTarget;

    public CinemachineImpulseSource damageScreenShake;
    public CinemachineImpulseSource healScreenShake;

    // Level Counter Sprites
    public SpriteRenderer[] numberSpriteRenderers = new SpriteRenderer[3];
    public Sprite[] numberSprites = new Sprite[10];

    // Menu Objects
    public Image darkenScreen;
    //      Start
    public GameObject startMenu;
    public GameObject loreButton;
    //      Game Over
    public TextMeshProUGUI levelText;
    public GameObject continueButton;
    public SpriteRenderer gameOver;
    //      Leaderboard
    public TextMeshProUGUI[] leaderboards = new TextMeshProUGUI[10];
    public GameObject leaderboardMenu;
    //      Lore
    public GameObject terminal;
    public TextMeshProUGUI[] paragraphs = new TextMeshProUGUI[9];

    // HUD Objects
    public Image progressChart;
    public TextMeshProUGUI progressText;

    // Audio
    public audioManager audioScript;

    // Health Variables
    private float trueMaxHealth = 9f;
    private int maxHealth = 9;
    private int health = 2;
    // Level Variables
    private int level = 0;
    //      Leaderboard
    private int schrodingerLvl = 0;
    private int[] challengeLvls = new int[] { 3, 5, 6, 7, 8, 9, 10, 11, 12 };
    private string[] leaderboardNames = new string[] { "Isaac", "Merlin", "Uriel", "Leonardo", "Alexander", "Tesla", "Icarus", "Oedipus", "Nero"};
    // Attack Action Count (For determining next attack phase)
    private int attackCount = 0;
    // Menu Variables
    private bool displayingLore = false;

    // Run on scene load
    void Awake() {
        bossScript = boss.GetComponent<bossActions>();
        increaseLevel(); // Set level to 1
        // Display start menu
        dead = true;
        darkenScreen.enabled = true;
        startMenu.SetActive(true);
        audioScript.play(audioManager.TRACK.menu);
        if (highscore >= challengeLvls[0]) { loreButton.SetActive(true); }
    }
    // Run next boss attack action 
    public void attackEnded() {
        if (dead) { return; }
        justLeveled = false;
        // Determine what action loop to use based on current level
        if (level <= 4) { // Levels 1-4. Only uses Yellow bullets (Straight)
            if (attackCount % 3 == 0) {
                bossScript.StartCoroutine(bossScript.spinAttack(level, 0.1f, level == 1 ? 0.2f : 0.1f));
            } else if (attackCount % 3 == 1) {
                bossScript.StartCoroutine(bossScript.targetedAttack(1 + level, 0.1f, level < 3 ? false : true, level, level < 3 ? 1f : 1f - (0.25f * ((float) level - 2f))));
            } else {
                bossScript.StartCoroutine(bossScript.targetPhase(6 + level, 0.55f - (0.025f * (float) level)));
            }
        } else if (level <= 7) { // Levels 5-7. Only uses Pink bullets (Homing)
            if (attackCount % 2 == 0) {
                bossScript.StartCoroutine(bossScript.spinAttack(3 * (level - 4), 0.15f - 0.025f * ((float) level - 4), 1f, true));
            } else {
                bossScript.StartCoroutine(bossScript.targetPhase(6 + level, 0.55f - (0.02f * (float)level)));
            }
        } else if (level <= 9) { // Levels 8-9. Intermixes Pink and Yellow bullets during targeted attack
            if (attackCount % 3 == 0) {
                bossScript.StartCoroutine(bossScript.spinAttack(level - 3, 0.1f, 0.1f));
            } else if (attackCount % 3 == 1) {
                bossScript.StartCoroutine(bossScript.targetedAttack(level - 7, 0.1f, false, level - 6, 1f - 0.2f * (float) level, true, true));
            } else {
                bossScript.StartCoroutine(bossScript.targetPhase(6 + level, 0.55f - (0.02f * (float)level)));
            }
        } else if (level == 10) { // Level 10. Fires Pink and Yellow bullets at the same time
            if (attackCount % 3 == 0) {
                bossScript.StartCoroutine(bossScript.spinAttack(level - 3, 0.1f, 0.1f));
            } else if (attackCount % 3 == 1) {
                bossScript.StartCoroutine(bossScript.targetedAttack(level - 3, 0.1f, true, 3, 0.5f, true, false, true));
            } else {
                bossScript.StartCoroutine(bossScript.targetPhase(6 + level, 0.55f - (0.02f * (float)level)));
            }
        } else if (level == 11) { // Level 11. Intermixes Pink and Yellow bullets during spin attack
            if (attackCount % 3 == 0) {
                bossScript.StartCoroutine(bossScript.spinAttack(level - 3, 0.2f, 0.2f, false, true));
            } else if (attackCount % 3 == 1) {
                bossScript.StartCoroutine(bossScript.targetedAttack(3, 0.1f, true, 3, 0.5f));
            } else {
                bossScript.StartCoroutine(bossScript.targetPhase(6 + level, 0.55f - (0.02f * (float)level)));
            }
        } else { // All future levels, uses a combination of Pink and Yellow bullets, as well as spawning multiple targets at once
            if (attackCount % 3 == 0) {
                bossScript.StartCoroutine(bossScript.spinAttack(level - 3, 0.2f, 0.15f, false, true, level % 2 == 0));
            } else if (attackCount % 3 == 1) {
                bossScript.StartCoroutine(bossScript.targetedAttack(level - 9, 0.1f, !(level % 2 == 0), level - 9, 0.3f, false, true));
            } else {
                bossScript.StartCoroutine(bossScript.targetPhase(6 + level, level == 12 ? 0.3f : 0.4f, 1, 1 + level / 10));
            }
        }
        attackCount++;
    }
    // Reload game scene
    public void restartGame() {
        dead = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    // Toggle lore menu visibility
    public void displayLore() {
        if (displayingLore) { // Hide menu (Reloads scene)
            restartGame();
        } else { // Show menu
            audioScript.play(audioManager.TRACK.glitch);
            displayingLore = true;
            startMenu.SetActive(false);
            terminal.SetActive(true);
            // Show all unlocked lore paragraphs
            schrodingerLvl = highscore;
            for (int paragraphIdx = 0; paragraphIdx < paragraphs.Length; paragraphIdx++) {
                if (schrodingerLvl >= challengeLvls[paragraphIdx]) {
                    paragraphs[paragraphIdx].enabled = true;
                    if (paragraphIdx == 5) { terminal.GetComponent<Image>().color = Color.red; } // Change menu background to red once 50% of paragraphs are unlocked
                }
            }
        }
    }
    // Display leaderboard menu
    public void displayLeaderboard() {
        // Show menu
        schrodingerLvl = highscore;
        startMenu.SetActive(false);
        leaderboardMenu.SetActive(true);
        // Update leaderboard name ranking
        //      Create list with all non-player leaderboard names
        List<string> orderedNames = new List<string>();
        for (int nameIdx = 0; nameIdx < leaderboardNames.Length; nameIdx++) {
            orderedNames.Add(leaderboardNames[nameIdx] + " - Lvl." + challengeLvls[8-nameIdx]);
        }
        //      Add player name to list in correct position relative to all other leaderboard name levels
        string schrodingerText = "Schrodinger - Lvl." + schrodingerLvl.ToString();
        if (schrodingerLvl >= challengeLvls[0]) {
            for (int challenger = 0; challenger < challengeLvls.Length; challenger++) {
                if (schrodingerLvl >= challengeLvls[8-challenger]) {
                    orderedNames.Insert(challenger, schrodingerText);
                    leaderboards[challenger].color = new Color32(115, 239, 232, 255); // Make player name blue
                    break;
                }
            }
        } else {
            orderedNames.Add(schrodingerText);
            leaderboards[9].color = new Color32(115, 239, 232, 255); // Make player name blue
        }
        //      Update leaderboard with new ranking
        for (int board = 0; board < leaderboards.Length; board++) {
            leaderboards[board].text = (board + 1).ToString() + ". " + orderedNames[board];
        }
    }
    // Activate tutorial
    public void startGame() {
        dead = false;
        darkenScreen.enabled = false;
        startMenu.SetActive(false);
        startingTarget.SetActive(true);
    }
    // Start boss attack loop (The gameplay loop)
    public void startTriggered() {
        boss.SetActive(true);
        bossIntroSprite.SetActive(false);
        audioScript.play(audioManager.TRACK.combat);
        attackEnded();
    }
    // Quit program
    public void quitGame() {
        Application.Quit();
    }
    // Increase player level
    public void increaseLevel() {
        if (dead) { return; }
        justLeveled = true;
        // Adjust player stats
        level++;
        trueMaxHealth *= 1.1f;
        maxHealth = (int) trueMaxHealth;
        health = maxHealth / 3;
        attackCount = 0;
        // Update HUD with new level information
        updateHealth((float)health / (float)maxHealth);
        //      Level counter
        numberSpriteRenderers[0].sprite = numberSprites[level % 10];
        numberSpriteRenderers[1].sprite = numberSprites[((level - (level % 10)) % 100) / 10];
        numberSpriteRenderers[2].sprite = numberSprites[(int) Mathf.Floor(((float)level) / 100f)];
    }
    // Decrease health
    public void decreaseHealth() {
        if (dead) { return; }
        health -= 1;
        damageScreenShake.GenerateImpulseWithForce(1f);
        updateHealth((float)health / (float)maxHealth);
        if (health <= 0) { // Kills player if health reaches 0
            if (highscore < level) { highscore = level; }
            dead = true;
            // Shows game over menu
            darkenScreen.enabled = true;
            gameOver.enabled = true;
            levelText.enabled = true;
            levelText.text = "Lvl. " + level.ToString();
            continueButton.SetActive(true);
            audioScript.play(audioManager.TRACK.gameOver);
        } else
        {
            audioScript.hurt();
        }
    }
    // Increase health
    public void increaseHealth() {
        if (dead) { return; }
        health += 1;
        healScreenShake.GenerateImpulseWithForce(1f);
        if (health >= maxHealth)
        {
            audioScript.level();
            increaseLevel();
        } // Increase level if health reaches max
        updateHealth((float)health / (float)maxHealth);
    }
    // Update displayed health
    void updateHealth(float percent) {
        if (dead) { return; }
        progressText.text = ((int)(percent * 100f)).ToString();
        progressChart.fillAmount = percent;
    }
}