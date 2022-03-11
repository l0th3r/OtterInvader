using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "Level Data", fileName = "New Level Data")]
public class LevelData : ScriptableObject
{
    public string displayName = "Level Display Name";
    public string strId = "level_0";
    public int Id = 0;
    public string path = "";
    public UIGroup uiGroup = UIGroup.empty;
}