// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Character/Input/PlayerInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""MainMap"",
            ""id"": ""d0c26e96-d6f0-4b2d-a219-305c9c49496a"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""9a17d047-1ac4-4c3e-b24f-4d501fc164f7"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Aim"",
                    ""type"": ""Value"",
                    ""id"": ""2682ba46-d4ff-423c-8f9e-d5d7f606d806"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""AimMouse"",
                    ""type"": ""Button"",
                    ""id"": ""3a5a9365-d752-4313-8a17-92c9f05a0754"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Fire"",
                    ""type"": ""Button"",
                    ""id"": ""3d378701-2364-4ae9-bb75-b48c93f71455"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Reload"",
                    ""type"": ""Button"",
                    ""id"": ""129d446b-1bb6-454d-a089-d84a81d1b517"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""06f0c30d-4ac5-4895-8a96-44b38e9124a1"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""b4d8d2f8-0309-4ac2-ad27-34498715d86c"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""b35b17f2-54f4-405b-b21b-7885e80ddff6"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""6ead00eb-59de-4f25-ad6b-591a2a90e589"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""5b7ab0b0-0b95-4cb9-be84-5f1f1405b13f"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""1055770e-529d-44f6-9d64-645d24c3d197"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""1efad6dd-4e0f-4230-9d55-9350030cca1a"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""50bf1821-e6dd-4779-8ba5-fd0ec5c8d681"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AimMouse"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a6dc971a-b14b-465d-946d-3a523b279c1f"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e4fb8967-0dce-435a-b295-dc9089f614b4"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a200bdcd-f967-4125-b40b-cc0ce3fb92e5"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Reload"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7aa800c4-2d61-47b3-a671-56e9dac8554a"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Reload"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // MainMap
        m_MainMap = asset.FindActionMap("MainMap", throwIfNotFound: true);
        m_MainMap_Move = m_MainMap.FindAction("Move", throwIfNotFound: true);
        m_MainMap_Aim = m_MainMap.FindAction("Aim", throwIfNotFound: true);
        m_MainMap_AimMouse = m_MainMap.FindAction("AimMouse", throwIfNotFound: true);
        m_MainMap_Fire = m_MainMap.FindAction("Fire", throwIfNotFound: true);
        m_MainMap_Reload = m_MainMap.FindAction("Reload", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // MainMap
    private readonly InputActionMap m_MainMap;
    private IMainMapActions m_MainMapActionsCallbackInterface;
    private readonly InputAction m_MainMap_Move;
    private readonly InputAction m_MainMap_Aim;
    private readonly InputAction m_MainMap_AimMouse;
    private readonly InputAction m_MainMap_Fire;
    private readonly InputAction m_MainMap_Reload;
    public struct MainMapActions
    {
        private @PlayerInput m_Wrapper;
        public MainMapActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_MainMap_Move;
        public InputAction @Aim => m_Wrapper.m_MainMap_Aim;
        public InputAction @AimMouse => m_Wrapper.m_MainMap_AimMouse;
        public InputAction @Fire => m_Wrapper.m_MainMap_Fire;
        public InputAction @Reload => m_Wrapper.m_MainMap_Reload;
        public InputActionMap Get() { return m_Wrapper.m_MainMap; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MainMapActions set) { return set.Get(); }
        public void SetCallbacks(IMainMapActions instance)
        {
            if (m_Wrapper.m_MainMapActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_MainMapActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_MainMapActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_MainMapActionsCallbackInterface.OnMove;
                @Aim.started -= m_Wrapper.m_MainMapActionsCallbackInterface.OnAim;
                @Aim.performed -= m_Wrapper.m_MainMapActionsCallbackInterface.OnAim;
                @Aim.canceled -= m_Wrapper.m_MainMapActionsCallbackInterface.OnAim;
                @AimMouse.started -= m_Wrapper.m_MainMapActionsCallbackInterface.OnAimMouse;
                @AimMouse.performed -= m_Wrapper.m_MainMapActionsCallbackInterface.OnAimMouse;
                @AimMouse.canceled -= m_Wrapper.m_MainMapActionsCallbackInterface.OnAimMouse;
                @Fire.started -= m_Wrapper.m_MainMapActionsCallbackInterface.OnFire;
                @Fire.performed -= m_Wrapper.m_MainMapActionsCallbackInterface.OnFire;
                @Fire.canceled -= m_Wrapper.m_MainMapActionsCallbackInterface.OnFire;
                @Reload.started -= m_Wrapper.m_MainMapActionsCallbackInterface.OnReload;
                @Reload.performed -= m_Wrapper.m_MainMapActionsCallbackInterface.OnReload;
                @Reload.canceled -= m_Wrapper.m_MainMapActionsCallbackInterface.OnReload;
            }
            m_Wrapper.m_MainMapActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Aim.started += instance.OnAim;
                @Aim.performed += instance.OnAim;
                @Aim.canceled += instance.OnAim;
                @AimMouse.started += instance.OnAimMouse;
                @AimMouse.performed += instance.OnAimMouse;
                @AimMouse.canceled += instance.OnAimMouse;
                @Fire.started += instance.OnFire;
                @Fire.performed += instance.OnFire;
                @Fire.canceled += instance.OnFire;
                @Reload.started += instance.OnReload;
                @Reload.performed += instance.OnReload;
                @Reload.canceled += instance.OnReload;
            }
        }
    }
    public MainMapActions @MainMap => new MainMapActions(this);
    public interface IMainMapActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnAim(InputAction.CallbackContext context);
        void OnAimMouse(InputAction.CallbackContext context);
        void OnFire(InputAction.CallbackContext context);
        void OnReload(InputAction.CallbackContext context);
    }
}
