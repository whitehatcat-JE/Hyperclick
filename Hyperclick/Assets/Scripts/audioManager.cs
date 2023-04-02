using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioManager : MonoBehaviour
{
    public enum TRACK
    {
        gameOver,
        glitch,
        combat,
        menu
    }

    // Audio Tracks
    public AudioSource menuLoopInstance;
    public static AudioSource menuLoop;

    public AudioSource menuIntroInstance;
    public static AudioSource menuIntro;

    public static bool introPlaying = false;

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

    public AudioSource[] shots = new AudioSource[5];
    public AudioSource[] homingShots = new AudioSource[5];
    public AudioSource[] damages = new AudioSource[5];

    private bool combatIntroPlaying = false;
    private bool gameOverPlaying = false;

    private int prevShotNum = 0;
    private int prevSHomNum = 0;
    private int prevHurtNum = 0;

    private const float HURT_COOLDOWN_TIME = 0.2f;

    private float hurtSFXCooldown = 0f;

    void Awake() {
        DontDestroyOnLoad(menuClick.gameObject); // Stop game over audio from being cut off
        if (menuLoop == null) {
            menuLoop = menuLoopInstance;
            menuIntro = menuIntroInstance;
            menuIntro.Play();
            introPlaying = true;
            DontDestroyOnLoad(menuLoop.gameObject); // Stop game over audio from being cut off
            DontDestroyOnLoad(menuIntro.gameObject); // Stop game over audio from being cut off
        }
    }

    void Start() {
        if (!menuLoop.isPlaying && !menuIntro.isPlaying) {
            menuIntro.Play();
            introPlaying = true;
        }
    }

    void Update() {
        if (combatIntroPlaying && !combatIntro.isPlaying) {
            combatIntroPlaying = false;
            combatLoop.Play();
        } else if (gameOverPlaying && !gameOver.isPlaying)
        {
            gameOverPlaying = false;
            menuIntro.Play();
        } else if (introPlaying && !menuIntro.isPlaying)
        {
            introPlaying = false;
            menuLoop.Play();
        }
        if (hurtSFXCooldown > 0f)
        {
            hurtSFXCooldown -= Time.deltaTime;
        }
    }

    public void play(TRACK requestedTrack)
    {
        switch (requestedTrack) {
            case TRACK.gameOver:
                gameOver.Play();
                combatLoop.Stop();
                combatIntro.Stop();
                menuIntro.Stop();
                combatIntroPlaying = false;
                introPlaying = false;
                gameOverPlaying = true;
                break;
            case TRACK.glitch:
                glitch.Play();
                menuLoop.Stop();
                menuIntro.Stop();
                gameOverPlaying = false;
                introPlaying = false;
                break;
            case TRACK.combat:
                combatIntro.Play();
                menuLoop.Stop();
                menuIntro.Stop();
                combatIntroPlaying = true;
                gameOverPlaying = false;
                introPlaying = false;
                break;
        }
    }

    public void click()
    {
        menuClick.Play();
    }

    public void pop()
    {
        targetPop.Play();
    }

    public void thud()
    {
        landing.Play();
    }

    public void approachGround()
    {
        approach.Play();
    }

    public void shoot()
    {
        int newShotNum = Random.Range(0, shots.Length);
        while (newShotNum == prevShotNum)
        {
            newShotNum = Random.Range(0, shots.Length);
        }
        shots[newShotNum].Play();
        prevShotNum = newShotNum;
    }

    public void shootHoming()
    {
        int newShotNum = Random.Range(0, shots.Length);
        while (newShotNum == prevSHomNum)
        {
            newShotNum = Random.Range(0, shots.Length);
        }
        homingShots[newShotNum].Play();
        prevSHomNum = newShotNum;
    }

    public void hurt()
    {
        if (hurtSFXCooldown > 0f) { return; }
        hurtSFXCooldown = HURT_COOLDOWN_TIME;
        int newHurtNum = Random.Range(0, damages.Length);
        while (newHurtNum == prevHurtNum)
        {
            newHurtNum = Random.Range(0, damages.Length);
        }
        damages[newHurtNum].Play();
        prevHurtNum = newHurtNum;
    }

    public void glitchEnter()
    {
        glitch2.Play();
    }

    public void glitchExit()
    {
        glitch2.Stop();
    }

    public void level()
    {
        levelUp.Play();
    }
}
