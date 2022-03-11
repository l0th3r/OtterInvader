using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LoadingSpinner : MonoBehaviour
{
    [SerializeField] private Transform rectTransform;
    [SerializeField] private float secondsPerSpin = 1f;
    [SerializeField] private string loadingText = "Loading...";

    private bool canSpin = true;

    void Start()
    {
        if (!rectTransform)
            rectTransform = GetComponent<Transform>();

        if (!rectTransform || loadingText.Length == 0)
            Debug.LogWarning(this.gameObject.name + "'s loading spinner script is not finding necessary components !");
        else
            BeginSpin();
    }

    public void BeginSpin()
    {
        canSpin = true;
        StartCoroutine(SpinOnce(secondsPerSpin, BeginSpin));
    }

    public void StopSpin()
    {
        canSpin = false;
    }

    private IEnumerator SpinOnce(float spinTime, UnityAction callback, float spinDirection = -1)
    {
        Vector3 startRotation = rectTransform.eulerAngles;
        Vector3 targetRotation = new Vector3(0, 0, rectTransform.localRotation.eulerAngles.z + 360f);

        float passedTime = 0f;
        while (canSpin && passedTime <= spinTime)
        {
            rectTransform.eulerAngles = Vector3.Lerp(startRotation, targetRotation, (passedTime / spinTime)) * spinDirection;

            passedTime += Time.deltaTime;

            if (passedTime > spinTime)
                rectTransform.eulerAngles = targetRotation;

            yield return null;
        }

        if (canSpin && callback != null)
            callback.Invoke();
    }

    private void OnEnable()
    {
        BeginSpin();
    }
}
