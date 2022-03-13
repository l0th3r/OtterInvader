using System.Collections;
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

    // values
    private Vector2 currentMovement = Vector2.zero;
    private Vector2 aimDirection = Vector2.zero;

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
    }

    private void FixedUpdate()
    {
        if(canMove)
        {
            // Movement
            this.transform.position += 4f * Time.fixedDeltaTime * (Vector3)currentMovement;

            // Rotation
            float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
            model.transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
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
