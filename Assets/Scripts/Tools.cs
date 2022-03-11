using System;
using System.Collections;
using UnityEngine;

public static class Tools
{
    public static IEnumerator DelayAction(float seconds, Action callback)
    {
        yield return new WaitForSeconds(seconds);
        callback.Invoke();
    }
}
