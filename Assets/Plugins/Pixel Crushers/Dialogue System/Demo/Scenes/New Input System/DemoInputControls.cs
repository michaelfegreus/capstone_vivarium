// GENERATED AUTOMATICALLY FROM 'Assets/Plugins/Pixel Crushers/Dialogue System/Demo/Scenes/New Input System/DemoInputControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @DemoInputControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @DemoInputControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""DemoInputControls"",
    ""maps"": [
        {
            ""name"": ""DemoActionMap"",
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
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // DemoActionMap
        m_DemoActionMap = asset.FindActionMap("DemoActionMap", throwIfNotFound: true);
        m_DemoActionMap_Horizontal = m_DemoActionMap.FindAction("Horizontal", throwIfNotFound: true);
        m_DemoActionMap_Vertical = m_DemoActionMap.FindAction("Vertical", throwIfNotFound: true);
        m_DemoActionMap_Fire1 = m_DemoActionMap.FindAction("Fire1", throwIfNotFound: true);
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

    // DemoActionMap
    private readonly InputActionMap m_DemoActionMap;
    private IDemoActionMapActions m_DemoActionMapActionsCallbackInterface;
    private readonly InputAction m_DemoActionMap_Horizontal;
    private readonly InputAction m_DemoActionMap_Vertical;
    private readonly InputAction m_DemoActionMap_Fire1;
    public struct DemoActionMapActions
    {
        private @DemoInputControls m_Wrapper;
        public DemoActionMapActions(@DemoInputControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Horizontal => m_Wrapper.m_DemoActionMap_Horizontal;
        public InputAction @Vertical => m_Wrapper.m_DemoActionMap_Vertical;
        public InputAction @Fire1 => m_Wrapper.m_DemoActionMap_Fire1;
        public InputActionMap Get() { return m_Wrapper.m_DemoActionMap; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(DemoActionMapActions set) { return set.Get(); }
        public void SetCallbacks(IDemoActionMapActions instance)
        {
            if (m_Wrapper.m_DemoActionMapActionsCallbackInterface != null)
            {
                @Horizontal.started -= m_Wrapper.m_DemoActionMapActionsCallbackInterface.OnHorizontal;
                @Horizontal.performed -= m_Wrapper.m_DemoActionMapActionsCallbackInterface.OnHorizontal;
                @Horizontal.canceled -= m_Wrapper.m_DemoActionMapActionsCallbackInterface.OnHorizontal;
                @Vertical.started -= m_Wrapper.m_DemoActionMapActionsCallbackInterface.OnVertical;
                @Vertical.performed -= m_Wrapper.m_DemoActionMapActionsCallbackInterface.OnVertical;
                @Vertical.canceled -= m_Wrapper.m_DemoActionMapActionsCallbackInterface.OnVertical;
                @Fire1.started -= m_Wrapper.m_DemoActionMapActionsCallbackInterface.OnFire1;
                @Fire1.performed -= m_Wrapper.m_DemoActionMapActionsCallbackInterface.OnFire1;
                @Fire1.canceled -= m_Wrapper.m_DemoActionMapActionsCallbackInterface.OnFire1;
            }
            m_Wrapper.m_DemoActionMapActionsCallbackInterface = instance;
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
            }
        }
    }
    public DemoActionMapActions @DemoActionMap => new DemoActionMapActions(this);
    public interface IDemoActionMapActions
    {
        void OnHorizontal(InputAction.CallbackContext context);
        void OnVertical(InputAction.CallbackContext context);
        void OnFire1(InputAction.CallbackContext context);
    }
}
