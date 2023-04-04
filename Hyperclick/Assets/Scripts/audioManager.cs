// Controls game audio instances
// Libraries
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioManager : MonoBehaviour
{
    // Music Enumerator
    public enum TRACK
    {
        gameOver,
        glitch,
        combat,
        menu
    }

    // Audio Tracks
    //      Global Audio (Not destroyed on scene reload)
    public AudioSource menuLoopInstance;
    public static AudioSource menuLoop;

    public AudioSource menuIntroInstance;
    public static AudioSource menuIntro;
    //      Global Audio States
    public static bool introPlaying = false;
    //      Local Audio
    public AudioSource gameOver;
    public AudioSource glitch;
    public AudioSource glitch2;
    public AudioSource combatIntro;
    public AudioSource combatLoop;
    public AudioSource menuClick;
    public AudioSource targetPop;
    public AudioSource landing;
    public AudioSource approach;
    public AudioSource levelUp;
    //      Audio Variants (For randomly pitch shifted SFX)
    public AudioSource[] shots = new AudioSource[5];
    public AudioSource[] homingShots = new AudioSource[5];
    public AudioSource[] damages = new AudioSource[5];
    // SFX Cooldown Variables
    private const float HURT_COOLDOWN_TIME = 0.2f;
    private float hurtSFXCooldown = 0f;
    // Music State Variables
    private bool combatIntroPlaying = false;
    private bool gameOverPlaying = false;
    // Variant Variables
    private int prevShotNum = 0;
    private int prevSHomNum = 0;
    private int prevHurtNum = 0;
    // Called on scene load
    void Awake() {
        DontDestroyOnLoad(menuClick.gameObject); // Stop button click SFX from being cut off
        if (menuLoop == null) { // Check if game has just been loaded
            // Set up global variable assignments
            menuLoop = menuLoopInstance;
            menuIntro = menuIntroInstance;
            // Play menu music
            menuIntro.Play();
            introPlaying = true;
            // Prevent global audio from being destroyed on scene deletion
            DontDestroyOnLoad(menuLoop.gameObject);
            DontDestroyOnLoad(menuIntro.gameObject);
        }
    }
    // Called when Audio Manager activated
    void Start() {
        if (!menuLoop.isPlaying && !menuIntro.isPlaying) { // Play menu music
            menuIntro.Play();
            introPlaying = true;
        }
    }
    // Called once per frame
    void Update() {
        if (combatIntroPlaying && !combatIntro.isPlaying) { // Combat fade in to combat loop transition
            combatIntroPlaying = false;
            combatLoop.Play();
        } else if (gameOverPlaying && !gameOver.isPlaying) { // Game over to menu transition
            gameOverPlaying = false;
            menuIntro.Play();
        } else if (introPlaying && !menuIntro.isPlaying) { // Menu fade in to menu loop transition
            introPlaying = false;
            menuIntro.Stop();
            menuLoop.Play();
        }
        if (hurtSFXCooldown > 0f) { // Record time since last SFX played
            hurtSFXCooldown -= Time.deltaTime;
        }
    }
    // Play requested music
    public void play(TRACK requestedTrack) {
        switch (requestedTrack) {
            case TRACK.gameOver:
                gameOver.Play();
                // Stop all other music from playing
                combatLoop.Stop();
                combatIntro.Stop();
                menuIntro.Stop();
                combatIntroPlaying = false;
                introPlaying = false;
                gameOverPlaying = true;
                break;
            case TRACK.glitch:
                glitch.Play();
                // Stop all other music from playing
                menuLoop.Stop();
                menuIntro.Stop();
                gameOverPlaying = false;
                introPlaying = false;
                break;
            case TRACK.combat:
                combatIntro.Play();
                // Stop all other music from playing
                menuLoop.Stop();
                menuIntro.Stop();
                combatIntroPlaying = true;
                gameOverPlaying = false;
                introPlaying = false;
                break;
        }
    }
    // SFX emitters
    public void click() { menuClick.Play(); }
    public void pop() { targetPop.Play(); }
    public void thud() { landing.Play(); }
    public void approachGround() { approach.Play(); }
    public void level() { levelUp.Play(); }
    // Glitch button hover SFX
    public void glitchEnter() { glitch2.Play(); }
    public void glitchExit() { glitch2.Stop(); }
    // Play bullet firing SFX
    public void shoot() {
        // Randomly select bullet pitch
        int newShotNum = Random.Range(0, shots.Length);
        while (newShotNum == prevShotNum) {
            newShotNum = Random.Range(0, shots.Length);
        }
        // Play selected bullet pitch SFX
        shots[newShotNum].Play();
        prevShotNum = newShotNum;
    }
    // Play homing bullet SFX
    public void shootHoming() {
        // Randomly select bullet pitch
        int newShotNum = Random.Range(0, shots.Length);
        while (newShotNum == prevSHomNum) {
            newShotNum = Random.Range(0, shots.Length);
        }
        // Play selected bullet pitch SFX
        homingShots[newShotNum].Play();
        prevSHomNum = newShotNum;
    }
    // Play player hurt SFX
    public void hurt() {
        // Stops multiple hurt SFX from being played at once
        if (hurtSFXCooldown > 0f) { return; }
        hurtSFXCooldown = HURT_COOLDOWN_TIME;
        // Randomly selects hurt pitch
        int newHurtNum = Random.Range(0, damages.Length);
        while (newHurtNum == prevHurtNum) {
            newHurtNum = Random.Range(0, damages.Length);
        }
        // Play selected hurt pitch SFX
        damages[newHurtNum].Play();
        prevHurtNum = newHurtNum;
    }
}
