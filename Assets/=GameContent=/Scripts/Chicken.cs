using UnityEngine;

namespace ClickerGame
{
    public class Chicken : MonoBehaviour
    {
        //===============================================================
        public void Select()
        {
            Debug.Log("I am Chicken " + transform.rotation);
        }
        //===============================================================
    }
}