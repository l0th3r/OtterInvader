using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoomManager : MonoBehaviour
{
    [SerializeField] private GameObject[] spawnPoints;
    [SerializeField] private GameObject playerPrefab;
    private PlayerController player;

    // Round Format
    [SerializeField] private RoundSettings settings;

    // Current round format
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
        player = newPlayer.GetComponent<PlayerController>();
        player.positionChangeEvent = UpdatePlayerPosition;

        StartRound();
    }

    public void StartRound()
    {
        currentRound = new Round(roundCount, settings);
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

            yield return null;
        }

        UpdateRoomUI();
        yield return new WaitForSeconds(settings.newRoundDelay);
        roundCount++;
        StartRound();
    }

    private void UpdateRoomUI()
    {
        TimeSpan timer = TimeSpan.FromSeconds(currentRound.GetTime());
        timerUI.text = timer.ToString("mm':'ss");

        roundUI.text = roundCount.ToString();

        enemiesLeftUI.text = currentRound.GetEnemiesCount().ToString();
    }

    private void UpdatePlayerPosition(Vector2 pos)
    {
        if(currentRound != null)
        {
            currentRound.StreamPlayerPosition(pos);
        }
    }

    private GameObject GetRandomSpawner()
    {
        int rnd = new System.Random().Next(0, spawnPoints.Length - 1);
        return spawnPoints[rnd];
    }
}

public class Round
{
    public float startTime;
    private List<CerealManager> cereals;
    private RoundSettings settings;

    private readonly int multiplier = 1;
    public int spawnedEnemies = 0;

    private float lastSpawnTime;

    public Round(int _multiplier, RoundSettings _settings)
    {
        startTime = Time.time;
        cereals = new List<CerealManager>();

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

    public void StreamPlayerPosition(Vector2 pos)
    {
        foreach(CerealManager cm in cereals)
        {
            cm.targetPosition = pos;
        }
    }

    private void Die(CerealManager cereal)
    {
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
}

[System.Serializable]
public struct RoundSettings
{
    public int enemiesAmount;
    public float spawnDelay;
    public float newRoundDelay;
}
