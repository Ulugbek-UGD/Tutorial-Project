using UnityEngine;

namespace ClickerGame
{
    public class Farm : MonoBehaviour, ISelectable
    {
        public void Select()
        {
            Debug.Log("I am Farm");
        }
    }
}