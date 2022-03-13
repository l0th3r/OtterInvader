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
    private const int magazineSize = 30;
    private const float fireCoolDown = 0.5f;
    private const float reloadCoolDown = 3f;

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
        var te = Pooler.instance.Spawn("Bullet", canonPos.transform.position, canonPos.transform.rotation);
        var shell = Pooler.instance.Spawn("Shell", shellEjectionPos.transform.position, shellEjectionPos.transform.rotation);
        shell.GetComponent<ShellController>().Eject();
    }

    private IEnumerator FireAction()
    {
        canShoot = false;
        Fire();

        yield return new WaitForSeconds(fireCoolDown);

        canShoot = true;
    }
}
