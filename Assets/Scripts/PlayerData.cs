using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerDataManager
{
    private static PlayerData persistant = new PlayerData();

    public static void ShotEvent()
    {
        persistant.shots++;

        if (persistant.shots >= 1000)
            AchievementManager.instance.Unlock("Mad Man");
    }

    public static void ReloadEvent()
    {
        persistant.reloads++;
    }

    public static void KillEvent()
    {
        persistant.kills++;

        if (persistant.kills == 1)
            AchievementManager.instance.Unlock("That's a start");

        if (persistant.kills >= 1000)
            AchievementManager.instance.Unlock("Cereal Killer");
    }

    public static void DeathEvent()
    {
        persistant.deaths++;

        if (persistant.deaths >= 10)
            AchievementManager.instance.Unlock("Try Hard");
    }
}

public class PlayerData
{
    public int shots = 0;
    public int reloads = 0;
    public int kills = 0;
    public int deaths = 0;
}
