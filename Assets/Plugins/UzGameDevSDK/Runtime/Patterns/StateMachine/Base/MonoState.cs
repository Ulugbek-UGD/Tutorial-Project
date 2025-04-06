using UnityEngine;
using System;

namespace UzGameDev.Pattern.StateMachine
{
    [Serializable]
    public abstract class MonoState : MonoBehaviour, IState
    {
        public string StateName { get; internal set; }
        
        public IFSM Machine { get; internal set; }
        public IFSM SuperMachine { get; internal set; }
        
        public virtual void Initialize()
        {
            //if no name hase been specified set the name of this instance to the the
            if (string.IsNullOrEmpty(StateName))
                StateName = GetType().Name;
        }
        
        public virtual void OnEnter() { }
        public virtual void OnExit() { }
        
        public virtual string GetStateName()
        {
            return StateName;
        }
    }
}