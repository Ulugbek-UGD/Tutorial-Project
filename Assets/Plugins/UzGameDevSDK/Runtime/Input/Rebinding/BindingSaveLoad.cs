using UnityEngine.InputSystem;
using UnityEngine;

namespace UzGameDev.Input
{
    [AddComponentMenu("UzGameDev/Input/Input Rebinding/Binding Save Load")]
    public class BindingSaveLoad : MonoBehaviour
    {
        public InputActionAsset actions;
        
        //============================================================
        public void OnEnable()
        {
            var rebinds = PlayerPrefs.GetString("rebinds");
            if (!string.IsNullOrEmpty(rebinds))
                actions.LoadBindingOverridesFromJson(rebinds);
        }
        //============================================================
        public void OnDisable()
        {
            var rebinds = actions.SaveBindingOverridesAsJson();
            PlayerPrefs.SetString("rebinds", rebinds);
        }
        //============================================================
    }
}