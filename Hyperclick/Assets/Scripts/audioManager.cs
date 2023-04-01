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
    public AudioSource gameOver;
    public AudioSource glitch;
    public AudioSource glitch2;
    public AudioSource combatIntro;
    public AudioSource combatLoop;
    public AudioSource menuLoopInstance;
    public static AudioSource menuLoop;
    public AudioSource menuClick;
    public AudioSource targetPop;
    public AudioSource landing;

    private bool combatIntroPlaying = false;
    private bool gameOverPlaying = false;

    void Awake() {
        DontDestroyOnLoad(menuClick.gameObject); // Stop game over audio from being cut off
        if (menuLoop == null) {
            menuLoop = menuLoopInstance;
            menuLoop.Play();
            DontDestroyOnLoad(menuLoop.gameObject); // Stop game over audio from being cut off
        }
    }

    void Start() {
        if (!menuLoop.isPlaying) {
            menuLoop.Play();
        }
    }

    void Update() {
        if (combatIntroPlaying && !combatIntro.isPlaying) {
            combatIntroPlaying = false;
            combatLoop.Play();
        } else if (gameOverPlaying && !gameOver.isPlaying)
        {
            gameOverPlaying = false;
            menuLoop.Play();
        }
    }

    public void play(TRACK requestedTrack)
    {
        switch (requestedTrack) {
            case TRACK.gameOver:
                gameOver.Play();
                combatLoop.Stop();
                combatIntro.Stop();
                combatIntroPlaying = false;
                gameOverPlaying = true;
                break;
            case TRACK.glitch:
                glitch.Play();
                menuLoop.Stop();
                gameOverPlaying = false;
                break;
            case TRACK.combat:
                combatIntro.Play();
                menuLoop.Stop();
                combatIntroPlaying = true;
                gameOverPlaying = false;
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

    public void glitchEnter()
    {
        glitch2.Play();
    }

    public void glitchExit()
    {
        glitch2.Stop();
    }
}
