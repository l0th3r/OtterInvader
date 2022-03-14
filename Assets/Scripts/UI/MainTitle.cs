using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTitle : MonoBehaviour
{
    int count = 0;
    
    public void Increment()
    {
        count++;
        if (count >= 20)
            AchievementManager.instance.Unlock("Bored");
    }
}
