using UnityEngine;
using UnityEngine.Events;

namespace ClickerGame
{
    public class UnitParts : MonoBehaviour, ISelectable
    {
        [SerializeField] private UnityEvent OnPartClick;
        
        public void Select()
        {
            Debug.Log("My name is " + gameObject.name + " and i am " + transform.parent.gameObject.name + " part");
            OnPartClick?.Invoke();
        }
    }
}