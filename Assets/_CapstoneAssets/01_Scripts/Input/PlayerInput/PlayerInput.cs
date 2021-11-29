// GENERATED AUTOMATICALLY FROM 'Assets/_CapstoneAssets/01_Scripts/Input/PlayerInput/PlayerInput.inputactions'

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
            ""name"": ""FreeControl"",
            ""id"": ""984c8832-cfd7-467b-9bc2-aaa32b6fb9f0"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""a9902de1-9d74-4b81-b3d3-9a0b8bebeb13"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Dash"",
                    ""type"": ""PassThrough"",
                    ""id"": ""448b3a70-7d0e-4e3c-8fef-73127746b7a6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Tool"",
                    ""type"": ""Button"",
                    ""id"": ""15734a04-4279-481a-9dba-584b51be388c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""3b20460c-1b82-471c-bc0b-cbb19cf147ad"",
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
                    ""id"": ""5c7e4c56-9652-4349-ae57-2b1c19dac524"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""e1a9c996-1c95-4649-8804-05c116d05f69"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""421555f0-57bc-49ed-93ac-def06a149ea4"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""e653922d-90aa-46fa-9d16-e505a9abd54a"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""5ab0648c-e68f-409f-90fa-682095919175"",
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
                    ""id"": ""40099657-2de1-4cd1-a122-2c3e5e5c2888"",
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
                    ""id"": ""f62dc6e5-c131-4298-a379-05af74f9b3fc"",
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
                    ""id"": ""a4fa60cf-d5d7-46f3-8e47-98cca336be03"",
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
                    ""id"": ""71579ff7-7625-40b0-b46f-f4999beb6075"",
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
                    ""id"": ""e3f0608f-99c4-46f7-9b35-7695460097dc"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""07b0e052-ee83-4e7f-9f5d-64fabc3c556e"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Tool"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // FreeControl
        m_FreeControl = asset.FindActionMap("FreeControl", throwIfNotFound: true);
        m_FreeControl_Move = m_FreeControl.FindAction("Move", throwIfNotFound: true);
        m_FreeControl_Dash = m_FreeControl.FindAction("Dash", throwIfNotFound: true);
        m_FreeControl_Tool = m_FreeControl.FindAction("Tool", throwIfNotFound: true);
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

    // FreeControl
    private readonly InputActionMap m_FreeControl;
    private IFreeControlActions m_FreeControlActionsCallbackInterface;
    private readonly InputAction m_FreeControl_Move;
    private readonly InputAction m_FreeControl_Dash;
    private readonly InputAction m_FreeControl_Tool;
    public struct FreeControlActions
    {
        private @PlayerInput m_Wrapper;
        public FreeControlActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_FreeControl_Move;
        public InputAction @Dash => m_Wrapper.m_FreeControl_Dash;
        public InputAction @Tool => m_Wrapper.m_FreeControl_Tool;
        public InputActionMap Get() { return m_Wrapper.m_FreeControl; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(FreeControlActions set) { return set.Get(); }
        public void SetCallbacks(IFreeControlActions instance)
        {
            if (m_Wrapper.m_FreeControlActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_FreeControlActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_FreeControlActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_FreeControlActionsCallbackInterface.OnMove;
                @Dash.started -= m_Wrapper.m_FreeControlActionsCallbackInterface.OnDash;
                @Dash.performed -= m_Wrapper.m_FreeControlActionsCallbackInterface.OnDash;
                @Dash.canceled -= m_Wrapper.m_FreeControlActionsCallbackInterface.OnDash;
                @Tool.started -= m_Wrapper.m_FreeControlActionsCallbackInterface.OnTool;
                @Tool.performed -= m_Wrapper.m_FreeControlActionsCallbackInterface.OnTool;
                @Tool.canceled -= m_Wrapper.m_FreeControlActionsCallbackInterface.OnTool;
            }
            m_Wrapper.m_FreeControlActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Dash.started += instance.OnDash;
                @Dash.performed += instance.OnDash;
                @Dash.canceled += instance.OnDash;
                @Tool.started += instance.OnTool;
                @Tool.performed += instance.OnTool;
                @Tool.canceled += instance.OnTool;
            }
        }
    }
    public FreeControlActions @FreeControl => new FreeControlActions(this);
    public interface IFreeControlActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnDash(InputAction.CallbackContext context);
        void OnTool(InputAction.CallbackContext context);
    }
}
