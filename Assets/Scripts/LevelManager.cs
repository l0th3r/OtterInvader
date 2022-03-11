using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // Store level data
    [SerializeField] private LevelData[] levels;

    // Settings
    [SerializeField] private float secondsBetweenLoadingCheck = 0f;

    // Store current level
    private CurrentLevel currentLevel = null;

    #region Level manager features
    public bool StartLoadLevel(uint id, Action doneLoadingCallback = null)
    {
        if (FindLevelData(id, out LevelData found))
        {
            this.currentLevel = new CurrentLevel(this, found, secondsBetweenLoadingCheck);
            this.currentLevel.StartLoading(doneLoadingCallback);
            return true;
        }
        else
        {
            Debug.LogError("Couldn't found or start loading of level with id: " + id);
            return false;
        }
    }
    public bool StartLoadLevel(string id, Action doneLoadingCallback = null)
    {
        if (FindLevelData(id, out LevelData found))
        {
            this.currentLevel = new CurrentLevel(this, found, secondsBetweenLoadingCheck);
            this.currentLevel.StartLoading(doneLoadingCallback);
            return true;
        }
        else
        {
            Debug.LogError("Couldn't found or start loading of level with id: " + id);
            return false;
        }
    }

    public void DisplayCurrentLevel()
    {
        if (currentLevel != null && currentLevel.CanDisplay())
            currentLevel.Display();
    }

    /// <summary>
    /// Get the current level and scene data.
    /// </summary>
    /// <param name="data">LevelData output, default if not found</param>
    /// <param name="scene">Scene output, current active scene</param>
    /// <returns> true: if LevelData "data" is found. false: if LevelData "data" is not found.
    /// </returns>
    public bool GetActiveLevel(out LevelData data, out Scene scene)
    {
        scene = SceneManager.GetActiveScene();

        if (currentLevel != null)
        {
            data = currentLevel.data;
            return true;
        }
        else
        {
            data = default;
            return false;
        }
    }
    public Scene GetActiveScene()
    {
        return SceneManager.GetActiveScene();
    }

    #region Not Usable
    public bool SwitchLevel(uint id, Action doneLoadingNextCallback = null)
    {
        if (currentLevel != null && currentLevel.CanUnload())
        {
            currentLevel.Unload(() => StartLoadLevel(id, doneLoadingNextCallback));
            return true;
        }
        else
        {
            Debug.LogError("Failed to switch level, found or start loading level with id: " + id);
            return false;
        }
    }
    public bool SwitchLevel(string id, Action doneLoadingNextCallback = null)
    {
        if (currentLevel != null && currentLevel.CanUnload())
        {
            currentLevel.Unload(() => StartLoadLevel(id, doneLoadingNextCallback));
            return true;
        }
        else
        {
            Debug.LogError("Failed to switch level, found or start loading level with id: " + id);
            return false;
        }
    }
    #endregion
    
    #endregion

    #region Search Levels
    private bool FindLevelData(uint id, out LevelData level)
    {
        bool isSuccess = false;

        // search depending on ID
        LevelData foundLevel = Array.Find(levels, (ld) => ld.Id >= 0 && ld.Id == id);

        if (foundLevel != null)
        {
            level = foundLevel;
            isSuccess = true;
        }
        else
            level = default;

        return isSuccess;
    }

    private bool FindLevelData(string strId, out LevelData level)
    {
        bool isSuccess = false;

        // search depending on ID
        LevelData foundLevel = Array.Find(levels, (ld) => ld.Id >= 0 && ld.strId == strId);

        if (foundLevel != null)
        {
            level = foundLevel;
            isSuccess = true;
        }
        else
            level = default;

        return isSuccess;
    }
    #endregion

    private class CurrentLevel
    {
        public readonly LevelData data;
        private readonly LevelManager manager;

        private LevelState state;

        private AsyncOperation operation;
        private readonly float checkDelay;

        public CurrentLevel(LevelManager levelManager, LevelData levelData, float secondsBetweenLoadingCheck = 0f)
        {
            this.manager = levelManager;
            this.state = LevelState.unloaded;
            this.data = levelData;
            this.checkDelay = secondsBetweenLoadingCheck;

            this.operation = null;
        }

        public void StartLoading(Action doneLoadingCallback = null)
        {
            this.manager.StartCoroutine(LoadLevelStall(doneLoadingCallback));
            this.state = LevelState.loading;
        }

        public void Unload(Action doneUnloadingCallback = null)
        {
            UnloadLevelStall(doneUnloadingCallback);
        }

        public void Display()
        {
            if (this.CanDisplay())
            {
                operation.allowSceneActivation = true;
                this.state = LevelState.displayed;
            }
        }

        public bool CanStartLoad()
        {
            return this.state == LevelState.unloaded;
        }
        public bool CanDisplay()
        {
            return this.state == LevelState.amorced;
        }
        public bool CanUnload()
        {
            return this.state == LevelState.displayed;
        }

        #region Loading and Unloading
        private IEnumerator LoadLevelStall(Action doneLoadingCallback)
        {
            yield return new WaitForSeconds(0.1f); // unity what is wrong with you ??

            this.operation = SceneManager.LoadSceneAsync(this.data.path, LoadSceneMode.Single);
            this.operation.allowSceneActivation = false;

            do
            {
                yield return new WaitForSeconds(checkDelay);
            } while (this.operation.progress < 0.9f);

            this.state = LevelState.amorced;

            if (doneLoadingCallback != null)
                doneLoadingCallback.Invoke();
        }

        private IEnumerator UnloadLevelStall(Action doneUnloadingCallback)
        {
            yield return new WaitForSeconds(0.1f); // unity what is wrong with you ??

            this.operation = SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());

            while (!this.operation.isDone)
            {
                yield return new WaitForSeconds(checkDelay);
            }

            this.state = LevelState.unloaded;

            if (doneUnloadingCallback != null)
                doneUnloadingCallback.Invoke();
        }
        #endregion
    }

    private enum LevelState
    {
        unloaded,
        loading,
        amorced,
        displayed,
    }
}