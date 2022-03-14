using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject acitveComponent;
    [SerializeField] private GameObject inactiveComponent;

    [SerializeField] private GameObject[] componentPrefabs;
    private List<UIElement> elements;

    [SerializeField] private Transform notification;
    private Coroutine notifRoutine;
    private Vector2 notifPos;
    private Vector2 hiddenNotifPos;

    public void Initialize()
    {
        elements = new List<UIElement>();

        inactiveComponent.SetActive(true);

        foreach (GameObject comp in componentPrefabs)
        {
            GameObject inst = Instantiate(comp, this.inactiveComponent.transform);
            UIElement em = inst.GetComponent<UIElement>();

            if (em != null)
                elements.Add(em);
        }

        inactiveComponent.SetActive(false);

        notifPos = notification.position;
        hiddenNotifPos = new Vector2(-notifPos.x, notification.position.y);
        notification.position = hiddenNotifPos;
    }

    #region Notification
    public void NewNotification(string content)
    {
        notification.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = content;
        StartCoroutine(DisplayNotification());
    }
    private IEnumerator DisplayNotification()
    {
        ShowNotification();
        yield return new WaitForSeconds(5);
        HideNotification();
    }

    private void HideNotification()
    {
        if(notifRoutine != null)
            StopCoroutine(notifRoutine);
        notifRoutine = StartCoroutine(MoveNotification(notification.position, hiddenNotifPos, 0.5f));
    }
    private void ShowNotification()
    {
        if (notifRoutine != null)
            StopCoroutine(notifRoutine);
        notifRoutine = StartCoroutine(MoveNotification(notification.position, notifPos, 0.5f));
    }
    private IEnumerator MoveNotification(Vector2 start, Vector2 end, float time)
    {
        float elapsedTime = 0;

        while (elapsedTime < time)
        {
            notification.position = Vector3.Lerp(start, end, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
    #endregion

    public void DisplayGroup(UIGroup group, bool additive = false)
    {
        if (!additive)
            HideAll();

        foreach (UIElement e in elements)
        {
            if (e.group == group)
                e.MoveTo(acitveComponent.transform);
        }
    }

    public void SetGroupActions(UIGroup group, Action mainMenuAction, Action<uint> requestLevelAction, Action creditsAction, Action achievementsAction)
    {
        foreach (UIElement e in elements)
        {
            if (e.group == group)
            {
                e.MainMenuEvent = mainMenuAction;
                e.RequestLevelEvent = requestLevelAction;
                e.CreditsEvent = creditsAction;
                e.AchievementsEvent = achievementsAction;
            }
        }
    }
    public void SetGroupActions(Action mainMenuAction, Action<uint> requestLevelAction, Action creditsAction, Action achievementsAction)
    {
        foreach (UIElement e in elements)
        {
            e.MainMenuEvent = mainMenuAction;
            e.RequestLevelEvent = requestLevelAction;
            e.CreditsEvent = creditsAction;
            e.AchievementsEvent = achievementsAction;
        }
    }

    public void HideGroup(UIGroup group)
    {
        foreach (UIElement e in elements)
        {
            if (e.group == group)
                e.MoveTo(inactiveComponent.transform);
        }
    }
    public void HideAll()
    {
        foreach (UIElement e in elements)
        {
            e.MoveTo(inactiveComponent.transform);
        }
    }
}
