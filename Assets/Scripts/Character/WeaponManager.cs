using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private GameObject canonPos;
    [SerializeField] private GameObject shellEjectionPos;
    [SerializeField] private TextMeshProUGUI magazineUI;

    // settings
    [SerializeField] private int magazineSize = 30;
    [SerializeField] private float fireCoolDown = 0.25f;
    [SerializeField] private float reloadCoolDown = 3f;

    // variables
    [HideInInspector] public bool isShootInput = false;
    private bool canShoot = true;
    private bool canReload = true;
    private int currentMagazine;

    // events
    public Action ShootEvent;
    public Action<bool> ReloadEvent; // true if mag is full

    private void Start()
    {
        currentMagazine = magazineSize;
        UpdateUI();
    }

    public void Update()
    {
        if (currentMagazine > 0 && canShoot && isShootInput && canonPos != null && shellEjectionPos != null)
            StartCoroutine(FireAction());
    }

    private void Fire()
    {
        var bullet = Pooler.instance.Spawn("Bullet", canonPos.transform.position, canonPos.transform.rotation);
        var shell = Pooler.instance.Spawn("Shell", shellEjectionPos.transform.position, shellEjectionPos.transform.rotation);

        bullet.GetComponent<PropController>().Event();
        shell.GetComponent<PropController>().Event();

        if (ShootEvent != null)
            ShootEvent.Invoke();

        currentMagazine--;
        UpdateUI();
    }
    public void Reload()
    {
        if (!canReload)
            return;

        if (ReloadEvent != null)
            ReloadEvent.Invoke(currentMagazine == magazineSize);

        canReload = false;
        currentMagazine = 0;
        UpdateUI();
        
        var mag = Pooler.instance.Spawn("Mag", shellEjectionPos.transform.position, shellEjectionPos.transform.rotation);
        mag.GetComponent<PropController>().Event();

        StartCoroutine(ReloadAction());
    }

    private IEnumerator FireAction()
    {
        canShoot = false;
        Fire();

        yield return new WaitForSeconds(fireCoolDown);

        canShoot = true;
    }
    private IEnumerator ReloadAction()
    {
        yield return new WaitForSeconds(reloadCoolDown);
        currentMagazine = magazineSize;
        UpdateUI();
        canReload = true;
    }

    private void UpdateUI()
    {
        magazineUI.text = $"{currentMagazine}/{magazineSize}";

        if (currentMagazine == 0)
            magazineUI.color = Color.red;
        else
            magazineUI.color = Color.white;
    }
}
