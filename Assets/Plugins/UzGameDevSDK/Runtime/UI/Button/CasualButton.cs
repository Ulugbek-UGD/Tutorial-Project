using UnityEngine.EventSystems;
using UnityEngine.UI;
using Ami.BroAudio;
using UnityEngine;
using System;

namespace UzGameDev.UI
{
    public class CasualButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
    {
        [SerializeField] private Sprite normal;
        [SerializeField] private Sprite highlighted;
        [SerializeField] private Sprite pressed;
        [Space(10)]
        [SerializeField] private float maxChildYPosition;
        [SerializeField] private RectTransform[] childElements;
        
        [Header("Audio")]
        [SerializeField] private SoundID down;
        [SerializeField] private SoundID up;
        
        public event Action OnEnter;
        public event Action OnExit;
        public event Action OnDown;
        public event Action OnUP;
        public event Action OnClick;
        
        private Image image;
        
        private bool isPointed;
        private bool isPressed;
        
        //============================================================
        private void Awake()
        {
            image = GetComponentInChildren<Image>();
        }
        //============================================================
        public void OnPointerEnter(PointerEventData eventData)
        {
            isPointed = true;
            if (isPressed)
            {
                image.sprite = pressed;
                BroAudio.Play(down);
                foreach (var element in childElements)
                {
                    element.localPosition = new Vector3(element.localPosition.x, element.localPosition.y - maxChildYPosition);
                }
            }
            else
            {
                image.sprite = highlighted;
            }
            OnEnter?.Invoke();
        }
        //============================================================
        public void OnPointerExit(PointerEventData eventData)
        {
            isPointed = false;
            image.sprite = normal;
            if (isPressed)
            {
                BroAudio.Play(up);
                foreach (var element in childElements)
                {
                    element.localPosition = new Vector3(element.localPosition.x, element.localPosition.y + maxChildYPosition);
                }
            }
            OnExit?.Invoke();
        }
        //============================================================
        public void OnPointerDown(PointerEventData eventData)
        {
            isPressed = true;
            image.sprite = pressed;
            BroAudio.Play(down);
            foreach (var element in childElements)
            {
                element.localPosition = new Vector3(element.localPosition.x, element.localPosition.y - maxChildYPosition);
            }
            OnDown?.Invoke();
        }
        //============================================================
        public void OnPointerUp(PointerEventData eventData)
        {
            isPressed = false;
            if (isPointed)
            {
                image.sprite = highlighted;
                BroAudio.Play(up);
                foreach (var element in childElements)
                {
                    element.localPosition = new Vector3(element.localPosition.x, element.localPosition.y + maxChildYPosition);
                }
            }
            OnUP?.Invoke();
        }
        //============================================================
        public void OnPointerClick(PointerEventData eventData)
        {
            OnClick?.Invoke();
        }
        //============================================================
    }
}