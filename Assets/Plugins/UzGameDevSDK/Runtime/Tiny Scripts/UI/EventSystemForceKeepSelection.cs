using UnityEngine.EventSystems;
using UzGameDev.Helpers;
using UnityEngine;

namespace UzGameDev.TinyScripts
{
    [AddComponentMenu("UzGameDev/Tiny Scripts/UI/EventSystem Force Keep Selection")]
    public sealed class EventSystemForceKeepSelection : MonoBehaviour
    {
        private GameObject lastSelected;
        
        //============================================================
        private void Update()
        {
            if (EventSystem.current.currentSelectedGameObject.IsNotNull())
            {
                lastSelected = EventSystem.current.currentSelectedGameObject;
            }
            else
            {
                EventSystem.current.SetSelectedGameObject(lastSelected);
            }
        }
        //============================================================
    }
}