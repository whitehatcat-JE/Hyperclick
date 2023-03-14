using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class target : MonoBehaviour
{
    public Animator targetAnims;
    void Start()
    {
        targetAnims.Play("target");
    }

    void OnMouseDown()
    {
        GameObject actionBar = GameObject.FindGameObjectWithTag("ActionBar");
        actionBar.GetComponent<actionBar>().increaseHealth();
        Destroy(gameObject);
    }

    public void decayed()
    {
        GameObject actionBar = GameObject.FindGameObjectWithTag("ActionBar");
        actionBar.GetComponent<actionBar>().decreaseHealth();
        Destroy(gameObject);
    }
}
