using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UzGameDev.Helpers;
using UnityEngine;
using System;
using TMPro;

namespace UzGameDev.Input
{
    /// <summary>
    /// A reusable component with a self-contained UI for rebinding a single action.
    /// </summary>
    [AddComponentMenu("UzGameDev/Input/Input Rebinding/Input Actions Rebinding")]
    public class InputActionsRebinding : MonoBehaviour
    {
        #region Fields
        private enum RebindCancelDevice
        {
            Keyboard,
            Gamepad,
        }
        [SerializeField] private RebindCancelDevice m_CancelWith;
        
        [Tooltip("Reference to action that is to be rebound from the UI.")]
        [SerializeField] private InputActionReference m_Action;
        
        [SerializeField] private string m_BindingId;
        
        [Tooltip("Text label that will receive the current, formatted binding string.")]
        [SerializeField] private TextMeshProUGUI m_BindingText;
        
        [Tooltip("Optional UI that will be shown while a rebind is in progress.")]
        [SerializeField] private GameObject m_RebindOverlay;
        
        [Tooltip("Optional text label that will be updated with prompt for user input.")]
        [SerializeField] private TextMeshProUGUI m_RebindText;
        
        [Tooltip("Event that is triggered when the way the binding is display should be updated. This allows displaying "
                 + "bindings in custom ways, e.g. using images instead of text.")]
        [SerializeField] private UpdateBindingUIEvent m_UpdateBindingUIEvent;
        
        [Tooltip("Event that is triggered when an interactive rebind is being initiated. This can be used, for example, "
                 + "to implement custom UI behavior while a rebind is in progress. It can also be used to further "
                 + "customize the rebind.")]
        [SerializeField] private InteractiveRebindEvent m_RebindStartEvent;
        
        [Tooltip("Event that is triggered when an interactive rebind is complete or has been aborted.")]
        [SerializeField] private InteractiveRebindEvent m_RebindStopEvent;
        
        private InputActionRebindingExtensions.RebindingOperation m_RebindOperation;
        
        private static List<InputActionsRebinding> s_RebindActionUIs;
        #endregion
        
        
        #region Properties
        /// <summary>
        /// Reference to the action that is to be rebound.
        /// </summary>
        public InputActionReference actionReference
        {
            get => m_Action;
            set
            {
                m_Action = value;
                UpdateBindingDisplay();
            }
        }
        
        /// <summary>
        /// ID (in string form) of the binding that is to be rebound on the action.
        /// </summary>
        /// <seealso cref="InputBinding.id"/>
        public string bindingId
        {
            get => m_BindingId;
            set
            {
                m_BindingId = value;
                UpdateBindingDisplay();
            }
        }
        
        /// <summary>
        /// Text component that receives the display string of the binding. Can be <c>null</c> in which
        /// case the component entirely relies on <see cref="updateBindingUIEvent"/>.
        /// </summary>
        public TextMeshProUGUI bindingText
        {
            get => m_BindingText;
            set
            {
                m_BindingText = value;
                UpdateBindingDisplay();
            }
        }
        
        /// <summary>
        /// Optional text component that receives a text prompt when waiting for a control to be actuated.
        /// </summary>
        /// <seealso cref="startRebindEvent"/>
        /// <seealso cref="rebindOverlay"/>
        public TextMeshProUGUI rebindPrompt
        {
            get => m_RebindText;
            set => m_RebindText = value;
        }
        
        /// <summary>
        /// Optional UI that is activated when an interactive rebind is started and deactivated when the rebind
        /// is finished. This is normally used to display an overlay over the current UI while the system is
        /// waiting for a control to be actuated.
        /// </summary>
        /// <remarks>
        /// If neither <see cref="rebindPrompt"/> nor <c>rebindOverlay</c> is set, the component will temporarily
        /// replaced the <see cref="bindingText"/> (if not <c>null</c>) with <c>"Waiting..."</c>.
        /// </remarks>
        /// <seealso cref="startRebindEvent"/>
        /// <seealso cref="rebindPrompt"/>
        public GameObject rebindOverlay
        {
            get => m_RebindOverlay;
            set => m_RebindOverlay = value;
        }
        
        /// <summary>
        /// Event that is triggered every time the UI updates to reflect the current binding.
        /// This can be used to tie custom visualizations to bindings.
        /// </summary>
        public UpdateBindingUIEvent updateBindingUIEvent => m_UpdateBindingUIEvent ??= new UpdateBindingUIEvent();
        
        /// <summary>
        /// Event that is triggered when an interactive rebind is started on the action.
        /// </summary>
        public InteractiveRebindEvent startRebindEvent => m_RebindStartEvent ??= new InteractiveRebindEvent();
        
        /// <summary>
        /// Event that is triggered when an interactive rebind has been completed or canceled.
        /// </summary>
        public InteractiveRebindEvent stopRebindEvent => m_RebindStopEvent ??= new InteractiveRebindEvent();
        
        /// <summary>
        /// When an interactive rebind is in progress, this is the rebind operation controller.
        /// Otherwise, it is <c>null</c>.
        /// </summary>
        public InputActionRebindingExtensions.RebindingOperation ongoingRebind => m_RebindOperation;
        #endregion
        
        
        #region Methods
        //============================================================
        protected void OnEnable()
        {
            s_RebindActionUIs ??= new List<InputActionsRebinding>();
            s_RebindActionUIs.Add(this);
            if (s_RebindActionUIs.Count == 1)
                InputSystem.onActionChange += OnActionChange;
        }
        //============================================================
        protected void OnDisable()
        {
            m_RebindOperation?.Dispose();
            m_RebindOperation = null;
            
            s_RebindActionUIs.Remove(this);
            if (s_RebindActionUIs.Count == 0)
            {
                s_RebindActionUIs = null;
                InputSystem.onActionChange -= OnActionChange;
            }
        }
        //============================================================
        /// <summary>
        /// Initiate an interactive rebind that lets the player actuate a control to choose a new binding
        /// for the action.
        /// </summary>
        public void StartInteractiveRebind()
        {
            if (!ResolveActionAndBinding(out var action, out var bindingIndex))
                return;
            
            // If the binding is a composite, we need to rebind each part in turn.
            if (action.bindings[bindingIndex].isComposite)
            {
                var firstPartIndex = bindingIndex + 1;
                if (firstPartIndex < action.bindings.Count && action.bindings[firstPartIndex].isPartOfComposite)
                    PerformInteractiveRebind(action, firstPartIndex, allCompositeParts: true);
            }
            else
            {
                PerformInteractiveRebind(action, bindingIndex);
            }
        }
        //============================================================
        private void PerformInteractiveRebind(InputAction action, int bindingIndex, bool allCompositeParts = false)
        {
            // Will null out m_RebindOperation.
            m_RebindOperation?.Cancel();
            
            void CleanUp()
            {
                m_RebindOperation?.Dispose();
                m_RebindOperation = null;
            }
            
            action.Disable();
            
            // Configure the rebind.
            m_RebindOperation = action.PerformInteractiveRebinding(bindingIndex);
            
            switch (m_CancelWith)
            {
                case RebindCancelDevice.Keyboard:
                    m_RebindOperation.WithCancelingThrough("<Keyboard>/escape");
                    break;
                case RebindCancelDevice.Gamepad:
                    m_RebindOperation.WithCancelingThrough("<Gamepad>/start");
                    break;
            }
            
            m_RebindOperation.OnCancel(
                operation => 
                { 
                    action.Enable();
                    m_RebindStopEvent?.Invoke(this, operation);
                    if (m_RebindOverlay != null)
                    {
                        m_RebindOverlay.SetActive(false);
                    }
                    UpdateBindingDisplay();
                    CleanUp();
                });
            
            m_RebindOperation.OnComplete(
                operation =>
                {
                    action.Enable();
                    if (m_RebindOverlay != null)
                    {
                        m_RebindOverlay.SetActive(false);
                    }
                    
                    m_RebindStopEvent?.Invoke(this, operation);
                    
                    action.RemoveDuplicateBindings(bindingIndex);
                    
                    UpdateBindingDisplay();
                    CleanUp();
                    
                    // If there's more composite parts we should bind, initiate a rebind
                    // for the next part.
                    if (allCompositeParts)
                    {
                        var nextBindingIndex = bindingIndex + 1;
                        if (nextBindingIndex < action.bindings.Count &&
                            action.bindings[nextBindingIndex].isPartOfComposite)
                            PerformInteractiveRebind(action, nextBindingIndex, true);
                    }
                });
            
            // If it's a part binding, show the name of the part in the UI.
            var partName = default(string);
            if (action.bindings[bindingIndex].isPartOfComposite)
                partName = $"Binding '{action.bindings[bindingIndex].name}'. ";
            
            // Bring up rebind overlay, if we have one.
            if (m_RebindOverlay != null)
            {
                m_RebindOverlay.SetActive(true);
            }
            if (m_RebindText != null)
            {
                var text = !string.IsNullOrEmpty(m_RebindOperation.expectedControlType)
                    ? $"{partName}Waiting for {m_RebindOperation.expectedControlType} input..."
                    : $"{partName}Waiting for input...";
                m_RebindText.text = text;
            }
            
            // If we have no rebind overlay and no callback but we have a binding text label,
            // temporarily set the binding text label to "<Waiting>".
            if (m_RebindOverlay == null && m_RebindText == null && m_RebindStartEvent == null && m_BindingText != null)
                m_BindingText.text = "<Waiting...>";
            
            // Give listeners a chance to act on the rebind starting.
            m_RebindStartEvent?.Invoke(this, m_RebindOperation);
            
            m_RebindOperation.Start();
        }
        //============================================================
        /// <summary>
        /// Remove currently applied binding overrides.
        /// </summary>
        public void ResetToDefault()
        {
            if (!ResolveActionAndBinding(out var action, out var bindingIndex))
                return;
            
            action.RemoveDuplicateBindings(bindingIndex, true);
            
            UpdateBindingDisplay();
        }
        //============================================================
        // When the action system re-resolves bindings, we want to update our UI in response. While this will
        // also trigger from changes we made ourselves, it ensures that we react to changes made elsewhere. If
        // the user changes keyboard layout, for example, we will get a BoundControlsChanged notification and
        // will update our UI to reflect the current keyboard layout.
        private static void OnActionChange(object obj, InputActionChange change)
        {
            if (change != InputActionChange.BoundControlsChanged)
                return;
            
            var action = obj as InputAction;
            var actionMap = action?.actionMap ?? obj as InputActionMap;
            var actionAsset = actionMap?.asset ? actionMap.asset : obj as InputActionAsset;
            
            foreach (var component in s_RebindActionUIs)
            {
                var referencedAction = component.actionReference != null ? component.actionReference.action : null;
                if (referencedAction == null)
                    continue;
                
                if (referencedAction == action ||
                    referencedAction.actionMap == actionMap ||
                    referencedAction.actionMap?.asset == actionAsset)
                    component.UpdateBindingDisplay();
            }
        }
        //============================================================
        /// <summary>
        /// Trigger a refresh of the currently displayed binding.
        /// </summary>
        public void UpdateBindingDisplay()
        {
            var displayString = string.Empty;
            var deviceLayoutName = default(string);
            var controlPath = default(string);
            
            // Get display string from action.
            var action = m_Action != null ? m_Action.action : null;
            if (action != null)
            {
                var bindingIndex = action.bindings.IndexOf(x => x.id.ToString() == m_BindingId);
                if (bindingIndex != -1)
                    displayString = action.GetBindingDisplayString(bindingIndex, out deviceLayoutName, out controlPath);
            }
            
            // Set on label (if any).
            if (m_BindingText != null)
                m_BindingText.text = displayString;
            
            // Give listeners a chance to configure UI in response.
            m_UpdateBindingUIEvent?.Invoke(this, displayString, deviceLayoutName, controlPath);
        }
        //============================================================
        /// <summary>
        /// Return the action and binding index for the binding that is targeted by the component
        /// according to
        /// </summary>
        /// <param name="action"></param>
        /// <param name="bindingIndex"></param>
        /// <returns></returns>
        private bool ResolveActionAndBinding(out InputAction action, out int bindingIndex)
        {
            bindingIndex = -1;
            
            action = m_Action != null ? m_Action.action : null;
            if (action == null)
                return false;
            
            if (string.IsNullOrEmpty(m_BindingId))
                return false;
            
            // Look up binding index.
            var bindID = new Guid(m_BindingId);
            bindingIndex = action.bindings.IndexOf(x => x.id == bindID);
            if (bindingIndex == -1)
            {
                Debug.LogError($"Cannot find binding with ID '{bindID}' on '{action}'", this);
                return false;
            }
            return true;
        }
        //============================================================
        #endregion
        
        // We want the label for the action name to update in edit mode, too, so
        // we kick that off from here.
#if UNITY_EDITOR
        protected void OnValidate()
        {
            UpdateBindingDisplay();
        }
#endif
        [Serializable] public class UpdateBindingUIEvent : UnityEvent<InputActionsRebinding, string, string, string> { }
        [Serializable] public class InteractiveRebindEvent : UnityEvent<InputActionsRebinding, InputActionRebindingExtensions.RebindingOperation> { }
    }
}