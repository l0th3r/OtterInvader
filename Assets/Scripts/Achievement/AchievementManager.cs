using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    #region Singleton
    public static AchievementManager instance;
    #endregion

    private UIManager uiManager;

    [SerializeField] private AchievementData[] achievements;
    private Dictionary<AchievementData, bool> achievementDico;

    private void Awake()
    {
        instance = this;
        achievementDico = new Dictionary<AchievementData, bool>();
        
        foreach(AchievementData ad in achievements)
        {
            achievementDico.Add(ad, false);
        }
    }

    public void SetUIManager(UIManager manager)
    {
        this.uiManager = manager;
    }

    public void Unlock(string name)
    {
        AchievementData data = null;

        for (int i = 0; i < achievements.Length && data == null; i++)
        {
            if (achievements[i].displayName == name)
            {
                data = achievements[i];
                if(!achievementDico[data])
                {
                    achievementDico[data] = true;
                    uiManager.NewNotification(data.displayName);
                }
            }

        }
    }

    public Dictionary<AchievementData, bool> GetAchievementsData()
    {
        return achievementDico;
    }
}
