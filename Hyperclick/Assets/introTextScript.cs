using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class introTextScript : MonoBehaviour
{

    public TextMeshProUGUI tauntText;

    public float speed = 0.2f;

    private float alphaVisiblity = 0f;
    private bool increasing = true;
    private bool active = false;

    public void taunt()
    {
        switch (Random.Range(0, 7))
        {
            case 0:
                tauntText.text = "Shall we begin?";
                break;
            case 1:
                tauntText.text = "Let us begin.";
                break;
            case 2:
                tauntText.text = "Your death shall be swift.";
                break;
            case 3:
                tauntText.text = "I will make your death painless.";
                break;
            case 4:
                tauntText.text = "It seems we are destined to fight.";
                break;
            case 5:
                tauntText.text = "Prove yourself worthy!";
                break;
            case 6:
                tauntText.text = "How far will you get this time?";
                break;
        }
        active = true;
    }

    void Update()
    {
        if (active)
        {
            if (increasing)
            {
                alphaVisiblity += Time.deltaTime * speed;
                if (alphaVisiblity >= 1f) {
                    increasing = false;
                    alphaVisiblity = 1f;
                }
            } else
            {
                alphaVisiblity -= Time.deltaTime * speed;
                if (alphaVisiblity <= 0f)
                {
                    increasing = false;
                    alphaVisiblity = 0f;
                    active = false;
                }
            }
            Color newColor = tauntText.color;
            newColor.a = alphaVisiblity;
            tauntText.color = newColor;
        }
    }
}
