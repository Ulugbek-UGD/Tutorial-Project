using UnityEngine.InputSystem;
using UnityEngine;

namespace UzGameDev.Input
{
    [AddComponentMenu("UzGameDev/Input/Input Rebinding/Reset Rebinding Actions")]
    public class ResetRebindingActions : MonoBehaviour
    {
        [SerializeField] private InputActionAsset inputActions;
        [SerializeField] private string targetControlScheme;
        
        //============================================================
        public void ResetControlSchemeBinding()
        {
            foreach (var actionMap in inputActions.actionMaps)
            {
                foreach (var action in actionMap.actions)
                {
                    action.RemoveBindingOverride(InputBinding.MaskByGroup(targetControlScheme));
                }
            }
        }
        //============================================================
        public void ResetAllBinding()
        {
            foreach (var actionMap in inputActions.actionMaps)
            {
                actionMap.RemoveAllBindingOverrides();
            }
        }
        //============================================================
    }
}