using UnityEngine;

namespace UzGameDev.TinyScripts
{
    public class DestroyByTime : MonoBehaviour
    {
        [SerializeField] private float second = 2;
        
        //============================================================
        private void Start()
        {
            Destroy(gameObject, second);
        }
        //============================================================
    }
}