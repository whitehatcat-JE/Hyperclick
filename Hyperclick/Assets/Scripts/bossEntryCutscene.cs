// Controls boss intro animation
// Libraries
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;

public class bossEntryCutscene : MonoBehaviour {
    // Events
    public UnityEvent introAnimFinished;
    // Screen Shake Object
    public CinemachineImpulseSource landScreenShake;
    // Audio
    public audioManager audioScript;
    // Shakes screen when boss approaches ground
    public void approachingGround() {
        landScreenShake.GenerateImpulseWithForce(1f);
        audioScript.thud();
    }
    // Replaces cutscene boss with actual boss
    public void animFinished() {
        introAnimFinished.Invoke();
    }
}
