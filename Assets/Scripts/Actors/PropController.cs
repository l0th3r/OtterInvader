using System.Collections;
using System;
using UnityEngine;

public class PropController : MonoBehaviour
{
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private float distance = 12f;
    [SerializeField] private float travelTime = 0.7f;

    public Action EndTrajectoryEvent;

    public void Event()
    {
        StartCoroutine(Drop());
    }

    private IEnumerator Drop()
    {
        Vector3 startingPos = transform.position;
        Vector3 finalPos = transform.position + transform.right * distance / 10f;
        float elapsedTime = 0;

        while (elapsedTime < travelTime)
        {
            transform.position = Vector3.Lerp(startingPos, finalPos, curve.Evaluate((elapsedTime / travelTime)));

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        if (EndTrajectoryEvent != null)
            EndTrajectoryEvent.Invoke();

    }
}
