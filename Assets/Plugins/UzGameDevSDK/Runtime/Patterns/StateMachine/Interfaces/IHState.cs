namespace UzGameDev.Pattern.StateMachine
{
    public interface IHState : IState
    {
        public new IFSM Machine { get; set; }
    }
}