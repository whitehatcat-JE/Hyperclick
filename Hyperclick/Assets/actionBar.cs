using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class actionBar : MonoBehaviour
{
    public GameObject self;
    

    private float maxHealthPos = -1.52f;
    private float maxScale = 331f;
    private float minHealthPos = -6.656f;
    private float minScale = 0f;

    private float percent = 0f;
    private int STARTING_HEALTH = 10;
    private int health;

    public actionBar()
    {
        health = STARTING_HEALTH;
    }

    void Start()
    {
        updateHealth((float)health / (float)STARTING_HEALTH);
    }

    public void decreaseHealth()
    {
        health -= 1;
        if (health < 0) { health = STARTING_HEALTH; }
        updateHealth((float)health / (float) STARTING_HEALTH);
    }

    void updateHealth(float percent)
    {
        Transform barTransform = self.transform;
        Vector3 newScale = new Vector3(minScale + (maxScale - minScale) * percent, 1f, 0f);
        barTransform.localScale = newScale;
        barTransform.position = new Vector3((minHealthPos + (maxHealthPos - minHealthPos) * percent), 4.5f, 0f);
        self.transform.position = barTransform.position;
        self.transform.localScale = barTransform.localScale;
    }
}
