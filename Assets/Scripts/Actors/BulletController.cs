using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<PropController>().EndTrajectoryEvent = HideBullet;
    }

    private void HideBullet()
    {
        this.gameObject.SetActive(false);
    }
}
