using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets._2D;

public class LifeCount : MonoBehaviour
{


    Text lifeDisplay;
    float lives;

    void Start()
    {
        lifeDisplay = this.GetComponent<Text>();
        var player = Transform.FindObjectOfType<PlatformerCharacter2D>();
        lives = player.lives;
        UpdateLives();
    }

    public void ExtraLife()
    {
        ++lives;
        UpdateLives();
    }

    public void Die()
    {
        --lives;
        UpdateLives();
    }

    private void UpdateLives()
    {
        if (lives < 10)
        {
            lifeDisplay.text = "x0" + lives;
        }
        else
        {
            lifeDisplay.text = "x" + lives;
        }
    }
}
