using UnityEngine.InputSystem;
using PrimeTween;

namespace UzGameDev.Helpers
{
    public static class InputHelpers
    {
        //============================================================
        /// <summary>
        /// Находит и очищает пути привязок совпавшими с привязкой "action"
        /// </summary>
        /// <param name="action">действия ввода с привязкой</param>
        /// <param name="index">индекс привязки "action"</param>
        /// <param name="reset">нужно ли сбросить привязку "action"?</param>
        public static void RemoveDuplicateBindings(this InputAction action, int index, bool reset = false)
        {
            if (reset) action.RemoveBindingOverride(index);
            foreach (var someAction in action.actionMap.actions)
            {
                if (action.bindings[index].isComposite)
                {
                    for (var i = index + 1; i < action.bindings.Count && action.bindings[i].isPartOfComposite; ++i)
                    {
                        if (reset) action.RemoveBindingOverride(i);
                        
                        for (var j = 0; j < someAction.bindings.Count; j++)
                        {
                            if (someAction.bindings[j].effectivePath == action.bindings[i].effectivePath)
                            {
                                someAction.ApplyBindingOverride(j, string.Empty);
                            }
                        }
                    }
                }
                for (var i = 0; i < someAction.bindings.Count; i++)
                {
                    if (someAction != action || i != index)
                    {
                        if (someAction.bindings[i].effectivePath == action.bindings[index].effectivePath)
                        {
                            someAction.ApplyBindingOverride(i, string.Empty);
                        }
                    }
                }
            }
        }
        //============================================================
        /// <summary>
        /// Передает вибрационный пульс текущему геймпаду
        /// </summary>
        /// <param name="big">Strong motor sense</param>
        /// <param name="small">Light motor sense</param>
        /// <param name="time">Duration</param>
        public static void VibrateGamepad(float big = 0.05f, float small = 0.05f, float time = 0.1f)
        {
            if (Gamepad.current == null) return;
            Gamepad.current.SetMotorSpeeds(big, small);
            Tween.Delay(time, StopVibration);
        }
        //============================================================
        /// <summary>
        /// Останавливает вибрационный пульс текущему геймпаду
        /// </summary>
        private static void StopVibration()
        {
            Gamepad.current.SetMotorSpeeds(0, 0);
        }
        //============================================================
    }
}