using UnityEngine.EventSystems;
using UnityEngine;

namespace UzGameDev.TinyScripts
{
    [AddComponentMenu("UzGameDev/Tiny Scripts/UI/Mouse Cursor Hover Icon")]
    public class MouseCursorHoverIcon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Texture2D cursorIcon;
        [SerializeField] private CursorMode cursorMode = CursorMode.Auto;
        [SerializeField] private Vector2 hotSpot;
        
        //============================================================
        private void Start ()
        {
            Cursor.SetCursor(null, hotSpot, cursorMode);
        }
        //============================================================
        private void OnMouseEnter()
        {
            Cursor.SetCursor(cursorIcon, hotSpot, cursorMode);
        }
        //============================================================
        private void OnMouseExit()
        {
            Cursor.SetCursor(null, hotSpot, cursorMode);
        }
        //============================================================
        public void OnPointerEnter(PointerEventData eventData)
        {
            Cursor.SetCursor(cursorIcon, hotSpot, cursorMode);
        }
        //============================================================
        public void OnPointerExit(PointerEventData eventData)
        {
            Cursor.SetCursor(null, hotSpot, cursorMode);
        }
        //============================================================
    }
}