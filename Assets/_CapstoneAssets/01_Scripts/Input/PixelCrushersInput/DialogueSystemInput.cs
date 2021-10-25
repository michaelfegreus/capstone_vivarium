// GENERATED AUTOMATICALLY FROM 'Assets/_CapstoneAssets/01_Scripts/Input/PixelCrushersInput/DialogueSystemInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @DialogueSystemInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @DialogueSystemInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""DialogueSystemInput"",
    ""maps"": [
        {
            ""name"": ""ActionMap"",
            ""id"": ""41649a10-fe04-42dc-b834-7b0e6b8f6f8e"",
            ""actions"": [
                {
                    ""name"": ""Horizontal"",
                    ""type"": ""Button"",
                    ""id"": ""ef3929c6-b315-4851-8f3e-ae170992d312"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Vertical"",
                    ""type"": ""Button"",
                    ""id"": ""74bfe387-c2ec-4a2e-9b81-cd1c81ee069b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Fire1"",
                    ""type"": ""Button"",
                    ""id"": ""804b48fe-6035-4b70-a3b4-877f04982d7d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""744dd463-4c40-4b6e-9640-defcd3137599"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""A-D"",
                    ""id"": ""988324e0-d947-4fa7-825f-8c22a3d5a9cd"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Horizontal"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""096967ca-ee92-45be-9f93-fc5e3a4f109d"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Horizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""fbfca6ac-a78f-40e1-b53a-27a6570672c4"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Horizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Left-Right"",
                    ""id"": ""80a8ea42-1404-4111-b927-3d3e018469dd"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Horizontal"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""bd31e001-3b16-4b7e-865c-dc188bb61918"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Horizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""98dc1df3-8219-4687-a480-47c71a1953df"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Horizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""S-W"",
                    ""id"": ""5fe719fc-bbc5-4091-b418-91c9a8699b54"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Vertical"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""5d7cca19-57a8-4a09-ab88-7dcac3570e64"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Vertical"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""6a6517d3-50c7-4c54-b86e-4dec04733436"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Vertical"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Down-Up"",
                    ""id"": ""8ae87a3c-1197-4725-baf0-9be8746497fb"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Vertical"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""5f943101-e079-4c5e-93f1-1602b61d2418"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Vertical"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""8322cbd3-368e-43ed-b41e-f7ce51f1f189"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Vertical"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""6de4a513-0301-4138-972a-db7bccc7e316"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Fire1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e5a158a9-f419-43dc-912d-97f01d68c681"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Fire1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4cc9fead-9bf5-4c57-9fb2-7e192225753d"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Fire1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""44b8f080-b1da-4495-9686-d0d7aff5382f"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // ActionMap
        m_ActionMap = asset.FindActionMap("ActionMap", throwIfNotFound: true);
        m_ActionMap_Horizontal = m_ActionMap.FindAction("Horizontal", throwIfNotFound: true);
        m_ActionMap_Vertical = m_ActionMap.FindAction("Vertical", throwIfNotFound: true);
        m_ActionMap_Fire1 = m_ActionMap.FindAction("Fire1", throwIfNotFound: true);
        m_ActionMap_Interact = m_ActionMap.FindAction("Interact", throwIfNotFound: true);
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

    // ActionMap
    private readonly InputActionMap m_ActionMap;
    private IActionMapActions m_ActionMapActionsCallbackInterface;
    private readonly InputAction m_ActionMap_Horizontal;
    private readonly InputAction m_ActionMap_Vertical;
    private readonly InputAction m_ActionMap_Fire1;
    private readonly InputAction m_ActionMap_Interact;
    public struct ActionMapActions
    {
        private @DialogueSystemInput m_Wrapper;
        public ActionMapActions(@DialogueSystemInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Horizontal => m_Wrapper.m_ActionMap_Horizontal;
        public InputAction @Vertical => m_Wrapper.m_ActionMap_Vertical;
        public InputAction @Fire1 => m_Wrapper.m_ActionMap_Fire1;
        public InputAction @Interact => m_Wrapper.m_ActionMap_Interact;
        public InputActionMap Get() { return m_Wrapper.m_ActionMap; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ActionMapActions set) { return set.Get(); }
        public void SetCallbacks(IActionMapActions instance)
        {
            if (m_Wrapper.m_ActionMapActionsCallbackInterface != null)
            {
                @Horizontal.started -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnHorizontal;
                @Horizontal.performed -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnHorizontal;
                @Horizontal.canceled -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnHorizontal;
                @Vertical.started -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnVertical;
                @Vertical.performed -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnVertical;
                @Vertical.canceled -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnVertical;
                @Fire1.started -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnFire1;
                @Fire1.performed -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnFire1;
                @Fire1.canceled -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnFire1;
                @Interact.started -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnInteract;
            }
            m_Wrapper.m_ActionMapActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Horizontal.started += instance.OnHorizontal;
                @Horizontal.performed += instance.OnHorizontal;
                @Horizontal.canceled += instance.OnHorizontal;
                @Vertical.started += instance.OnVertical;
                @Vertical.performed += instance.OnVertical;
                @Vertical.canceled += instance.OnVertical;
                @Fire1.started += instance.OnFire1;
                @Fire1.performed += instance.OnFire1;
                @Fire1.canceled += instance.OnFire1;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
            }
        }
    }
    public ActionMapActions @ActionMap => new ActionMapActions(this);
    public interface IActionMapActions
    {
        void OnHorizontal(InputAction.CallbackContext context);
        void OnVertical(InputAction.CallbackContext context);
        void OnFire1(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
    }
}
