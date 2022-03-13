using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private GameObject canonPos;
    [SerializeField] private GameObject shellEjectionPos;

    [SerializeField] private GameObject magazinePrefab;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject shellPrefab;

    // settings
    [SerializeField] private int magazineSize = 30;
    [SerializeField] private float fireCoolDown = 0.25f;
    [SerializeField] private float reloadCoolDown = 3f;

    // variables
    [HideInInspector] public bool isShootInput = false;
    private bool canShoot = true;

    public void Update()
    {
        if(canShoot && isShootInput)
            StartCoroutine(FireAction());
    }

    private void Fire()
    {
        var bullet = Pooler.instance.Spawn("Bullet", canonPos.transform.position, canonPos.transform.rotation);
        var shell = Pooler.instance.Spawn("Shell", shellEjectionPos.transform.position, shellEjectionPos.transform.rotation);
        
        bullet.GetComponent<PropController>().Event();
        shell.GetComponent<PropController>().Event();
    }

    private IEnumerator FireAction()
    {
        canShoot = false;
        Fire();

        yield return new WaitForSeconds(fireCoolDown);

        canShoot = true;
    }
}
