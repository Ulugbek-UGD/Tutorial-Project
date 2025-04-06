using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UzGameDev.Helpers;
using UnityEngine.UI;
using UnityEngine;

namespace UzGameDev.UI
{
    [AddComponentMenu("UzGameDev/UI/Buttons Selector")]
    [RequireComponent(typeof(Image))]
    public sealed class ButtonsSelector : MonoBehaviour
    {
        [Header("Buttons to Select")]
        [SerializeField] private List<Button> buttons;
        
        [Header("Selection Options")]
        [SerializeField] private bool selectOnStart;
        [SerializeField] private bool selectOnPointerEnter;
        [SerializeField] private bool deselectOnEmptyClick;
        
        [Header("Selecting Tween Options")]
        [SerializeField] private Vector2 positionOffset;
        [SerializeField] private Vector2 sizeDeltaOffset;
        [SerializeField] [Range(10f, 20f)] private float sizeDeltaTweenSpeed = 15f;
        
        [Header("Color Options")]
        [SerializeField] private Color selectColor = new(0.6f, 1, 1, 1);
        [SerializeField] private Color normalColor = new(1, 1, 1, 1);
        [SerializeField] private Color disableColor = new(0.6f, 0.6f, 0.6f, 1);
        
        [Header("Info")]
        [SerializeField] private Button CurrentButton;
        [SerializeField] private Button PreviousButton;
        
        private RectTransform selectorRectTransform;
        private Image selectorImage;
        
        private InputAction mouseClick;
        
        //============================================================
        private void Awake()
        {
            CacheComponents();
            mouseClick = new InputAction(binding: "<Mouse>/leftButton");
        }
        //============================================================
        private void CacheComponents()
        {
            selectorRectTransform = GetComponent<RectTransform>();
            selectorImage = GetComponent<Image>();
        }
        //============================================================
        private void OnEnable()
        {
            mouseClick.started += OnMouseLeftDown;
            mouseClick.Enable();
            
            SubscribeButtonsEvent();
        }
        //============================================================
        private void OnDisable()
        {
            mouseClick.started -= OnMouseLeftDown;
            mouseClick.Disable();
            
            UnsubscribeButtonsEvent();
        }
        //============================================================
        private void SubscribeButtonsEvent()
        {
            if (ButtonInList().IsNull()) return;
            foreach (var button in buttons)
            {
                button.onPointerEnter += OnAnyPointerEnter;
                button.onSelect += OnAnySelect;
                button.onDeselect += OnAnyDeselect;
                button.onInteractable += OnAnyInteractable;
                button.onNonInteractable += OnAnyNonInteractable;
            }
        }
        //============================================================
        private void UnsubscribeButtonsEvent()
        {
            if (ButtonInList().IsNull()) return;
            foreach (var button in buttons)
            {
                button.onPointerEnter -= OnAnyPointerEnter;
                button.onSelect -= OnAnySelect;
                button.onDeselect -= OnAnyDeselect;
                button.onInteractable -= OnAnyInteractable;
                button.onNonInteractable -= OnAnyNonInteractable;
            }
        }
        //============================================================
        private void Start()
        {
            SetNonInteractableButtonsColor();
            SelectStartingButton();
        }
        //============================================================
        private void SetNonInteractableButtonsColor()
        {
            if (ButtonInList().IsNull()) return;
            foreach (var button in buttons)
            {
                if (!button.IsInteractable())
                {
                    button.targetGraphic.color = disableColor;
                }
            }
        }
        //============================================================
        private void SelectStartingButton()
        {
            if (selectOnStart && ButtonInList().IsNotNull())
            {
                ButtonInList().Select();
            }
        }
        //============================================================
        private void OnAnyPointerEnter(Button button)
        {
            if (selectOnPointerEnter)
            {
                button.Select();
            }
        }
        //============================================================
        private void OnAnySelect(Button button)
        {
            button.targetGraphic.color = selectColor;
            if (CurrentButton.IsNull())
            {
                PreviousButton = null;
            }
            CurrentButton = button;
            Set_Selector_Start_SizeDelta();
        }
        //============================================================
        private void OnAnyDeselect(Button button)
        {
            button.targetGraphic.color = normalColor;
            if (ButtonInList().IsNull())
            {
                CurrentButton = null;
            }
            PreviousButton = button;
        }
        //============================================================
        private void OnAnyInteractable(Button button)
        {
            button.targetGraphic.color = normalColor;
            if (CurrentButton.IsNull())
            {
                ButtonInList().Select();
            }
        }
        //============================================================
        private void OnAnyNonInteractable(Button button)
        {
            button.targetGraphic.color = disableColor;
            if (CurrentButton == button && ButtonInList().IsNotNull())
            {
                ButtonInList().Select();
            }
        }
        //============================================================
        private void OnMouseLeftDown(InputAction.CallbackContext context)
        {
            if (deselectOnEmptyClick && CurrentButton.IsNotNull() && NoPointedButton())
            {
                PreviousButton = CurrentButton;
                CurrentButton = null;
                EventSystem.current.SetSelectedGameObject(null);
            }
        }
        //============================================================
        private void Set_Selector_Start_SizeDelta()
        {
            var buttonDelta = CurrentButton.RectTransform.sizeDelta;
            selectorRectTransform.sizeDelta = sizeDeltaOffset + new Vector2(buttonDelta.x + 80, buttonDelta.y);
        }
        //============================================================
        private void Update()
        {
            UpdateSelectorTween();
        }
        //============================================================
        private void UpdateSelectorTween()
        {
            selectorImage.enabled = CurrentButton.IsNotNull();
            if (CurrentButton.IsNull()) return;
            selectorRectTransform.localPosition = (Vector2)CurrentButton.RectTransform.localPosition + positionOffset;
            var desiredSizeDelta = CurrentButton.RectTransform.sizeDelta + sizeDeltaOffset;
            selectorRectTransform.sizeDelta = Vector2.Lerp(selectorRectTransform.sizeDelta, desiredSizeDelta, sizeDeltaTweenSpeed * Time.deltaTime);
        }
        //============================================================
        #region Returnable Methods
        private Button ButtonInList()
        {
            foreach (var button in buttons)
            {
                if (button.IsNotNull() && button.IsInteractable()) return button;
                //if (button.IsNotNull() && button.IsActive() && button.IsInteractable()) return button;
            }
            return null;
        }
        //============================================================
        private bool NoPointedButton()
        {
            foreach (var button in buttons)
            {
                if (button.IsPointed) return false;
            }
            return true;
        }
        #endregion
        //============================================================
#if UNITY_EDITOR
        private void Reset()
        {
            selectorImage = GetComponent<Image>();
            selectorImage.color = new Color(0.7f, 0.7f, 0.7f, 0.2f);
            selectorImage.raycastTarget = false;
        }
#endif
        //============================================================
    }
}