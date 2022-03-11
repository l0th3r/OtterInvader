using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Managers
    private LevelManager levelManager;
    private UIManager uiManager;

    private GameState state;
    public GameState State
    {
        get { return this.state; }
        set
        {
            this.state = value;
            HandleStateChange(value);
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        // Check if a GameManager Already exist, if not exec "StartUp";
        if (FindObjectsOfType<GameManager>().Length > 1)
            Destroy(this.gameObject);
        else
            StartUp();
    }

    private void StartUp()
    {
        this.State = GameState.starting;

        // Enable EventSystem
        GetComponentInChildren<EventSystem>().enabled = true;

        // Get managers
        levelManager = GetComponentInChildren<LevelManager>();
        uiManager = GetComponentInChildren<UIManager>();

        uiManager.Initialize();
        uiManager.SetGroupActions(GoToMainMenu, ChangeLevel);

        if (levelManager.GetActiveScene().name == "_initial")
        {
            uiManager.DisplayGroup(UIGroup.loading);
            StartCoroutine(Tools.DelayAction(1f, () => this.State = GameState.mainMenu));
        }
        else
        {
            SetLevelUI();
            this.State = GameState.waitingInput;
        }
    }

    public void SetLevelUI()
    {
        if (levelManager.GetActiveLevel(out LevelData level, out _))
            uiManager.DisplayGroup(level.uiGroup);
        else
            uiManager.DisplayGroup(UIGroup.baseGame);
    }

    #region Level Manager
    private void ChangeLevel(uint level)
    {
        this.State = GameState.loading;
        levelManager.StartLoadLevel(level, ChangeLevelDone);
    }
    public IEnumerator SetupLevel()
    {
        yield return null;
        this.State = GameState.waitingInput;
    }
    private void ChangeLevelDone()
    {
        levelManager.DisplayCurrentLevel();
        StartCoroutine(SetupLevel());
    }
    public void GoToMainMenu()
    {
        this.State = GameState.mainMenu;
    }
    #endregion

    #region State
    private void HandleStateChange(GameState state)
    {
        switch (state)
        {
            case GameState.loading:
                uiManager.DisplayGroup(UIGroup.loading);
                break;
            case GameState.mainMenu:
                uiManager.DisplayGroup(UIGroup.mainMenu);
                break;
            case GameState.waitingInput:
                SetLevelUI();
                break;
        }
    }

    public enum GameState
    {
        loading,
        starting,
        mainMenu,
        waitingInput,
    }
    #endregion
}
