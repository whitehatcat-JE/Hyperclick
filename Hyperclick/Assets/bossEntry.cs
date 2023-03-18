using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;

public class bossEntry : MonoBehaviour
{
    public UnityEvent introAnimFinished;
    public CinemachineImpulseSource landScreenShake;

    public void approachingGround()
    {

        landScreenShake.GenerateImpulseWithForce(1f);
    }

    public void animFinished()
    {
        introAnimFinished.Invoke();
    }
}
