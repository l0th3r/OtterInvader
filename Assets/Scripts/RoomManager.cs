using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoomManager : MonoBehaviour
{
    [SerializeField] private GameObject[] spawnPoints;
    [SerializeField] private GameObject playerPrefab;

    // Round settings
    [SerializeField] private RoundSettings settings;

    // Current round
    private Coroutine roundFlow;
    public Round currentRound;
    private int roundCount = 1;

    // UI
    [SerializeField] private TextMeshProUGUI roundUI;
    [SerializeField] private TextMeshProUGUI timerUI;
    [SerializeField] private TextMeshProUGUI enemiesLeftUI;

    public void Load()
    {
        Pooler.instance.InitPools();

        var newPlayer = Instantiate(playerPrefab, Vector2.zero, Quaternion.identity);
        var weapon = newPlayer.GetComponent<WeaponManager>();
        var player = newPlayer.GetComponent<PlayerController>();
        
        // set events
        player.positionChangeEvent = UpdatePlayerPosition;
        player.DieEvent = PlayerDieEvent;
        weapon.ShootEvent = PlayerShootEvent;
        weapon.ReloadEvent = PlayerRealoadEvent;

        StartRound();
    }

    #region Round
    public void StartRound()
    {
        currentRound = new Round(roundCount, settings);

        if (roundCount == 5)
            AchievementManager.instance.Unlock("Survivor");
        if (roundCount == 10)
            AchievementManager.instance.Unlock("Bear Grylls");

        roundFlow = StartCoroutine(RoundFlow());
        UpdateRoomUI();
    }

    private IEnumerator RoundFlow()
    {
        while(!currentRound.HasSpawnedAllEnemies() || currentRound.GetEnemiesCount() > 0)
        {
            if(currentRound.CanSpawn())
                currentRound.SpawnCereal(GetRandomSpawner());

            UpdateRoomUI();
            CheckRoundProgress();

            yield return null;
        }

        UpdateRoomUI();
        CheckRoundEnd();
        
        yield return new WaitForSeconds(settings.newRoundDelay);
        roundCount++;
        StartRound();
    }

    private void CheckRoundProgress()
    {
        if (currentRound.GetTime() >= 30 && currentRound.data.shots == 0)
            AchievementManager.instance.Unlock("Dodge Master");
    }

    private void CheckRoundEnd()
    {
        if(currentRound.GetTime() <= 45)
            AchievementManager.instance.Unlock("Speed Runner");

        if(currentRound.data.reloads == 0)
            AchievementManager.instance.Unlock("Thrifty");
    }
    #endregion

    #region Player Events
    private void UpdatePlayerPosition(Vector2 pos)
    {
        if(currentRound != null)
        {
            currentRound.StreamPlayerPosition(pos);
        }
    }
    private void PlayerShootEvent()
    {
        currentRound.data.shots++;
        PlayerDataManager.ShotEvent();
    }
    private void PlayerRealoadEvent(bool isMagFull)
    {
        currentRound.data.reloads++;
        PlayerDataManager.ReloadEvent();

        if (isMagFull)
            AchievementManager.instance.Unlock("Waste");
    }
    private void PlayerDieEvent()
    {
        StopCoroutine(roundFlow);
        PlayerDataManager.DeathEvent();
        StartCoroutine(Tools.DelayAction(3f, () => FindObjectOfType<GameManager>().ChangeLevel(0)));
    }
    #endregion

    private GameObject GetRandomSpawner()
    {
        int rnd = new System.Random().Next(0, spawnPoints.Length - 1);
        return spawnPoints[rnd];
    }

    private void UpdateRoomUI()
    {
        TimeSpan timer = TimeSpan.FromSeconds(currentRound.GetTime());
        timerUI.text = timer.ToString("mm':'ss");

        roundUI.text = roundCount.ToString();

        enemiesLeftUI.text = currentRound.GetEnemiesCount().ToString();
    }
}

public class Round
{
    public float startTime;
    private List<CerealManager> cereals;
    private RoundSettings settings;
    public PlayerData data;

    private readonly int multiplier = 1;
    public int spawnedEnemies = 0;
    private float lastSpawnTime;

    public Round(int _multiplier, RoundSettings _settings)
    {
        startTime = Time.time;
        cereals = new List<CerealManager>();
        data = new PlayerData();

        multiplier = _multiplier;
        settings = _settings;
    }

    public int GetEnemiesAmount()
    {
        return settings.enemiesAmount * multiplier;
    }
    public int GetEnemiesCount()
    {
        return cereals.Count;
    }

    public bool HasSpawnedAllEnemies()
    {
        return (GetEnemiesAmount() - spawnedEnemies) == 0;
    }

    public void SpawnCereal(GameObject spawner)
    {
        var cereal = Pooler.instance.Spawn("Cereal", spawner.transform.position, spawner.transform.rotation);
        var cerealManager = cereal.GetComponent<CerealManager>();

        cereals.Add(cerealManager);
        cerealManager.dieEvent = Die;

        spawnedEnemies++;
        lastSpawnTime = GetTime();
    }

    private void Die(CerealManager cereal)
    {
        this.data.kills++;
        PlayerDataManager.KillEvent();

        cereals.Remove(cereal);
        cereal.gameObject.SetActive(false);
    }

    public bool CanSpawn()
    {
        return (GetTime() - lastSpawnTime >= settings.spawnDelay) && spawnedEnemies < GetEnemiesAmount();
    }

    public float GetTime()
    {
        return Time.time - startTime;
    }

    // player data
    public void StreamPlayerPosition(Vector2 pos)
    {
        foreach (CerealManager cm in cereals)
        {
            cm.targetPosition = pos;
        }
    }
}

[System.Serializable]
public struct RoundSettings
{
    public int enemiesAmount;
    public float spawnDelay;
    public float newRoundDelay;
}
