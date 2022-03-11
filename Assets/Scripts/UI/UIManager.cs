using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject acitveComponent;
    [SerializeField] private GameObject inactiveComponent;

    [SerializeField] private GameObject[] componentPrefabs;
    private List<UIElement> elements;

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
    }

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

    public void SetGroupActions(UIGroup group, Action mainMenuAction, Action<uint> requestLevelAction)
    {
        foreach (UIElement e in elements)
        {
            if (e.group == group)
            {
                e.MainMenuEvent = mainMenuAction;
                e.RequestLevelEvent = requestLevelAction;
            }
        }
    }
    public void SetGroupActions(Action mainMenuAction, Action<uint> requestLevelAction)
    {
        foreach (UIElement e in elements)
        {
            e.MainMenuEvent = mainMenuAction;
            e.RequestLevelEvent = requestLevelAction;
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
