using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AchievementsUI : MonoBehaviour
{
    [SerializeField] private GameObject achievementPrefab;
    [SerializeField] private Transform contentParent;

    private void OnEnable()
    {
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        foreach (KeyValuePair<AchievementData, bool> data in AchievementManager.instance.GetAchievementsData())
        {
            var obj = Instantiate(achievementPrefab, contentParent);

            var ad = data.Key;

            TextMeshProUGUI[] texts = obj.GetComponentsInChildren<TextMeshProUGUI>();
            texts[0].text = ad.displayName;
            texts[1].text = ad.description;

            obj.GetComponentInChildren<Toggle>().isOn = data.Value;
        }
    }
}
