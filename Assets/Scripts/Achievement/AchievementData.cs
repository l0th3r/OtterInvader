using UnityEngine;

[CreateAssetMenu(menuName = "Achievement", fileName = "New_Achievement")]
public class AchievementData : ScriptableObject
{
    public string displayName = "Achievement Display Name";
    public string description = "Achievement Display description";
}