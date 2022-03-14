using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    private float startTime;

    private void Update()
    {
        if (Time.time - startTime >= 60f)
        {
            AchievementManager.instance.Unlock("Undecided");
        }
    }

    public void FoundMemeMaster()
    {
        AchievementManager.instance.Unlock("The Meme Lord");
    }

    private void OnEnable()
    {
        startTime = Time.time;
    }
}
