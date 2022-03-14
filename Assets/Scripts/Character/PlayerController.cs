using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject model;

    private WeaponManager weapon;
    private PlayerInput playerInput;

    // Conditions
    private bool canMove = true;
    private bool isDead = false;

    // values
    private Vector2 currentMovement = Vector2.zero;
    private Vector2 aimDirection = Vector2.zero;

    // Events
    public Action<Vector2> positionChangeEvent;
    public Action DieEvent;

    private void Awake()
    {
        playerInput = new PlayerInput();

        // Get manager
        weapon = GetComponentInChildren<WeaponManager>();

        // Movements
        playerInput.MainMap.Move.performed += OnMovementChange;
        playerInput.MainMap.Move.canceled += OnMovementChange;

        // Aim
        playerInput.MainMap.Aim.performed += OnAimChange;
        playerInput.MainMap.AimMouse.performed += OnAimMouseChange;

        // Fire
        playerInput.MainMap.Fire.started += OnFire;
        playerInput.MainMap.Fire.canceled += OnFire;

        // Reload
        playerInput.MainMap.Reload.started += OnReload;
    }

    private void FixedUpdate()
    {
        if(canMove)
        {
            // Movement (4f is speed)
            this.transform.position += 4f * Time.fixedDeltaTime * (Vector3)currentMovement;

            // Rotation
            float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
            model.transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);

            if (positionChangeEvent != null)
                positionChangeEvent.Invoke(this.transform.position);
        }
    }

    #region Input Events
    private void OnMovementChange(InputAction.CallbackContext ctx)
    {
        currentMovement = ctx.ReadValue<Vector2>();
    }
    private void OnAimChange(InputAction.CallbackContext ctx)
    {
        aimDirection = ctx.ReadValue<Vector2>();
    }
    private void OnAimMouseChange(InputAction.CallbackContext ctx)
    {
        aimDirection = ctx.ReadValue<Vector2>();
        aimDirection.x -= Screen.width / 2;
        aimDirection.y -= Screen.height / 2;
    }
    
    private void OnFire(InputAction.CallbackContext ctx)
    {
        weapon.isShootInput = ctx.ReadValueAsButton();
    }
    private void OnReload(InputAction.CallbackContext ctx)
    {
        weapon.Reload();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isDead && collision.gameObject.CompareTag("Cereal"))
        {
            if (DieEvent != null)
                DieEvent.Invoke();

            canMove = false;
            isDead = true;
            Destroy(model);
        }
    }

    private void OnEnable()
    {
        playerInput.MainMap.Enable();
    }

    private void OnDisable()
    {
        playerInput.MainMap.Disable();
    }
    #endregion
}
