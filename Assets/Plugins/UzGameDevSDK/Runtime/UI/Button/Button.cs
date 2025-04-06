using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using System;

namespace UzGameDev.UI
{
    [AddComponentMenu("UzGameDev/UI/Button")]
    public sealed class Button : Selectable, IPointerClickHandler, ISubmitHandler
    {
        public event Action<Button> onPointerEnter;
        public event Action<Button> onPointerExit;
        public event Action<Button> onPointerDown;
        public event Action<Button> onPointerUp;
        public event Action<Button> onPointerClick;
        public event Action<Button> onSelect;
        public event Action<Button> onDeselect;
        public event Action<Button> onMove;
        public event Action<Button> onSubmit;
        public event Action<Button> onInteractable;
        public event Action<Button> onNonInteractable;
        
        public bool IsPointed { get; private set; }
        public bool IsSelected { get; private set; }
        
        public RectTransform RectTransform { get; private set; }
        
        private bool expectedInteractable = true;
        
        //============================================================
        protected override void Awake()
        {
            base.Awake();
            RectTransform = GetComponent<RectTransform>();
        }
        //============================================================
        private void Update()
        {
            UpdateInteractableEvent();
        }
        //============================================================ On Pointer Enter
        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);
            IsPointed = true;
            onPointerEnter?.Invoke(this);
        }
        //============================================================ On Pointer Exit
        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
            IsPointed = false;
            onPointerExit?.Invoke(this);
        }
        //============================================================ On Pointer Down
        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            onPointerDown?.Invoke(this);
        }
        //============================================================ On Pointer Up
        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            onPointerUp?.Invoke(this);
        }
        //============================================================ On Pointer Click
        public void OnPointerClick(PointerEventData eventData)
        {
            onPointerClick?.Invoke(this);
        }
        //============================================================ On Select
        public override void OnSelect(BaseEventData eventData)
        {
            base.OnSelect(eventData);
            IsSelected = true;
            onSelect?.Invoke(this);
        }
        //============================================================ On Deselect
        public override void OnDeselect(BaseEventData eventData)
        {
            base.OnDeselect(eventData);
            IsSelected = false;
            onDeselect?.Invoke(this);
        }
        //============================================================ On Move
        public override void OnMove(AxisEventData eventData)
        {
            base.OnMove(eventData);
            onMove?.Invoke(this);
        }
        //============================================================ On Submit
        public void OnSubmit(BaseEventData eventData)
        {
            onSubmit?.Invoke(this);
        }
        //============================================================ Update Interactable Event
        private void UpdateInteractableEvent()
        {
            var currentInteractable = IsInteractable();
            if (currentInteractable != expectedInteractable)
            {
                expectedInteractable = currentInteractable;
                if (expectedInteractable)
                {
                    targetGraphic.raycastTarget = true;
                    onInteractable?.Invoke(this);
                }
                else
                {
                    targetGraphic.raycastTarget = false;
                    onNonInteractable?.Invoke(this);
                }
            }
        }
        //============================================================
    }
}