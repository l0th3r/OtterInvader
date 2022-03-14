using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIElement : MonoBehaviour
{
    public UIGroup group = UIGroup.empty;
    public int layer = 0;

    // UI Events
    public Action MainMenuEvent;
    public Action CreditsEvent;
    public Action AchievementsEvent;
    public Action<uint> RequestLevelEvent;

    public void MoveTo(Transform newParent)
    {
        this.gameObject.transform.SetParent(newParent);
    }

    public void GoToMainMenu()
    {
        if (MainMenuEvent != null)
            MainMenuEvent.Invoke();
    }


    public void GoToLevel(int id)
    {
        if (RequestLevelEvent != null)
            RequestLevelEvent.Invoke((uint)id);
    }

    public void GoToCredits()
    {
        if (CreditsEvent != null)
            CreditsEvent.Invoke();
    }

    public void GoToAchievements()
    {
        if (AchievementsEvent != null)
            AchievementsEvent.Invoke();
    }
}

[System.Serializable]
public enum UIGroup
{
    empty,
    initial,
    loading,
    mainMenu,
    baseGame,
    credits,
    achievements
}